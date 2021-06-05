open System.IO

open Overtone.Resources.Shape

[<EntryPoint>]
let main: string[] -> int = function
| [|"info"; inputFilePath|] ->
    use stream = new FileStream(inputFilePath, FileMode.Open)
    let file = ShapeFile stream
    printfn $"Shape file %s{Path.GetFileName inputFilePath}"
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
    0
| _ ->
    printfn "Usage:\ninfo <path-to-shp-file>: print shp file info"
    1
