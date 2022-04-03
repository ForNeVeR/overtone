module Overtone.Resources.Font

open System.IO
open Overtone.Resources

type FontGlyph(transparentColor: int, pixelData: int[,]) =
    member _.Render(palette: Palette) = ()

type Font = {
    Characters: Map<char, FontGlyph>
}

let read(input: Stream) =
    use reader = new BinaryReader(input)
    let versionData = reader.ReadBytes 4
    if versionData <> "1.\000\000"B then failwith "Invalid font file version."

    let count = reader.ReadInt32()
    let rowsPerGlyph = reader.ReadInt32()
    let transparencyKey = reader.ReadInt32()
    let glyphOffsets = Array.init count (fun _ -> reader.ReadInt32())

    ()
