﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CrazyArcade.Content;
using System.Diagnostics;
using CrazyArcade.CAFramework;
using Microsoft.Xna.Framework;

namespace CrazyArcade.UI.GUI_Components
{
    public class GUITestTexture : GUIBase
    {
        public GUITestTexture(string name)
        {
            this.name = name;
            Sprite = new SpriteAnimation(TextureSingleton.GetSplashImage(), new Rectangle(0,0,996,995));
            ToggleRectangleFlag();
            ChangeComponentTextureOutputRect(50, 50);
            SetPosition(new Vector2(0, 23));
        }
    }
}
