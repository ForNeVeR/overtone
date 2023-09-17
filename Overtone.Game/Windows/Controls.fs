namespace Overtone.Game.Windows

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Microsoft.Xna.Framework.Input
open Overtone.Game
open Overtone.Game.Config

type Control(normalTexture: Texture2D, hoverTexture: Texture2D option, position: Rectangle) =

    let mutable isHover = false

    member _.Update(mouseState: MouseState): unit =
        isHover <- position.Contains mouseState.Position

    member _.Draw(batch: SpriteBatch): unit =
        let colorMask = Color.White
        let texture =
            match isHover, hoverTexture with
            | true, Some t -> t
            | _ -> normalTexture

        batch.Draw(
            texture,
            position = position.Location.ToVector2(),
            color = colorMask
        )

module Controls =
    let Load(lifetime: Lifetime, textureManager: Textures.Manager, entry: WindowEntry) =
        let normalTexture = textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame)
        let hoverTexture =
            if entry.MouseFocus && entry.WindowType = 1
            then Some <| textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame + 1)
            else None
        Control(normalTexture, hoverTexture, entry.Pane)
