open Accord.Video.VFW

let filePath = @"D:\Games\TONE\INTRO.AVI"

[<EntryPoint>]
let main (_: string[]): int =
    use aviFile = new AVIReader()
    aviFile.Open filePath
    printfn $"%s{aviFile.Codec}"
    let f = aviFile.GetNextFrame()
    let f = aviFile.GetNextFrame()
    let f = aviFile.GetNextFrame()
    let f = aviFile.GetNextFrame()
    let f = aviFile.GetNextFrame()
    let f = aviFile.GetNextFrame()
    f.Save @"T:\Temp\frame01.bmp"
    0
