namespace Overtone.Game.UI

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Microsoft.Xna.Framework.Input
open Overtone.Game.Textures

type Image(texture: Texture2DWithOffset, position: Rectangle) =
    let mutable isHover = false

    interface IDrawableUI with
        member _.isHover= isHover

        member _.Update(mouseState: MouseState) : (int * int * int) =
            isHover <- position.Contains mouseState.Position
            (0, 0, 0)

        member _.Draw(batch: SpriteBatch) : unit =
            let colorMask = Color.White
            batch.Draw(texture.texture, position = position.Location.ToVector2() + texture.offset, color = colorMask)
