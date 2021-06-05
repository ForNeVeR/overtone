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
        let sprint(struct (x, y)) = sprintf $"%A{x}, %A{y}"

        let palette = if Option.isSome header.PaletteOffset then "present" else "absent"
        printfn $"Sprite {i}. Offset {header.SpriteOffset}, custom palette: {palette}"
        let sprite = file.ReadSprite header
        printfn $"  Size: {sprite.Width} × {sprite.Height}"
        printfn $"  Origin: {sprint sprite.Origin}"
        printfn $"  Start: {sprint sprite.Start}"
        printfn $"  End: {sprint sprite.End}"
        printfn $"  Data offset: {sprite.DataOffset}"
    )
    0
| _ ->
    printfn "Usage:\ninfo <path-to-shp-file>: print shp file info"
    1
