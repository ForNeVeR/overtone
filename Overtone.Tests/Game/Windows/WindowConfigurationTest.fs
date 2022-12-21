module Overtone.Tests.Game.Windows.WindowConfigurationTest

open System.IO
open System.Text
open System.Threading.Tasks

open Microsoft.Xna.Framework
open Xunit

open Overtone.Game.Windows

let private mockedConfig = @"
SHAPECACHE_SIZE 1
; ---
WINTYPE     0 ; ---
NAME        XXX
STATE       0 1
SHAPEID     SCR
SHAPEFRAME  0
MOUSEFOCUS  0
PANE        0 0 639 479 0 -3
SENDMESSAGE 0 0 0
MSGDEST     DEST
CONTREDRAW  0
MOVIENAME aaa.avi
; ---
WINTYPE     0 // ---
NAME        XXY
STATE       0
SHAPEID     SCR
SHAPEFRAME  0
MOUSEFOCUS  0
PANE        0 0 1 1
SENDMESSAGE 0 1 0
MSGDEST     DEST
NSTATES     11
HILITEFRAME 4
OPENPANE 1 1 4 4
// --- End:
END -1
"

[<Fact>]
let ``WindowConfiguration should read correctly``(): Task = task {
    let bytes = Encoding.UTF8.GetBytes mockedConfig
    use stream = new MemoryStream(bytes)
    use reader = new StreamReader(stream)
    let! config = WindowConfiguration.Read reader

    let emptyEntry = {
        WindowType = 0
        Name = ""
        States = Set.empty
        ShapeId = ""
        ShapeFrame = 0
        MouseFocus = false
        Pane = Rectangle.Empty
        Message = 0, 0, 0
        MessageDestination = ""
        ContRedraw = ValueNone
        NumberOfStates = ValueNone
        HighlightFrame = ValueNone
        MovieName = ValueNone
        OpenPane = ValueNone
    }

    Assert.Equal<WindowEntry>([|
        { emptyEntry with
            Name = "XXX"
            States = Set.ofArray [| 0; 1 |]
            ShapeId = "SCR"
            Pane = Rectangle(0, 0, 639, 479)
            MessageDestination = "DEST"
            ContRedraw = ValueSome false
            MovieName = ValueSome "aaa.avi"
        }
        { emptyEntry with
            Name = "XXY"
            States = Set.singleton 0
            ShapeId = "SCR"
            Pane = Rectangle(0, 0, 1, 1)
            Message = 0, 1, 0
            MessageDestination = "DEST"
            NumberOfStates = ValueSome 11
            HighlightFrame = ValueSome 4
            OpenPane = ValueSome <| Rectangle(1, 1, 3, 3)
        }
    |], config.Entries)
}
