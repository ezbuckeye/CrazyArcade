﻿using System;
namespace CrazyArcade.CAFramework.Controller
{
	public interface IControllerDelegate
	{
		void KeyUp();
        void KeyDown();
        void KeyLeft();
        void KeyRight();
        void KeySpace();
        void RightClick();
        
        void LeftClick(int x, int y);
        void LeftClick();
    }
}
