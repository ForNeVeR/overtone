namespace Overtone.Game.Config

open Overtone.Resources

//
// HIGH LEVEL FILE FORMAT
//
// SIZE_COUNT
//
// FOREACH ENTRY: (SIZE_COUNT)
//    ISLAND_COUNT
//    FOREACH ISLAND: (ISLAND_COUNT)
//       ANGLE RANGE 0 SHAPEID (SERVES AS MAPID !)
//    END
//    UNTIL -1: (BRIDGE DEFINITIONS)
//       ISLAND_FROM ISLAND_TO
//    END
// END
//

type IslandsConfiguration(names: Map<string, string>) =

    static member Read(shapesTxt: byte[]): IslandsConfiguration =
        shapesTxt
        |> TextConfiguration.extractLines
        |> Seq.map TextConfiguration.readKeyValueEntry
        |> Seq.map(fun(a, b) -> b, a)
        |> Map.ofSeq
        |> IslandsConfiguration

    member _.GetShapeName(shapeId: string): string =
        // printfn "Loading shape : %s" shapeId
        if names.ContainsKey shapeId then
            names[shapeId]
        else
            shapeId
