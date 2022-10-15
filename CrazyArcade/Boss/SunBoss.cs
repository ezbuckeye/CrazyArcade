﻿using System;
using System.Collections.Generic;
using CrazyArcade.CAFramework;
using Microsoft.Xna.Framework;
using CrazyArcade.GameGridSystems;

namespace CrazyArcade.Boss
{
	public class SunBoss: CAEntity, ISunBossDelegate, IGridable
	{

        ISceneDelegate sceneDelegate;
        private float unitSize = 44/40;
        public SunBoss(ISceneDelegate sceneDelegate)
		{
            this.sceneDelegate = sceneDelegate;
            this.GameCoord = new Vector2(5, 5);
		}

        IStates states;
        public override List<SpriteAnimation> SpriteAnimList => states.Animation;

        private Vector2 gamePos;
        private Vector2 pos;
        public Vector2 ScreenCoord
        {
            get => pos;
            set
            {
                pos = value;
                this.X = (int)value.X;
                this.Y = (int)value.Y;
            }
        }
        public Vector2 GameCoord { get => gamePos; set => gamePos = value; }

        public override void Load()
        {
            states = new SunBossStartStates(this, new GameTime());
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
            //Console.Out.Write("update sun boss\n");
            states = states.Update(time);
        }

        public bool DidGetDemaged()
        {
            return false;
        }

        public Vector2 GetCharacterRelativePosition()
        {
            return GetCharacterPosition() - this.gamePos;
        }

        public Vector2 GetCharacterPosition()
        {
            Random rand = new Random();
            return new Vector2(5 + (float)rand.Next(0, 100) / 100,
                5 + (float)rand.Next(0, 100) / 100);
        }

        public void Move(Vector2 distance)
        {
            //Console.Out.Write(distance.X);
            //Console.Out.Write(distance.Y);
            this.GameCoord += distance;
        }

        public ISceneDelegate Command()
        {
            return sceneDelegate;
        }

        public Vector2 GetCenter()
        {
            return new Vector2(GameCoord.X + unitSize, GameCoord.Y + unitSize);
        }
    }
}

