using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes
{
    class Sleep
    {
        //Houers of Sleep Goal
        public int HoSG { get; set; }
        //Houers of Sleep Current
        public int HoSC { get; set; }

        public Sleep(int hoSG, int hoSC)
        {
            HoSG = hoSG;
            HoSC = hoSC;
        }
    }
}
