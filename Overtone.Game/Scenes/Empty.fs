// SPDX-FileCopyrightText: 2023-2025 Overtone contributors <https://github.com/ForNeVeR/overtone>
//
// SPDX-License-Identifier: MIT

namespace Overtone.Game.Scenes

open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

type Empty () =
    interface IScene with
    
        member _.DrawBackground(batch: SpriteBatch): unit = ()

        member _.Draw(batch: SpriteBatch): unit = ()

        member _.Update(time: GameTime, mouse: MouseState): unit = ()

// Ressources to display :

// BIGFLOAT (anim + foreach race)
// GLYPHS (anim + foreach race)
// NGREALMS (foreach race)
// LEVIDIF (depends on current dif)
