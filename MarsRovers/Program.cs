using System;
using System.Collections.Generic;

namespace MarsRovers
{
    class Program
    {
        static ExplorationGrid grid;
        static void Main(string[] args)
        {
            Console.WriteLine(Messages.WELCOME_MESSAGE);
            grid = new ExplorationGrid();
            var gridEstablished = false;
            while (!gridEstablished)
            {
                try
                {
                    gridEstablished = EstablishGrid();
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(Messages.NORTHERN_BOUND_REQUIRED);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(Messages.POSITIVE_INTEGERS_ONLY);
                }
            }
        }

        static bool EstablishGrid()
        {
            Console.WriteLine(Messages.GRID_ESTABLISHMENT_INSTRUCTIONS);
            var input = Console.ReadLine();
            var coordinates = input.Split(" ");
            grid.EasternGridBound = Convert.ToInt32(coordinates[0]);
            grid.NorthernGridBound = Convert.ToInt32(coordinates[1]);
            if (grid.EasternGridBound < 1 || grid.NorthernGridBound < 1)
            {
                throw new Exception();
            }
            return true;
        }
    }
}
