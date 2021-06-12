﻿open System.Drawing.Imaging
open System.IO

open Overtone.Resources
open Overtone.Resources.Shape

let private info name (file: ShapeFile) =
    printfn $"Shape file {name}"
    let headers = file.ReadSpriteHeaders() |> Seq.toArray
    printfn $"Sprite count: %d{headers.Length}"
    headers |> Array.iteri (fun i header ->
        let sprint(struct (x, y)) = sprintf $"({x}, {y})"

        let palette =
            match header.PaletteOffset with
            | Some offset -> string offset
            | None -> "absent"
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

    let palFilePath = Path.Combine(Path.GetDirectoryName inputFile, ShapePalette.get inputFile)
    use palStream = new FileStream(palFilePath, FileMode.Open)
    let palette = Palette.Read palStream

    let headers = file.ReadSpriteHeaders() |> Seq.toArray
    printfn $"  ({headers.Length} sprites)"
    if headers.[0].IsCorrupted then
        printfn "  File skipped due to sprite 0 being corrupted."
    else
        headers
        |> Seq.iteri (fun index header ->
            Directory.CreateDirectory outputDirectory |> ignore
            let outputFileName = $"{Path.GetFileNameWithoutExtension inputFile}_{index}.png"
            let outputFilePath = Path.Combine(outputDirectory, outputFileName)

            printf $"  Saving sprite {index} as {outputFileName}: "

            try
                let sprite = file.ReadSprite header
                if sprite.IsEmpty then printf "(empty) "
                use bitmap = file.Render palette sprite
                bitmap.Save(outputFilePath, ImageFormat.Png)
                printfn "ok."
            with
            | e ->
                printfn $"error: {e}"
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
    let mutable processed = 0
    for path in files do
        printfn $"Processing file \"{path}\"."
        render path outputDirectory
        processed <- processed + 1
    printfn $"Processed: {processed}"
    0
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
