﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine;
using System.Diagnostics;
using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Reflection;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using Roll.Content;

namespace Roll
{
    public class RollGame : EngineGame
    {
        public RollGame() : base()
        {
            IsMouseVisible = true;
        }

        public override void CustomInitialize()
        {
            foreach (string file in Directory.EnumerateFiles("Content/", "*.png", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);

                instance.Textures[Path.GetFileName(fixedPath)] = Texture2D.FromStream(GraphicsDevice, File.OpenRead(file));
            }

            foreach (string file in Directory.EnumerateFiles("Content/", "*.wav", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);

                //Debug.WriteLine("Found a wav!");

                instance.Audio[Path.GetFileName(fixedPath)] = SoundEffect.FromStream(File.OpenRead(file));
            }

            foreach (string file in Directory.EnumerateFiles("Content/", "*.fx", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);

                //Debug.WriteLine("Found a wav!");

                instance.Effects[Path.GetFileName(fixedPath)] = Content.Load<Effect>(Path.GetFileName(fixedPath));
            }

            InitializeStages(new List<Stage>()
            {
                new World1_1()
            });

            base.CustomInitialize();
        }

        public override int windowWidth => 256;
        public override int windowHeight => 144;
        public override int windowScale => 2;

        public override void CustomUpdate()
        {
            base.CustomUpdate();
        }

        public override void InitializeStages(List<Stage> _stages)
        {
            base.InitializeStages(_stages);
        }

        public override void CustomPostDraw(SpriteBatch spriteBatch)
        {
            //spriteBatch.Draw(EngineHelpers.GetTexture("Cursor"), mousePos * windowScale, new Rectangle(0, 0, 7, 10), Color.White, 0f, Vector2.Zero, windowScale, SpriteEffects.None, 0f);

            base.CustomPostDraw(spriteBatch);
        }

        public void DrawString(SpriteBatch spriteBatch, string str, Vector2 pos, int spacing)
        {
            for (int i = 0; i < str.Length; i++)
            {
                int charId = str[i] - 48;

                if (charId < 0)
                {
                    Debug.WriteLine("Invalid character detected");
                    continue;
                }

                if (charId == -16)
                    spriteBatch.Draw(EngineHelpers.GetTexture("CharacterSheet"), pos + new Vector2(i * spacing, 0), new Rectangle((str[i] - 48) * 3, 0, 3, 5), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                if (charId < 65 - 48)
                    spriteBatch.Draw(EngineHelpers.GetTexture("CharacterSheet"), pos + new Vector2(i * spacing, 0), new Rectangle((str[i] - 48) * 3, 0, 3, 5), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                else if (charId < 91 - 48 && charId >= 65 - 48)
                    spriteBatch.Draw(EngineHelpers.GetTexture("CharacterSheet"), pos + new Vector2(i * spacing, 0), new Rectangle((str[i] - 65) * 3, 5, 3, 5), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                else if (charId < 123 - 48 && charId >= 97 - 48)
                    spriteBatch.Draw(EngineHelpers.GetTexture("CharacterSheet"), pos + new Vector2(i * spacing, 0), new Rectangle((str[i] - 97) * 3, 10, 3, 5), Color.White, 0f, Vector2.Zero, 1f, SpriteEffects.None, 0f);

                else
                    Debug.WriteLine("Invalid character detected");
            }
        }
    }
}
