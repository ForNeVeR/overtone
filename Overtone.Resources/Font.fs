module Overtone.Resources.Font

open System.IO
open System.Text
open Overtone.Resources

type FontGlyph(transparentColor: int, pixelData: byte[,]) =
    member _.Render(palette: Palette) = ()

type Font = {
    Characters: Map<char, FontGlyph>
}

let read(input: Stream): Font =
    use reader = new BinaryReader(input)
    let versionData = reader.ReadBytes 4
    if versionData <> "1.\000\000"B then failwith "Invalid font file version."

    let count = reader.ReadInt32()
    let rowsPerGlyph = reader.ReadInt32()
    let transparencyKey = reader.ReadInt32()
    let glyphOffsets = Array.init count (fun _ -> reader.ReadInt32())

    let readGlyph offset =
        let oldPosition = input.Position
        try
            input.Position <- Checked.int64 offset
            use glyphReader = new BinaryReader(input, Encoding.UTF8, leaveOpen = true)
            let cols = glyphReader.ReadInt32()
            let data = Array2D.zeroCreate cols rowsPerGlyph
            for x in 0..cols - 1 do
                for y in 0..rowsPerGlyph - 1 do
                    data.[x, y] <- glyphReader.ReadByte()

            FontGlyph(transparencyKey, data)
        finally
            input.Position <- oldPosition

    let chars = glyphOffsets |> Array.map readGlyph

    {
        Characters =
            chars
            |> Seq.mapi (fun idx glyph -> char idx, glyph)
            |> Map.ofSeq
    }
