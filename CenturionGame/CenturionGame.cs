﻿using System;
using CrownEngine;
using System.Collections.Generic;
using CenturionGame.Content;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Reflection;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace CenturionGame
{
    public class CenturionGame : EngineGame
    {
        public CenturionGame()
        {

        }

        public override void CustomInitialize()
        {
            foreach (string file in Directory.EnumerateFiles("Content/", "*.png", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);
                Textures[Path.GetFileName(fixedPath)] = Texture2D.FromStream(GraphicsDevice, File.OpenRead(file));
            }

            foreach (KeyValuePair<string, Texture2D> str in Textures)
            {
                Debug.WriteLine(str.Value);
            }

            InitializeStages(new List<Stage>()
            {
                new MainMenu(),
                new Sandbox()
            });

            base.CustomInitialize();
        }

        public override int windowHeight => 144;
        public override int windowWidth => 256;
        public override int windowScale => 2;

        public override void CustomUpdate()
        {
            /*if (keyboardState.IsKeyDown(Keys.OemPlus) && windowScale < 6)
            {
                windowScale++;
            }
            if (keyboardState.IsKeyDown(Keys.OemMinus) && windowScale > 1)
            {
                windowScale--;
            }*/

            base.CustomUpdate();
        }

        public override void CustomDraw()
        {
            base.CustomDraw();
        }

        public override void InitializeStages(List<Stage> _stages)
        {
            base.InitializeStages(_stages);
        }
    }
}