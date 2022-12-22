module Overtone.Resources.TextConfiguration

open System
open System.IO
open System.Text

let private clearLine(line: string) =
    let toOption int = if int < 0 then ValueNone else ValueSome int
    let ``;`` = line.IndexOf ';' |> toOption
    let ``//`` = line.IndexOf "//" |> toOption
    let commentPos =
        match ``;``, ``//`` with
        | ValueNone, x -> x
        | x, ValueNone -> x
        | ValueSome x, ValueSome y -> ValueSome <| min x y
    match commentPos with
    | ValueNone -> line
    | ValueSome pos -> line.Substring(0, pos)

let readKeyValueEntry(line: string): string*string =
    let components = line.Split([| ' '; '\t' |], 2, StringSplitOptions.RemoveEmptyEntries)
    match components with
    | [|key; value|] -> key, value
    | _ -> failwithf $"Cannot parse line: \"{line}\"."

/// Preprocess a text file, stripping comments and removing the empty lines.
let extractLines(data: byte[]): string seq = seq {
    use stream = new MemoryStream(data)
    use reader = new StreamReader(stream, Encoding.UTF8)

    let mutable line = reader.ReadLine()
    while not <| isNull line do
        let withoutComments = (clearLine line).TrimEnd()
        if withoutComments <> "" then
            yield withoutComments

        line <- reader.ReadLine()
}
