module Overtone.Game.Program

open Overtone.Game.Config
open Overtone.Resources

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
    let mainTheme = soundsConfig.GetSoundPerName("START.WAV",GameState.getDisc())
    mainTheme.Play() |> ignore
    game.Run()
    0
