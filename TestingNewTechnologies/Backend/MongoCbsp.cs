using System;
using MongoDB.Bson;
using MongoDB.Driver;

namespace MongoDBConnection
{
    class Program
    {
        static void Main(string[] args)
        {
            MongoClient dbClient = new MongoClient();//<-- Hier den Atlas Connection String

            var dbList = dbClient.ListDatabases().ToList();

            Console.WriteLine("The list of databases on this server is: ");
            foreach (var db in dbList)
            {
                Console.WriteLine(db);
            }


            var database = dbClient.GetDatabase("sample_training");
            var collection = database.GetCollection<BsonDocument>("grades");

            //Bsp für ein Bson Data 
            var document = new BsonDocument { { "student_id", 10000 }, {
                "scores",
                new BsonArray {
                new BsonDocument { { "type", "exam" }, { "score", 88.12334193287023 } },
                new BsonDocument { { "type", "quiz" }, { "score", 74.92381029342834 } },
                new BsonDocument { { "type", "homework" }, { "score", 89.97929384290324 } },
                new BsonDocument { { "type", "homework" }, { "score", 82.12931030513218 } }
                }
                }, { "class_id", 480 }
        };
            //Inserten des student
            collection.InsertOne(document);


            
            //Lesen des ersten documents in der datenbank
            var firstDocument = collection.Find(new BsonDocument()).FirstOrDefault();
            Console.WriteLine(firstDocument.ToString());

            //Filter erstellen und benutzten
            var filter = Builders<BsonDocument>.Filter.Eq("student_id", 10000);
            var studentDocument = collection.Find(filter).FirstOrDefault();
            Console.WriteLine(studentDocument.ToString());

            //Lesen aller documenten
            var documents = collection.Find(new BsonDocument()).ToList();
            foreach (BsonDocument doc in documents)
            {
                Console.WriteLine(doc.ToString());
            }

            //Filter für hohe scores
            var highExamScoreFilter = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>(
                                       "scores", new BsonDocument { { "type", "exam" },
                                       { "score", new BsonDocument { { "$gte", 95 } } }
                                       });
            var highExamScores = collection.Find(highExamScoreFilter).ToList();

            //Erstellung eines Cursor um durch die gefundene Liste zu kommen
            var cursor = collection.Find(highExamScoreFilter).ToCursor();
            foreach (var doc in cursor.ToEnumerable())
            {
                Console.WriteLine(document);
            }

            //Sort
            var sort = Builders<BsonDocument>.Sort.Descending("student_id");

            var highestScores = collection.Find(highExamScoreFilter).Sort(sort); //.First() wäre auch möglich

            Console.WriteLine(highestScores);



            //Updaten von Daten in Mongo(One field or more)
            filter = Builders<BsonDocument>.Filter.Eq("student_id", 10000);
            var update = Builders<BsonDocument>.Update.Set("class_id", 483);
            //filter= das item das gändert werden soll, update=die gänderte version des items
            collection.UpdateOne(filter, update);


            //Updaten eines Arrays
            var arrayFilter = Builders<BsonDocument>.Filter.Eq("student_id", 10000)
                                    & Builders<BsonDocument>.Filter.Eq("scores.type", "quiz");
            var arrayUpdate = Builders<BsonDocument>.Update.Set("scores.$.score", 84.92381029342834);
            collection.UpdateOne(arrayFilter, arrayUpdate);





            //deleten von One item
            var deleteFilter = Builders<BsonDocument>.Filter.Eq("student_id", 10000);
            collection.DeleteOne(deleteFilter);
            //delete von many items
            var deleteLowExamFilter = Builders<BsonDocument>.Filter.ElemMatch<BsonValue>("scores",
                                      new BsonDocument { { "type", "exam" }, {"score", new BsonDocument { { "$lt", 60 }}}
                                      });
            collection.DeleteMany(deleteLowExamFilter);
        }
    }
}
