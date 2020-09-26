let printUsage() =
    printfn "Usage: Overtone.Cob <path to cob file> <path to output directory>\n\n  Extracts the COB archive contents."

[<EntryPoint>]
let main = function
| [|_filePath; _outputPath|] ->
    0
| _ ->
    printUsage()
    0
