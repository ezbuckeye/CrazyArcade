﻿using CrazyArcade.BombFeature;
using CrazyArcade.CAFramework;
using CrazyArcade.Content;
using CrazyArcade.Items;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CrazyArcade.PlayerStateMachine
{
    public class CharacterStateTurtle : ICharacterState
    {
        private Character character;
        private PlayerTurtle turtle;
        public CharacterStateTurtle(Character character)
        {
            this.character = character;
            character.animationHandleInt = 0;
            turtle = new PlayerTurtle(character);
            //character.SceneDelegate.ToAddEntity(turtle);

            character.spriteAnims = SetSprites();
            character.ModifiedSpeed = character.DefaultSpeed * .8f;

        }

        public bool CouldPutBomb { get => true; }

        public bool CouldGetItem { get => true; }

        public void ProcessAttaction()
        {
            // when player takes attaction by the explosion, it switches to its free state
            character.playerState = new CharacterStateFree(character);
        }

        public void ProcessItem()
        {
            // could, more code in the future
        }

        public void ProcessRide()
        {
            // could, more code in the future
        }

        public void ProcessState(GameTime time)
        {
            character.CalculateMovement();
            character.UpdatePosition();
            character.animationHandleInt = (int)character.direction;
            if (character.CurrentSpeed.X == 0 && character.CurrentSpeed.Y == 0)
            {
                character.SpriteAnim.playing = false;
                character.SpriteAnim.setFrame(0);
                turtle.SpriteAnim.playing = false;
                turtle.SpriteAnim.setFrame(0);
            }
            else
            {
                character.SpriteAnim.playing = true;
                turtle.SpriteAnim.playing = true;
            }

        }
        public void endState()
        {
            turtle.Delete_Self();
        }

        public SpriteAnimation[] SetSprites()
        {
            SpriteAnimation[] spriteAnims = new SpriteAnimation[4];
            spriteAnims[(int)Dir.Up] = new SpriteAnimation(TextureSingleton.GetPlayer1(), new Rectangle(12, 460, 44, 52));
            spriteAnims[(int)Dir.Down] = new SpriteAnimation(TextureSingleton.GetPlayer1(), new Rectangle(60, 460, 44, 52));
            spriteAnims[(int)Dir.Left] = new SpriteAnimation(TextureSingleton.GetPlayer1(), new Rectangle(110, 460, 42, 56));
            spriteAnims[(int)Dir.Right] = new SpriteAnimation(TextureSingleton.GetPlayer1(), new Rectangle(156, 460, 42, 56));

            for (int i = 0; i < 4; i++)
            {
                spriteAnims[i].Scale = character.SpriteAnim.Scale;
            }
            return spriteAnims;
        }

    }
}
