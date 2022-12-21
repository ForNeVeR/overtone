namespace Overtone.Game.Windows

open System

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

type MenuScene(config: WindowConfiguration, graphicsDevice: GraphicsDevice) =
    // TODO: Finalize this
    //let sceneId = 0 // "Starting state" of windows.txt
//
    //let controls = config.GetControls sceneId
    //let background = controls["BACKGRND"]
    //let newGameButton = controls["NEWGAME"]
    //let exitButton = controls["EXITAPP"]
    //let resumeButton = controls["RESUME"]
    //let loadButton = controls["LOADGAME"]
//
    interface IDisposable with
        member _.Dispose() =
            failwith "TODO"
            //
            // for control in controls.Values do
            //     (control :> IDisposable).Dispose()

    member _.Draw(gameTime: GameTime): unit =
        failwith "TODO"
        // use batch = new SpriteBatch(graphicsDevice)
        // batch.Begin()
        // for control in controls.Values do
        //     control.Draw batch
        // batch.End()
