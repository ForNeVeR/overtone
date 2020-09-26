module Overtone.Cob.Program

open System
open System.IO

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
        printfn "%d: %s (%d bytes)" e.Offset e.Path e.Size
    )
    0
| _ ->
    printUsage()
    0
