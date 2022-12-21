module Overtone.Tests.Resources.Fnt

open System
open System.IO
open System.Text

open Xunit

open Overtone.Resources

let private file = [|
    let version = "1.\000\000"B
    let count = 2
    let rowsPerGlyph = 2
    let transparent = 0
    let offsets = [| 24; 32 |]

    let character bytes = [|
        let columns = (Array.length bytes) / rowsPerGlyph
        yield! BitConverter.GetBytes columns
        yield! bytes
    |]

    yield! version
    yield! BitConverter.GetBytes count
    yield! BitConverter.GetBytes rowsPerGlyph
    yield! BitConverter.GetBytes transparent
    yield! offsets |> Array.collect BitConverter.GetBytes
    yield! character [|
        0uy; 1uy
        1uy; 0uy
    |]
    yield! character [|
        1uy; 1uy
        1uy; 0uy
    |]
|]

[<Fact>]
let ``FNT file is read correctly``(): unit =
    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance)

    use stream = new MemoryStream(file)
    let font = Font.read stream
    let expected = Map.ofArray [|
        '\000', {
            Font.Glyph.TransparentColor = 0uy
            Font.Glyph.PixelData = array2D [|
                [| 0uy; 1uy |]
                [| 1uy; 0uy |]
            |]
        }
        '\001', {
            TransparentColor = 0uy
            PixelData = array2D [|
                [| 1uy; 1uy |]
                [| 1uy; 0uy |]
            |]
        }
    |]
    let result = expected = font.Characters
    Assert.True result
