module Overtone.Game.Textures

open System.IO

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open SkiaSharp

open Overtone.Game.Config
open Overtone.Resources
open Overtone.Utils

let toTexture (bitmap: SKBitmap, lifetime: Lifetime, device: GraphicsDevice) : Texture2D =
    let width = bitmap.Width
    let height = bitmap.Height
    let texture = new Texture2D(device, width, height) |> Lifetimes.attach lifetime

    let colors =
        Array.init (width * height) (fun i ->
            let x = i % width
            let y = i / width
            let drawingColor = bitmap.GetPixel(x, y)

            Color(
                r = int drawingColor.Red,
                g = int drawingColor.Green,
                b = int drawingColor.Blue,
                alpha = int drawingColor.Alpha
            ))

    texture.SetData colors
    texture

type Texture2DWithOffset (texture:Texture2D, offsetX:int, offsetY:int)=
    member _.texture=texture
    member _.offset:Vector2 = Vector2(float32(offsetX),float32(offsetY))

type Manager(disc: GameDisc, device: GraphicsDevice, config: ShapesConfiguration) =

    let mutable InternalCache: Map<string, Texture2DWithOffset[]> = Map.empty

    let emptyTexture = Texture2DWithOffset(new Texture2D(device,1,1),0,0)

    member _.LoadWholeShape(lifetime: Lifetime, shapeId: string) : Texture2DWithOffset[] =
        let key = shapeId.ToUpper()
        if key = "NONE" then
            let emptyTextureArray= 
                [emptyTexture]
                |> Seq.toArray
            InternalCache <- InternalCache.Add(key, emptyTextureArray);
        else if not (InternalCache.ContainsKey key) then
            let shapeName = config.GetShapeName key
            use shapeStream = new MemoryStream(disc.GetData shapeName)
            let shape = Shape.ShapeFile shapeStream

            let paletteName = ShapePalette.getWithDisk (shapeName, disc)

            let palette =
                use paletteStream = new MemoryStream(disc.GetData paletteName)
                Palette.Read paletteStream

            // TODO[#30]: Better resource management, do not read the whole shape again for every new sprite
            let array =
                shape.ReadSpriteHeaders()
                |> Seq.map (fun header -> shape.ReadSprite header)
                // |> Seq.map (fun sprite -> shape.Render palette sprite)
                |> Seq.map (fun sprite -> shape.SmallRender palette sprite)
                |> Seq.map (fun bitmap -> Texture2DWithOffset(toTexture (bitmap.bitmap, lifetime, device), bitmap.offsetX, bitmap.offsetY))
                |> Seq.toArray

            InternalCache <- InternalCache.Add(key, array)

        InternalCache[key]

    member this.LoadTexture(lifetime: Lifetime, shapeId: string, spriteIndex: int) : Texture2DWithOffset =
        this.LoadWholeShape(lifetime, shapeId)[spriteIndex]
