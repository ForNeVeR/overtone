// SPDX-FileCopyrightText: 2022-2025 Overtone contributors <https://github.com/ForNeVeR/overtone>
//
// SPDX-License-Identifier: MIT

namespace Overtone.Game.Controller

type CursorShape =
    Normal = 0

type CursorParameters = CursorParameters of CursorShape * isVisible: bool
