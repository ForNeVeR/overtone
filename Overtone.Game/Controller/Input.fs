namespace Overtone.Game.Controller

type CursorShape =
    Normal = 0

type CursorParameters = CursorParameters of CursorShape * isVisible: bool
