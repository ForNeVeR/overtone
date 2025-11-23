// SPDX-FileCopyrightText: 2022-2025 Friedrich von Never <friedrich@fornever.me>
//
// SPDX-License-Identifier: MIT

module Overtone.Utils.Lifetimes

open System
open JetBrains.Lifetimes

let attach<'a when 'a :> IDisposable> (lifetime: Lifetime) (object: 'a): 'a =
    lifetime.AddDispose object |> ignore
    object
