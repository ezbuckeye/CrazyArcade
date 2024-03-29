﻿using CrazyArcade.CAFramework;
using CrazyArcade.CAFrameWork.Transition;
using Microsoft.Xna.Framework;
using System;
namespace CrazyArcade.CAFrameWork.CAGame
{
	public interface IGameDelegate
	{
		public static Point ScreenSize { get; }
		public void NewGame();
		public void StartGame();
		public void Restart();
		public ISceneState Scene { get; set; }
		public void Quit();
        void StageTransitTo(int stageNum, int dir);
    }
}

