using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes
{
    class Activity
    {
        public int Steps { get; set; }
        public double AktiveTime { get; set; }
        public  double GoalTime { get; set; }
        public double BurnedCalories { get; set; }
        public double Distance { get; set; }
    }
}
