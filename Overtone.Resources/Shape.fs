namespace Overtone.Resources.Shape

open System
open System.IO
open System.Text

type SpriteData = {
    SpriteOffset: int
    PaletteOffset: int option
}

type ShapeFile(input: Stream) =
    member _.ReadSprites(): SpriteData seq =
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
