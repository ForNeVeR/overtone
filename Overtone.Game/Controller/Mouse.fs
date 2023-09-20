namespace Overtone.Game.Controller

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

open Overtone.Game
open Overtone.Resources
open Overtone.Game.Controller

type Mouse(cursors: Map<CursorShape, MouseCursor>) =

    let getCursor shape = cursors[shape]

    let mutable cursorParameters = None

    member _.UpdateCursor(state: MouseState, scene: IController, game: Game): unit =
        let newCursorParameters = scene.GetCursor state
        if cursorParameters <> Some newCursorParameters then
            cursorParameters <- Some newCursorParameters

            let (CursorParameters(shape, isVisible)) = newCursorParameters
            Mouse.SetCursor(getCursor shape)
            game.IsMouseVisible <- isVisible

module Mouse =

    let Load(lifetime: Lifetime, device: GraphicsDevice, floatExeFile: byte[]): Mouse =
        let loadTexture(cursor: Cursor.CursorStructure) =
            use bitmap = Cursor.Render cursor
            let texture = Textures.toTexture(bitmap, lifetime, device)
            MouseCursor.FromTexture2D(texture, int cursor.HotspotX, int cursor.HotspotY)

        let allCursors =
            Cursor.Load floatExeFile
            |> Seq.map (fun c -> c.Id, c.Cursor)
            |> Map.ofSeq
        let cursorMap =
            [| CursorShape.Normal, loadTexture allCursors[19u] |]
            |> Map.ofSeq
        Mouse cursorMap
