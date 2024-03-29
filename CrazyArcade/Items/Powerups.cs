﻿ using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using CrazyArcade.Blocks;
using CrazyArcade.CAFramework;
using CrazyArcade.CAFrameWork.SoundEffectSystem;
using CrazyArcade.Enemies;
using CrazyArcade.PlayerStateMachine;
using CrazyArcade.PlayerStateMachine.PlayerItemInteractions;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrazyArcade.Items
{
    public class Balloon : Item
    {

        private static Rectangle source = new Rectangle(11, 73, 40, 53);
        public Balloon(Vector2 position) : base(position, source, Content.TextureSingleton.GetBomb(), 5, 5)
        {

        }
        public override void CollisionLogic(Rectangle overlap, IPlayerCollisionBehavior collisionPartner)
        {
            if (collisionPartner.State.CouldGetItem)
            {
                collisionPartner.ObtainItem(new BombCountModifier());
                this.DeleteSelf();
                this.SceneDelegate.ToAddEntity(new CASoundEffect("SoundEffects/PowerUpSound"));
            }
        }
    }
    public class CoinBag : Item
    {

        private static Rectangle source = new Rectangle(519, 134, 50, 52);
        public CoinBag(Vector2 position) : base(position, source, Content.TextureSingleton.GetCoinbag(), 2, 5)
        {

        }
        public override void CollisionLogic(Rectangle overlap, IPlayerCollisionBehavior collisionPartner)
        {
            if (collisionPartner.State.CouldGetItem)
            {
                collisionPartner.IncreaseScore(50);
                this.DeleteSelf();
                this.SceneDelegate.ToAddEntity(new CASoundEffect("SoundEffects/CoinSound"));
            }
        }
    }
    public class Sneaker : Item
    {
        private static Rectangle source = new Rectangle(396, 138, 40, 44);
        public Sneaker(Vector2 position) : base(position, source, Content.TextureSingleton.GetRollerskates(), 3, 5)
        {
        }
        public override void CollisionLogic(Rectangle overlap, IPlayerCollisionBehavior collisionPartner)
        {
            if (collisionPartner.State.CouldGetItem)
            {
                collisionPartner.ObtainItem(new SpeedModifier());
                this.DeleteSelf();
                this.SceneDelegate.ToAddEntity(new CASoundEffect("SoundEffects/PowerUpSound"));
            }
        }
    }
    public class Turtle : Item
    {
        private static Rectangle source = new Rectangle(14, 131, 37, 59);
        public Turtle(Vector2 position) : base(position, source, Content.TextureSingleton.GetTurtle(), 5, 5)
        {
        }
        public override void CollisionLogic(Rectangle overlap, IPlayerCollisionBehavior collisionPartner)
        {
            if (collisionPartner.State.CouldGetItem)
            {
                Random rand = new Random();
                int n = rand.Next(10);
                // 30% pirate Turtle, 70% turtle
                RideType type = n <= 2 ? RideType.PirateTurtle : RideType.Turtle;
                collisionPartner.State.ProcessRide(type, this.GameCoord);
                this.DeleteSelf(); this.SceneDelegate.ToAddEntity(new CASoundEffect("SoundEffects/PowerUpSound"));
            }
        }

    }

    public class Owl : Item
    {
        private static Rectangle source = new Rectangle(14, 131, 37, 59);
        public Owl(Vector2 position) : base(position, source, Content.TextureSingleton.GetOwl(), 3, 5)
        {
        }
        public override void CollisionLogic(Rectangle overlap, IPlayerCollisionBehavior collisionPartner)
        {
            if (collisionPartner.State.CouldGetItem)
            {
                RideType type = RideType.Owl;
                collisionPartner.State.ProcessRide(type, this.GameCoord);
                this.DeleteSelf(); this.SceneDelegate.ToAddEntity(new CASoundEffect("SoundEffects/PowerUpSound"));
            }
        }

    }
    public class Potion : Item
    {

        private static Rectangle source = new Rectangle(330, 65, 43, 59);
        public Potion(Vector2 position) : base(position, source, Content.TextureSingleton.GetPotion(), 5, 5)
        {
        }
        public override void CollisionLogic(Rectangle overlap, IPlayerCollisionBehavior collisionPartner)
        {
            if (collisionPartner.State.CouldGetItem)
            {
                collisionPartner.ObtainItem(new BlastLengthModifier());
                this.DeleteSelf(); 
                this.SceneDelegate.ToAddEntity(new CASoundEffect("SoundEffects/PowerUpSound"));
            }
        }

        public override void Update(GameTime time)
        {
            //Console.WriteLine("Potion" + base.ScreenCoord);
            base.Update(time);
        }
    }
    public class Coin : Item
    {
        private static Rectangle source = new Rectangle(0, 0, 60, 60);
        public Coin(Vector2 position) : base(position, source, Content.TextureSingleton.GetCoin(), 10, 10)
        {
            canExplode = false;
        }
        public override void CollisionLogic(Rectangle overlap, IPlayerCollisionBehavior collisionPartner)
        {
            if (collisionPartner.State.CouldGetItem)
            {
                collisionPartner.IncreaseScore(10);
                this.DeleteSelf(); 
                this.SceneDelegate.ToAddEntity(new CASoundEffect("SoundEffects/CoinSound"));

            }
        }
    }
    public class KickBoot : Item
    {
        private static Rectangle source = new Rectangle(0, 0, 42, 46);
        public KickBoot(Vector2 position) : base(position, source, Content.TextureSingleton.GetKick(), 3, 10)
        {

        }
        public override void CollisionLogic(Rectangle overlap, IPlayerCollisionBehavior collisionPartner)
        {
            if (collisionPartner.State.CouldGetItem)
            {
                collisionPartner.ObtainItem(new KickModifier());
                this.DeleteSelf(); 
                this.SceneDelegate.ToAddEntity(new CASoundEffect("SoundEffects/PowerUpSound"));
            }
        }
    }
}
