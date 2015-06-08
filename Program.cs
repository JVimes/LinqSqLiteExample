using System;
using System.IO;
using System.Linq;
using LinqAndEfOnSqLite;

namespace LinqSqLiteExample
{
    class Program
    {
        static void Main()
        {
            using (var context = new ChinookContext())
            {
                Console.WriteLine("Opening " + context.Database.Connection.ConnectionString);

                // Connection string in App.config
                Output(GetArtists(context), "1 original.txt");

                // Add a new Artist record, "Anberlin". It also adds a record to the Album table because of the relationship.
                context.Artists.Add(new Artist
                {
                    Name = "Anberlin",
                    Album =
                    {
                        new Album {Title = "Cities"},
                        new Album {Title = "New Surrender"}
                    }
                });
                context.SaveChanges();
                Output(GetArtists(context), "2 add anberline.txt");

                // Modify an artist record. Rename "The Police" to "Police, The".
                var police = context.Artists.Single(a => a.Name == "The Police");
                police.Name = "Police, The";
                context.SaveChanges();
                Output(GetArtists(context), "3 rename the police.txt");

                // Delete and artist record, "Avril Lavigne"
                var avril = context.Artists.Single(a => a.Name == "Avril Lavigne");
                context.Artists.Remove(avril);
                context.SaveChanges();
                Output(GetArtists(context), "4 remove Avril.txt");
            }
        }

        // Outputs artists as human-readable text.
        private static void Output(IQueryable<Artist> artists, string outputFilePath)
        {
            Output(string.Join(Environment.NewLine, (from a in artists select a.Name).ToArray()), outputFilePath);
        }

        // Output for the user. This implementation outputs to file for easy diffing.
        private static void Output(string message, string outputFilePath)
        {
            Console.WriteLine("Writing to " + outputFilePath);
            File.WriteAllText(outputFilePath, message);
        }

        // Gets artist names from the database.
        private static IOrderedQueryable<Artist> GetArtists(ChinookContext context)
        {
            return (from a in context.Artists
                    orderby a.Name
                    select a);
        }
    }
}
