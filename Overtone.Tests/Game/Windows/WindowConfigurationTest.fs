module Overtone.Tests.Game.Windows.WindowConfigurationTest

open System.Text

open Microsoft.Xna.Framework
open Overtone.Game.Config
open Xunit

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
let ``WindowConfiguration should read correctly``(): unit =
    let bytes = Encoding.UTF8.GetBytes mockedConfig
    let config = WindowsConfiguration.Read bytes

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
            Pane = Rectangle(0, 0, width = 640, height = 480)
            MessageDestination = "DEST"
            ContRedraw = ValueSome false
            MovieName = ValueSome "aaa.avi"
        }
        { emptyEntry with
            Name = "XXY"
            States = Set.singleton 0
            ShapeId = "SCR"
            Pane = Rectangle(0, 0, width = 2, height = 2)
            Message = 0, 1, 0
            MessageDestination = "DEST"
            NumberOfStates = ValueSome 11
            HighlightFrame = ValueSome 4
            OpenPane = ValueSome <| Rectangle(1, 1, width = 4, height = 4)
        }
    |], config.Entries)

[<Fact>]
let ``WindowConfiguration GetControls should output elements assigned only to the sceneId``(): unit =
    let bytes = Encoding.UTF8.GetBytes mockedConfig
    let config = WindowsConfiguration.Read bytes

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

    let Scene0 = config.GetControls 0
    Assert.Equal<string>([|"XXX";"XXY"|], Scene0.Keys)
    Assert.Equal<WindowEntry>(
        { emptyEntry with
            Name = "XXY"
            States = Set.singleton 0
            ShapeId = "SCR"
            Pane = Rectangle(0, 0, width = 2, height = 2)
            Message = 0, 1, 0
            MessageDestination = "DEST"
            NumberOfStates = ValueSome 11
            HighlightFrame = ValueSome 4
            OpenPane = ValueSome <| Rectangle(1, 1, width = 4, height = 4)
        },
        Scene0["XXY"])
    Assert.Equal<WindowEntry>(
        { emptyEntry with
            Name = "XXX"
            States = Set.ofArray [| 0; 1 |]
            ShapeId = "SCR"
            Pane = Rectangle(0, 0, width = 640, height = 480)
            MessageDestination = "DEST"
            ContRedraw = ValueSome false
            MovieName = ValueSome "aaa.avi"
        },
        Scene0["XXX"])
        
    let Scene1 = config.GetControls 1
    Assert.Equal<string>([|"XXX"|], Scene1.Keys)
    Assert.Equal<WindowEntry>(
        { emptyEntry with
            Name = "XXX"
            States = Set.ofArray [| 0; 1 |]
            ShapeId = "SCR"
            Pane = Rectangle(0, 0, width = 640, height = 480)
            MessageDestination = "DEST"
            ContRedraw = ValueSome false
            MovieName = ValueSome "aaa.avi"
        },
        Scene0["XXX"])