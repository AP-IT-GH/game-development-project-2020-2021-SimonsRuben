﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using TiledSharp;
using System.Diagnostics;
using GameDevProject.Detections;

namespace GameDevProject.World
{
    public enum side {Top,Right,Bottem,Left}
    class Room
    {
        private TmxMap map;

        private Texture2D tileset;
        private int tileWidth;
        private int tileHeight;
        private int tilesetTilesWide;
        private int tilesetTilesHigh;
        List<Rectangle> collisions = new List<Rectangle>();

        public List<Rectangle> Doors = new List<Rectangle>();
        public List<Vector2> SpawnAreas = new List<Vector2>();
        public Room(TmxMap m , Texture2D texture)
        {
            this.map = m;
            this.tileset = texture;

            tileWidth = map.Tilesets[0].TileWidth;
            tileHeight = map.Tilesets[0].TileHeight;

            tilesetTilesWide = tileset.Width / tileWidth;
            tilesetTilesHigh = tileset.Height / tileHeight;

            foreach (var o in map.ObjectGroups[0].Objects)
            {
                collisions.Add(new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height));
            }

            for (int i = 0; i < 4; i++)
            {
                Doors.Add(Rectangle.Empty);
                SpawnAreas.Add(Vector2.Zero);
                //making list 4 long and putting an empty vector in it so i can check in other places if it is empty or not

            }
            FillDoors();
            FillSpawnAreas();
            
        }

        private void FillDoors()
        {
            foreach (var o in map.ObjectGroups[1].Objects)
            {
                if (o.X < 300)
                {
                    Doors[(int)side.Left] = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);
                }
                else if (o.X > 1250)
                {
                    Doors[(int)side.Right] = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);
                }
                else if (o.Y < 400)
                {
                    Doors[(int)side.Top] = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);
                }
                else if (o.Y > 500)
                {
                    Doors[(int)side.Bottem] = new Rectangle((int)o.X, (int)o.Y, (int)o.Width, (int)o.Height);
                }
            }
        }
        private void FillSpawnAreas()
        {
            foreach (var o in map.ObjectGroups[2].Objects)
            {
                if (o.X < 300)
                {
                    SpawnAreas[(int)side.Left] = new Vector2((float)o.X, (float)o.Y);
                }
                else if (o.X > 1250)
                {
                    SpawnAreas[(int)side.Right] = new Vector2((float)o.X, (float)o.Y);
                }
                else if (o.Y < 400)
                {
                    SpawnAreas[(int)side.Top] = new Vector2((float)o.X, (float)o.Y);
                }
                else if (o.Y > 500)
                {
                    SpawnAreas[(int)side.Bottem] = new Vector2((float)o.X, (float)o.Y);
                }
            }
        }

        

  
        public List<Rectangle> GetCollisions()
        {
            return this.collisions;
        }
        public Vector2 GetSpawn()
        {
            Vector2 pos = new Vector2((float)map.ObjectGroups[2].Objects[0].X, (float)map.ObjectGroups[2].Objects[0].Y);
            return pos;
        }

        public void Draw(SpriteBatch _spriteBatch)
        {
            for (int ji = 0; ji < map.Layers.Count; ji++)
            {

                for (var i = 0; i < map.Layers[ji].Tiles.Count; i++)
                {
                    int gid = map.Layers[ji].Tiles[i].Gid;

                    // Empty tile, do nothing
                    if (gid == 0)
                    {

                    }
                    else
                    {
                        int tileFrame = gid - 1;
                        int column = tileFrame % tilesetTilesWide;
                        int row = (int)Math.Floor((double)tileFrame / (double)tilesetTilesWide);

                        float x = (i % map.Width) * map.TileWidth;
                        float y = (float)Math.Floor(i / (double)map.Width) * map.TileHeight;

                        Rectangle tilesetRec = new Rectangle(tileWidth * column, tileHeight * row, tileWidth, tileHeight);

                        _spriteBatch.Draw(tileset, new Rectangle((int)x, (int)y, tileWidth, tileHeight), tilesetRec, Color.White);
                    }
                }
            }
        }

    }
}