using MongoDB.Bson;

namespace DietTracker_DataAccess
{
    public record Achievement(
        ObjectId Id, 
        string Name,
        double Now,
        double Goal) : IHaveId;
}
