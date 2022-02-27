namespace Overtone.Resources

open System.IO

type GameDisc(rootPath: string) =
    member val DataArchivePath: string = Path.Combine(rootPath, "THING1", "TONE01.COB")
