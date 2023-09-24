module Overtone.Game.Audio

open Microsoft.Xna.Framework.Audio

// Todo add ability to load audio

let loadSound(stream): SoundEffect=
    SoundEffect.FromStream(stream)
