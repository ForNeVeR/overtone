namespace Overtone.Resources.Palette

open System.Drawing
open System.IO

open Checked

/// Type supporting reading .pal files from The Tone Rebellion.
type PalFile = {
    Colors: Color[]
}
    with
    static member Read(input: Stream): PalFile =
        use reader = new BinaryReader(input)
        let colors = Array.init 256 (fun _ -> Color.Transparent)
        for index in 0..255 do
            let r = reader.ReadByte() * 4uy
            let g = reader.ReadByte() * 4uy
            let b = reader.ReadByte() * 4uy
            colors.[index] <- Color.FromArgb(int r, int g, int b)
        { Colors = colors }
