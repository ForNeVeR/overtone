namespace Overtone.Game.Windows

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Microsoft.Xna.Framework.Input
open Overtone.Game
open Overtone.Game.Config
open Overtone.Game.Constants

type Control(normalTexture: Texture2D, hoverTexture: Texture2D option, entry: WindowEntry) =
    let mutable isHover = false

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
            | true, Some t -> t
            | _ -> normalTexture

        batch.Draw(texture, position = entry.Pane.Location.ToVector2(), color = colorMask)

module Controls =
    let Load (lifetime: Lifetime, textureManager: Textures.Manager, entry: WindowEntry) =
        printfn "Try loading : %s at frame %d" entry.ShapeId entry.ShapeFrame

        let normalTexture =
            textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame)

        let hoverTexture =
            if entry.MouseFocus && entry.WindowType = WindowTypes.Button then
                Some
                <| textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame + 1)
            else
                None

        Control(normalTexture, hoverTexture, entry)
