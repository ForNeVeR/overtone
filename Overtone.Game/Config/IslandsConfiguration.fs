namespace Overtone.Game.Config

open System
open System.IO
open System.Text
open Overtone.Resources
open Overtone.Utils.Constants

//
// HIGH LEVEL FILE FORMAT
//
// SIZE_COUNT
//
// FOREACH ENTRY: (SIZE_COUNT)
//    ISLAND_COUNT
//    FOREACH ISLAND: (ISLAND_COUNT)
//       ANGLE RANGE 0 SHAPEID (SERVES AS MAPID !)
//    END
//    UNTIL -1: (BRIDGE DEFINITIONS)
//       ISLAND_FROM ISLAND_TO
//    END
// END
//


//
// shapedId is visible planet
// shapedId + GameData.IslandsCount is the same planet but in "not opened" state
// shapedId + GameData.IslandsCount*2 is the same planet but in opened and mouse-hovered state
//

//
// Bridges should connect iterating on their initial order to each bridge pair on each planet, taking the first "free" bridge entity
//

type IslandData (shapedId: int, distance: int, baseAngle: int)=
    member _.shapeIndexDisplayed=shapedId
    member _.shapeIndexHidden=shapedId + GameData.IslandsCount
    member _.shapeIndexHovered=shapedId + GameData.IslandsCount*2
    member _.distance=distance
    member _.baseAngle=baseAngle

type WorldDefinition ()=
    member _.IslandData=0

    member _.islands: ResizeArray<IslandData> = new ResizeArray<IslandData>()

    member this.addIslandEntry(angle:int, distance:int, shapeid:int)=
        this.islands.Add(new IslandData(shapeid,distance,angle))

type IslandsConfiguration() =

    let mutable currentId = 0
    let mutable currentIsland = 0

    member _.Read(shapesTxt: byte[]): unit =

        let mutable currentWorldEdition = new WorldDefinition()
        let mutable islandData = new IslandData(0,0,0)

        let parseline(line:string):unit =
            let components = line.Split([| ' '; '\t' |], 4, StringSplitOptions.RemoveEmptyEntries)
            match components with
            | [|"-1"|] ->
                currentId <- currentId + 1
                printfn($"Switch to world size {currentId}")
                currentIsland <- 0
            | [|bridgeFrom; bridgeTo|] -> printfn($"{bridgeFrom} -> {bridgeTo}")
            | [|angle; distance; _; shapeid|] ->
                printfn($"Island {currentIsland} shape : {shapeid} at angle {angle}, distance {distance}")
                currentIsland <- currentIsland + 1
            | _ -> printfn($"noop : {line}")

        shapesTxt
        |> TextConfiguration.extractLines
        |> Seq.iter parseline
