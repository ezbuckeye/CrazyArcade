﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrazyArcade.CAFramework;
using CrazyArcade.Blocks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using CrazyArcade.GameGridSystems;
using CrazyArcade.BombFeature;
using CrazyArcade.CAFrameWork.GridBoxSystem;
using System.Diagnostics;

namespace CrazyArcade.Items
{
    //Interface made to catagorise those that implement it, items
    public interface IItem : IEntity
    {

    }
    public abstract class Item : CAGridBoxEntity, IItem, IExplosionCollidable, IPlayerCollidable
    {
        //----------IGridable Start------------
        private Vector2 gamePos;
        private Vector2 pos;
       
        public override Vector2 ScreenCoord
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
            hitbox = new Rectangle((int)ScreenCoord.X, (int)ScreenCoord.Y, 36, 36);
        }
        public override Vector2 GameCoord
        {
            get => gamePos;
            set
            {
                gamePos = value;
                ScreenCoord = trans.Trans(value);
            }
        }
        private IGridTransform trans = new NullTransform();

        public override IGridTransform Trans
        {
            get => trans;
            set
            {
                trans = value;
                ScreenCoord = value.Trans(GameCoord);
            }
        }
        //----------IGridable End------------
        protected Rectangle hitbox;
        protected SpriteAnimation spriteAnimation;
        public bool canExplode = true;
        //protected ISceneDelegate parentScene;
        public Item(Vector2 position, Rectangle source, Texture2D texture, int frames, int fps)
            : base(new GridBoxPosition((int)position.X, (int)position.Y, (int)GridObjectDepth.Item))
        {
            //this.parentScene = parentScene;
            spriteAnimation = new SpriteAnimation(texture, frames, fps);
            spriteAnimation.Scale = 0.6f;
            if (texture == Content.TextureSingleton.GetOwl()) spriteAnimation.SetScale(1);
            GameCoord = position;
            this.DrawOrder = -1;
        }

        public override SpriteAnimation SpriteAnim => this.spriteAnimation;

        public Rectangle boundingBox => this.hitbox;

        public override void Update(GameTime time)
        {

        }
        public override void Load()
        {
        }
        //Assumes that @this is in the IScene that is passed
        public void DeleteSelf()
        {
            SceneDelegate.ToRemoveEntity(this);
        }

        public bool Collide(IExplosion bomb)
        {
            if (!canExplode) return true;
            DeleteSelf();
            return true;
        }

        public abstract void CollisionLogic(Rectangle overlap, IPlayerCollisionBehavior collisionPartner);
        private static Dictionary<int, Func<Vector2, Item>> randList;
        public static Dictionary<int, Func<Vector2, Item>> RandList
        {
            get
            {
                if (randList == null)
                {
                    randList = new Dictionary<int, Func<Vector2, Item>>();
                    randList[5] = (pos) => new CoinBag(pos);   //0-5  (5%)
                    randList[30] = (pos) => new Balloon(pos);   //5-20 (15%)
                    randList[35] = (pos) => new KickBoot(pos);
                    randList[40] = (pos) => new Sneaker(pos);
                    randList[60] = (pos) => new Potion(pos);
                    randList[70] = (pos) => new Coin(pos);
					randList[80] = (pos) => new Turtle(pos);
					randList[90] = (pos) => new Owl(pos);
					randList[100] = (_) => null;                //70-100 (30%)
                }
                return randList;
            }
        }

        public static Func<Vector2, IItem> Random()
        {
            List<int> keys = RandList.Keys.ToList();
            keys.Sort();

            Random rand = new Random();
            int number = rand.Next(0, keys[keys.Count - 1]);
            foreach (int key in keys)
            {
                if (key > number)
                    return (Vector2 pos) => randList[key](pos);
            }
            return null;
        }

		public override bool IsSolid(Dir dir, bool couldKick)
        {
            return false;
        }
		public override HashSet<Point> PotentialDangerousTile()
        {
            return new HashSet<Point>();
        }
	}
}