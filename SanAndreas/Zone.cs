using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SanAndreas
{
    public class Zone
    {
        public string name;
        public double[] points;
        public Zone(string name, double[] points)
        {
            this.name = name;
            this.points = points;
        }
    }
}
