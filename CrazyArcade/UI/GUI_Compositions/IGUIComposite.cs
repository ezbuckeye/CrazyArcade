﻿using CrazyArcade.UI.GUI_Components;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyArcade.UI.GUI_Compositions
{
    public interface IGUIComposite
    {
        int DrawOrder { get; set; }
        bool Visible { get; set; }
        Vector2 Position { get; set; }
        List<IGUIComponent> Components { get; set; }
        Dictionary<string, IGUIComponent> ComponentDict { get; set; }
        string Name { get; set; }
        public void Draw(GameTime time, SpriteBatch batch);
        public void AddComponent(IGUIComponent newComponent);
        public void RemoveComponent(string componentName);
        public void SetPosition(Vector2 position);
    }
}
