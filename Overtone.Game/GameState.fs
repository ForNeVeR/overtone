// SPDX-FileCopyrightText: 2023-2025 Overtone contributors <https://github.com/ForNeVeR/overtone>
//
// SPDX-License-Identifier: MIT

namespace Overtone.Game

open Overtone.Utils.Constants

//
// This holds the gamestate
//

module GameState =
    let mutable currentRace: int = -1
    let mutable currentDifficulty: int = 0
    let mutable currentMapSize: int = 0

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
