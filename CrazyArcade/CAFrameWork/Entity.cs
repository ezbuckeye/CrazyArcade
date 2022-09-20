﻿using System;
using CrazyArcade.AWFrameWork;
using CrazyArcade.CAFrameWork.EntityBehaviors;
using Microsoft.Xna.Framework;

//This should be the base class for all entities
namespace CrazyArcade.CAFrameWork
{
	public abstract class Entity: AWSprite, IEntity
	{
		public Entity()
		{
		}
		private IStageModifyProxy stage;
        public IStageModifyProxy Stage
		{
			set
			{
				stage = value;
			}
		}

    }
}

