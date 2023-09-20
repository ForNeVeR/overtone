namespace Overtone.Game.UI

open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

type IDrawableUI =
    abstract member isHover: bool 
    abstract member Update: (MouseState) -> (int * int * int) 
    abstract member Draw: (SpriteBatch) -> unit
