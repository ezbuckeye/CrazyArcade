﻿using System;
using Microsoft.Xna.Framework;

namespace CrazyArcade.CAFrameWork.EntityBehaviors
{
	public interface IGridTransform
	{
		public Rectangle Trans(double x, double y);
	}
}

