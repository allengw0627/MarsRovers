using System;
using System.Collections.Generic;
using System.Linq;

namespace MarsRovers
{
    class Program
    {
        static ExplorationGrid Grid;
        static bool GridEstablished;
        static List<string> Headings = new List<string>() { Constants.NORTH, Constants.EAST, Constants.SOUTH, Constants.WEST };
        static void Main(string[] args)
        {
            Console.WriteLine(Constants.WELCOME_MESSAGE);
            Grid = new ExplorationGrid();
            while (!GridEstablished)
            {
                try
                {
                    EstablishGrid();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            while(1 < 2)
            {
                try
                {
                    ConfirmCoordinatesAndHeading();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        static bool ValidInput(string[] input, int step)
        {
            var inputValid = false;
            var inputAsInt32 = new int();
            var inputArray = input.ToList();
            if (inputArray[0] == string.Empty && inputArray.Count() == 1)
            {
                throw new Exception(Constants.INPUT_REQUIRED);
            }
            switch (step)
            {
                case Constants.GRID_ESTABLISHMENT_STEP:
                    if (inputArray.Count() < 2)
                    {
                        throw new Exception(Constants.NORTHERN_BOUND_REQUIRED);
                    }
                    foreach (var value in inputArray)
                    {
                        if (int.TryParse(value, out inputAsInt32))
                        {
                            if (inputAsInt32 < 1)
                            {
                                throw new Exception(Constants.POSITIVE_INTEGERS_ONLY);
                            }
                        }
                        else
                        {
                            throw new Exception(Constants.POSITIVE_INTEGERS_ONLY);
                        }
                    }
                    break;
                case Constants.ROVER_COORDINATE_CONFIRMATION_STEP:
                    if (inputArray.Count() < 2)
                    {
                        throw new Exception(Constants.NORTH_COORDINATE_REQUIRED);
                    }
                    if (inputArray.Count() < 3)
                    {
                        throw new Exception(Constants.HEADING_REQUIRED);
                    }

                    int.TryParse(inputArray[0], out inputAsInt32);
                    if (inputAsInt32 < 1)
                    {
                        throw new Exception(Constants.POSITIVE_INTEGERS_ONLY);
                    }

                    int.TryParse(inputArray[1], out inputAsInt32);
                    if (inputAsInt32 < 1)
                    {
                        throw new Exception(Constants.POSITIVE_INTEGERS_ONLY);
                    }

                    var heading = inputArray[2];
                    if (!Headings.Contains(heading))
                    {
                        throw new Exception(Constants.HEADING_INVALID);
                    }

                    break;
            }
            return inputValid;
        }

        static void EstablishGrid()
        {
            Console.WriteLine(Constants.GRID_ESTABLISHMENT_INSTRUCTIONS);
            var coordinates = Console.ReadLine()
                .Split(" ");
            var validInput = ValidInput(coordinates, Constants.GRID_ESTABLISHMENT_STEP);
            if (validInput)
            {
                Grid.EasternGridBound = Convert.ToInt32(coordinates[0]);
                Grid.NorthernGridBound = Convert.ToInt32(coordinates[1]);
            }
            GridEstablished = true;
        }

        static void ConfirmCoordinatesAndHeading()
        {
            Console.WriteLine(Constants.ROVER_CONFIRMATION_INSTRUCTIONS);
            var coordinatesAndHeading = Console.ReadLine()
                .Split(" ");
            var validInput = ValidInput(coordinatesAndHeading, Constants.ROVER_COORDINATE_CONFIRMATION_STEP);
            if (validInput)
            {
                var rover = new Rover();
                rover.EastCoordinate = Convert.ToInt32(coordinatesAndHeading[0]);
                rover.NorthCoordinate = Convert.ToInt32(coordinatesAndHeading[1]);
                rover.Heading = coordinatesAndHeading[2];
            }
        }
    }
}
