namespace Overtone.Game.Windows

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Overtone.Resources

type MenuScene(lifetime: Lifetime, disc: GameDisc, config: WindowConfiguration, graphicsDevice: GraphicsDevice) =
    let sceneId = 0 // "Starting state" of windows.txt

    let controls = config.GetControls sceneId
    let loadControl id = Controls.Load(lifetime, graphicsDevice, disc, controls[id])
    let background = loadControl "BACKGRND"
    let newGameButton = loadControl "NEWGAME"
    let exitButton = loadControl "EXITAPP"
    let resumeButton = loadControl "RESUME"
    let loadButton = loadControl "LOADGAME"

    member _.Draw(gameTime: GameTime): unit =
        use batch = new SpriteBatch(graphicsDevice)
        batch.Begin()
        for control in [background; newGameButton; exitButton; resumeButton; loadButton] do
            control.Draw batch
