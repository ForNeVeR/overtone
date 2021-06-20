namespace Overtone.Game

open Microsoft.Xna.Framework

type OvertoneGame() as this =
    inherit Game()

    let graphics = new GraphicsDeviceManager(this)

    override this.Initialize() =
        this.Window.Title <- "Overtone"

    override this.Draw gameTime =
        this.GraphicsDevice.Clear(Color.Coral)
        base.Draw gameTime

    override _.Dispose disposing =
        if disposing then graphics.Dispose()
