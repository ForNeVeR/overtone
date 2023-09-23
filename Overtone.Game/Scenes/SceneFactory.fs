namespace Overtone.Game.Scenes

open JetBrains.Lifetimes
open Microsoft.Xna.Framework.Graphics

open Overtone.Utils
open Overtone.Game

type SceneFactory () =

    static member GetScene (sceneId: int, lifetime: Lifetime, device: GraphicsDevice, textureManager: Textures.Manager): IScene =
        
            match sceneId with
            | id when id = Constants.Scenes.MainMenu -> MainMenu(lifetime,device,textureManager)
            | id when id = Constants.Scenes.NewGame -> NewGame(lifetime,device,textureManager)
            | id when id = Constants.Scenes.IslandsView -> Planets(lifetime,device,textureManager)
            | _ -> Empty()

// Ressources to display :

// BIGFLOAT (anim + foreach race)
// GLYPHS (anim + foreach race)
// NGREALMS (foreach race)
// LEVIDIF (depends on current dif)
