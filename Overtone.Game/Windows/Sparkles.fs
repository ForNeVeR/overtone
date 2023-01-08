namespace Overtone.Game.Windows

open System
open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Overtone.Utils

type private Sparkle(texture: Texture2D, position: Point, initialPhase: int) =
    let mutable φ = initialPhase

    member _.Update(time: GameTime): unit =
        φ <- (initialPhase + int time.TotalGameTime.TotalMilliseconds) % 2000

    member _.Draw(sprite: SpriteBatch): unit =
        let brightness =
            (double φ / 2000.0) * Math.PI
            |> sin
            |> fun x -> Math.Clamp(x, 0.0, 1.0)
        let value = int(brightness * 255.0)
        let mask = Color(value, value, value)
        sprite.Draw(texture, position.ToVector2(), mask)

    static member Initialize (texture: Texture2D) (seed: byte): Sparkle =
        // TODO: Better parameters
        let x = int seed * 3 % 640
        let y = int seed * 2 % 480
        let initialPhase = int seed * 4 % 2000
        Sparkle(texture, Point(x, y), initialPhase)

type Sparkles(lifetime: Lifetime, device: GraphicsDevice) =
    let texture =
        let t = new Texture2D(device, 1, 1)
        t.SetData [| Color.White |] // TODO: Figure out the real color boundaries
        t |> Lifetimes.attach lifetime

    let randomBuffer =
        let arr = Array.zeroCreate 200
        Random(42).NextBytes arr
        arr

    let sparkles = randomBuffer |> Array.map(Sparkle.Initialize texture)

    member _.Update(time: GameTime): unit =
        sparkles |> Array.iter(fun x -> x.Update time)

    member _.Draw(sprite: SpriteBatch): unit =
        sparkles |> Array.iter(fun x -> x.Draw sprite)
