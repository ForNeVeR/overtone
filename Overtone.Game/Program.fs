module Overtone.Game.Program

open Overtone.Game.Config
open Overtone.Resources

[<EntryPoint>]
let main(args: string[]): int =
    let discRoot = args[0]

    use disc = new GameDisc(discRoot)
    let shapesConfig = ShapesConfiguration.Read <| disc.GetConfig "shapes.txt"
    let windowsConfig = WindowsConfiguration.Read <| disc.GetConfig "windows.txt"

    use game = new OvertoneGame(disc, shapesConfig, windowsConfig)
    game.Run()
    0
