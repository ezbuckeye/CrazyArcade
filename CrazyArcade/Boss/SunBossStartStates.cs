﻿using System;
using System.Collections.Generic;
using CrazyArcade.CAFramework;
using Microsoft.Xna.Framework;

namespace CrazyArcade.Boss
{
	public class SunBossStartStates: SunBossStates
	{
		public SunBossStartStates(ISunBossDelegate bossDelegate, GameTime time): base(bossDelegate, time)
        {

            animation = new List<SpriteAnimation>();
            animation.Add(new SpriteAnimation(Singletons.SpriteSheet.SunBoss, new Rectangle(43, 0, 88, 88)));
        }
        List<SpriteAnimation> animation;
        public override List<SpriteAnimation> Animation => animation;

        public override IStates Update(GameTime time)
        {
            base.Update(time);
            if (timer.TotalMili > 1500)
			{
				return new SunBossNormalStates(bossDelegate, time);
			}
            return this;
        }
    }
}

