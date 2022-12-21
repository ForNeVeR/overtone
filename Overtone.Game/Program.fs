module Overtone.Game.Program

open System.IO

open Overtone.Game.Windows
open Overtone.Resources

let private readWindowsConfiguration(disc: GameDisc) =
    task {
        use stream = new MemoryStream(disc.GetConfig "windows.txt")
        use reader = new StreamReader(stream)
        return! WindowConfiguration.Read reader
    } |> Async.AwaitTask |> Async.RunSynchronously


[<EntryPoint>]
let main(args: string[]): int =
    let discRoot = args[0]

    use disc = new GameDisc(discRoot)
    let config = readWindowsConfiguration disc

    use game = new OvertoneGame(disc, config)
    game.Run()
    0
