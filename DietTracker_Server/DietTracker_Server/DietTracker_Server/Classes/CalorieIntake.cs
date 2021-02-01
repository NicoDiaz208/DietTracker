using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes
{
    class CalorieIntake
    {
        public double Current { get; set; }
        public double Now { get; set; }

        public CalorieIntake(double current, double now)
        {
            Current = current;
            Now = now;
        }
    }
}
