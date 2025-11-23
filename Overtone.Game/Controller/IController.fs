// SPDX-FileCopyrightText: 2022-2025 Overtone contributors <https://github.com/ForNeVeR/overtone>
//
// SPDX-License-Identifier: MIT

namespace Overtone.Game.Controller

open Microsoft.Xna.Framework.Input
open Overtone.Game.Controller

type IController =
    abstract member GetCursor: MouseState -> CursorParameters
