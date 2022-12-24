module Overtone.Game.Windows.Controls

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Overtone.Game
open Overtone.Game.Config
open Overtone.Utilities

type Control(position: Rectangle, texture: Texture2D) =
    member _.Draw(batch: SpriteBatch): unit =
        let colorMask = Color.White
        batch.Draw(texture, position, colorMask)

let Load(lifetime: Lifetime, textureManager: TextureManager, entry: WindowEntry) =
    let texture = textureManager.LoadTexture(entry.ShapeId, entry.ShapeFrame) |> Lifetimes.attach lifetime
    Control(entry.Pane, texture)
