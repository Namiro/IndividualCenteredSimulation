﻿using Newtonsoft.Json;

namespace MultiAgentSystem.Cores.Helpers.Grids
{
    public class Coordinate
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Coordinate(int x = int.MinValue, int y = int.MinValue)
        {
            X = x;
            Y = y;
        }

        /// <summary>
        /// This ToString is do simply to cast the object in a string formated in Json
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public override bool Equals(object obj)
        {
            if (!(obj is Coordinate)) return false;
            Coordinate coord = (Coordinate)obj;
            return this.X == coord.X && this.Y == coord.Y;
        }
    }
}
