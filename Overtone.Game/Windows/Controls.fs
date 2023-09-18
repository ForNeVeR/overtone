namespace Overtone.Game.Windows

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Microsoft.Xna.Framework.Input

open Overtone.Game.Config
open Overtone.Game.Constants
open Overtone.Game.Textures


type IDrawableUI =
    abstract member isHover: bool 
    abstract member Update: (MouseState) -> (int * int * int) 
    abstract member Draw: (SpriteBatch) -> unit


type Button(normalTexture: Texture2DWithOffset, hoverTexture: Texture2DWithOffset, entry: WindowEntry) =

    let mutable isHover = false

    interface IDrawableUI with

        member _.isHover= isHover
        member _.Update(mouseState: MouseState) : (int * int * int) =
            isHover <- entry.Pane.Contains mouseState.Position

            if
                isHover
                && mouseState.LeftButton = ButtonState.Pressed
            then
                // Debug for now
                printfn "should act !"
                entry.Message
            else
                (0, 0, 0)

        member _.Draw(batch: SpriteBatch) : unit =
            let colorMask = Color.White

            let texture =
                match isHover, hoverTexture with
                | true, t -> t
                | _ -> normalTexture

            batch.Draw(texture.texture, position = entry.Pane.Location.ToVector2() + texture.offset, color = colorMask)

type Basic(texture: Texture2DWithOffset, position: Rectangle) =
    let mutable isHover = false

    interface IDrawableUI with
        member _.isHover= isHover

        member _.Update(mouseState: MouseState) : (int * int * int) =
            isHover <- position.Contains mouseState.Position
            (0, 0, 0)

        member _.Draw(batch: SpriteBatch) : unit =
            let colorMask = Color.White
            batch.Draw(texture.texture, position = position.Location.ToVector2() + texture.offset, color = colorMask)

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

module Controls =
    let Load (lifetime: Lifetime, textureManager: Manager, entry: WindowEntry): IDrawableUI =

        let normalTexture =
            textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame)

        if entry.MouseFocus && entry.WindowType = WindowTypes.Button then
            let hoverTexture = textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame + 1)
            Button(normalTexture, hoverTexture, entry)
        else if entry.MouseFocus && entry.WindowType = WindowTypes.DropDownMenu then
            DropDownMenuButton(normalTexture, entry)
        else
            Basic(normalTexture, entry.Pane)


    let LoadImg (lifetime: Lifetime, textureManager: Manager, entry: WindowEntry): IDrawableUI =

        let normalTexture =
            textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame)

        Basic(normalTexture, entry.Pane)
