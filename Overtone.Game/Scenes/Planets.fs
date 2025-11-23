// SPDX-FileCopyrightText: 2023-2025 Overtone contributors <https://github.com/ForNeVeR/overtone>
//
// SPDX-License-Identifier: MIT

namespace Overtone.Game.Scenes

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

open Overtone.Game

type Planets (lifetime: Lifetime, device: GraphicsDevice, textureManager: Textures.Manager) =
    
    // Gotta love sparkles !
    let sparkles:Sparkles = Sparkles(lifetime, device)

    interface IScene with

        member _.DrawBackground(batch: SpriteBatch): unit =
            sparkles.Draw(batch)

        member _.Draw(batch: SpriteBatch): unit = ()

        member _.Update(time: GameTime, mouse: MouseState): unit =
            sparkles.Update(time)

// Planet img : SMISLE
// Planet count/position : https://github.com/Fadoli/ToneRebellion_Raw/blob/master/original_content/WORLDPOS.TXT
