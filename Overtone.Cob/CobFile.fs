namespace Overtone.Cob

open System
open System.IO
open System.Text

type CobFileEntry = {
    Path: string
    Offset: int
    Size: int
}

type CobFile(input: Stream) =
    let reader = new BinaryReader(input)
    interface IDisposable with
        member _.Dispose() =
            (reader :> IDisposable).Dispose()
            (input :> IDisposable).Dispose()

    member _.ReadEntries(): CobFileEntry seq =
        input.Seek(0L, SeekOrigin.Begin) |> ignore

        let count = reader.ReadInt32()
        if count < 0 then failwithf $"Item count in the archive: %d{count} is less than zero"
        if count = 0 then
            upcast Array.empty
        else
            let setEntrySizeFromNextOffset entry nextOffset =
                let sizedEntry = { entry with Size = nextOffset - entry.Offset }
                if sizedEntry.Size < 0 then
                    failwithf $"Couldn't calculate size for archive entry at %d{entry.Offset} (%s{entry.Path})"
                sizedEntry

            let entries = Array.zeroCreate count
            for i in 0..count - 1 do
                let path =
                    reader.ReadBytes 50
                    |> Array.takeWhile (fun x -> x <> 0uy)
                    |> Encoding.UTF8.GetString
                entries.[i] <- {
                    Path = path
                    Offset = 0
                    Size = 0
                }

            for i in 0..count - 1 do
                let offset = reader.ReadInt32()
                if i > 0 then
                    entries.[i - 1] <- setEntrySizeFromNextOffset entries.[i - 1] offset
                entries.[i] <- { entries.[i] with Offset = offset }

            // All the other entries are ready; now, fill the last one:
            entries.[count - 1] <- setEntrySizeFromNextOffset entries.[count - 1] (Checked.int input.Length)
            upcast entries

    member _.ReadEntry(entry: CobFileEntry): byte[] =
        input.Seek(int64 entry.Offset, SeekOrigin.Begin) |> ignore
        reader.ReadBytes entry.Size
