namespace Overtone.Game

open Overtone.Utils.Constants
open Overtone.Game.Config
open Overtone.Resources

//
// This holds the gamestate
//

module GameState =
    let mutable currentRace: int = -1
    let mutable currentDifficulty: int = 0
    let mutable currentMapSize: int = 0
    let mutable discRoot: string = ""
    let mutable disc: Option<GameDisc> = None

    let mutable soundsConfig: SoundsConfiguration= new SoundsConfiguration(Map.empty)
    let mutable shapesConfig: ShapesConfiguration= new ShapesConfiguration(Map.empty)


    let init(rootPath): unit=
        discRoot <- rootPath
        let currentDisc = new GameDisc(rootPath)
        disc <- Some(currentDisc)
        let islands = new IslandsConfiguration()
        islands.Read <| currentDisc.GetData "data\\worldpos.txt"
        soundsConfig <- SoundsConfiguration.Read <| currentDisc.GetConfig "sound.txt"
        shapesConfig <- ShapesConfiguration.Read <| currentDisc.GetConfig "shapes.txt"
        // windowsConfig <- WindowsConfiguration.Read <| currentDisc.GetConfig "windows.txt"

    let getDisc(): GameDisc=
        match disc with
            | Some(output) -> output
            | None ->
                let output = new GameDisc(discRoot)
                output


    let handleButtonEvent() =
        //
        printfn("stuff !")

    let ChangeDifficulty() =
        currentDifficulty <- currentDifficulty + 1
        if (currentDifficulty >= GameData.DifficultyCount) then
            currentDifficulty <- 0
            
    let ChangeWorldSize() =
        currentMapSize <- currentMapSize + 1
        if (currentMapSize >= GameData.WorldSizeCount) then
            currentMapSize <- 0
            
    let SelectRace(newRace: int) =
        if newRace = currentRace then
            currentRace <- -1
        else
            currentRace <- newRace
