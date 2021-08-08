﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine.Engine;
using System.Diagnostics;

namespace CrownEngine.Content
{
    public class Sandbox : Stage
    {
        public override Color bgColor => Color.CornflowerBlue;

        public override void Update()
        {
            base.Update();
        }

        public override void Load()
        {
            AddActor(new SolidTileLayer(Vector2.Zero, this, new int[,] {
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 0, 0, 0, 0, 3, 3, 0, 0, 0, 2, 2, 2, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 0, 0, 0, 0, 3, 3, 0, 0, 0, 2, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 0, 0, 0, 0, 3, 3, 0, 0, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 0, 0, 0, 0, 3, 3, 0, 0, 0, 0, 2, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 0, 0, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 0, 2, 2, 0, 0, 0, 2, 2, 2, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 },
                { 1, 1, 0, 2, 2, 0, 0, 0, 2, 2, 2, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
                }, new List<Texture2D>() { EngineHelpers.GetTexture("Grass"), EngineHelpers.GetTexture("Dirt"), EngineHelpers.GetTexture("Stone") }));

            AddActor(new Player(new Vector2(64, 64), this));
        }

        public int sceneTransTimer = -1;
        public bool sceneTranssing = false;
        public override void Draw(SpriteBatch spriteBatch)
        {
            base.Draw(spriteBatch);

            if (sceneTransTimer % 6 == 0)
            {
                for (int i = 0; i < 8; i++)
                {
                    squares.Add(new sceneTransSquare(new Vector2(4 + (i * 8), 4 + ((sceneTransTimer * 8) / 6)), 0f));
                }
            }

            for (int j = 0; j < squares.Count; j++)
            {
                var square = squares[j];
                square.Update();
                squares[j] = square;

                spriteBatch.Draw(EngineHelpers.GetTexture("MagicPixel"), squares[j].pos, new Rectangle(0, 0, 2, 2), Color.Black, 0f, new Vector2(1, 1), squares[j].size, SpriteEffects.None, 0f);
            }

            if (sceneTranssing) sceneTransTimer++;

            if (sceneTransTimer > 80) EngineHelpers.SwitchStages(0);
        }

        public List<sceneTransSquare> squares = new List<sceneTransSquare>();

        public struct sceneTransSquare
        {
            public Vector2 pos;
            public float size;

            public sceneTransSquare(Vector2 _pos, float _size)
            {
                pos = _pos;
                size = _size;
            }

            public void Update()
            {
                size += 0.25f;

                size = size.Clamp(0, 4f);
            }
        }
    }
}
