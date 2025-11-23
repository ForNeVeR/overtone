// SPDX-FileCopyrightText: 2023-2025 Overtone contributors <https://github.com/ForNeVeR/overtone>
//
// SPDX-License-Identifier: MIT

module Overtone.Game.Audio

open Microsoft.Xna.Framework.Audio

// Todo add ability to load audio

let loadSound(stream): SoundEffect=
    SoundEffect.FromStream(stream)
