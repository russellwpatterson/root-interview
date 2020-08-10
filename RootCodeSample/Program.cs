using System;
using System.Collections.Generic;
using System.IO;
using RootCodeSample.Factories;
using RootCodeSample.Models;
using RootCodeSample.Services;

namespace RootCodeSample
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> dataLines = default;

            if (args.Length == 0 && Console.IsInputRedirected)
            {
                // File is coming via stdin
                dataLines = ReadPipedInput();
            }
            else if (args.Length == 1)
            {
                // Filename passed as argument
                dataLines = ReadFile(args[0]);
            }
            else
            {
                Console.WriteLine("Input file needs to be specified as part of the command or piped into the executable.");
                
                Console.WriteLine($"Try {System.AppDomain.CurrentDomain.FriendlyName} <filename> OR <filename> | {System.AppDomain.CurrentDomain.FriendlyName}");
                return;
            }

            try
            {
                var factory = new ParserFactory();
                var parsingService = new ParsingService(
                    factory.GetParser<Driver>(), 
                    factory.GetParser<Trip>()
                );

                var driverSummaries = parsingService.ParseDataFile(dataLines);
                foreach(var ds in driverSummaries)
                {
                    Console.WriteLine(ds);
                }                    
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

        private static List<string> ReadPipedInput()
        {
            var dataLines = new List<string>();
            while (Console.In.Peek() != -1)
            {
                dataLines.Add(Console.ReadLine());
            }

            return dataLines;
        }

        static List<string> ReadFile(string filename)
        {
            using var streamReader = new StreamReader(filename);
            
            var dataLines = new List<string>();
            while (streamReader.Peek() != -1)
            {
                dataLines.Add(streamReader.ReadLine());
            }

            return dataLines;
        }
    }
}
