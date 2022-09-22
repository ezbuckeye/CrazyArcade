﻿using System;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace CrazyArcade.Singletons
{
	public static class SpriteSheet
	{
		public static ContentManager Content;
		private static Texture2D character;
        public static Texture2D Character
		{
			get
			{
                if (character == null)
                {
					character = Content.Load<Texture2D>("walk");
                }
				return character;
            }
		}
    }
}

