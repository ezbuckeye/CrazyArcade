﻿using CrazyArcade.CAFramework.Controller;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyArcade.CAFramework
{
    public class CAGameLogicSystem : IGameSystem
    {
        List<ISprite> sprites = new List<ISprite>();
        List<ISprite> removeSpriteList = new List<ISprite>();
        List<ISprite> addSpriteList = new List<ISprite>();
        public CAGameLogicSystem()
        {
            sprites = new List<ISprite>();
        }

        public void AddSprite(ISprite sprite)
        {
            //sprites.Add(sprite);
            QueueAdd(sprite);
        }

        public void RemoveAll()
        {
            sprites = new List<ISprite>();
        }

        public bool RemoveSprite(ISprite sprite)
        {
            //return sprites.Remove(sprite);
            QueueRemove(sprite);
            return false;
        }

        public void Update(GameTime time)
        {
            foreach (ISprite sprite in sprites)
            {
                sprite.Update(time);
            }
            foreach (ISprite removeSprite in removeSpriteList)
            {
                sprites.Remove(removeSprite);
            }
            foreach (ISprite addSprite in addSpriteList)
            {
                sprites.Add(addSprite);
            }
            removeSpriteList.Clear();
            addSpriteList.Clear();
        }
        private void QueueRemove(ISprite sprite)
        {
            removeSpriteList.Add(sprite);
        }
        private void QueueAdd(ISprite sprite)
        {
            addSpriteList.Add(sprite);
        }
    }
}