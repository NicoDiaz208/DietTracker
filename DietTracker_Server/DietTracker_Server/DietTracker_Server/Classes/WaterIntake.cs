using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes
{
    class WaterIntake
    {
        //GoWG=Glasses of Water Goal
        public int GoWG { get; set; }
        //GoWC=Glasses of Water Current
        public int GoWC { get; set; }

        public WaterIntake(int goWG, int goWC)
        {
            GoWG = goWG;
            GoWC = goWC;
        }
    }
}
