// SPDX-FileCopyrightText: 2022-2025 Friedrich von Never <friedrich@fornever.me>
//
// SPDX-License-Identifier: MIT

module Overtone.Resources.Font

open System.IO
open System.Text

[<StructuralEquality; StructuralComparison>]
type Glyph = {
    TransparentColor: byte
    PixelData: byte[,]
}

type Font = {
    Characters: Map<char, Glyph>
}

let read(input: Stream): Font =
    let encoding = Encoding.GetEncoding 437

    use reader = new BinaryReader(input)
    let versionData = reader.ReadBytes 4
    if versionData <> "1.\000\000"B then failwith "Invalid font file version."

    let count = reader.ReadInt32()
    let rowsPerGlyph = reader.ReadInt32()
    let transparencyKey = reader.ReadInt32() |> Checked.byte
    let glyphOffsets = Array.init count (fun _ -> reader.ReadInt32())

    let readGlyph offset =
        let oldPosition = input.Position
        try
            input.Position <- Checked.int64 offset
            use glyphReader = new BinaryReader(input, encoding, leaveOpen = true) // encoding doesn't matter here
            let cols = glyphReader.ReadInt32()
            let data = Array2D.zeroCreate cols rowsPerGlyph
            for y in 0..rowsPerGlyph - 1 do
                for x in 0..cols - 1 do
                    data.[x, y] <- glyphReader.ReadByte()

            if cols = 0 then None
            else Some <| {
                TransparentColor = transparencyKey
                PixelData = data
            }
        finally
            input.Position <- oldPosition

    let chars =
        glyphOffsets
        |> Seq.map readGlyph

    {
        Characters =
            chars
            |> Seq.mapi(fun idx glyph ->
                let char = encoding.GetChars [| Checked.byte idx |] |> Seq.exactlyOne
                char, glyph
            )
            |> Seq.choose(fun (char, optGlyph) -> Option.map (fun g -> (char, g)) optGlyph)
            |> Map.ofSeq
    }
