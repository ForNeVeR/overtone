namespace Overtone.Game.UI

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Microsoft.Xna.Framework.Input

open Overtone.Game.Config
open Overtone.Game.Textures
open Overtone.Game


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
                // printfn "should act !"
                let sound = GameState.soundsConfig.GetSoundPerName("BUTTON",GameState.getDisc())
                sound.Play() |> ignore
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
