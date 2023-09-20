namespace Overtone.Game.Config

open Overtone.Resources

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
