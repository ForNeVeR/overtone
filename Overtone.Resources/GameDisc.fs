namespace Overtone.Resources

open System
open System.IO
open System.Threading.Tasks

open Overtone.Resources.Cob

type GameDisc(rootPath: string) =
    let configArchivePath: string = Path.Combine(rootPath, "THING1", "TONE00.COB")
    let dataArchivePath: string = Path.Combine(rootPath, "THING1", "TONE01.COB")

    let configStream = new FileStream(configArchivePath, FileMode.Open)
    let dataStream = new FileStream(dataArchivePath, FileMode.Open)

    let configArchive = new CobFile(configStream)
    let dataArchive = new CobFile(dataStream)

    let configEntries =
        configArchive.ReadEntries()
        |> Seq.map(fun e -> e.Path.ToUpper(), e)
        |> Map.ofSeq
    let dataEntries =
        dataArchive.ReadEntries()
        |> Seq.map(fun e -> e.Path.ToUpper(), e)
        |> Map.ofSeq

    member _.ReadFileAsStream(relativePath: string): FileStream =
        Path.Combine(rootPath, relativePath) |> File.OpenRead

    member _.ReadFile(relativePath: string): Task<byte[]> =
        Path.Combine(rootPath, relativePath) |> File.ReadAllBytesAsync

    member _.GetConfig(name: string): byte[] = configEntries[name.ToUpper()] |> configArchive.ReadEntry
    member _.GetData(name: string): byte[] = dataEntries[name.ToUpper()] |> dataArchive.ReadEntry

    member _.hasDataEntry(name: string): bool = 
        dataEntries.ContainsKey (name.ToUpper())

    interface IDisposable with
        member _.Dispose() =
            (dataArchive :> IDisposable).Dispose()
            (configArchive :> IDisposable).Dispose()
            dataStream.Dispose()
            configStream.Dispose()
