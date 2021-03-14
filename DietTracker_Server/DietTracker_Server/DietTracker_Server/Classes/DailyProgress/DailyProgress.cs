using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.DailyProgress
{
    public record DailyProgress(string Name, double Now, double Goal);
    
}
