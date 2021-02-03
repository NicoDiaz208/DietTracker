using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.DailyProgress
{
    class DailyProgress
    {
        public string Name { get; set; }
        public double Now { get; set; }
        public double Goal { get; set; }

        public DailyProgress(string name, double now, double goal)
        {
            Name = name;
            Now = now;
            Goal = goal;
        }
    }
}
