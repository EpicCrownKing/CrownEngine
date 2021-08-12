﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CrownEngine;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;
using System.Reflection;
using System.Linq;
using System.IO;
using Microsoft.Xna.Framework.Audio;
using System.Diagnostics;

namespace CrownEngine
{
    public class EngineGame : Game
    {
        public GraphicsDeviceManager _graphics;
        public SpriteBatch _spriteBatch;

        public static EngineGame instance;

        public virtual int windowWidth => 256;
        public virtual int windowHeight => 144;
        public virtual int windowScale => 2;

        public Stage activeStage;
        public List<Stage> stages = new List<Stage>();

        public Texture2D MissingTexture;

        public Dictionary<string, Texture2D> Textures = new Dictionary<string, Texture2D>();

        public Dictionary<string, SoundEffect> SoundEffects = new Dictionary<string, SoundEffect>();

        public Random random;

        public KeyboardState oldKeyboardState;
        public KeyboardState keyboardState;

        public MouseState oldMouseState;
        public MouseState mouseState;

        public PrimitiveType primitiveBatch;

        public EngineGame()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;

            instance = this;
        }

        protected override void Initialize()
        {
            scene = new RenderTarget2D(GraphicsDevice, windowWidth, windowHeight, false, SurfaceFormat.Color, DepthFormat.None);

            /*foreach (string file in Directory.EnumerateFiles("Content/", "*.wav", SearchOption.AllDirectories))
            {
                string fixedPath = file.Substring(Content.RootDirectory.Length).TrimStart(Path.DirectorySeparatorChar);
                SoundEffects[Path.GetFileName(fixedPath)] = SoundEffect.FromStream(TitleContainer.OpenStream(fixedPath));
            }


            foreach (KeyValuePair<string, SoundEffect> fx in SoundEffects)
            {
                Debug.WriteLine(fx.Key);
            }*/

            random = new Random();

            _spriteBatch = new SpriteBatch(GraphicsDevice);

            CustomInitialize();

            base.Initialize();
        }

        public virtual void CustomInitialize()
        {

        }

        public virtual void InitializeStages(List<Stage> _stages)
        {
            stages = _stages;

            activeStage = stages[0];

            activeStage.Load();
        }

        protected override void Update(GameTime gameTime)
        {
            oldKeyboardState = keyboardState;
            keyboardState = Keyboard.GetState();

            oldMouseState = mouseState;
            mouseState = Mouse.GetState();

            if (keyboardState.IsKeyDown(Keys.Escape))
                Exit();

            activeStage.Update();

            CustomUpdate();

            base.Update(gameTime);
        }

        public virtual void CustomUpdate()
        {

        }

        protected void DrawSceneToTexture(RenderTarget2D renderTarget)
        {
            GraphicsDevice.SetRenderTarget(renderTarget);
            GraphicsDevice.Clear(activeStage.bgColor);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);


            activeStage.Draw(_spriteBatch);


            _spriteBatch.End();

            // Drop the render target
            GraphicsDevice.SetRenderTarget(null);
        }

        //Drawing the scene
        private RenderTarget2D scene;
        protected override void Draw(GameTime gameTime)
        {
            _graphics.PreferredBackBufferWidth = windowWidth * windowScale;
            _graphics.PreferredBackBufferHeight = windowHeight * windowScale;
            _graphics.ApplyChanges();

            DrawSceneToTexture(scene);

            _spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            _spriteBatch.Draw(scene, new Rectangle(0, 0, windowWidth * windowScale, windowHeight * windowScale), Color.White);

            CustomDraw();

            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public virtual void CustomDraw()
        {

        }
    }
}