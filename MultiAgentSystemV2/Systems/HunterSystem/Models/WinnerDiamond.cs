﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MultiAgentSystem.Cores.Helpers.Grids;
using MultiAgentSystem.HunterSystem.ViewModels;

namespace MultiAgentSystem.HunterSystem.Models
{
    class WinnerDiamond : Cell
    {
        public WinnerDiamond()
        {
            Color = Color.Transparent;
            Texture = XNAHunterGrid.ContentManager.Load<Texture2D>("door");
        }
    }
}