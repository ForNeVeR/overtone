namespace Overtone.Game.Config

open System
open System.IO
open Overtone.Resources
open Microsoft.Xna.Framework.Audio

// The file looks like that for sound.txt
//
// effect  bhood       data\bhood.wav
// tune    tribe0      protect.wav
// effect  bhood       data\bhood.wav
// TYPE NAME FILEPATH

// typeOfSound is either :
// 1. effect -> Played on events
// 1. tune -> Background music
// Playing any tune MUST stop previous tune
// Effect are all free to happen anytime and halts nothing

type SoundConfig(name:string, typeOfSound:string, path:string)=
    member _.name= name
    member _.typeOfSound= typeOfSound
    member _.path= path
    member _.sound= path


type SoundsConfiguration(names: Map<string, SoundConfig>) =

    static member Read(shapesTxt: byte[]): SoundsConfiguration =
        shapesTxt
        |> TextConfiguration.extractLines
        |> Seq.map TextConfiguration.readPreformatedEntries
        // Filter elements whose length isn't 3 !
        |> Seq.filter(fun(array) -> array.Length = 3)
        |> Seq.map(fun(array) -> array[1].ToUpper(), SoundConfig(array[1].ToUpper(),array[0], array[2].ToUpper()))
        |> Map.ofSeq
        |> SoundsConfiguration

    member _.GetSoundPerName(effectName: string, disc: GameDisc): SoundEffect =
        let mutable filename = effectName
        // printfn "Loading shape : %s" shapeId
        if names.ContainsKey effectName then
            filename <- names[effectName].path
            
        if filename.StartsWith("DATA\\") then
            let stream = new MemoryStream(disc.GetData filename)
            SoundEffect.FromStream(stream)
        else
            filename <- Path.Combine("THING2", filename)
            SoundEffect.FromStream(disc.ReadFileAsStream filename)
