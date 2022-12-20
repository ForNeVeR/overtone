module Overtone.Game.Texture

open System.IO
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics

open Overtone.Resources
open Overtone.Resources.Cob
open Overtone.Resources.Shape

let loadShape (device: GraphicsDevice) (disc: GameDisc) (name: string): Texture2D =
    let paletteName = ShapePalette.get name

    use stream = new FileStream(disc.DataArchivePath, FileMode.Open)
    use cob = new CobFile(stream)

    let readFile dataName =
        cob.ReadEntries()
        |> Seq.filter(fun x -> x.Path = dataName)
        |> Seq.exactlyOne
        |> cob.ReadEntry

    use shapeStream = new MemoryStream(readFile name)
    let shape = ShapeFile shapeStream

    let palette =
        use paletteStream = new MemoryStream(readFile paletteName)
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

