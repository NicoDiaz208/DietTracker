using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.User
{
    class User
    {
        public string Name { get; set; }
        public DateTime DateofBirth { get; set; }
        public string Gender { get; set; }
        public double GoalWeight { get; set; }
        public int Height { get; set; }
        public string Email { get; set; }
        public string Phonenumber { get; set; }
        public int ActivityLevel { get; set; }

        public User(string name, DateTime dateofBirth, string gender, double goalWeight, int height, string email, string phonenumber, int activityLevel)
        {
            Name = name;
            DateofBirth = dateofBirth;
            Gender = gender;
            GoalWeight = goalWeight;
            Height = height;
            Email = email;
            Phonenumber = phonenumber;
            ActivityLevel = activityLevel;
        }
    }
}
