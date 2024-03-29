﻿using System;
using System.Collections.Generic;
using CrazyArcade.CAFramework;
using CrazyArcade.GameGridSystems;
using CrazyArcade.Projectile;
using Microsoft.Xna.Framework;
using CrazyArcade.Blocks;

namespace CrazyArcade.Boss
{
	public class SunBossProjectile: CAEntity, IPlayerCollidable, IGridable
	{
        float timeAdaptor = 4;
        ISceneDelegate sceneDelegate;
        ITimer timer;
        private Vector2 speed;
        public SunBossProjectile(ISceneDelegate sceneDelegate, Vector2 speed, Vector2 position, ITimer timer)
		{
            this.sceneDelegate = sceneDelegate;
            this.timer = timer;
            this.gamePos.Y = position.Y;
            this.gamePos.X = position.X;
            this.speed = speed;
            Rectangle[] rectList = new Rectangle[6];
            rectList[0] = new Rectangle(193, 291, 13, 13);
            rectList[1] = new Rectangle(207, 294, 9, 9);
            rectList[2] = new Rectangle(217, 296, 5, 5);
            rectList[3] = new Rectangle(223, 290, 15, 15);
            rectList[4] = new Rectangle(239, 292, 11, 11);
            rectList[5] = new Rectangle(251, 294, 7, 7);
            animation = new SpriteAnimation(Singletons.SpriteSheet.SunBoss, rectList);
        }
        private SpriteAnimation animation;
        public override SpriteAnimation SpriteAnim => animation;

        public Rectangle internalRectangle = new Rectangle(0, 0, 10, 10);

        public Rectangle boundingBox => internalRectangle;

        private Vector2 gamePos;
        private Vector2 pos;
        public Vector2 ScreenCoord
        {
            get => pos;
            set
            {
                pos = value;
                this.UpdateCoord(value);
            }
        }
        public Vector2 GameCoord { get => gamePos; set => gamePos = value; }
        private IGridTransform trans = new NullTransform();
        public IGridTransform Trans { get => trans; set => trans = value; }

        public void UpdateCoord(Vector2 value)
        {
            this.X = (int)value.X;
            this.Y = (int)value.Y;
            this.internalRectangle.X = (int)ScreenCoord.X;
            this.internalRectangle.Y = (int)ScreenCoord.Y;
        }


        public override void Load()
        {

        }
        public override void Update(GameTime time)
        {
            timer.Update(time.TotalGameTime);
            gamePos.X += speed.X * timer.FrameDiff.Milliseconds / timeAdaptor;
            gamePos.Y += speed.Y * timer.FrameDiff.Milliseconds / timeAdaptor;
            if (timer.TotalMili > 1500)
            {
                sceneDelegate.ToRemoveEntity(this);
            }
        }

        public void CollisionLogic(Rectangle overlap, IPlayerCollisionBehavior collisionPartner)
        {
            collisionPartner.CollisionDestroyLogic();
        }
    }
}

