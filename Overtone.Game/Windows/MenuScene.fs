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
               textureManager: TextureManager,
               config: WindowsConfiguration,
               mouse: Mouse) =
    let sceneId = 0 // "Starting state" of windows.txt

    let controls = config.GetControls sceneId
    let loadControl id = Controls.Load(lifetime, textureManager, controls[id])
    let background = loadControl "BACKGRND"
    let title = Control(
        textureManager.LoadTexture(lifetime, Shapes.TitleScreen.Id, Shapes.TitleScreen.TitleFrame),
        Rectangle(0, -41, 640, 480)
    )
    let newGameButton = loadControl "NEWGAME"
    let exitButton = loadControl "EXITAPP"
    let resumeButton = loadControl "RESUME"
    let loadButton = loadControl "LOADGAME"
    let allControls = [| background; title; newGameButton; exitButton; resumeButton; loadButton |]

    member _.Draw(gameTime: GameTime): unit =
        use batch = new SpriteBatch(graphicsDevice)
        batch.Begin()
        for control in allControls do
            control.Draw batch
        mouse.Draw batch
        batch.End()

    interface IScene with
        member this.GetCursor(mouseState: MouseState) =
            // TODO: Figure out the focused control
            failwith "TODO"

