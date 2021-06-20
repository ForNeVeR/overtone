module Overtone.Game.Program

[<EntryPoint>]
let main(_: string[]): int =
    use game = new OvertoneGame()
    game.Run()
    0
