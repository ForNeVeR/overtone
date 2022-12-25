module Overtone.Resources.Cursor

open System.IO

open AsmResolver.PE
open AsmResolver.PE.Win32Resources

type CursorData = {
    Name: string
    Data: byte[]
}

let private readDataEntry(entry: IResourceData) =
    let name = string entry.ParentDirectory.Id
    // TODO[#14]: Figure out the file format.
    {
        Name = name
        Data = entry.CreateReader().ReadToEnd()
    }

let rec private extractCursor(entry: IResourceEntry) =
    match entry with
    | :? IResourceDirectory as dir -> dir.Entries |> Seq.collect extractCursor
    | :? IResourceData as data -> Seq.singleton <| readDataEntry data
    | _ -> failwith $"Unknown entry format of entry {entry}."

let Load(input: byte[]): CursorData[] =
    let image = PEImage.FromBytes input
    image.Resources.GetDirectory(ResourceType.Cursor).Entries
    |> Seq.collect extractCursor
    |> Seq.toArray

let Save (outDir: string) (cursors: CursorData seq): unit =
    for cursor in cursors do
        let path = Path.Combine(outDir, $"{cursor.Name}.cur")
        File.WriteAllBytes(path, cursor.Data)
