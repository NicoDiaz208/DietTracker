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
    }
}
