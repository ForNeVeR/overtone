module Overtone.Utilities.Lifetimes

open System
open JetBrains.Lifetimes

let attach<'a when 'a :> IDisposable> (lifetime: Lifetime) (object: 'a): 'a =
    lifetime.AddDispose object |> ignore
    object
