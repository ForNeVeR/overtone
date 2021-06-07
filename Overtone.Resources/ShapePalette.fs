module Overtone.Resources.ShapePalette

open System
open System.IO

let private (|Island|_|): string -> string option = function
| island when island.StartsWith("i") && Char.IsDigit island.[1] && Char.IsDigit island.[2] ->
    Some(island.Substring(1, 2))
| _ -> None

let get(shapeFilePath: string): string =
    let palette = Path.ChangeExtension(shapeFilePath, "pal")
    if File.Exists palette then
        Path.GetFileName palette
    else
        let name = Path.GetFileName shapeFilePath
        match name with
        | Island(number) -> $"island{number}.pal"
        | "bigfloat.shp" | "glyphs.shp" | "levidif.shp" | "newback.shp" -> "newgame.pal"
        | "l-cryton.shp" | "l-enrich.shp" | "l-extend.shp" -> "newgame.pal"
        | "end.shp" | "endglw.shp" | "mapback.shp" | "smisle.shp" -> "mainmap.pal"
        | "endtemp.shp" -> "endgame.pal"
        | endtem when endtem.StartsWith("endtem") -> $"endgam{endtem.[6]}.pal"
        | "smrealms.shp" | "life.shp" | "seek.shp" -> "endgame.pal"
        | _ -> "game.pal"
