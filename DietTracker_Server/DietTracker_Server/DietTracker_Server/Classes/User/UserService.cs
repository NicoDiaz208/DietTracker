using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Text;

namespace DietTracker_Server.Classes.User
{
    class UserService
    {
        UserRepository ur = new UserRepository();

        public String AddUser(User user)
        {
            var newUser = ConvertToBson(user);
            return ur.AddUser(newUser);
        }

        public String DeleteUser(User user)
        {
            var toDeleteUser = ConvertToBson(user);
            return ur.DeleteUser(toDeleteUser);
        }

        public String ReplaceUser(User oldInfo,User newInfo)
        {
            var toReplace = ConvertToBson(oldInfo);
            var replacement = ConvertToBson(newInfo);
            return ur.ReplaceUser(toReplace, replacement);
        }

        public BsonDocument ConvertToBson(User user)
        {
            var document = new BsonDocument 
            { 
                { "name", user.Name },
                { "DateOfBirth", user.DateofBirth },
                { "Gender", user.Gender },
                { "GoalWeight", user.GoalWeight },
                { "Height", user.Height },
                { "Email", user.Email },
                { "Phonenumber", user.Phonenumber },
                { "ActivityLevel", user.ActivityLevel } 
            };
            return document;
        }
    }
}
