using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.Activity
{
    public record Activity(int Steps, double AktiveTime, double GoalTime, double BurnedCalories, double Distance);
    
}
