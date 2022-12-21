module Overtone.Tests.Resources.Shp

open System.Collections.Generic
open System.Drawing
open System.IO
open System.Text

open Xunit

open Overtone.Resources
open Overtone.Resources.Shape

open Checked

[<Fact>]
let ``SpriteHeader.IsCorrupted tests``(): unit =
    Assert.False { SpriteOffset = 0; PaletteOffset = None }.IsCorrupted
    Assert.True { SpriteOffset = 0; PaletteOffset = Some 0 }.IsCorrupted

let private zeroSprite = {
    Header = { SpriteOffset = 0; PaletteOffset = None }
    CanvasDimensions = struct (0us, 0us)
    Origin = struct (0us, 0us)
    Start = struct (0, 0)
    End = struct (0, 0)
    DataOffset = 0L
}

let private emptySprite = {
    zeroSprite with
        Start = struct (0x7fff0000, 0x7fff0000)
        End = struct (0x7fff0000, 0x7fff0000)
}

[<Fact>]
let ``Empty sprite should be detected by 0x7fff``(): unit =
    Assert.False zeroSprite.IsEmpty
    Assert.True emptySprite.IsEmpty

let private newShapeStream(sprites: IList<SpriteData * byte[]>) =
    let stream = new MemoryStream()
    use writer = new BinaryWriter(stream, Encoding.UTF8, true)

    writer.Write(Encoding.UTF8.GetBytes "1.10")
    writer.Write sprites.Count

    let mutable lastOffset = int stream.Position + sprites.Count * 8
    for _, data in sprites do
        writer.Write lastOffset
        writer.Write 0

        let spriteDataHeaderSize = 24
        lastOffset <- lastOffset + spriteDataHeaderSize + data.Length

    for sprite, data in sprites do
        let struct (canvasWidth, canvasHeight) = sprite.CanvasDimensions
        let struct (originX, originY) = sprite.Origin
        let struct (startX, startY) = sprite.Start
        let struct (endX, endY) = sprite.End
        writer.Write(canvasHeight - 1us)
        writer.Write(canvasWidth - 1us)
        writer.Write originY
        writer.Write originX
        writer.Write startX
        writer.Write startY
        writer.Write endX
        writer.Write endY

        writer.Write data

    stream

[<Fact>]
let ``Empty sprite file should be read successfully``(): unit =
    use input = newShapeStream Array.empty
    let file = ShapeFile input
    Assert.Empty(file.ReadSpriteHeaders())

[<Fact>]
let ``Sprite should be read correctly``(): unit =
    let expectedSprite1 = {
        zeroSprite with
            CanvasDimensions = struct (1us, 1us)
    }
    let expectedSprite2 = {
        zeroSprite with
            CanvasDimensions = struct (10us, 20us)
            Origin = struct (5us, 10us)
            Start = struct (-5, -10)
            End = struct (5, 10)
    }
    use input = newShapeStream [| expectedSprite1, Array.empty; expectedSprite2, Array.empty |]
    let file = ShapeFile input
    let sprites = file.ReadSpriteHeaders() |> Seq.map file.ReadSprite |> Seq.toArray
    Assert.Equal(2, sprites.Length)
    let sprite1 = sprites.[0]
    let sprite2 = sprites.[1]

    Assert.Equal(expectedSprite1.CanvasDimensions, sprite1.CanvasDimensions)
    Assert.Equal(expectedSprite1.Origin, sprite1.Origin)
    Assert.Equal(expectedSprite1.Start, sprite1.Start)
    Assert.Equal(expectedSprite1.End, sprite1.End)

    Assert.Equal(expectedSprite2.CanvasDimensions, sprite2.CanvasDimensions)
    Assert.Equal(expectedSprite2.Origin, sprite2.Origin)
    Assert.Equal(expectedSprite2.Start, sprite2.Start)
    Assert.Equal(expectedSprite2.End, sprite2.End)

let blackPalette =
    let data = Array.zeroCreate(256 * 3)
    use stream = new MemoryStream(data)
    Palette.Read stream

let private assertColor (expected: Color) (actual: Color) =
    if expected.A = 0uy
    then Assert.Equal(expected.A, actual.A)
    else
        Assert.Equal(Color.FromArgb(int expected.A, int expected.R, int expected.G, int expected.B), actual)

[<Fact>]
let ``Empty sprite should render to an empty image``(): unit =
    let sprite = { emptySprite with CanvasDimensions = struct (1us, 1us) }
    use input = newShapeStream [| sprite, Array.empty |]
    let file = ShapeFile input
    use bitmap = file.ReadSpriteHeaders() |> Seq.exactlyOne |> file.ReadSprite |> file.Render blackPalette
    Assert.Equal(1, bitmap.Width)
    Assert.Equal(1, bitmap.Height)
    assertColor Color.Transparent (bitmap.GetPixel(0, 0))

module private Draw =
    let color palette (clr: Color) =
        palette.Colors
        |> Array.findIndex (fun c -> c.ToArgb() = clr.ToArgb())
        |> byte

    let skip(count: byte) = seq {
        1uy
        count
    }
    let fill (palette: Palette) (length: byte) (c: Color) =
        if length >= 128uy then failwith $"Length of {length} is invalid for fill"
        seq {
            length <<< 1
            color palette c
        }
    let pixels (palette: Palette) (pixels: IList<_>) =
        seq {
            byte pixels.Count <<< 1 ||| 1uy
            yield! Seq.map (color palette) pixels
        }
    let endRow = seq { 0uy }

[<Fact>]
let ``Zero indicator should switch to a new row``(): unit =
    let sprite = {
        zeroSprite with
            CanvasDimensions = struct (2us, 2us)
            End = struct (1, 1)
    }
    let data = [|
        yield! Draw.endRow
        yield! Draw.pixels blackPalette [| Color.Black |]
        yield! Draw.endRow
    |]

    use input = newShapeStream [| sprite, data |]
    let file = ShapeFile input
    use bitmap = file.ReadSpriteHeaders() |> Seq.exactlyOne |> file.ReadSprite |> file.Render blackPalette

    assertColor Color.Transparent (bitmap.GetPixel(0, 0))
    assertColor Color.Transparent (bitmap.GetPixel(1, 0))
    assertColor Color.Black (bitmap.GetPixel(0, 1))
    assertColor Color.Transparent (bitmap.GetPixel(1, 1))

[<Fact>]
let ``One indicator should skip pixels``(): unit =
    let sprite = {
        zeroSprite with
            CanvasDimensions = struct (3us, 1us)
            End = struct (2, 0)
    }
    let data = [|
        yield! Draw.skip 1uy
        yield! Draw.pixels blackPalette [| Color.Black |]
        yield! Draw.endRow
    |]

    use input = newShapeStream [| sprite, data |]
    let file = ShapeFile input
    use bitmap = file.ReadSpriteHeaders() |> Seq.exactlyOne |> file.ReadSprite |> file.Render blackPalette

    assertColor Color.Transparent (bitmap.GetPixel(0, 0))
    assertColor Color.Black (bitmap.GetPixel(1, 0))
    assertColor Color.Transparent (bitmap.GetPixel(2, 0))

[<Fact>]
let ``Even indicator should do fill``(): unit =
    let sprite = {
        zeroSprite with
            CanvasDimensions = struct (6us, 1us)
            End = struct (5, 0)
    }
    let data = [|
        yield! Draw.fill blackPalette 3uy Color.Black
        yield! Draw.endRow
    |]

    use input = newShapeStream [| sprite, data |]
    let file = ShapeFile input
    use bitmap = file.ReadSpriteHeaders() |> Seq.exactlyOne |> file.ReadSprite |> file.Render blackPalette

    for x in 0..2 do assertColor Color.Black (bitmap.GetPixel(x, 0))
    for x in 3..5 do assertColor Color.Transparent (bitmap.GetPixel(x, 0))

[<Fact>]
let ``Odd indicator should do several pixels in a row``(): unit =
    let palette = {
        Colors = [| Color.Red; Color.Green; Color.Blue |]
    }
    let sprite = {
        zeroSprite with
            CanvasDimensions = struct (5us, 1us)
            End = struct (4, 0)
    }
    let data = [|
        yield! Draw.pixels palette [| Color.Red; Color.Green; Color.Blue |]
        yield! Draw.endRow
    |]

    use input = newShapeStream [| sprite, data |]
    let file = ShapeFile input
    use bitmap = file.ReadSpriteHeaders() |> Seq.exactlyOne |> file.ReadSprite |> file.Render palette

    assertColor Color.Red (bitmap.GetPixel(0, 0))
    assertColor Color.Green (bitmap.GetPixel(1, 0))
    assertColor Color.Blue (bitmap.GetPixel(2, 0))
    for x in 3..4 do assertColor Color.Transparent (bitmap.GetPixel(x, 0))
