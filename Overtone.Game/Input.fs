namespace Overtone.Game.Input

type CursorShape =
    Normal = 0

type CursorParameters = CursorParameters of CursorShape * isVisible: bool
