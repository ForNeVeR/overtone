open System.Drawing.Imaging
open System.IO

open Overtone.Resources
open Overtone.Resources.Shape

let private info name (file: ShapeFile) =
    printfn $"Shape file {name}"
    let headers = file.ReadSpriteHeaders() |> Seq.toArray
    printfn $"Sprite count: %d{headers.Length}"
    headers |> Array.iteri (fun i header ->
        let sprint(struct (x, y)) = sprintf $"({x}, {y})"

        let palette = if Option.isSome header.PaletteOffset then "present" else "absent"
        printfn $"Sprite {i}. Offset {header.SpriteOffset}, custom palette: {palette}"
        let sprite = file.ReadSprite header
        let struct (width, height) = sprite.CanvasDimensions
        printfn $"  Canvas: {width} × {height}"
        printfn $"  Origin: {sprint sprite.Origin}"
        printfn $"  Start (relative): {sprint sprite.Start}"
        printfn $"  End (relative): {sprint sprite.End}"
        printfn $"  Data offset: {sprite.DataOffset}"
    )

let private render inputFile outputDirectory =
    use stream = new FileStream(inputFile, FileMode.Open)
    let file = ShapeFile stream
    info (Path.GetFileName inputFile) file

    let palFilePath = Path.Combine(Path.GetDirectoryName inputFile, ShapePalette.get inputFile)
    use palStream = new FileStream(palFilePath, FileMode.Open)
    let palette = Palette.Read palStream

    file.ReadSpriteHeaders()
    |> Seq.iteri (fun index header ->
        Directory.CreateDirectory outputDirectory |> ignore
        let outputFilePath = Path.Combine(outputDirectory,
                                          $"{Path.GetFileNameWithoutExtension inputFile}_{index}.png")

        let sprite = file.ReadSprite header
        use bitmap = file.Render palette sprite
        bitmap.Save(outputFilePath, ImageFormat.Png)
    )

[<EntryPoint>]
let main: string[] -> int = function
| [|"info"; inputFilePattern|] ->
    let parent = Path.GetDirectoryName inputFilePattern
    let files = Directory.GetFiles(parent, Path.GetFileName inputFilePattern)
    let mutable errors = 0
    for path in files do
        try
            use stream = new FileStream(path, FileMode.Open)
            let file = ShapeFile stream
            info (Path.GetFileName path) file
        with
        | e ->
            errors <- errors + 1
            printfn $"%A{e}"
    printfn $"Errors: {errors}"
    if errors = 0 then 0 else 1
| [| "render"; shpFilePattern; outputDirectory |] ->
    let parent = Path.GetDirectoryName shpFilePattern
    let files = Directory.GetFiles(parent, Path.GetFileName shpFilePattern)
    let mutable success = 0
    let mutable errors = 0
    for path in files do
        try
            render path outputDirectory
            success <- success + 1
        with
        | e ->
            errors <- errors + 1
            printfn $"%A{e}"
    printfn $"Success: {success}"
    printfn $"Errors: {errors}"
    if errors = 0 then 0 else 1
| [| "palette"; inputDirectoryPath |] ->
    for file in Directory.GetFiles(inputDirectoryPath, "*.shp") do
        let name = Path.GetFileName file
        let palette = ShapePalette.get file
        let status = if File.Exists(Path.Combine(Path.GetDirectoryName file, palette)) then "ok" else "not found"
        printfn $"{name}: {palette}, {status}"
    0
| _ ->
    printfn "Usage:"
    printfn "  info <path-to-shp-file>: print shp file info (accepts glob)"
    printfn "  render <path-to-shp-file> <output-directory>: render all the sprites from the file (accepts glob)"
    printfn "  palette <path-to-directory>: list the palettes for each file in the directory"
    1
