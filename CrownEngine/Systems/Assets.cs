﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using System.IO;
using Microsoft.Xna.Framework.Audio;

namespace CrownEngine.Systems
{
    public class Assets : GameSystem
    {
        public static Assets Instance { get; private set; }

        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();
        public Dictionary<string, SoundEffect> Audio = new Dictionary<string, SoundEffect>();
        public Dictionary<string, Effect> Effects = new Dictionary<string, Effect>();

        public Assets() : base()
        {

        }

        public void RegisterContent()
        {
            foreach (string file in Directory.EnumerateFiles("Content/", "*.png", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(EngineGame.instance.Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);

                Textures[Path.GetFileName(fixedPath)] = Texture2D.FromStream(EngineGame.instance.GraphicsDevice, File.OpenRead(file));
            }

            foreach (string file in Directory.EnumerateFiles("Content/", "*.wav", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(EngineGame.instance.Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);

                Audio[Path.GetFileName(fixedPath)] = SoundEffect.FromStream(File.OpenRead(file));
            }

            foreach (string file in Directory.EnumerateFiles("Content/", "*.fx", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(EngineGame.instance.Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);

                Effects[Path.GetFileName(fixedPath)] = EngineGame.instance.Content.Load<Effect>(Path.GetFileName(fixedPath));
            }
        }

        public Texture2D GetTexture(string file)
        {
            string fixedPath = file.Substring(EngineGame.instance.Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);

            return Texture2D.FromStream(EngineGame.instance.GraphicsDevice, File.OpenRead(file));
        }

        public SoundEffect GetSound(string file)
        {
            string fixedPath = file.Substring(EngineGame.instance.Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);

            return SoundEffect.FromStream(File.OpenRead(file));
        }

        public Effect GetEffect(string file)
        {
            string fixedPath = file.Substring(EngineGame.instance.Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);

            return EngineGame.instance.Content.Load<Effect>(Path.GetFileName(fixedPath));
        }
    }
}
