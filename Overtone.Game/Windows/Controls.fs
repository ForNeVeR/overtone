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
                && entry.WindowType = WindowTypes.Button
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

module Controls =
    let Load (lifetime: Lifetime, textureManager: Manager, entry: WindowEntry): IDrawableUI =

        let normalTexture =
            textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame)

        if entry.MouseFocus && entry.WindowType = WindowTypes.Button then
            let hoverTexture = textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame + 1)
            Button(normalTexture, hoverTexture, entry)
        else
            Basic(normalTexture, entry.Pane)


    let LoadImg (lifetime: Lifetime, textureManager: Manager, entry: WindowEntry): IDrawableUI =

        let normalTexture =
            textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame)

        Basic(normalTexture, entry.Pane)
