using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.Recipe
{
    public record Recipe(string Name, double PrepareTime, double Difficulty, string Kategorie);
    
}
