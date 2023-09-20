namespace Overtone.Game.Controller

open Microsoft.Xna.Framework.Input
open Overtone.Game.Controller

type IController =
    abstract member GetCursor: MouseState -> CursorParameters
