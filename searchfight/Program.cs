using searchfight.facade;
using SearchFight.Utilities;
using SearchFight.Service;
using System;
using System.Collections.Generic;

namespace SearchFight
{
    class Program
    {
        static void Main(string[] args)
        {
            bool value = false;
            Console.WriteLine("Welcome to Search Fight Program:");
            while (!value)
            {
                if (args.Length>0)
                {
                    if (Validation.IsEmptyTextArray(args))
                    break;
                }
                else
                {
                    Console.WriteLine("Menu: ");
                    Console.WriteLine("1. Please enter a query to search");
                    Console.WriteLine("2. Press 2 and Enter to Exit");
                    args = Validation.GetArguments(Console.ReadLine());
                }
            }

            if (args[0].Trim() != "2")
            {
                Console.Clear();
                string [] listArgs = Validation.GetArgumentsNotEmpty(args);
                Console.WriteLine("Loading results...");
                startSearchInit(listArgs);
            }
            Console.ReadLine();
        }

        private static void startSearchInit(IEnumerable<string> args) {
            ISearchFightFacade facadeSearchFight = new SearchFightFacade();
            var results = facadeSearchFight.InitSearch(args);
            if (facadeSearchFight._messageResponse == null)
            {
                Console.Clear();
                var totalResults = facadeSearchFight.GetProcessWinners(results);
                var winnerTotal  = facadeSearchFight.GetProcessTotalWinner(results);
                facadeSearchFight.PrintResults(results,totalResults,winnerTotal);
            }
            else {
                Console.WriteLine(facadeSearchFight._messageResponse);
            }
            Console.ReadLine();
        }

        
    }
}
