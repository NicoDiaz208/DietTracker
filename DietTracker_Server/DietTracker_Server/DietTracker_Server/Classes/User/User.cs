using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.User
{
    public record User(string Name, DateTime DateofBirth, string Gender, double GoalWeight, int Height, string Email, string Phonenumber, int ActivityLevel);
    
}
