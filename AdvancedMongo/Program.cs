using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;

namespace AdvancedMongo
{
    class Program
    {
        static void Main(string[] args)
        {
            MainAsync().Wait();

            Console.WriteLine("Press enter to exit");
            Console.ReadLine();
        }

        static async Task MainAsync()
        {
            var connectionString = "mongodb://localhost:27017";
            var client = new MongoClient(connectionString);

            IMongoDatabase database = client.GetDatabase("school");
            //IMongoCollection<BsonDocument> collection = database.GetCollection<BsonDocument>("schools");

            //Insert document
            //var students = CreateNewStudents();

            //await collection.InsertManyAsync(students);

            //Find Document

            //FilterDefinition<BsonDocument> filterDefinition = FilterDefinition<BsonDocument>.Empty;
            //FindOptions<BsonDocument> findOptions = new FindOptions<BsonDocument>
            //{
            //    BatchSize = 2,
            //    NoCursorTimeout = false
            //};

            //using IAsyncCursor<BsonDocument> cursor = await collection.FindAsync(filterDefinition, findOptions);
            //var batch = 0;
            //while (await cursor.MoveNextAsync())
            //{
            //    IEnumerable<BsonDocument> doccuments = cursor.Current;
            //    batch++;
            //    Console.WriteLine($"Batch: {batch}");

            //    foreach (var bsonDocument in doccuments)
            //    {
            //        Console.WriteLine(bsonDocument);
            //        Console.WriteLine();
            //    }
            //}
            //Console.WriteLine($"Total Batch: { batch}");

            //FilterDefinition

            //var builder = Builders<BsonDocument>.Filter;
            //var filter = builder.Lt("age", 40) & builder.Eq("firstname", "Julie");
            ////var result = collection.Find(filter).ToList();
            //var cursor = await collection.FindAsync(filter);
            //var results = cursor.ToListAsync().Result;

            //foreach (var result in results)
            //{
            //    Console.WriteLine(result);
            //}

            //By Linq
            IMongoCollection<Student> collection = database.GetCollection<Student>("schools");

            //await collection.Find(x => x.age < 25 && x.firstname == "Ugo")
            //    .ForEachAsync(s => Console.WriteLine($"Id: {s._id}, FirstName: {s.firstname}"));


            //int count = 1;
            //await collection.Find(x => x.age < 40)
            //    .Skip(1)
            //    .ForEachAsync(
            //        student =>
            //        {
            //            Console.WriteLine($"S/N: {count} \t Id: {student._id}, FirstName: {student.firstname}, " +
            //                              $"LastName: {student.lastname}");
            //            count++;
            //        });

            //pagination

            //int currentPage = 1 ,pageSize = 2;
            //double totalDocuments = await collection.CountDocumentsAsync(FilterDefinition<Student>.Empty);
            //var totalPages = Math.Ceiling(totalDocuments / pageSize);

            //for (int i = 1; i <= totalPages; i++)
            //{
            //    Console.WriteLine($"Page: {currentPage}");
            //    Console.WriteLine();

            //    int count = 1;

            //    await collection.Find(FilterDefinition<Student>.Empty)
            //        .Skip((currentPage -1) * pageSize)
            //        .Limit(pageSize)
            //        .Sort(Builders<Student>.Sort.Descending(x => x.lastname)
            //            .Ascending(x=>x.firstname))
            //        .ForEachAsync(
            //            student =>
            //            {
            //                Console.WriteLine($"S/N: {count}, \t Id: {student._id}, FirstName: {student.firstname}, " +
            //                                  $"LastName: {student.lastname}");
            //                count++;
            //            });

            //    //with projection
            //    //await collection.Find(FilterDefinition<Student>.Empty)
            //    //    .Skip((currentPage - 1) * pageSize)
            //    //    .Limit(pageSize)
            //    //    .Sort(Builders<Student>.Sort.Descending(x => x.lastname)
            //    //        .Ascending(x => x.firstname))
            //    //    .Project(x => new { x.firstname, x.lastname })
            //    //    .ForEachAsync(
            //    //        student =>
            //    //        {
            //    //            Console.WriteLine($"S/N: {count}, \t FirstName: {student.firstname}, " +
            //    //                              $"LastName: {student.lastname}");
            //    //            count++;
            //    //        });

            //    Console.WriteLine();
            //    currentPage++;
            //}


            //update
            var updateBuilder = Builders<Student>.Update;
            var builder = Builders<Student>.Filter;
            var updateOptions = new UpdateOptions() {IsUpsert = false};

            //var filter = builder.Lt("age", 40) & builder.Eq("firstname", "Julie");
            var filter = builder.Eq("firstname", "Mohammad");

            //var update = updateBuilder
            //    .Set(x => x.firstname, "Mohammad")
            //    .Set(x => x.lastname, "Fahad");

            var push = updateBuilder.Push(x => x.subjects, "ICT");

            await collection.UpdateManyAsync(filter, push,updateOptions);

            //whole-sale update

            //var user = new User()
            //{
            //    Id = 10,
            //    Name = "Hamed",
            //    Email = "hamed@example.com",
            //};
            //var filter = Builders<User>.Filter.Eq(u => u.Id, 10);
            //collection.ReplaceOne(filter, user);

            //Remove

            //collection.DeleteMany(filter);
            //collection.DeleteOne(filter);
        }

        private static IEnumerable<BsonDocument> CreateNewStudents()
        {
            var student1 = new BsonDocument
            {
                {"firstname", "Ugo"},
                {"lastname", "Damian"},
                {"subjects", new BsonArray {"English", "Mathematics", "Physics", "Biology"}},
                {"class", "JSS 3"},
                {"age", 23}
            };

            var student2 = new BsonDocument
            {
                {"firstname", "Julie"},
                {"lastname", "Lerman"},
                {"subjects", new BsonArray {"English", "Mathematics", "Spanish"}},
                {"class", "JSS 3"},
                {"age", 23}
            };

            var student3 = new BsonDocument
            {
                {"firstname", "Julie"},
                {"lastname", "Lerman"},
                {"subjects", new BsonArray {"English", "Mathematics", "Physics", "Chemistry"}},
                {"class", "JSS 1"},
                {"age", 25}
            };

            var newStudents = new List<BsonDocument>();
            newStudents.Add(student1);
            newStudents.Add(student2);
            newStudents.Add(student3);

            return newStudents;
        }
    }
}
