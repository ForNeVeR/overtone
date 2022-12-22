namespace Overtone.Game

open JetBrains.Lifetimes
open Microsoft.Xna.Framework

open Overtone.Game.Config
open Overtone.Game.Windows
open Overtone.Resources

type OvertoneGame(disc: GameDisc, shapesConfig: ShapesConfiguration, windowConfig: WindowsConfiguration) as this =
    inherit Game()

    let lifetimeDef = new LifetimeDefinition()

    let graphics = new GraphicsDeviceManager(this)
    let textureManager = lazy TextureManager(disc, this.GraphicsDevice, shapesConfig)
    let scene = lazy MenuScene(lifetimeDef.Lifetime, this.GraphicsDevice, textureManager.Value, windowConfig)

    override this.Initialize() =
        this.Window.Title <- "Overtone"

        graphics.PreferredBackBufferWidth <- 640
        graphics.PreferredBackBufferHeight <- 480
        graphics.ApplyChanges()

    override this.Draw gameTime =
        this.GraphicsDevice.Clear(Color.Black)
        scene.Value.Draw(gameTime)
        base.Draw gameTime

    override _.Dispose disposing =
        if disposing then
            lifetimeDef.Terminate()
            graphics.Dispose()
