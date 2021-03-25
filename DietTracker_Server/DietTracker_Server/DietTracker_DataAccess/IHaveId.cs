using MongoDB.Bson;

namespace DietTracker_DataAccess
{
    public interface IHaveId
    {
        public ObjectId Id { get; }
    }
}
