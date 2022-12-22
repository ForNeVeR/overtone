namespace Overtone.Game.Config

open Overtone.Resources

type ShapesConfiguration(names: Map<string, string>) =

    static member Read(shapesTxt: byte[]): ShapesConfiguration =
        shapesTxt
        |> TextConfiguration.extractLines
        |> Seq.map TextConfiguration.readKeyValueEntry
        |> Map.ofSeq
        |> ShapesConfiguration

    member _.GetShapeName(shapeId: string): string =
        names[shapeId]
