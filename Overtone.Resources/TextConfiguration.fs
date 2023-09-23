module Overtone.Resources.TextConfiguration

open System
open System.IO
open System.Text

let commentsChar= "/;".ToCharArray()

let private clearLine(line: string) =
    let commentStart = line.IndexOfAny(commentsChar)
    if commentStart <> -1 then
        line.Substring(0, commentStart)
    else
        line

// Used for `WINTYPE        1`
let readKeyValueEntry(line: string): string*string =
    let components = line.Split([| ' '; '\t' |], 2, StringSplitOptions.RemoveEmptyEntries)
    match components with
    | [|key; value|] -> key, value
    | _ -> failwithf $"Cannot parse line: \"{line}\"."
    
// Used for `0       2000    0        0`
let readPreformatedEntries(line: string): string array =
    line.Split([| ' '; '\t' |], StringSplitOptions.RemoveEmptyEntries)

/// Preprocess a text file, stripping comments and removing the empty lines.
let extractLines(data: byte[]): string seq = seq {
    use stream = new MemoryStream(data)
    use reader = new StreamReader(stream, Encoding.UTF8)

    let mutable line = reader.ReadLine()
    while not (isNull line) do
        let withoutComments = (clearLine line).TrimEnd()
        if withoutComments <> "" then
            yield withoutComments

        line <- reader.ReadLine()
}
