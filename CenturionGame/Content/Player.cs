﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using Microsoft.Xna.Framework.Input;
using System.Diagnostics;

namespace CenturionGame.Content
{
    public class Player : Actor
    {
        public Player(Vector2 pos, Stage myStage) : base(pos, myStage)
        {

        }

        private int jumpTimer;

        public float counter;

        private Point frame;

        public int direction;

        private int walkFrame;

        public override void Load()
        {
            AddComponent(new Rigidbody(this));
            AddComponent(new BoxCollider(this));

            GetComponent<Rigidbody>().gravityDir = Vector2.UnitY;
            GetComponent<Rigidbody>().gravityForce = 0.05f;

            myShield = new Shield(Center + new Vector2(10, 0), myStage);

            myStage.AddActor(myShield);

            width = 16;
            height = 16;

            base.Load();
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);

            EngineHelpers.DrawOutline(spriteBatch, 1f, Color.Green, EngineHelpers.GetTexture("Player"), new Vector2((int)position.X - (int)myStage.screenPosition.Y + 16, (int)position.Y - (int)myStage.screenPosition.Y + 32), new Rectangle(frame.X * width, frame.Y * height, width, height), 0f, 1f, direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally);
            
            spriteBatch.Draw(EngineHelpers.GetTexture("Player"), new Rectangle((int)position.X - (int)myStage.screenPosition.Y, (int)position.Y - (int)myStage.screenPosition.Y, 16, 16), new Rectangle(frame.X * width, frame.Y * height, width, height), Color.White, 0f, Vector2.Zero, direction == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);

            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.Default, RasterizerState.CullNone);
        }

        public Shield myShield;

        public override void Update()
        {
            Rigidbody rb = GetComponent<Rigidbody>();
            if (Centurion.game.keyboardState.IsKeyDown(Keys.A))
                rb.velocity.X -= 0.05f;

            if (Centurion.game.keyboardState.IsKeyDown(Keys.D))
                rb.velocity.X += 0.05f;

            if (!Centurion.game.keyboardState.IsKeyDown(Keys.D) && rb.velocity.X >= 0.05f)
            {
                rb.velocity.X *= 0.01f;
            }

            if (!Centurion.game.keyboardState.IsKeyDown(Keys.A) && rb.velocity.X <= -0.05f)
            {
                rb.velocity.X *= 0.01f;
            }


            rb.velocity.X = rb.velocity.X.Clamp(-1.5f, 1.5f);

            if (Centurion.game.keyboardState.IsKeyDown(Keys.Space) && rb.velocity.Y == 0 && jumpTimer == 0) //scale.X = 0.25, should be 8
            {
                EngineGame.instance.Audio["SpireShoot.wav"].Play();

                rb.velocity.Y -= 2.5f;
            }

            if (rb.velocity.Y <= -0.01f)
            {
                if (jumpTimer < 10 && Centurion.game.keyboardState.IsKeyDown(Keys.Space))
                {
                    rb.gravityForce -= (rb.gravityForce / 10f);
                }

                frame = new Point(0, 2);

                jumpTimer++;
            }

            if(rb.velocity.Y >= 0.01f)
            {
                frame = new Point(0, 3);
            }

            if(Math.Abs(rb.velocity.Y) <= 0.01f)
            {
                frame = new Point(0, 0);

                if(Math.Abs(rb.velocity.X) >= 0.01f && (Centurion.game.keyboardState.IsKeyDown(Keys.A) || Centurion.game.keyboardState.IsKeyDown(Keys.D)))
                {
                    counter += Math.Abs((int)(rb.velocity.X * 4) / 4);

                    if ((int)counter % 6 == 0)
                    {
                        walkFrame++;

                        if (walkFrame >= 4) walkFrame = 0;
                    }

                    frame = new Point(1, walkFrame);
                }
            }

            if (rb.velocity.Y == 0 && rb.oldVelocity.Y > 0)
            {
                jumpTimer = 0;
                rb.gravityForce = 0.3f;
            }

            if (Math.Abs(rb.velocity.X) >= 0.01f)
            {
                if (rb.velocity.X < 0)
                    direction = -1;
                else
                    direction = 1;
            }

            myShield.position = Center + new Vector2(10, 0) + new Vector2(-myShield.width / 2, -myShield.height / 2);

            SetComponent<Rigidbody>(rb);

            base.Update();
        }
    }
}
