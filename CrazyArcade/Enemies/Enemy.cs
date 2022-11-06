﻿using System;
using CrazyArcade.Blocks;
using CrazyArcade.CAFramework;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;
using CrazyArcade.GameGridSystems;

namespace CrazyArcade.Enemies
{
    public abstract class Enemy: CAEntity, IPlayerCollidable, IGridable
    {
        public SpriteAnimation[] spriteAnims;
        public SpriteAnimation spriteAnim;
        public  CAScene scene;
        protected Dir direction;
        protected float xDifference;
        protected float yDifference;
        protected SpriteEffects effect;
        protected Vector2 Start;
        //----------IGridable Start------------
        private Vector2 gamePos;
        private Vector2 pos;
        public IEnemyState state;
        public SpriteAnimation deathAnimation;
        private float timer;
        public Vector2 ScreenCoord
        {
            get => pos;
            set
            {
                pos = value;
                this.UpdateCoord(value);
            }
        }
        public void UpdateCoord(Vector2 value)
        {
            this.X = (int)value.X;
            this.Y = (int)value.Y;
        }
        public Vector2 GameCoord
        {
            get => gamePos;
            set
            {
                gamePos = value;
                ScreenCoord = trans.Trans(value);
            }
        }
        private IGridTransform trans = new NullTransform();
        public IGridTransform Trans { get => trans; set {
                trans = value;
                ScreenCoord = value.Trans(GameCoord);
                X = (int)ScreenCoord.X;
                Y = (int)ScreenCoord.Y;
                internalRectangle.X = X;
                internalRectangle.Y = Y;
            }
        }
        //----------IGridable End------------
        public Enemy(int x, int y, CAScene scene)

        {
            timer = 0;
            this.scene = scene;
            GameCoord = new Vector2(x, y-2);
            Start = GameCoord;
        }
        protected Rectangle internalRectangle = new Rectangle(0, 0, 30, 30);

        public Rectangle boundingBox => internalRectangle;

        public void CollisionLogic(Rectangle overlap, IPlayerCollisionBehavior collisionPartner)
        {
            collisionPartner.CollisionDestroyLogic();

        }
        public void animateDeath()
        {
            spriteAnims[0] = deathAnimation;
            spriteAnim = spriteAnims[0];
        }
        public override void Update(GameTime time)
        {

            // handled animation updated (position and frame) in abstract level

            SpriteAnim.Position = new Vector2(X, Y);
            SpriteAnim.setEffect(effect);
            SpriteAnim.Update(time);


            xDifference = GameCoord.X - Start.X;
            yDifference = GameCoord.Y - Start.Y;

            if (timer > 1f / 6)
            {
                move(direction);
            }
            else
            {
                timer += (float)time.ElapsedGameTime.TotalMilliseconds;
            }
            internalRectangle.X = X;
            internalRectangle.Y = Y;
        }
        private bool ChangeDir(Dir dir)
        {
            switch (dir)
            {
                case Dir.Right:
                    return xDifference >= 4;
                case Dir.Up:
                    return yDifference <= 0;
                case Dir.Down:
                    return yDifference >= 4;
                case Dir.Left:
                    return xDifference <= 0;
            }
            return false;
        }
        public abstract void UpdateAnimation(Dir dir);

        protected abstract Vector2[] SpeedVector { get; }

        protected void move(Dir dir)
        {
            if (ChangeDir(dir))
            {
                direction = (Dir)((((int)dir) + 1) % 4);
                effect = direction == Dir.Right ? SpriteEffects.FlipHorizontally : SpriteEffects.None;
                UpdateAnimation(dir);
            }
            else
            {
                GameCoord += SpeedVector[(int)dir];
            }
        }

    }
}

