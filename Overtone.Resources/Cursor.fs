// SPDX-FileCopyrightText: 2022-2025 Friedrich von Never <friedrich@fornever.me>
//
// SPDX-License-Identifier: MIT

module Overtone.Resources.Cursor

open System
open System.Collections
open System.IO

open AsmResolver.PE
open AsmResolver.PE.Win32Resources
open SkiaSharp

open type BitConverter

[<Struct>]
type CursorStructure = {
    HotspotX: int16
    HotspotY: int16
    Blob: byte[] // CUR file InfoHeader and all the data below
}

type NamedCursor = {
    Id: uint32
    Cursor: CursorStructure
}

let private readDataEntry(entry: IResourceData) =
    let reader = entry.CreateReader()
    let cursor = {
        HotspotX = reader.ReadInt16()
        HotspotY = reader.ReadInt16()
        Blob = reader.ReadToEnd()
    }
    {
        Id = entry.ParentDirectory.Id
        Cursor = cursor
    }

let rec private extractCursor(entry: IResourceEntry) =
    match entry with
    | :? IResourceDirectory as dir -> dir.Entries |> Seq.collect extractCursor
    | :? IResourceData as data -> Seq.singleton <| readDataEntry data
    | _ -> failwith $"Unknown entry format of entry {entry}."

let Load(input: byte[]): NamedCursor[] =
    let image = PEImage.FromBytes input
    image.Resources.GetDirectory(ResourceType.Cursor).Entries
    |> Seq.collect extractCursor
    |> Seq.toArray


let Save (outDir: string) (cursors: NamedCursor seq): unit =
    for cursor in cursors do
        let path = Path.Combine(outDir, $"{string cursor.Id}.cur")
        let cursor = cursor.Cursor
        let file = [|
            // See the cursor file description here: http://www.daubnet.com/en/file-format-cur
            // Cursor file header:
            yield! GetBytes 0s // reserved
            yield! GetBytes 2s // type
            yield! GetBytes 1s // cursor count
            // Entry list (1 item):
            32uy // width
            32uy // height
            0uy // color count
            0uy // reserved
            yield! GetBytes cursor.HotspotX
            yield! GetBytes cursor.HotspotY
            yield! GetBytes cursor.Blob.Length // size of InfoHeader + ANDBitmap + XORBitmap,
                                               // i.e. size of everything else in the resource except the hotspot coords
            yield! GetBytes 22 // offset from the beginning of the file to InfoHeader,
                               // i.e. the position where the BLOB starts
            yield! cursor.Blob
        |]
        File.WriteAllBytes(path, file)

let Render(cursor: CursorStructure): SKBitmap =
    let infoHeader = cursor.Blob |> Seq.take 40

    let width = infoHeader |> Seq.skip 4 |> Seq.take 4 |> Seq.toArray |> BitConverter.ToInt32
    let height = infoHeader |> Seq.skip 8 |> Seq.take 4 |> Seq.toArray |> BitConverter.ToInt32
    assert (width = 32)
    assert (height = 64) // 32 for xor bitmap + 32 for and bitmap

    let colors = cursor.Blob |> Seq.skip 40 |> Seq.take 8
    let readColor offset =
        match colors |> Seq.skip offset |> Seq.take 3 |> Seq.toArray with
        | [| r; g; b |] -> SKColor(r, g, b)
        | _ -> failwith "Not enough bytes for color."
    let color1 = readColor 0
    let color2 = readColor 4

    let bitmaps = cursor.Blob |> Seq.skip 48
    let xorBitmap = bitmaps |> Seq.take(32 * 32 / 8) |> Seq.toArray // 32×32 px, 8 px per byte
    let andBitmap = bitmaps |> Seq.skip(32 * 32 / 8) |> Seq.take(32 * 32 / 8) |> Seq.toArray

    let xorBits = xorBitmap |> BitArray
    let andBits = andBitmap |> BitArray

    let getColor x y =
        let lineIndex = (31 - y) * 32
        let byteIndex = x / 8
        let bitIndexInsideByte = 8 - x % 8 - 1
        let bitIndex = lineIndex + byteIndex * 8 + bitIndexInsideByte
        match andBits[bitIndex], xorBits[bitIndex] with
        | false, false -> color1
        | false, true -> color2
        | true, false -> SKColors.Transparent
        | true, true -> failwith "Inverted background pixel requested"

    let cursorBitmap = new SKBitmap(width, height / 2)
    for x in 0..31 do
        for y in 0..31 do
            cursorBitmap.SetPixel(x, y, getColor x y)
    cursorBitmap

let RenderAll (outDir: string) (cursors: NamedCursor seq): unit =
    for cursor in cursors do
        let outPath = Path.Combine(outDir, $"{string cursor.Id}.png")
        use bitmap = Render cursor.Cursor
        SkiaUtils.Render(bitmap, outPath)
