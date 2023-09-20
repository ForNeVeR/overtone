namespace Overtone.Game.UI

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

open Overtone.Game.Config
open Overtone.Game.Textures

type DropDownMenuButton(texture: Texture2DWithOffset, entry: WindowEntry) =
    let mutable isHover = false
    let mutable isOpened = false
    let mutable ActivePosition: Rectangle = entry.Pane

    interface IDrawableUI with
        member _.isHover= isHover

        member _.Update(mouseState: MouseState) : (int * int * int) =
            isHover <- ActivePosition.Contains mouseState.Position

            if mouseState.LeftButton = ButtonState.Pressed then
                if isHover && not isOpened then
                    isOpened <- true
                    ActivePosition <- entry.OpenPane.Value
                    (0, 0, 0)
                else if not isHover && isOpened then
                    isOpened <- false
                    ActivePosition <- entry.Pane
                    (0, 0, 0)
                else if isHover && isOpened then
                    (1, 0, 0)
                else
                    (0, 0, 0)
            else
                (0, 0, 0)

        member _.Draw(batch: SpriteBatch) : unit =
            let colorMask = Color.White
            batch.Draw(texture.texture, ActivePosition, color = colorMask)
