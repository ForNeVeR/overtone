namespace Overtone.Game.Windows

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

open Overtone.Game
open Overtone.Game.Config
open Overtone.Game.Constants
open Overtone.Game.Input

type MainScene
    (lifetime: Lifetime, device: GraphicsDevice, textureManager: Textures.Manager, config: WindowsConfiguration) =

    let mutable sceneId = Scenes.MainMenu // "Starting state" of windows.txt

    let mutable controls = config.GetControlsArray sceneId

    let loadControl element =
        Controls.Load(lifetime, textureManager, element)
    
    let title: IControlable = Basic(
        textureManager.LoadTexture(lifetime, Shapes.TitleScreen.Id, Shapes.TitleScreen.TitleFrame),
        Rectangle(0, -41, 640, 480)
    )
    let mutable allControls: IControlable[] = [||]
    let sparkles = Sparkles(lifetime, device)


    member _.changeSceneId(newSceneId: int) : unit =
        sceneId <- newSceneId
        controls <- config.GetControlsArray sceneId
        allControls <- controls |> Seq.map (fun e -> loadControl e) |> Seq.toArray
        

    member this.event(id: int, id2: int, id3: int) : unit =
        match (id, id2) with
        | (1, id) -> this.changeSceneId id // Only 0 and 4 are actually used. event 5-0 should be 1-1
        | (2, _) -> exit 0
        | (5, _) -> this.changeSceneId Scenes.NewGame
        | any -> ()

    member this.Update(time: GameTime, mouseState: MouseState) : unit =
        allControls
        |> Array.map (fun c -> c.Update mouseState)
        |> Array.filter (fun (a, b, c) -> a <> 0)
        |> Array.iter (fun (a, b, c) -> this.event (a, b, c))

        if sceneId = Scenes.MainMenu then
            sparkles.Update time

    member _.Draw() : unit =
        use batch = new SpriteBatch(device)
        batch.Begin()

        for control in allControls do
            control.Draw batch

        if sceneId = Scenes.MainMenu then
            sparkles.Draw batch
            title.Draw batch

        batch.End()

    interface IScene with
        member this.GetCursor _ =
            CursorParameters(CursorShape.Normal, isVisible = true)
