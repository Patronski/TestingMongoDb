using System;
using System.Diagnostics;
using System.Linq;
using System.Timers;
using TestingMongoDb.models;

namespace TestingMongoDb
{
    class Program
    {
        static void Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            stopwatch.Start();
            TestOneConnection();
            var milliseconds = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"test one connection finished: {milliseconds} ms");
            Console.WriteLine();

            stopwatch.Restart();
            TestManyConnections();
            milliseconds = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"test many connections finished: {milliseconds} ms");
            Console.WriteLine();

            stopwatch.Restart();
            TestOneConnection();
            milliseconds = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"test one connection finished: {milliseconds} ms");
            Console.WriteLine();

            stopwatch.Restart();
            TestManyConnections();
            milliseconds = stopwatch.ElapsedMilliseconds;
            Console.WriteLine($"test many connections finished: {milliseconds} ms");
            Console.WriteLine();
        }

        private static void TestOneConnection()
        {
            var collectionName = "retail-returns";
            var dbName = "returns-superdry";
            MongoCRUD db = new MongoCRUD(dbName);

            var ids = db.GetIds<Return>(collectionName).Take(2);
            long count = 0;
            foreach (var id in ids)
            {
                var record = db.LoadRecordById<Return>(collectionName, id);
                if (record.ReturnReasonId != null)
                {
                    count += (int)record.ReturnReasonId;
                }
            }
            Console.WriteLine($"total documents: {ids.Count()}; ReturnReasonId: {count}");
        }

        private static void TestManyConnections()
        {
            var collectionName = "retail-returns";
            var dbName = "returns-superdry";
            MongoCRUD db = new MongoCRUD(dbName);

            var ids = db.GetIds<Return>(collectionName).Take(2);
            long count = 0;
            foreach (var id in ids)
            {
                var record = db.LoadRecordById<Return>(collectionName, id);
                if (record.ReturnReasonId != null)
                {
                    count += (int)record.ReturnReasonId;
                }
                db.ReopenConnection(dbName);
            }
            Console.WriteLine($"total queries: {ids.Count()}; ReturnReasonId: {count}");
        }

        private static void CrudTest()
        {
            var collectionName = "Users";
            var personModel = new PersonModel
            {
                FirstName = "Iliana",
                LastName = "Patronska",
                PrimaryAddress = new AddressModel
                {
                    City = "Sofia",
                    State = "Sofia",
                    StreetAddress = "Ralevica 1",
                    ZipCode = "123"
                }
            };

            MongoCRUD db = new MongoCRUD("AddresBook");

            db.InsertRecord(collectionName, personModel);

            var records = db.LoadRecords<PersonModel>(collectionName);

            foreach (var r in records)
            {
                Console.WriteLine($"{r.Id} : {r.FirstName} {r.LastName}");

                if (r.PrimaryAddress != null)
                {
                    Console.WriteLine(r.PrimaryAddress.City);
                }
                Console.WriteLine();
            }

            var rec = db.LoadRecordById<PersonModel>(collectionName, "7cece97d-5ac3-4dc0-b773-97de91d320ad");
            rec.DateOfBirth = new DateTime(1988, 08, 29, 0, 0, 0, DateTimeKind.Utc);

            db.UpsertRecord(collectionName, rec.Id.ToString(), rec);
            Console.WriteLine(rec.Id);
        }
    }
}
