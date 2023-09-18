namespace Overtone.Game.Handler

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Overtone.Game
open Overtone.Game.Config
open Overtone.Game.Constants
open Overtone.Game.Input

type MainMenu () =

    static member onLoad(): unit =
        printfn "Loaded main menu !"

    static member Draw(batch: SpriteBatch): unit =
        printfn "Draw main menu !"

    static member Update(time: GameTime): unit =
        printfn "Update main menu !"