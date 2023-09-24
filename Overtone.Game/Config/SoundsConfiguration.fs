namespace Overtone.Game.Config

open System
open Overtone.Resources

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
        |> Seq.map(fun(array) -> array[1], SoundConfig(array[1],array[0], array[2]))
        |> Map.ofSeq
        |> SoundsConfiguration

    member _.GetSoundsPath(effectName: string): string =
        // printfn "Loading shape : %s" shapeId
        if names.ContainsKey effectName then
            names[effectName].path
        else
            effectName
