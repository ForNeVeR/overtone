namespace Overtone.Game.Config

open System

open Microsoft.FSharp.Core
open Microsoft.Xna.Framework

open Overtone.Resources
open Overtone.Utils

type WindowEntry = {
    WindowType: int
    Name: string
    States: Set<int>
    ShapeId: string
    ShapeFrame: int
    MouseFocus: bool
    Pane: Rectangle
    Message: int*int*int
    MessageDestination: string
    ContRedraw: bool voption
    NumberOfStates: int voption
    HighlightFrame: int voption
    MovieName: string voption
    OpenPane: Rectangle voption
}

type WindowsConfiguration = {
    Entries: WindowEntry[]
} with
    static member Read(windowsTxt: byte[]): WindowsConfiguration =
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
        let numberOfStates = ref ValueNone
        let highlightFrame = ref ValueNone
        let movieName = ref ValueNone
        let openPane = ref ValueNone

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
            | "NSTATES" -> numberOfStates
            | "HILITEFRAME" -> highlightFrame
            | "MOVIENAME" -> movieName
            | "OPENPANE" -> openPane
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
            let toRect field = function
                | [| x0; y0; x1; y1 |] -> Geometry.rectFromCorners(struct(x0, y0), struct(x1, y1))
                | [| x0; y0; x1; y1; _; _ |] -> // TODO[#31]: Analyze the last two optional values
                    Geometry.rectFromCorners(struct(x0, y0), struct(x1, y1))
                | _ -> failwith $"Unrecognized value of field {field} for entry \"{entryName}\"."

            let requiredString field = requiredString field entryName
            let requiredInt field = requiredString field |> int
            let requiredIntSeq field =
                (requiredString field).Split([| ' '; '\t' |], StringSplitOptions.RemoveEmptyEntries)
                |> Seq.map int
            let requiredIntSet = requiredIntSeq >> Set.ofSeq
            let requiredBool field = requiredInt field |> toBool field
            let requiredIntArray = requiredIntSeq >> Seq.toArray
            let requiredRect field = requiredIntArray field |> toRect field
            let requiredMessage field =
                let data = requiredIntArray field
                match data with
                | [| a; b; c |] -> a, b, c
                | _ -> failwith $"Unrecognized value of field {field} for entry \"{entryName}\"."

            let optionalString field = (getValue field).Value
            let optionalInt = optionalString >> ValueOption.map int
            let optionalBool field = optionalInt field |> ValueOption.map(toBool field)
            let optionalRect field =
                optionalString field
                |> ValueOption.map(fun s ->
                    s.Split([| ' '; '\t' |], StringSplitOptions.RemoveEmptyEntries)
                    |> Array.map int
                    |> toRect field
                )

            let entry = {
                WindowType = requiredInt "WINTYPE"
                Name = entryName
                States = requiredIntSet "STATE"
                ShapeId = requiredString "SHAPEID"
                ShapeFrame = requiredInt "SHAPEFRAME"
                MouseFocus = requiredBool "MOUSEFOCUS"
                Pane = requiredRect "PANE"
                Message = requiredMessage "SENDMESSAGE"
                MessageDestination = requiredString "MSGDEST"
                ContRedraw = optionalBool "CONTREDRAW"
                NumberOfStates = optionalInt "NSTATES"
                HighlightFrame = optionalInt "HILITEFRAME"
                MovieName = optionalString "MOVIENAME"
                OpenPane = optionalRect "OPENPANE"
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
            numberOfStates.Value <- ValueNone
            highlightFrame.Value <- ValueNone
            movieName.Value <- ValueNone
            openPane.Value <- ValueNone

            entries.Add entry

        TextConfiguration.extractLines windowsTxt
        |> Seq.map TextConfiguration.readKeyValueEntry
        |> Seq.iter(
            function
            | "SHAPECACHE_SIZE", _ -> ()
            | "END", _ -> ()
            | key, value ->
                if hasValue key then
                    finalizeEntry()
                setValue key value
        )

        finalizeEntry()

        {
            Entries = Seq.toArray entries
        }

    member this.GetControls(stateId: int): Map<string, WindowEntry> =
        this.Entries
        |> Seq.filter(fun e -> e.States.Contains stateId)
        |> Seq.map(fun e -> e.Name, e)
        |> Map.ofSeq

    member this.GetControlsArray(stateId: int): WindowEntry[] =
        this.Entries
        |> Seq.filter(fun e -> e.States.Contains stateId)
        |> Seq.toArray
