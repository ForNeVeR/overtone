// SPDX-FileCopyrightText: 2022-2025 Overtone contributors <https://github.com/ForNeVeR/overtone>
//
// SPDX-License-Identifier: MIT

namespace Overtone.Game.Config

open Overtone.Resources

type ShapesConfiguration(names: Map<string, string>) =

    static member Read(shapesTxt: byte[]): ShapesConfiguration =
        shapesTxt
        |> TextConfiguration.extractLines
        |> Seq.map TextConfiguration.readKeyValueEntry
        |> Seq.map(fun(a, b) -> b, a)
        |> Map.ofSeq
        |> ShapesConfiguration

    member _.GetShapeName(shapeId: string): string =
        // printfn "Loading shape : %s" shapeId
        if names.ContainsKey shapeId then
            names[shapeId]
        else
            shapeId
