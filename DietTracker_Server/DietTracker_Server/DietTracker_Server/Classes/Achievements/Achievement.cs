using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.Achievement
{
    public record Achievement(ObjectId Id, string Name,double Now,double Goal);
    
}
