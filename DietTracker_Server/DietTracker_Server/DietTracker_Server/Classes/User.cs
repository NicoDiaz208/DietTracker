using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes
{
    class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Gender { get; set; }
        public double GoalWeight { get; set; }
        public int Height { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public int ActivityLevel { get; set; }
    }
}
