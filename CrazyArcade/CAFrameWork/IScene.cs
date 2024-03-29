﻿using System;
using CrazyArcade.CAFramework;
using CrazyArcade.CAFrameWork.Transition;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CrazyArcade.CAFramework
{
	public interface IScene
    {
        void Load();
        void Update(GameTime time);
        void Draw(GameTime time, SpriteBatch batch);
        void AddSprite(IEntity sprite);
        void RemoveSprite(IEntity sprite);
        void RemoveAllSprite();
        LevelPersistence GetData();
        void LoadData(LevelPersistence saveData);
    }
}

