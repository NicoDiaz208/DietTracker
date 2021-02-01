using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes
{
    class Recipe
    {
        public string Name { get; set; }
        public double PrepareTime { get; set; }
        public double Difficulty { get; set; }
        public string Kategorie { get; set; }

        public Recipe(string name, double prepareTime, double difficulty, string kategorie)
        {
            Name = name;
            PrepareTime = prepareTime;
            Difficulty = difficulty;
            Kategorie = kategorie;
        }
    }
}
