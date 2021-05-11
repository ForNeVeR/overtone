module Overtone.Tests.Cob

open System
open System.IO
open System.Text

open Xunit

open Overtone.Resources.Cob

let private file1 = Encoding.UTF8.GetBytes "Hello, world"
let private file2 = Encoding.UTF8.GetBytes(String('x', 50000))

let private cobFile =
    let entries = [|
        "file1.txt", file1
        @"testDir\file2.txt", file2
    |]

    let count = entries.Length

    use output = new MemoryStream()
    do
        use writer = new BinaryWriter(output)
        writer.Write count // 4 bytes: count
        for path, _ in entries do
            let pathBytes = Array.zeroCreate 50
            Encoding.UTF8.GetBytes(path.AsSpan(), pathBytes.AsSpan()) |> ignore
            writer.Write pathBytes // 50 bytes: path for each item
        let mutable currentOffset = int output.Position + count * 4
        for _, content in entries do
            writer.Write currentOffset // 4 bytes: offset to the current item
            currentOffset <- currentOffset + int content.LongLength
        for _, content in entries do
            writer.Write content // content of the current file

    output.ToArray()

[<Fact>]
let ``Entries should be properly enumerated``(): unit =
    use archive = new MemoryStream(cobFile)
    use file = new CobFile(archive)
    let expectedEntries = [|
        { Path = "file1.txt"; Offset = 112; Size = 12 }
        { Path = @"testDir\file2.txt"; Offset = 124; Size = 50000 }
    |]

    let entries = file.ReadEntries()
    Assert.Equal(expectedEntries, entries)


[<Fact>]
let ``Entry should be read properly``(): unit =
    use archive = new MemoryStream(cobFile)
    use file = new CobFile(archive)
    let entry = Seq.head(file.ReadEntries())
    let content = file.ReadEntry entry
    Assert.Equal<byte>(file1, content)
