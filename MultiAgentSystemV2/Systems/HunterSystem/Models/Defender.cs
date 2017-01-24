using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MultiAgentSystem.Cores.Helpers.Grids;
using MultiAgentSystem.HunterSystem.ViewModels;
using System;

namespace MultiAgentSystem.HunterSystem.Models
{
    class Defender : Cell
    {
        public static int LifeTimeSecondes { get; private set; }
        public TimeSpan CreationTime { get; set; }

        public Defender()
        {
            Color = Color.Transparent;
            Texture = XNAHunterGrid.ContentManager.Load<Texture2D>("defender");

            int a = (int)(0.20 * App.GridSizeX);
            int b = (int)(0.20 * App.GridSizeY);

            if (a > b)
                LifeTimeSecondes = a;
            else
                LifeTimeSecondes = b;

            if (LifeTimeSecondes < 2)
                LifeTimeSecondes = 2;
        }

        public bool IsLifeTimeOut(TimeSpan currentTime)
        {
            if ((CreationTime.Seconds + LifeTimeSecondes) < currentTime.Seconds)
                return true;
            else
                return false;
        }
    }
}
