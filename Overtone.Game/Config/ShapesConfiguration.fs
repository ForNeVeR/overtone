namespace Overtone.Game.Config

open Overtone.Resources

type ShapesConfiguration(names: Map<string, string>) =

    static member Read(shapesTxt: byte[]): ShapesConfiguration =
        shapesTxt
        |> TextConfiguration.extractLines
        |> Seq.map TextConfiguration.readKeyValueEntry
        |> Seq.map(fun(a, b) -> b.ToUpper(), a.ToUpper())
        |> Map.ofSeq
        |> ShapesConfiguration

    member _.GetShapeName(shapeId: string): string =
        // printfn "Loading shape : %s" shapeId
        if names.ContainsKey shapeId then
            names[shapeId]
        else
            shapeId
