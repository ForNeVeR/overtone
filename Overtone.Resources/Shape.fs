namespace Overtone.Resources.Shape

open System
open System.IO
open System.Text

open SkiaSharp

open Overtone.Resources

open Checked

type SpriteHeader = {
    SpriteOffset: int
    PaletteOffset: int option
} with
    member this.IsCorrupted: bool =
        Option.isSome this.PaletteOffset

type UShortSize = (struct (uint16 * uint16))
type UShortPoint = (struct (uint16 * uint16))
type Point = (struct (int * int))

type SpriteData = {
    Header: SpriteHeader
    CanvasDimensions: UShortSize
    Origin: UShortPoint
    Start: Point
    End: Point
    DataOffset: int64
} with
    member this.IsEmpty: bool =
        let struct (startX, startY) = this.Start
        let struct (endX, endY) = this.End
        let empty x = x &&& 0x7fff0000 = 0x7fff0000
        empty startX && empty startY && empty endX && empty endY

    member this.Width: int =
        let struct (x0, _) = this.Start
        let struct (x1, _) = this.End
        abs(x1 - x0)

    member this.Height: int =
        let struct (_, y0) = this.Start
        let struct (_, y1) = this.End
        abs(y1 - y0)

type ShapeFile(input: Stream) =
    member _.ReadSpriteHeaders(): SpriteHeader[] =
        input.Seek(0L, SeekOrigin.Begin) |> ignore
        use reader = new BinaryReader(input, Encoding.UTF8, leaveOpen = true)

        let headerBytes = reader.ReadBytes 4
        let header = Encoding.UTF8.GetString headerBytes
        if header <> "1.10" then failwithf $"Invalid shape file header: %s{BitConverter.ToString headerBytes}"

        let spriteCount = reader.ReadInt32()
        Array.init spriteCount (fun _ -> {
            SpriteOffset = reader.ReadInt32()
            PaletteOffset =
                match reader.ReadInt32() with
                    | 0 -> None
                    | offset -> Some offset
        })

    member _.ReadSprite(header: SpriteHeader): SpriteData =
        input.Seek(int64 header.SpriteOffset, SeekOrigin.Begin) |> ignore
        use reader = new BinaryReader(input, Encoding.UTF8, leaveOpen = true)
        let canvasHeight = reader.ReadUInt16() + 1us
        let canvasWidth = reader.ReadUInt16() + 1us
        let originY = reader.ReadUInt16()
        let originX = reader.ReadUInt16()
        let startX = reader.ReadInt32()
        let startY = reader.ReadInt32()
        let endX = reader.ReadInt32()
        let endY = reader.ReadInt32()
        {
            Header = header
            CanvasDimensions = struct (canvasWidth, canvasHeight)
            Origin = struct (originX, originY)
            Start = struct (startX, startY)
            End = struct (endX, endY)
            DataOffset = input.Position
        }

    member _.Render (palette: Palette) (sprite: SpriteData): SKBitmap =
        let struct (canvasWidth, canvasHeight) = sprite.CanvasDimensions
        let bitmap = new SKBitmap(int canvasWidth, int canvasHeight)

        if not sprite.IsEmpty then
            let struct (originX, originY) = sprite.Origin
            let struct (startX, startY) = sprite.Start
            let struct (endX, endY) = sprite.End
            let spriteHeight = endY - startY + 1

            let minCanvasX = int originX + startX
            let minCanvasY = int originY + startY
            let maxCanvasX = int originX + endX
            let maxCanvasY = int originY + endY

            input.Seek(sprite.DataOffset, SeekOrigin.Begin) |> ignore
            use reader = new BinaryReader(input, Encoding.UTF8, leaveOpen = true)

            for spriteY in 0..spriteHeight - 1 do
                let canvasY = int originY + startY
                if canvasY < minCanvasY || canvasY > maxCanvasY then
                    failwith $"Pixel row {canvasY} is outside of allowed bounds [{minCanvasY}, {maxCanvasY}]"

                let mutable canvasX = int originX + startX

                let addPixel idx =
                    if canvasX < minCanvasX || canvasX > maxCanvasX then
                        failwith $"Pixel column {canvasX} is outside of allowed bounds [{minCanvasX}; {maxCanvasX}]"
                    bitmap.SetPixel(canvasX, canvasY + spriteY, palette.GetColor idx)
                    canvasX <- canvasX + 1
                let skipPixels count =
                    canvasX <- canvasX + int count

                let mutable endRow = false
                while not endRow do
                    let indicator = reader.ReadByte()
                    let length = indicator >>> 1
                    match indicator with
                    | 0uy -> endRow <- true
                    | 1uy -> skipPixels(reader.ReadByte())
                    | x when x &&& 1uy = 0uy ->
                        let color = reader.ReadByte()
                        for _ in 1uy..length do
                            addPixel color
                    | _ -> reader.ReadBytes(int length) |> Seq.iter addPixel

        bitmap
