module Overtone.Game.Windows.Controls

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Overtone.Game
open Overtone.Resources
open Overtone.Utilities

type Control(position: Rectangle, texture: Texture2D) =
    member _.Draw(batch: SpriteBatch): unit =
        batch.Draw(texture, position, Color.Black)

let Load(lifetime: Lifetime, device: GraphicsDevice, disc: GameDisc, entry: WindowEntry) =
    let texture = Texture.loadShape device disc entry.Shape entry.ShapeFrame |> Lifetimes.attach lifetime
    Control(entry.Pane, texture)
