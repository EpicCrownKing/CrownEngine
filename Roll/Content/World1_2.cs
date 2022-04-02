﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using CrownEngine;
using CrownEngine.Prefabs;
using System.Diagnostics;
using Microsoft.Xna.Framework.Input;

namespace Roll.Content
{
    public class World1_2 : Level
    {
        public Actor mainMap;

        public override void CustomLoad()
        {
            tilemaps = new List<Actor>();

            mainMap = new SolidTilemap(new Vector2(0, 0), this, new int[,] {
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,},
{1,1,1,1,1,0,0,0,0,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,0,0,0,},
{1,1,1,1,1,1,0,0,0,0,0,0,0,1,1,1,1,1,1,1,1,1,1,1,1,1,0,0,},
{1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,1,}, }, new List<Texture2D> { EngineHelpers.GetTexture("Dirt"), EngineHelpers.GetTexture("Stone") }, 8);
            
            AddActor(mainMap);

            AddActor(new SpikeTilemap(new Vector2(-32, -40), this, new int[,] {
{0,0},
{0,0}, }, new List<Texture2D> { EngineHelpers.GetTexture("Spikes") }, 8));

            AddActor(new SemisolidPlatform(new Vector2(-32, -40), this, new int[,] {
{0,0},
{0,0}, }, new List<Texture2D> { EngineHelpers.GetTexture("SemisolidPlatform") }, 8));

            AddActor(new Checkpoint(new Vector2(336, 32), this));

            AddActor(new Checkpoint(new Vector2(652, 8), this));

            player = new Player(Vector2.One * 32, this);

            AddActor(player);

            foreach (Actor actor in actors)
            {
                if (actor.HasComponent<TileRenderer>()) tilemaps.Add(actor);
            }
        }

        

        public override void Update()
        {
            //screenPosition.X = screenPosition.X.Clamp(0, 200);

            base.Update();

            screenPosition.X = player.Center.X - EngineGame.instance.windowWidth / 2;

            if (player.Center.X < 336) screenPosition.Y = -24;

            if (player.Center.X >= 256 && player.Center.X < 336)
            {
                screenPosition.Y = -24 + ((player.Center.X - 256) / 80) * -24;
            }

            if (player.Center.X >= 336) screenPosition.Y = -48;
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            SolidTilemap trueTilemap = (mainMap as SolidTilemap);

            for (int i = 0; i < trueTilemap.grid.GetLength(1); i++)
            {
                for (int j = 0; j < trueTilemap.grid.GetLength(0); j++)
                {
                    if (j == 0) continue;

                    if (trueTilemap.grid[j, i] == 1 && trueTilemap.grid[j - 1, i] == 0)
                    {
                        Vector2 pos = trueTilemap.position + (new Vector2(i, j) * trueTilemap.tileSize);

                        switch ((int)(j * i + (i - j)) % 3)
                        {
                            case 0:
                                spriteBatch.Draw(EngineHelpers.GetTexture("Grass2"), pos - screenPosition + new Vector2(1, 0), new Rectangle(0, 0, 2, 4), Color.White, (float)Math.Sin(((ticks / 12f) + (j * i))) / 3f, new Vector2(1, 4), 1f, SpriteEffects.None, 0f);
                                spriteBatch.Draw(EngineHelpers.GetTexture("Grass2"), pos - screenPosition + new Vector2(5, 0), new Rectangle(0, 0, 2, 4), Color.White, (float)Math.Sin(((ticks / 12f) + (j * i))) / 3f, new Vector2(1, 4), 1f, SpriteEffects.None, 0f);
                                break;                                                                                                                                                          
                                                                                                                                                                                                
                            case 1:                                                                                                                                                             
                                spriteBatch.Draw(EngineHelpers.GetTexture("Grass1"), pos - screenPosition + new Vector2(2, 0), new Rectangle(0, 0, 2, 3), Color.White, (float)Math.Sin(((ticks / 10f) + (j * i))) / 3f, new Vector2(1, 3), 1f, SpriteEffects.None, 0f);
                                spriteBatch.Draw(EngineHelpers.GetTexture("Grass3"), pos - screenPosition + new Vector2(5, 0), new Rectangle(0, 0, 2, 5), Color.White, (float)Math.Sin(((ticks / 15f) + (j * i))) / 3f, new Vector2(1, 5), 1f, SpriteEffects.None, 0f);
                                break;                                                                                                                                                                         
                                                                                                                                                                                                               
                            case 2:                                                                                                                                                                            
                                spriteBatch.Draw(EngineHelpers.GetTexture("Grass3"), pos - screenPosition + new Vector2(1, 0), new Rectangle(0, 0, 2, 5), Color.White, (float)Math.Sin(((ticks / 15f) + (j * i))) / 3f, new Vector2(1, 5), 1f, SpriteEffects.None, 0f);
                                spriteBatch.Draw(EngineHelpers.GetTexture("Grass2"), pos - screenPosition + new Vector2(6, 0), new Rectangle(0, 0, 2, 4), Color.White, (float)Math.Sin(((ticks / 12f) + (j * i))) / 3f, new Vector2(1, 4), 1f, SpriteEffects.None, 0f);
                                break;
                        }
                    }
                }
            }

            base.Draw(spriteBatch);
        }
    }
}