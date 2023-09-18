module Overtone.Game.Textures

open System.IO

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open SkiaSharp

open Overtone.Game.Config
open Overtone.Resources
open Overtone.Utils

let toTexture (bitmap: SKBitmap, lifetime: Lifetime, device: GraphicsDevice): Texture2D =
    let width = bitmap.Width
    let height = bitmap.Height
    let texture = new Texture2D(device, width, height) |> Lifetimes.attach lifetime
    let colors = Array.init (width * height) (fun i ->
        let x = i % width
        let y = i / width
        let drawingColor = bitmap.GetPixel(x, y)
        Color(
            r = int drawingColor.Red,
            g = int drawingColor.Green,
            b = int drawingColor.Blue,
            alpha = int drawingColor.Alpha
        )
    )
    texture.SetData colors
    texture

type Manager(disc: GameDisc, device: GraphicsDevice, config: ShapesConfiguration) =

    let mutable InternalCache: Map<string, Texture2D[]> = Map.empty;

    member _.LoadWholeShape(lifetime: Lifetime, shapeId: string): Texture2D[] =
        let key = shapeId;
        printfn "Try loading : %s" shapeId
        if not (InternalCache.ContainsKey key) then
            let shapeName = config.GetShapeName shapeId
            use shapeStream = new MemoryStream(disc.GetData shapeName)
            let shape = Shape.ShapeFile shapeStream

            let paletteName = ShapePalette.getWithDisk(shapeName,disc)
            let palette =
                use paletteStream = new MemoryStream(disc.GetData paletteName)
                Palette.Read paletteStream

            // TODO[#30]: Better resource management, do not read the whole shape again for every new sprite
            let array = 
                shape.ReadSpriteHeaders()
                |> Seq.map (fun header -> shape.ReadSprite header)
                |> Seq.map (fun sprite -> shape.Render palette sprite)
                |> Seq.map (fun bitmap -> toTexture(bitmap, lifetime, device))
                |> Seq.toArray
            InternalCache <- InternalCache.Add(key,array)
        else
            printfn "Entry was in cache !"
        InternalCache[key]

    member this.LoadTexture(lifetime: Lifetime, shapeId: string, spriteIndex: int): Texture2D =
        this.LoadWholeShape(lifetime,shapeId)[spriteIndex]
