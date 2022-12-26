namespace Overtone.Game.Input

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input
open Overtone.Game
open Overtone.Resources

type Mouse(cursors: Map<CursorShape, MouseCursor>) =

    let getCursor shape = cursors[shape]

    let mutable cursorShape = CursorShape.Normal

    member _.UpdateCursor(state: MouseState, scene: IScene): unit =
        let newCursorShape = scene.GetCursor state
        if cursorShape <> newCursorShape then
            cursorShape <- newCursorShape
            Mouse.SetCursor(getCursor cursorShape)

    member _.Draw(sprite: SpriteBatch): unit = failwith "TODO"

module Mouse =

    let private loadTexture cursor = failwith "TODO"

    let Load(floatExeFile: byte[]): Mouse =
        let allCursors =
            Cursor.Load floatExeFile
            |> Seq.map (fun c -> c.Id, c)
            |> Map.ofSeq
        let cursorMap =
            [| CursorShape.Normal, loadTexture allCursors[4u] |]
            |> Map.ofSeq
        Mouse cursorMap
