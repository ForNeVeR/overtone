namespace Overtone.Game

open System

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Overtone.Resources

type MenuScene(gameDisc: GameDisc, graphicsDevice: GraphicsDevice) =
    let background = Texture.loadShape graphicsDevice gameDisc @"data\titscrn.shp"

    interface IDisposable with
        member _.Dispose() =
            background.Dispose()

    member _.Draw(gameTime: GameTime): unit =
        use batch = new SpriteBatch(graphicsDevice)
        batch.Begin()
        batch.Draw(background, Vector2.Zero, Color.Aquamarine)
        batch.End()


