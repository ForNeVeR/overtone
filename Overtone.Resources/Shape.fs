namespace Overtone.Resources.Shape

open System
open System.IO
open System.Text

type SpriteHeader = {
    SpriteOffset: int
    PaletteOffset: int option
}

type UShortPoint = (struct (uint16 * uint16))
type Point = (struct (int * int))

type SpriteData = {
    Header: SpriteHeader
    Width: uint16
    Height: uint16
    Origin: UShortPoint
    Start: Point
    End: Point
    DataOffset: int64
}

type ShapeFile(input: Stream) =
    member _.ReadSpriteHeaders(): SpriteHeader seq =
        input.Seek(0L, SeekOrigin.Begin) |> ignore
        use reader = new BinaryReader(input, Encoding.UTF8, leaveOpen = true)

        let headerBytes = reader.ReadBytes 4
        let header = Encoding.UTF8.GetString headerBytes
        if header <> "1.10" then failwithf $"Invalid shape file header: %s{BitConverter.ToString headerBytes}"

        let spriteCount = reader.ReadInt32()
        upcast Array.init spriteCount (fun _ -> {
            SpriteOffset = reader.ReadInt32()
            PaletteOffset =
                match reader.ReadInt32() with
                    | 0 -> None
                    | offset -> Some offset
        })

    member _.ReadSprite(header: SpriteHeader): SpriteData =
        input.Seek(int64 header.SpriteOffset, SeekOrigin.Begin) |> ignore
        use reader = new BinaryReader(input, Encoding.UTF8, leaveOpen = true)
        let height = reader.ReadUInt16() + 1us
        let width = reader.ReadUInt16() + 1us
        let originY = reader.ReadUInt16()
        let originX = reader.ReadUInt16()
        let startX = reader.ReadInt32()
        let startY = reader.ReadInt32()
        let endX = reader.ReadInt32()
        let endY = reader.ReadInt32()
        {
            Header = header
            Width = width
            Height = height
            Origin = struct (originX, originY)
            Start = struct (startX, startY)
            End = struct (endX, endY)
            DataOffset = input.Position
        }
