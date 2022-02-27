module Overtone.Game.Texture

open System
open System.IO
open Microsoft.Xna.Framework.Graphics

open Overtone.Resources
open Overtone.Resources.Cob
open Overtone.Resources.Shape

let loadShape (disc: GameDisc) (name: string): Texture2D =
    use stream = new FileStream(disc.DataArchivePath)
    use cob = new CobFile(stream)

    let readFile dataName =
        cob.ReadEntries()
        |> Seq.filter(fun x -> x.Path = dataName)
        |> Seq.exactlyOne
        |> cob.ReadEntry

    let shapeData = readFile name
    let paletteName = ShapePalette.get()

    use stream = new MemoryStream(data)
    let shape = ShapeFile(stream)
    shape.Render
