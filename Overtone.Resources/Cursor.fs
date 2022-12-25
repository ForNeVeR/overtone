module Overtone.Resources.Cursor

open System.IO

open AsmResolver.PE
open AsmResolver.PE.Win32Resources

open type System.BitConverter

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
