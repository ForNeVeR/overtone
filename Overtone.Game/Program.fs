// SPDX-FileCopyrightText: 2021-2026 Overtone contributors <https://github.com/ForNeVeR/overtone>
//
// SPDX-License-Identifier: MIT

module Overtone.Game.Program

open Overtone.Game.Config

[<EntryPoint>]
let main(args: string[]): int =
    let discRoot = args[0]

    GameState.discRoot <- discRoot
    GameState.init(discRoot)
    let islands = new IslandsConfiguration()
    islands.Read <| GameState.getDisc().GetData "data\\worldpos.txt"

    let soundsConfig = SoundsConfiguration.Read <| GameState.getDisc().GetConfig "sound.txt"
    let shapesConfig = ShapesConfiguration.Read <| GameState.getDisc().GetConfig "shapes.txt"
    let windowsConfig = WindowsConfiguration.Read <| GameState.getDisc().GetConfig "windows.txt"

    use game = new OvertoneGame(GameState.getDisc(), shapesConfig, windowsConfig)

    // TODO: Should become a part of a scene.
    let mainTheme = soundsConfig.GetSoundPerName("START.WAV",GameState.getDisc())
    mainTheme.Play() |> ignore
    game.Run()
    0
