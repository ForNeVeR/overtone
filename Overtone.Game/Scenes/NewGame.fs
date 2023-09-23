namespace Overtone.Game.Scenes

open JetBrains.Lifetimes
open Microsoft.Xna.Framework
open Microsoft.Xna.Framework.Graphics
open Microsoft.Xna.Framework.Input

open Overtone.Utils.Constants.GameData.NewGameMenu
open Overtone.Game

type NewGame (lifetime: Lifetime, device: GraphicsDevice, textureManager: Textures.Manager) =

    // Gotta love sparkles !
    let sparkles:Sparkles = Sparkles(lifetime, device)
    // Shapes used inside the menu !!!
    let DifficultyRendering = textureManager.LoadWholeShape(lifetime, DifficultyRendering)
    let GlyphRendering = textureManager.LoadWholeShape(lifetime, GlyphRendering)
    let FloaterRendering = textureManager.LoadWholeShape(lifetime, FloaterRendering)
    let RealmRendering = textureManager.LoadWholeShape(lifetime, RealmRendering)
    let colorMask = Color.White
    let mutable currentFrame = 0

    interface IScene with

        member _.DrawBackground(batch: SpriteBatch): unit =
            sparkles.Draw(batch)

        member _.Draw(batch: SpriteBatch): unit =
            currentFrame <- currentFrame + 1

            // Render Difficulty
            let difficultyTexture = DifficultyRendering[GameState.currentDifficulty]
            batch.Draw(difficultyTexture.texture, difficultyTexture.offset + Vector2(320f,240f), colorMask)

            if (GameState.currentRace <> -1) then
                // Render Realms
                let realmTexture = RealmRendering[GameState.currentRace]
                batch.Draw(realmTexture.texture, realmTexture.offset + Vector2(60f,40f), colorMask)
                let realmNextTexture = RealmRendering[(GameState.currentRace + 1) % 4]
                batch.Draw(realmNextTexture.texture, realmNextTexture.offset + Vector2(140f,40f), colorMask)

                // Render bigfloat
                let floaterOffset = Vector2(500f,floor(15f + BigFloat.Bouncyness*cos(MathHelper.ToRadians(BigFloat.BouncynessFrequency*(float32)currentFrame))))
                let floaterTextureBody = FloaterRendering[GameState.currentRace * BigFloat.OffsetPerRace]
                batch.Draw(floaterTextureBody.texture, floaterOffset, colorMask)
                let floaterTextureLegs = FloaterRendering[GameState.currentRace * BigFloat.OffsetPerRace + 1 + (currentFrame/BigFloat.FrameFrequency%BigFloat.FrameCount)]
                batch.Draw(floaterTextureLegs.texture, floaterTextureLegs.offset-floaterTextureBody.offset+floaterOffset, colorMask)
            
            let glyphDist = -150f
            let baseOffset = Vector2(320f ,240f)
            let GlyphIndex = (currentFrame/Glyphs.FrameFrequency%Glyphs.FrameCount);
            for x in 0..3 do
                let glyph = GlyphRendering[x * Glyphs.OffsetPerRace + GlyphIndex];
                let angle = MathHelper.ToRadians(90f * (float32)x)
                batch.Draw(glyph.texture, glyph.offset + baseOffset + Vector2(glyphDist*cos(angle),glyphDist*sin(angle)), colorMask)

        member _.Update(time: GameTime, mouse: MouseState): unit =
            sparkles.Update(time)

// Ressources to display :

// BIGFLOAT (anim + foreach race), rendered top right
// GLYPHS (anim + foreach race), 90° angle around the center
// NGREALMS (foreach race), top left screen current realm + the next one that one warrior class is in, weak against the previous one
// LEVIDIF (depends on current dif), rendered in the middle of the screen, levidif is 280x280, game size is 640x480 -> offset for rendering (180x100)
// World size is rendered with noisy blue stuff
