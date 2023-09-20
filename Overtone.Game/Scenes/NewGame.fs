namespace Overtone.Game.Scenes

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

open Overtone.Utils.Constants
open Overtone.Game
open Overtone.Game.UI

type NewGame (lifetime: Lifetime, device: GraphicsDevice, textureManager: Textures.Manager) =


    interface IScene with

        member _.Draw(batch: SpriteBatch): unit =
            printfn "Draw main menu !"

        member _.Update(time: GameTime, mouse: MouseState): unit =
            printfn "Update main menu !"

// Ressources to display :

// BIGFLOAT (anim + foreach race)
// GLYPHS (anim + foreach race)
// NGREALMS (foreach race)
// LEVIDIF (depends on current dif)
