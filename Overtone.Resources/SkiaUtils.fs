module Overtone.Resources.SkiaUtils

open System.IO

open SkiaSharp

let Render(bitmap: SKBitmap, outputFilePath: string): unit =
    use image = SKImage.FromBitmap bitmap
    use data = image.Encode(SKEncodedImageFormat.Png, 100)
    use output = new FileStream(outputFilePath, FileMode.Create)
    data.SaveTo output
