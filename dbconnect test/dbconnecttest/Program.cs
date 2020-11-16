using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text.Json;
using ClassLibrary1;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace dbconnecttest
{
    // Entity Framework Core
    // database-first approach steps...
    /*
     * 1. have a data access library project separate from the startup application project.
     *    (with a project reference from the latter to the former).
     *    the library needs to target at least .NET Standard 2.1 or .NET Core 3.1.
     * 2. install Microsoft.EntityFrameworkCore.Design and Microsoft.EntityFrameworkCore.SqlServer
     *    to both projects.
     * 3. using Git Bash / terminal, from the data access project folder run (split into several lines for clarity):
     *    dotnet ef dbcontext scaffold <connection-string-in-quotes>
     *      Microsoft.EntityFrameworkCore.SqlServer
     *      --startup-project <path-to-startup-project-folder>
     *      --force
     *      --no-onconfiguring
     *    https://docs.microsoft.com/en-us/ef/core/miscellaneous/cli/dotnet#dotnet-ef-dbcontext-scaffold
     *    (if you don't have dotnet ef installed, run: "dotnet tool install --global dotnet-ef")
     *    (this will fail if your projects do not compile)
     * 4. any time you change the structure of the tables (DDL), go to step 3.
     */

    class Program
    {
        static DbContextOptions<ChinookContext> s_dbContextOptions;

        static void Main(string[] args)
        {
            //LinqStuff();

            using var logStream = new StreamWriter("ef-logs.txt");
            // DbContextOptions is how we give the context its connection string (to log in to the sql server),
            // tell it to use SQL Server (and not MySQL etc), and any other EF-side options.
            var optionsBuilder = new DbContextOptionsBuilder<ChinookContext>();
            optionsBuilder.UseSqlServer(GetConnectionString());
            optionsBuilder.LogTo(logStream.Write, LogLevel.Debug);
            //optionsBuilder.LogTo(Console.WriteLine, LogLevel.Error);
            s_dbContextOptions = optionsBuilder.Options;

            Display5Tracks();
            Console.WriteLine();

            // implement these 3 methods, changing what they're doing if you want
            // bonus: user input instead of hardcoded stuff...
            // bonus: involve multiple tables besides just track.

            //EditOneOfThoseTracks();

           // Display5Tracks();
            Console.WriteLine();

            InsertANewTrack();

           // Display5Tracks();
            Console.WriteLine();

            DeleteThatTrack();

            Display5Tracks();
            Console.WriteLine();
        }

        static string GetConnectionString()
        {
            string path = "C:/Users/mgm21/Desktop/revature/mattm-code/dbconnect test/connectionstring.json";
            string json;
            try
            {
                json = File.ReadAllText(path);
                Console.WriteLine("file read");
            }

            catch (IOException)
            {
                Console.WriteLine($"required file {path} not found. should just be the connection string in quotes.");
                throw;
            }
            string connectionString = JsonSerializer.Deserialize<string>(json);
            return connectionString;
        }

        static void Display5Tracks()
        {
            using var context = new ChinookContext(s_dbContextOptions);

            IQueryable<Track> tracks = context.Tracks
                .Include(t => t.Genre)
                .OrderBy(t => t.Name)
                .Take(5);

            // at this point, the query has not yet even been sent, let alone the results downloaded.
            // (because LINQ uses deferred execution)

            foreach (var track in tracks)
            {
                Console.WriteLine($"{track.TrackId} - {track.Name} ({track.Genre.Name})");
            }

            //List<string> info = context.Tracks
            //    .Include(t => t.Genre)
            //    .OrderBy(t => t.Name)
            //    .Where(track => SomeComplexMethod(track)) // this can't become sql, so, EF will fetch every row and then discard them
            //    .Take(5)
            //    .ToList();
        }

        static void EditOneOfThoseTracks()
        {
            using var context = new ChinookContext(s_dbContextOptions);
            {
                IQueryable<Track> tracks = context.Tracks
                 .Include(t => t.Genre)
                 .OrderBy(t => t.Name)
                 .Take(5);
                if (tracks != null)
                {
                    tracks.First().Name = "'000001'";
                    context.SaveChanges();
                }
            }
   
        }

        static void InsertANewTrack()
        {
            using var context = new ChinookContext(s_dbContextOptions);
            var newTrack = new Track { Name = "testTrackName", MediaTypeId = 4, Milliseconds = 1000000, UnitPrice = 1 };
            context.Add<Track>(newTrack);
            context.SaveChanges();
        }

        static void DeleteThatTrack()
        {
            using var context = new ChinookContext(s_dbContextOptions);
            context.Remove(context.Tracks.Single(x => x.Name == "testtrackname"));
            context.SaveChanges();
        }

        static void LinqStuff()
        {
            // the best/most useful application of lambda expression / delegate types etc
            // is a part of the base class library called LINQ - stands for Language Integrated Query
            // - there's two ways to write it, one is weird and looks like SQL, called "query syntax"
            // - the other is called method syntax

            int[] scores = new int[] { 97, 92, 81, 60 };

            // query syntax
            IEnumerable<int> scoreQuery =
                from score in scores
                where score > 80
                select score;

            // method syntax
            IEnumerable<int> scoreQuery2 = scores.Where(s => s > 80).ToList();
            //                                        ^                 ^
            //                    wouldn't run "yet" except...       this part makes it run right now.

            scores[0] = 50;
            // scoreQuery if we look at it now, would not have the 97.
            // but scoreQuery2 would, because we called ToList to run the query then.

            // calculate the average length of the strings in a list
            var list = new List<string> { "abc", "abcdefg" };
            double average = list.Average(s => s.Length); // 5

            // LINQ is just a big pile of overloaded extension methods defined for the IEnumerable<T> interface

            // three types of LINQ methods
            // 1. the ones that return a new IEnumerable collection (they never modify the original collection)
            //    they don't execute "yet" - they use deferred execution.
            // 2. the ones that return any concrete value - like Average, First- do not use deferred execution
            // 3. things like ToArray, ToList. these return collections that need to be "all there"
            //     so they also don't use deferred execution.
            //    "ToList" lets you effectively force execution of type-1 methods whenever you want.

            // - Select: maps each element to something new
            var firstCharacters = list.Select(x => x[0]); // ['a', 'a']
            // - Where: filters the collection according to some condition

            // - Distinct: filters out duplicates
            var distinctFirstCharacters = firstCharacters.Distinct(); // ['a']
            // - Skip: skips n elements
            // - Take: skips AFTER n elements

            // - Count: counts how many items (match a condition)
            int howManyDistinctFirstChars = distinctFirstCharacters.Count(); // 1
            // - First: returns the first item (matching a condition)
            string firstStringLongerThanFive = list.First(x => x.Length > 5); // "abcdefg"
            // there's also FirstOrDefault, which returns null (or whatever struct default, e.g. 0)
            //    if there are no matches, unlike First, which throws an exception.

            // there's two versions of every LINQ extension method.
            // IEnumerable, and IQueryable.
            // IEnumerable runs in the CLR, with .NET objects.
            //     "LINQ to Objects"
            // IQueryable can be converted to some very different way to get the data, like SQL query.
            //     "LINQ to SQL", "LINQ to XML"
        }
    }
}
