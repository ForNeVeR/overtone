namespace Overtone.Game.Windows

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

open Overtone.Game
open Overtone.Game.Config
open Overtone.Game.Constants
open Overtone.Game.Input

type GameController (lifetime: Lifetime, device: GraphicsDevice, textureManager: Textures.Manager, config: WindowsConfiguration) =

    let mutable sceneId = Scenes.MainMenu // "Starting state" of windows.txt

    let mutable controls = config.GetControlsArray sceneId
    
    let title: IDrawableUI = Basic(
        textureManager.LoadTexture(lifetime, Shapes.TitleScreen.Id, Shapes.TitleScreen.TitleFrame),
        Rectangle(0, -41, 640, 480)
    )
    let mutable UIElements: IDrawableUI[] = [||]
    let mutable UIButton: IDrawableUI[] = [||]
    let mutable canInteract: bool = false
    let sparkles = Sparkles(lifetime, device)


    member _.changeSceneId(newSceneId: int) : unit =
        sceneId <- newSceneId
        controls <- config.GetControlsArray sceneId
        UIElements <- controls |> Seq.filter (fun e -> e.WindowType <> 1 && e.WindowType < 5) |> Seq.map (fun e -> Controls.LoadImg(lifetime, textureManager, e)) |> Seq.toArray
        UIButton <- controls |> Seq.filter (fun e -> e.WindowType = 1) |> Seq.map (fun e -> Controls.Load(lifetime, textureManager, e)) |> Seq.toArray
        

    member this.event(id: int, id2: int, id3: int) : unit =
        // Disable further interactions
        canInteract <- false
        match (id, id2) with
        | (1, id) -> this.changeSceneId id // Only 0 and 4 are actually used. event 5-0 should be 1-1
        | (2, _) -> exit 0
        | (5, _) -> this.changeSceneId Scenes.NewGame
        | (52, _) -> this.changeSceneId Scenes.IslandsView // hacks !
        | any -> printfn "unhandled event %d - %d - %d" id id2 id3

    member this.Update(time: GameTime, mouseState: MouseState) : unit =
        if canInteract then
            UIButton
            |> Array.map (fun c -> c.Update mouseState)
            |> Array.filter (fun (a, b, c) -> a <> 0)
            |> Array.iter (fun (a, b, c) -> this.event (a, b, c))
            
        // We can only interact when we release the button
        canInteract <- (mouseState.LeftButton = ButtonState.Released)

        if sceneId = Scenes.MainMenu then
            sparkles.Update time

    member _.Draw() : unit =
        use batch = new SpriteBatch(device)
        batch.Begin()

        for control in UIElements do
            control.Draw batch
        for control in UIButton do
            control.Draw batch

        if sceneId = Scenes.MainMenu then
            sparkles.Draw batch
            title.Draw batch

        batch.End()

    interface IController with
        member _.GetCursor _ =
            CursorParameters(CursorShape.Normal, isVisible = true)
