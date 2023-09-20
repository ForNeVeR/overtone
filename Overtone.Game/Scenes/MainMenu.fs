namespace Overtone.Game.Scenes

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

open Overtone.Utils.Constants
open Overtone.Game
open Overtone.Game.UI

type MainMenu (lifetime: Lifetime, device: GraphicsDevice, textureManager: Textures.Manager) =
    
    let sparkles:Sparkles = Sparkles(lifetime, device)
    let title: IDrawableUI = Image(
        textureManager.LoadTexture(lifetime, Shapes.TitleScreen.Id, Shapes.TitleScreen.TitleFrame),
        Rectangle(0, -41, 640, 480)
    )

    interface IScene with
        member _.Draw(batch: SpriteBatch): unit =
            sparkles.Draw(batch)
            title.Draw(batch)

        member _.Update(time: GameTime, mouse: MouseState): unit =
            sparkles.Update(time)
