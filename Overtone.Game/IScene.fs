namespace Overtone.Game

open Microsoft.Xna.Framework.Input
open Overtone.Game.Input

type IScene =
    abstract member GetCursor: MouseState -> CursorParameters
