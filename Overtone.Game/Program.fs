module Overtone.Game.Program

open Overtone.Resources

[<EntryPoint>]
let main(args: string[]): int =
    let discRoot = args[0]
    let disc = GameDisc discRoot

    use game = new OvertoneGame(disc)
    game.Run()
    0
