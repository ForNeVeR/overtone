// SPDX-FileCopyrightText: 2021-2025 Friedrich von Never <friedrich@fornever.me>
//
// SPDX-License-Identifier: MIT

module Overtone.Tests.Resources.Pal

open System.IO

open SkiaSharp
open Xunit

open Overtone.Resources

let greenPalFile = [|
    let color = SKColors.Green
    for _ in 0..255 do
        yield color.Red / 4uy
        yield color.Green / 4uy
        yield color.Blue / 4uy
|]

[<Fact>]
let ``.pal file should be read``(): unit =
    use stream = new MemoryStream(greenPalFile)
    let file = Palette.Read stream
    for color in file.Colors do
        Assert.Equal(SKColors.Green, color)
