﻿using Microsoft.Xna.Framework;

using CrazyArcade.Levels;
using CrazyArcade.GameGridSystems;

namespace CrazyArcade.Levels
{
    public class CreateMap
	{
        public MapSchema mapObject { get; }
        public ReadJSON Reader;
        public CreateMap(string fileName)
        {
            Reader = new ReadJSON(fileName, ReadJSON.fileType.MapFile);
            mapObject = new MapSchema();
            mapObject = Reader.mapObject;
        }
    }
    //allows for scene to load in level
    public class CreateLevel
	{
		public LevelSchema levelObject { get; }
        public ReadJSON Reader;
		public CreateLevel(string fileName)
		{
			Reader = new ReadJSON(fileName, ReadJSON.fileType.LevelFile);
			levelObject = Reader.levelObject;
		}

		public enum LevelItem
		{
			PlayerPosition,
			DoorPosition,
			LightSandPosition,
			DarkSandPosition,
			StonePosition,
            CactusPosition,
            DarkTreePosition,
            LightTreePosition,
            SunBossPosition,
			OctoBossPosition,
			BombPosition,
			SquidPosition,
			BatPosition,
			RobotPosition,
			CoinBagPosition,
			BalloonPosition,
			SneakerPosition,
			TurtlePosition,
			PotionPosition,
			CoinPosition,
			KickPosition,
			PiratePosition,
			MimicPosition,
			CyanPosition
		}

		public Vector2 GetStartPosition(int[] coord)
		{
			return new Vector2(coord[0], coord[1]);
		}
		public Vector2[] GetStartPositionArray(int[][] coordArray)
		{
			Vector2[] result = new Vector2[coordArray.Length];
			for (int i = 0; i < coordArray.Length; i++)
			{
				result[i] = GetStartPosition(coordArray[i]);

			}
			return result;
		}

		public Vector2 GetPlayerStart()
		{
			Vector2 playerStart = GetStartPosition(levelObject.Player);
			return new Vector2(playerStart.X + 1, playerStart.Y + 1);
		}
		public Color GetBackgroundColor()
		{
			return new Color(levelObject.Background[0], levelObject.Background[1], levelObject.Background[2]);
		}
		public Vector2 GetBorder()
		{
			return new Vector2((float)levelObject.Border[0], (float)levelObject.Border[1]);
		}
		//returns an array of vectors for the location
		public Vector2[] GetItemLocation(LevelItem property)
		{
			Vector2[] array = new Vector2[1];
			LevelItem item = property;
			switch (item)
			{
				case LevelItem.PlayerPosition:
					array[0] = GetStartPosition(levelObject.Player);
					break;
					
				case LevelItem.DoorPosition:
					array = GetStartPositionArray(levelObject.Blocks.Door);
					break;

                case LevelItem.LightSandPosition:
                    array = GetStartPositionArray(levelObject.Blocks.LightSand);
                    break;
                case LevelItem.DarkSandPosition:
					array = GetStartPositionArray(levelObject.Blocks.DarkSand);
					break;
					
				case LevelItem.StonePosition:
					array = GetStartPositionArray(levelObject.Blocks.Stone);

					break;
                case LevelItem.CactusPosition:
                    array = GetStartPositionArray(levelObject.Blocks.Cactus);
                    break;

                case LevelItem.DarkTreePosition:
                    array = GetStartPositionArray(levelObject.Blocks.DarkTree);
                    break;

                case LevelItem.LightTreePosition:
                    array = GetStartPositionArray(levelObject.Blocks.LightTree);
                    break;
				case LevelItem.CyanPosition:
					array = GetStartPositionArray(levelObject.Blocks.Cyan);
					break;

				case LevelItem.SunBossPosition:
					array = GetStartPositionArray(levelObject.Boss.Sun);
					break;
					
				case LevelItem.OctoBossPosition:
					array = GetStartPositionArray(levelObject.Boss.Octo);
					break;
			
				case LevelItem.BombPosition:
					array = GetStartPositionArray(levelObject.Enemies.Bomb);
					break;
			
				case LevelItem.SquidPosition:
					array = GetStartPositionArray(levelObject.Enemies.Squid);
					break;
					
				case LevelItem.BatPosition:
					array = GetStartPositionArray(levelObject.Enemies.Bat);
					break;
					
				case LevelItem.RobotPosition:
					array = GetStartPositionArray(levelObject.Enemies.Robot);
					break;
					
				case LevelItem.CoinBagPosition:
					array = GetStartPositionArray(levelObject.Items.CoinBag);
					break;
					
				case LevelItem.BalloonPosition:
					array = GetStartPositionArray(levelObject.Items.Balloon);
					break;
				
				case LevelItem.SneakerPosition:
					array = GetStartPositionArray(levelObject.Items.Sneaker);
					break;
				
				case LevelItem.TurtlePosition:
					array = GetStartPositionArray(levelObject.Items.Turtle);
					break;
					
				case LevelItem.PotionPosition:
					array = GetStartPositionArray(levelObject.Items.Potion);
					break;
				
				case LevelItem.CoinPosition:
					array = GetStartPositionArray(levelObject.Items.Coin);
					break;
                case LevelItem.KickPosition:
                    array = GetStartPositionArray(levelObject.Items.Kick);
                    break;
				case LevelItem.PiratePosition:
					array = GetStartPositionArray(levelObject.Pirate);
					break;
                case LevelItem.MimicPosition:
                    array = GetStartPositionArray(levelObject.Enemies.Mimic);
                    break;
				default:

					break;
				
			}
			return getJustifiedPositionArr(array);
		}

		// probably temporary method
		// since the initialization of blocks, the indexes of border thus have to be negative
		// which would cause issue in terms of collision due to integer casting
        private Vector2[] getJustifiedPositionArr(Vector2[] array)
        {
			for(int i = 0; i < array.Length; i++)
            {
				Vector2 pos = array[i];
                array[i] = new Vector2(pos.X + 1, pos.Y + 1);
            }
			return array;
        }
    }
}
