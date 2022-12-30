namespace Overtone.Game

open JetBrains.Lifetimes
open Microsoft.Xna.Framework

open Microsoft.Xna.Framework.Input

open Overtone.Game.Config
open Overtone.Game.Input
open Overtone.Game.Windows
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
        (disc.ReadFile "THING1/FLOAT.EXE").Result // TODO: Show a loader at start instead of .Result
    )
    let scene = lazy MenuScene(lifetime, this.GraphicsDevice, textureManager.Value, windowConfig)

    override this.Initialize() =
        this.Window.Title <- "Overtone"

        graphics.PreferredBackBufferWidth <- 640
        graphics.PreferredBackBufferHeight <- 480
        graphics.ApplyChanges()

    override this.Update gameTime =
        let mouseState = Mouse.GetState()
        scene.Value.Update mouseState
        mouse.Value.UpdateCursor(mouseState, scene.Value, this)

    override this.Draw gameTime =
        this.GraphicsDevice.Clear(Color.Black)
        scene.Value.Draw(gameTime)
        base.Draw gameTime

    override _.Dispose disposing =
        if disposing then
            lifetimeDef.Terminate()
            graphics.Dispose()
