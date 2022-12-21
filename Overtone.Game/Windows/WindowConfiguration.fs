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
            let components = line.Split([| ' '; '\t' |], 2, StringSplitOptions.RemoveEmptyEntries)
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
                fun() -> failwith $"Value of {valueName} for entry \"{entityName}\" not defined."

            let requiredString field entity =
                (getValue field)
                |> fun c -> c.Value
                |> ValueOption.defaultWith(undefined field entity)

            let entryName = requiredString "NAME" "<unknown>"

            let toBool field = function
                | 0 -> false
                | 1 -> true
                | o -> failwith $"Unrecognized bool value of field {field}: {o} for entry \"{entryName}\"."

            let requiredString field = requiredString field entryName
            let requiredInt field = requiredString field |> int
            let requiredIntSeq field =
                (requiredString field).Split([| ' '; '\t' |], StringSplitOptions.RemoveEmptyEntries)
                |> Seq.map int
            let requiredIntSet = requiredIntSeq >> Set.ofSeq
            let requiredBool field = requiredInt field |> toBool field
            let requiredIntArray = requiredIntSeq >> Seq.toArray
            let requiredRect field =
                let data = requiredIntArray field
                match data with
                | [| x0; y0; x1; y1 |] -> Rectangle(x0, y0, x1 - x0, y1 - y0)
                | _ -> failwith $"Unrecognized value of field {field} for entry \"{entryName}\"."
            let requiredMessage field =
                let data = requiredIntArray field
                match data with
                | [| a; b; c |] -> a, b, c
                | _ -> failwith $"Unrecognized value of field {field} for entry \"{entryName}\"."

            let optionalBool field = (getValue field).Value |> ValueOption.map(int >> toBool field)

            let entry = {
                WindowType = requiredInt "WINTYPE"
                Name = entryName
                States = requiredIntSet "STATE"
                Shape = requiredString "SHAPEID"
                ShapeFrame = requiredInt "SHAPEFRAME"
                MouseFocus = requiredBool "MOUSEFOCUS"
                Pane = requiredRect "PANE"
                Message = requiredMessage "SENDMESSAGE"
                MessageDestination = requiredString "MSGDEST"
                ContRedraw = optionalBool "CONTREDRAW"
            }

            windowType.Value <- ValueNone
            name.Value <- ValueNone
            states.Value <- ValueNone
            shape.Value <- ValueNone
            shapeFrame.Value <- ValueNone
            mouseFocus.Value <- ValueNone
            pane.Value <- ValueNone
            message.Value <- ValueNone
            messageDestination.Value <- ValueNone
            contRedraw.Value <- ValueNone

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
