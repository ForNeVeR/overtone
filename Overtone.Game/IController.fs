namespace Overtone.Game

open Microsoft.Xna.Framework.Input
open Overtone.Game.Input

type IController =
    abstract member GetCursor: MouseState -> CursorParameters
