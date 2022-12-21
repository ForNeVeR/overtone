namespace Overtone.Game.Windows

open System
open System.IO
open System.Threading.Tasks
open Microsoft.FSharp.Core
open Microsoft.Xna.Framework

type WindowEntry = {
    WindowType: int
    Name: string
    States: Set<int>
    Shape: string
    ShapeFrame: int
    MouseFocus: bool
    Pane: Rectangle
    Message: int*int*int
    MessageDestination: string
    ContRedraw: bool voption
}

type WindowConfiguration = {
    Entries: WindowEntry[]
} with
    static member Read(reader: TextReader): Task<WindowConfiguration> = task {
        let readEntry(line: string) =
            let components = line.Split([| ' '; '\t' |], StringSplitOptions.RemoveEmptyEntries)
            match components with
            | [||] -> None
            | [|key; value|] -> Some(key, value)
            | _ -> failwithf $"Cannot parse windows.txt line: \"{line}\"."

        let entries = ResizeArray()

        let windowType = ref ValueNone
        let name = ref ValueNone
        let states = ref ValueNone
        let shape = ref ValueNone
        let shapeFrame = ref ValueNone
        let mouseFocus = ref ValueNone
        let pane = ref ValueNone
        let message = ref ValueNone
        let messageDestination = ref ValueNone
        let contRedraw = ref ValueNone

        let getValue = function
            | "WINTYPE" -> windowType
            | "NAME" -> name
            | "STATE" -> states
            | "SHAPEID" -> shape
            | "SHAPEFRAME" -> shapeFrame
            | "MOUSEFOCUS" -> mouseFocus
            | "PANE" -> pane
            | "SENDMESSAGE" -> message
            | "MSGDEST" -> messageDestination
            | "CONTREDRAW" -> contRedraw
            | other -> failwith $"Unrecognized window.txt entry key: {other}"
        let hasValue = getValue >> (fun c -> c.Value) >> ValueOption.isSome
        let setValue k v =
            let ref = getValue k
            ref.Value <- ValueSome v

        let finalizeEntry() =
            let undefined (valueName: string) (entityName: string) =
                fun() -> failwith $"Value of {valueName} for entity \"{entityName}\" not defined."

            let requiredString field entity =
                (getValue field)
                |> fun c -> c.Value
                |> ValueOption.defaultWith(undefined field entity)

            let entityName = requiredString "NAME" "<unknown>"

            let requiredString field = requiredString field entityName
            let requiredInt field = requiredString field |> int
            let requiredIntSet field =
                (requiredString field).Split([| ' '; '\t' |], StringSplitOptions.RemoveEmptyEntries)
                |> Seq.map int
                |> Set.ofSeq

            let entry = {
                WindowType = requiredInt "WindowType"
                Name = entityName
                States = requiredIntSet "STATE"
                Shape = failwith "todo"
                ShapeFrame = failwith "todo"
                MouseFocus = failwith "todo"
                Pane = failwith "todo"
                Message = failwith "todo"
                MessageDestination = failwith "todo"
                ContRedraw = failwith "todo"
            }
            entries.Add entry

        let! line = reader.ReadLineAsync()
        let mutable line = line
        while not <| isNull line do
            match readEntry line with
            | None -> ()
            | Some("SHAPECACHE_SIZE", _) -> ()
            | Some("END", _) -> ()
            | Some(key, value) ->
                if hasValue key then
                    finalizeEntry()
                setValue key value

            let! newLine = reader.ReadLineAsync()
            line <- newLine

        finalizeEntry()

        return {
            Entries = Seq.toArray entries
        }
    }
