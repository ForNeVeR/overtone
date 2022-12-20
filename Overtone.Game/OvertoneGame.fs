namespace Overtone.Game

open System

open Microsoft.Xna.Framework

open Overtone.Resources

type OvertoneGame(disc: GameDisc) as this =
    inherit Game()

    let graphics = new GraphicsDeviceManager(this)
    let scene = lazy (
        new MenuScene(disc, this.GraphicsDevice)
    )

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
            if scene.IsValueCreated then
                (scene.Value :> IDisposable).Dispose()
            graphics.Dispose()
