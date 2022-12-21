﻿module Overtone.Tests.Resources.Pal

open System.Drawing
open System.IO

open Xunit

open Overtone.Resources

let greenPalFile = [|
    let color = Color.Green
    for _ in 0..255 do
        yield color.R / 4uy
        yield color.G / 4uy
        yield color.B / 4uy
|]

[<Fact>]
let ``.pal file should be read``(): unit =
    use stream = new MemoryStream(greenPalFile)
    let file = Palette.Read stream
    for color in file.Colors do
        Assert.Equal(Color.Green.ToArgb(), color.ToArgb())