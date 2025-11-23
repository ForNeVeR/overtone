// SPDX-FileCopyrightText: 2021-2025 Friedrich von Never <friedrich@fornever.me>
//
// SPDX-License-Identifier: MIT

namespace Overtone.Resources

open System.IO

open SkiaSharp

open Checked

/// Type supporting reading .pal files from The Tone Rebellion.
type Palette = {
    Colors: SKColor[]
}
    with
    static member Read(input: Stream): Palette =
        use reader = new BinaryReader(input)
        let colors = Array.init 256 (fun _ -> SKColors.Transparent)
        for index in 0..255 do
            let r = reader.ReadByte() * 4uy
            let g = reader.ReadByte() * 4uy
            let b = reader.ReadByte() * 4uy
            colors.[index] <- SKColor(r, g, b)
        { Colors = colors }

    member this.GetColor(index: byte): SKColor =
        this.Colors[int index]
