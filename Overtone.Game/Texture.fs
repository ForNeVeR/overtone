module Overtone.Game.Texture

open System.IO
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Overtone.Resources
open Overtone.Resources.Shape

let loadShape (device: GraphicsDevice) (disc: GameDisc) (name: string): Texture2D =
    use shapeStream = new MemoryStream(disc.GetData name)
    let shape = ShapeFile shapeStream

    let paletteName = ShapePalette.get name
    let palette =
        use paletteStream = new MemoryStream(disc.GetData paletteName)
        Palette.Read paletteStream

    let header = shape.ReadSpriteHeaders() |> Seq.head // TODO: Properly enumerate resources
    let sprite = shape.ReadSprite header
    let bitmap = shape.Render palette sprite

    let texture = new Texture2D(device, sprite.Width, sprite.Height)
    let colors = Array.init (sprite.Width * sprite.Height) (fun i ->
        let x = i % sprite.Width
        let y = i / sprite.Width
        let drawingColor = bitmap.GetPixel(x, y)
        Color(r = int drawingColor.R, g = int drawingColor.G, b = int drawingColor.B)
    )
    texture.SetData colors
    texture

