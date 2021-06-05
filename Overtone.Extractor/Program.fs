open System.IO

open Overtone.Resources.Shape

[<EntryPoint>]
let main: string[] -> int = function
| [|"info"; inputFilePath|] ->
    use stream = new FileStream(inputFilePath, FileMode.Open)
    let file = ShapeFile stream
    printfn $"Shape file %s{Path.GetFileName inputFilePath}"
    let sprites = file.ReadSprites() |> Seq.toArray
    printfn $"Sprite count: %d{sprites.Length}"
    sprites |> Array.iteri (fun i sprite ->
        let palette = if Option.isSome sprite.PaletteOffset then "present" else "absent"
        printfn $"Sprite %d{i}. Offset %d{sprite.SpriteOffset}, custom palette: %s{palette}"
    )
    0
| _ ->
    printfn "Usage:\ninfo <path-to-shp-file>: print shp file info"
    1
