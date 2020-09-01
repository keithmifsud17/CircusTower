using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NDesk.Options;

namespace CircusTower
{
    internal class Program
    {
        private static readonly OptionSet parameterStore = new OptionSet() {
            { "f|file=", key => { if (!string.IsNullOrEmpty(key)) file = key; } },
            { "w|weightFirst", key => { weightFirst = key != null; } },
            { "h|?|help",  key => { showHelp = key != null; } },
        };

        private static string file = default;
        private static bool showHelp = default;
        private static bool weightFirst = default;

        private static async Task Main(string[] args)
        {
            parameterStore.Parse(args);

            if (showHelp)
            {
                Console.WriteLine("-f | --file: File containing the people's data forming our circus tower");
                Console.WriteLine("-w | --weightFirst: Expects the input to be a {weight},{height}");
            }
            else if (!string.IsNullOrEmpty(file))
            {
                if (File.Exists(file))
                {
                    var people = new List<Person>();

                    using (var fs = new FileStream(file, FileMode.Open, FileAccess.Read, FileShare.Read))
                    using (var sr = new StreamReader(fs))
                        while (!sr.EndOfStream)
                        {
                            var input = (await sr.ReadLineAsync()).Split(',', StringSplitOptions.RemoveEmptyEntries);
                            if (2.Equals(input.Length) && int.TryParse(input[0].Trim(), out var value1) && int.TryParse(input[1].Trim(), out var value2))
                            {
                                if (weightFirst) people.Add(new Person(value1, value2));
                                else people.Add(new Person(value2, value1));
                            }
                        }

                    var tower = new CircusTowerFactory(people);
                    var result = tower.BuildCircusTower();

                    foreach (var person in result)
                    {
                        Console.WriteLine(weightFirst ? $"{person.Weight},{person.Height}" : $"{person.Height},{person.Weight}");
                    }
                }
                else
                {
                    Console.WriteLine("Provided file does not exist.");
                }
            }
            else
            {
                Console.WriteLine("Try running \"CircusTower --help\"");
            }
        }
    }
}