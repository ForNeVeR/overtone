namespace Overtone.Game.Windows

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

open Overtone.Game
open Overtone.Game.Config
open Overtone.Game.Input

type MainScene(lifetime: Lifetime,
               device: GraphicsDevice,
               textureManager: Textures.Manager,
               config: WindowsConfiguration) =
    let mutable sceneId = 0 // "Starting state" of windows.txt

    let mutable controls = config.GetControlsArray sceneId
    let loadControl element = Controls.Load(lifetime, textureManager, element)

    // let title = Control(
    //     textureManager.LoadTexture(lifetime, Shapes.TitleScreen.Id, Shapes.TitleScreen.TitleFrame),
    //     None,
    //     Rectangle(0, -41, 640, 480),
    // )
    let mutable allControls = [| |]
    let sparkles = Sparkles(lifetime, device)

    member _.changeSceneId(newSceneId: int): unit=
        sceneId <- newSceneId;
        controls <- config.GetControlsArray sceneId
        allControls <- controls
        |> Seq.map(fun e -> loadControl e)
        |> Seq.toArray


    member _.Update(time: GameTime, mouseState: MouseState): unit =
        allControls |> Array.iter(fun c -> c.Update mouseState)
        if sceneId = 0 then
            sparkles.Update time

    member _.Draw(): unit =
        use batch = new SpriteBatch(device)
        batch.Begin()
        for control in allControls do
            control.Draw batch
        if sceneId = 0 then
            sparkles.Draw batch
        batch.End()

    interface IScene with
        member this.GetCursor _ =
            CursorParameters(CursorShape.Normal, isVisible = true)
