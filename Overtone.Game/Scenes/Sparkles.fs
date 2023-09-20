namespace Overtone.Game.Scenes

open System
open System.Runtime.InteropServices

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Overtone.Utils

type private Sparkle(texture: Texture2D, position: Point, initialPhase: int) =
    static let periodMs = 3000
    let mutable φ = initialPhase

    member _.Update(time: GameTime): unit =
        φ <- (initialPhase + int time.TotalGameTime.TotalMilliseconds) % periodMs

    member _.Draw(sprite: SpriteBatch): unit =
        let brightness =
            (double φ / double periodMs) * Math.PI * 2.0
            |> sin
            |> fun x -> Math.Clamp(x, 0.0, 1.0)
        let value = int(brightness * 255.0)
        let mask = Color(value, value, value)
        sprite.Draw(texture, position.ToVector2(), mask)

    static member Initialize (texture: Texture2D) (seed: uint16): Sparkle =
        let x = (int seed >>> 8) * 3 % 640
        let y = int seed * 2 % 480
        let initialPhase = int seed * 4 % periodMs
        Sparkle(texture, Point(x, y), initialPhase)

type Sparkles(lifetime: Lifetime, device: GraphicsDevice) =
    let texture =
        let t = new Texture2D(device, 1, 1)
        t.SetData [| Color.White |]
        t |> Lifetimes.attach lifetime

    let sparkles =
        let arr = Array.zeroCreate 20
        let bytes = MemoryMarshal.Cast(arr.AsSpan())
        Random(42).NextBytes bytes
        arr |> Array.map(Sparkle.Initialize texture)

    member _.Update(time: GameTime): unit =
        sparkles |> Array.iter(fun x -> x.Update time)

    member _.Draw(sprite: SpriteBatch): unit =
        sparkles |> Array.iter(fun x -> x.Draw sprite)
