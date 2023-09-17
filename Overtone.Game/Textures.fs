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

    let mutable InternalCache: Map<(string*int), Texture2D> = Map.empty;

    member _.LoadTexture(lifetime: Lifetime, shapeId: string, spriteIndex: int): Texture2D =
        let key = (shapeId, spriteIndex);
        printfn "Try loading : %s index %d" shapeId spriteIndex
        if not (InternalCache.ContainsKey key) then
            let shapeName = config.GetShapeName shapeId
            use shapeStream = new MemoryStream(disc.GetData shapeName)
            let shape = Shape.ShapeFile shapeStream

            let paletteName = ShapePalette.getWithDisk(shapeName,disc)
            let palette =
                use paletteStream = new MemoryStream(disc.GetData paletteName)
                Palette.Read paletteStream

            // TODO[#30]: Better resource management, do not read the whole shape again for every new sprite
            let header = shape.ReadSpriteHeaders()[spriteIndex]
            let sprite = shape.ReadSprite header
            use bitmap = shape.Render palette sprite
            let texture = toTexture(bitmap, lifetime, device)
            InternalCache <- InternalCache.Add(key,texture)
        else
            printfn "Entry was in cache !"
        InternalCache[(shapeId,spriteIndex)]
