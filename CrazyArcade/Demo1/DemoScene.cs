﻿using CrazyArcade.Boss;
using CrazyArcade.BombFeature;
using CrazyArcade.CAFramework;
using CrazyArcade.CAFramework.Controller;
using CrazyArcade.PlayerStateMachine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrazyArcade.Enemy;
using CrazyArcade.GameGridSystems;
using Microsoft.Xna.Framework;

namespace CrazyArcade.Demo1
{
    public class DemoScene : CAScene
    {

        public DemoScene(Game1 game)
        {
            gameRef = game;
        }
        public override void LoadSystems()
        {
            this.systems.Add(new CAControllerSystem());
            this.systems.Add(new CAGameLogicSystem());
            this.systems.Add(new Blocks.Sprint2Manager(this));
            this.systems.Add(new EnemyManager(this));
            this.systems.Add(new CAGameGridSystems(new Vector2(0, 0), 40));
        }

        public override void LoadSprites()
        {

            Console.Out.Write("added Boss");
            //this.AddSprite(new DemoCharacter(new DemoController()));
            //this.AddSprite(new BombEnemySprite(100,100));
            this.AddSprite(new SunBoss(this));
            this.AddSprite(new PlayerCharacter(new DemoController(), this));
        }
    }
}
