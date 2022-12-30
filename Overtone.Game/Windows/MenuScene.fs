namespace Overtone.Game.Windows

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

open Overtone.Game
open Overtone.Game.Config
open Overtone.Game.Input

type MenuScene(lifetime: Lifetime,
               graphicsDevice: GraphicsDevice,
               textureManager: Textures.Manager,
               config: WindowsConfiguration) =
    let sceneId = 0 // "Starting state" of windows.txt

    let controls = config.GetControls sceneId
    let loadControl id = Controls.Load(lifetime, textureManager, controls[id])
    let background = loadControl "BACKGRND"
    let title = Control(
        textureManager.LoadTexture(lifetime, Shapes.TitleScreen.Id, Shapes.TitleScreen.TitleFrame),
        None,
        Rectangle(0, -41, 640, 480)
    )
    let newGameButton = loadControl "NEWGAME"
    let exitButton = loadControl "EXITAPP"
    let resumeButton = loadControl "RESUME"
    let loadButton = loadControl "LOADGAME"
    let allControls = [| background; title; newGameButton; exitButton; resumeButton; loadButton |]

    member _.Update(mouseState: MouseState): unit =
        allControls |> Array.iter(fun c -> c.Update mouseState)

    member _.Draw _: unit =
        use batch = new SpriteBatch(graphicsDevice)
        batch.Begin()
        for control in allControls do
            control.Draw batch
        batch.End()

    interface IScene with
        member this.GetCursor _ =
            CursorParameters(CursorShape.Normal, isVisible = true)
