namespace Overtone.Game.Windows

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Overtone.Game
open Overtone.Game.Config

type MenuScene(lifetime: Lifetime,
               graphicsDevice: GraphicsDevice,
               textureManager: TextureManager,
               config: WindowsConfiguration) =
    let sceneId = 0 // "Starting state" of windows.txt

    let controls = config.GetControls sceneId
    let loadControl id = Controls.Load(lifetime, textureManager, controls[id])
    let background = loadControl "BACKGRND"
    let newGameButton = loadControl "NEWGAME"
    let exitButton = loadControl "EXITAPP"
    let resumeButton = loadControl "RESUME"
    let loadButton = loadControl "LOADGAME"
    let allControls = [| background; newGameButton; exitButton; resumeButton; loadButton |]

    member _.Draw(gameTime: GameTime): unit =
        use batch = new SpriteBatch(graphicsDevice)
        batch.Begin()
        for control in allControls do
            control.Draw batch
        batch.End()
