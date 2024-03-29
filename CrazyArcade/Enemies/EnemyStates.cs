﻿using CrazyArcade.CAFramework;
using CrazyArcade.CAFrameWork.SoundEffectSystem;
using CrazyArcade.CAFrameWork.GridBoxSystem;
using Microsoft.Xna.Framework;
using System;
using static System.Formats.Asn1.AsnWriter;


namespace CrazyArcade.Enemies
{
    public interface IEnemyState
    {
        void ChangeDirection();
        void Update(GameTime time);

    }
    public abstract class EnemyState: IEnemyState
    {
        protected ISceneDelegate scene;
        protected Enemy enemy;
        protected float timer = 0;
        private readonly float shootTime = 1000;
        public EnemyState(Enemy Enemy)
        {
            this.enemy = Enemy;
            scene = enemy.SceneDelegate;
        }
        public virtual void ChangeDirection()
        {

        }
        public virtual void Update(GameTime time)
        {
            if (timer > shootTime)
            {
                enemy.ShootProjectile(time);
                timer = 0;
            }
            else
            {
                timer += (float)time.ElapsedGameTime.TotalMilliseconds;
            }
            
            enemy.Move();
        }
    }
    
    public class EnemyLeftState : EnemyState
    {
        private readonly int xOffset = -4;
        private readonly int yOffset = 2;
        private readonly int width = 1;
        private readonly int height = 26;
        public EnemyLeftState(Enemy Enemy): base(Enemy)
        {
          
            enemy.direction = Dir.Left;
            //This sets the size and location of the enemy block collision detector
            //It changes location and orientation based on which direction the enemy is going.
            enemy.SetDetectorValues(xOffset, yOffset, width, height);
        }
        public override void ChangeDirection()
        {
            Random rnd = new();
            int num = rnd.Next();
            if (num % 2 == 0)
            {
                enemy.state = new EnemyDownState(enemy);
            }
            else
            {
                enemy.state = new EnemyRightState(enemy);
            }
            
        }

    }
    public class EnemyRightState : EnemyState
    {
        private readonly int xOffset = 33;
        private readonly int yOffset = 2;
        private readonly int width = 1;
        private readonly int height = 26;
        public EnemyRightState(Enemy Enemy) : base(Enemy)
        {
            enemy.direction = Dir.Right;
            //This sets the size and location of the enemy block collision detector
            //It changes location and orientation based on which direction the enemy is going.
            enemy.SetDetectorValues(xOffset, yOffset, width, height);

        }
        public override void ChangeDirection()
        {
            Random rnd = new();
            int num = rnd.Next();
            if (num % 2 == 0)
            {
                enemy.state = new EnemyUpState(enemy);
            }
            else
            {
                enemy.state = new EnemyLeftState(enemy);
            }


        }

    }
    public class EnemyUpState : EnemyState
    {
        private readonly int xOffset = 2;
        private readonly int yOffset = -4;
        private readonly int width = 26;
        private readonly int height = 1;
        public EnemyUpState(Enemy Enemy) : base(Enemy)
        {
            enemy.direction = Dir.Up;
            //This sets the size and location of the enemy block collision detector
            //It changes location and orientation based on which direction the enemy is going.
            enemy.SetDetectorValues(xOffset, yOffset, width, height);

        }
        public override void ChangeDirection()
        {

            Random rnd = new();
            int num = rnd.Next();
            
            if (num % 2 == 0)
            {
                enemy.state = new EnemyLeftState(enemy);
            }
            else
            {
                enemy.state = new EnemyDownState(enemy);
            }

        }

      
    }
    public class EnemyDownState : EnemyState
    {
        private readonly int xOffset = 2;
        private readonly int yOffset = 33;
        private readonly int width = 26;
        private readonly int height = 1;
        public EnemyDownState(Enemy enemy): base(enemy)
        {
            enemy.direction = Dir.Down;
            //This sets the size and location of the enemy block collision detector
            //It changes location and orientation based on which direction the enemy is going.
            enemy.SetDetectorValues(xOffset, yOffset, width, height);
        }
        public override void ChangeDirection()
        {
            Random rnd = new();
            int num = rnd.Next();

            if (num % 2 == 0)
            {
                enemy.state = new EnemyRightState(enemy);
            }
            else
            {
                enemy.state = new EnemyDownState(enemy);
            }


        }

    }

    public class EnemyDeathState : EnemyState
    {
        private float opacity;
        private readonly float fadeTime;
        public EnemyDeathState(Enemy Enemy) : base(Enemy)
        {
            enemy.spriteAnims = new SpriteAnimation[1];
            //Changes animation frames to death animation
            enemy.spriteAnims[0] = enemy.deathAnimation;
            enemy.direction=0;
            opacity = 1f;
            fadeTime = 100f;
            enemy.SceneDelegate.ToAddEntity(new CASoundEffect("SoundEffects/DoorSound"));

        }


        public override void Update(GameTime time)
        {
            if (timer > fadeTime)
            {
                scene.ToRemoveEntity(enemy);
            }
            else
            {
                //fades out with timer
                opacity = 1f - timer / fadeTime;
                enemy.spriteAnims[0].Color = Color.White * opacity;
                timer += (float)time.ElapsedGameTime.TotalMilliseconds;
            }

        }
    }
    
}
