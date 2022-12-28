namespace Overtone.Game.Windows

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Overtone.Game
open Overtone.Game.Config

type Control(texture: Texture2D, position: Rectangle) =
    member _.Draw(batch: SpriteBatch): unit =
        let colorMask = Color.White
        batch.Draw(
            texture,
            destinationRectangle = position,
            color = colorMask
        )

module Controls =
    let Load(lifetime: Lifetime, textureManager: Textures.Manager, entry: WindowEntry) =
        let texture = textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame)
        Control(texture, entry.Pane)
