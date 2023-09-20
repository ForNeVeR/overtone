namespace Overtone.Game

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Input

open Overtone.Utils.Constants
open Overtone.Game.Config
open Overtone.Game.Controller
open Overtone.Resources

type OvertoneGame(disc: GameDisc, shapesConfig: ShapesConfiguration, windowConfig: WindowsConfiguration) as this =
    inherit Game()

    let lifetimeDef = new LifetimeDefinition()
    let lifetime = lifetimeDef.Lifetime

    let graphics = new GraphicsDeviceManager(this)
    let textureManager = lazy Textures.Manager(disc, this.GraphicsDevice, shapesConfig)
    let mouse = lazy Mouse.Load(
        lifetime,
        this.GraphicsDevice,
        (disc.ReadFile "THING1/FLOAT.EXE").Result // TODO[#35]: Show a loader at start instead of .Result
    )
    let scene = lazy GameController(lifetime, this.GraphicsDevice, textureManager.Value, windowConfig)

    override this.Initialize() =
        this.Window.Title <- "Overtone"
        scene.Value.changeSceneId Scenes.MainMenu

        graphics.PreferredBackBufferWidth <- 640
        graphics.PreferredBackBufferHeight <- 480
        graphics.ApplyChanges()

    override this.Update time =
        let mouseState = Mouse.GetState()
        scene.Value.Update(time, mouseState)
        mouse.Value.UpdateCursor(mouseState, scene.Value, this)

    override this.Draw _ =
        this.GraphicsDevice.Clear(Color.Black)
        scene.Value.Draw()

    override _.Dispose disposing =
        if disposing then
            lifetimeDef.Terminate()
            graphics.Dispose()
