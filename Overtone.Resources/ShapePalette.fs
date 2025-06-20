module Overtone.Resources.ShapePalette

open System
open System.IO
open Overtone.Resources

let private (|Island|_|): string -> string option = function
| island when island.StartsWith("i") && Char.IsDigit island.[1] && Char.IsDigit island.[2] ->
    Some(island.Substring(1, 2))
| _ -> None

let get(shapeFilePath: string): string =
    let name = (Path.GetFileName (shapeFilePath)).ToLower()
    let resName =
        match name with
        | Island(number) -> $"island{number}.pal"
        | "bigfloat.shp" | "glyphs.shp" | "levidif.shp" | "newback.shp" -> "newgame.pal"
        | "l-cryton.shp" | "l-enrich.shp" | "l-extend.shp" -> "newgame.pal"
        | "end.shp" | "endglw.shp" | "mapback.shp" | "smisle.shp" -> "mainmap.pal"
        | "endtemp.shp" -> "endgame.pal"
        | ngb when ngb.StartsWith("ngb") -> "mainmap.pal"
        | ng when ng.StartsWith("ng") -> "newgame.pal"
        | endtem when endtem.StartsWith("endtem") -> $"endgam{endtem.[6]}.pal"
        | "smrealms.shp" | "life.shp" | "seek.shp" -> "endgame.pal"
        | _ -> "game.pal"
    $@"data\{resName}"

let getWithDisk(shapeFilePath: string, disc: GameDisc): string =
    let name = Path.GetFileName shapeFilePath
    let simpleRename = "data\\"+Path.ChangeExtension(name, "pal").ToUpper()
    if disc.hasDataEntry simpleRename
    then
        simpleRename
    else
        get shapeFilePath
