// SPDX-FileCopyrightText: 2023-2026 Overtone contributors <https://github.com/ForNeVeR/overtone>
//
// SPDX-License-Identifier: MIT

namespace Overtone.Game.UI

open JetBrains.Lifetimes

open Overtone.Game
open Overtone.Game.Config
open Overtone.Utils.Constants
open Overtone.Game.Textures

module Controls =
    let Load (lifetime: Lifetime, textureManager: Manager, entry: WindowEntry): IDrawableUI =

        let normalTexture =
            textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame)

        if entry.MouseFocus && entry.WindowType = WindowTypes.Button then
            let hoverTexture = textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame + 1)
            Button(
                normalTexture,
                hoverTexture,
                entry,
                GameState.soundsConfig.GetSoundPerName("BUTTON", GameState.getDisc()) // TODO[#106]: Migrate to the SoundManager, only load once
            )
        else if entry.MouseFocus && entry.WindowType = WindowTypes.DropDownMenu then
            DropDownMenuButton(normalTexture, entry)
        else
            Image(normalTexture, entry.Pane)


    let LoadImg (lifetime: Lifetime, textureManager: Manager, entry: WindowEntry): IDrawableUI =

        let normalTexture =
            textureManager.LoadTexture(lifetime, entry.ShapeId, entry.ShapeFrame)

        Image(normalTexture, entry.Pane)
