using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1
{
    public class Coordinates
    {
        public Coordinates(int X, int Y)
        { 
            this.X = X; 
            this.Y = Y;
        }
        public int X { get; set; }
        public int Y { get; set; }
    }
}
