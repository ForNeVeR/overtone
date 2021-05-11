module Overtone.Cob.Program

open System
open System.IO

open Overtone.Resources.Cob

let private printUsage() =
    Console.WriteLine (
        "Usage:\n" +
        "Overtone.Cob ls <path to a COB archive>\n" +
        "  Lists the archive contents.\n\n" +
        "Overtone.Cob x <path to a COB archive> <path to the output directory>\n" +
        "  Extracts the COB archive contents to the output directory (will be created if not exists)."
    )

[<EntryPoint>]
let main: string[] -> int = function
| [|"ls"; archivePath|] ->
    use input = new FileStream(archivePath, FileMode.Open)
    use file = new CobFile(input)
    file.ReadEntries() |> Seq.iter (fun e ->
        printfn $"%d{e.Offset}: %s{e.Path} (%d{e.Size} bytes)"
    )
    0
| [|"x"; archivePath; outputPath|] ->
    use input = new FileStream(archivePath, FileMode.Open)
    use file = new CobFile(input)
    file.ReadEntries() |> Seq.iter (fun e ->
        let targetPath = Path.Combine(outputPath, e.Path.Replace('\\', Path.DirectorySeparatorChar))
        printfn $"Extracting file %s{e.Path} to %s{targetPath} (%d{e.Size} bytes)"

        Directory.CreateDirectory(Path.GetDirectoryName targetPath) |> ignore

        let data = file.ReadEntry e
        File.WriteAllBytes(targetPath, data)
    )
    0
| _ ->
    printUsage()
    0
