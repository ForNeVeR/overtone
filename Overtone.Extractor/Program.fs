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
            eprintf $"%A{e}"
    printf $"Errors: {errors}"
    if errors = 0 then 0 else 1
| [| "render"; shpFilePath; imageFilePath |] ->
    use stream = new FileStream(shpFilePath, FileMode.Open)
    let file = ShapeFile stream
    info (Path.GetFileName shpFilePath) file

    let palFilePath = Path.ChangeExtension(shpFilePath, "pal")
    use palStream = new FileStream(palFilePath, FileMode.Open)
    let palette = Palette.Read palStream

    let bitmap =
        file.ReadSpriteHeaders()
        |> Seq.head
        |> file.ReadSprite
        |> file.Render palette

    Directory.CreateDirectory(Path.GetDirectoryName imageFilePath) |> ignore
    bitmap.Save(imageFilePath, ImageFormat.Png)

    0
| _ ->
    printfn "Usage:"
    printfn "  info <path-to-shp-file>: print shp file info (accepts glob)"
    printfn "  render <path-to-shp-file> <path-to-output-file>: render the first sprite from the file"
    1
