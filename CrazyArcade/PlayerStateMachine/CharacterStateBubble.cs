﻿using CrazyArcade.CAFramework;
using CrazyArcade.CAFrameWork.SoundEffectSystem;
using CrazyArcade.Content;
using CrazyArcade.Enemies;
using CrazyArcade.Items;
using CrazyArcade.Levels;
using CrazyArcade.UI;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyArcade.PlayerStateMachine
{
    public class CharacterStateBubble : ICharacterState
    {
        private Character character;
        private PlayerBubble bubble;
        private float elapsedTime = 0f;
        private float popTime = 3000f;
        private bool isPirate;
        public CharacterStateBubble(Character character, bool isPirate)
        {
            this.isPirate = isPirate;
            this.character = character;
            character.animationHandleInt = 0;
            bubble = new PlayerBubble(character, character.parentScene);
            character.spriteAnims = SetSprites();
            character.ModifiedSpeed = 2;
            character.SceneDelegate.ToAddEntity(bubble);

        }

        public bool CouldGetItem { get => false; }

        public bool CouldPutBomb { get => false; }

        public void ProcessAttaction()
        {
            //player wouldn't take attaction any more when in bubble state
        }

        public void ProcessItem(string itemName)
        {
            if (itemName == "needle")
            {
                NeedleItem();
            }
        }

        public void ProcessRide(RideType type, Vector2 pos)
        {
            //player cannot get a ride when in bubble state
        }

        public void ProcessState(GameTime time)
        {
            character.CalculateMovement();
            character.UpdatePosition();
            if (elapsedTime > popTime)
            {
                character.playerState = new CharacterStateFree(character, isPirate);
                bubble.bubbleInt = 2;
                character.lives--;
                if (!isPirate) UI_Singleton.ChangeComponentText("lifeCounter", "count", "Lives: " + character.lives);
                if (character.lives == 0)
                {
                    character.playerState = new CharacterStateDie(this.character, isPirate);
                }
            }
            elapsedTime += (float)time.ElapsedGameTime.TotalMilliseconds;
        }

        public SpriteAnimation[] SetSprites()
        {
            SpriteAnimation[] newSprites = new SpriteAnimation[1];
            newSprites[0] = new SpriteAnimation(TextureSingleton.GetPlayer(isPirate), 12, 389, 44, 56, 2, 4, 10);
            return newSprites;
        }
        private void NeedleItem()
        {
            if (character.needles <= 0) return;
            character.needles--;
            UI_Singleton.ChangeComponentText("needle", "itemCount", "X" + character.needles);
            character.playerState = new CharacterStateFree(character, isPirate);
            bubble.bubbleInt = 2;
        }
    }
}
