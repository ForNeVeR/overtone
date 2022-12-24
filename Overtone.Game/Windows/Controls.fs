module Overtone.Game.Windows.Controls

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

let Load(lifetime: Lifetime, textureManager: TextureManager, entry: WindowEntry) =
    let texture = textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame)
    Control(texture, entry.Pane)
