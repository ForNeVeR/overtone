namespace Overtone.Game

open System.IO

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Overtone.Game.Config
open Overtone.Resources
open Overtone.Utils

type TextureManager(disc: GameDisc, device: GraphicsDevice, config: ShapesConfiguration) =

    member _.LoadTexture(lifetime: Lifetime, shapeId: string, spriteIndex: int): Texture2D =
        let shapeName = config.GetShapeName shapeId
        use shapeStream = new MemoryStream(disc.GetData shapeName)
        let shape = Shape.ShapeFile shapeStream

        let paletteName = ShapePalette.get shapeName
        let palette =
            use paletteStream = new MemoryStream(disc.GetData paletteName)
            Palette.Read paletteStream

        // TODO: Better resource management, do not read the whole shape again for every new sprite
        let header = shape.ReadSpriteHeaders()[spriteIndex]
        let sprite = shape.ReadSprite header
        let bitmap = shape.Render palette sprite

        let struct (width, height) = sprite.CanvasDimensions
        let width = int width
        let height = int height

        let texture = new Texture2D(device, width, height) |> Lifetimes.attach lifetime
        let colors = Array.init (width * height) (fun i ->
            let x = i % width
            let y = i / width
            let drawingColor = bitmap.GetPixel(x, y)
            Color(r = int drawingColor.R, g = int drawingColor.G, b = int drawingColor.B, alpha = int drawingColor.A)
        )
        texture.SetData colors
        texture
