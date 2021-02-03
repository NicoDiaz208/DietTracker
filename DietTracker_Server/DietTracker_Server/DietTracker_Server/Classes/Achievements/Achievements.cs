using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.Achievements
{
    class Achievements
    {
        public string Name { get; set; }
        public double Now { get; set; }
        public double Goal { get; set; }

        public Achievements(string name, double now, double goal)
        {
            Name = name;
            Now = now;
            Goal = goal;
        }
    }
}
