namespace Overtone.Game.Scenes

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

type IScene =
    abstract member Draw: SpriteBatch -> unit
    abstract member DrawBackground: SpriteBatch -> unit
    abstract member Update: GameTime * MouseState -> unit 
