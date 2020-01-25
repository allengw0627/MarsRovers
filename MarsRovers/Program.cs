using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace MarsRovers
{
    class Program
    {
        static ExplorationGrid Grid;

        static Rover Rover;

        static bool GridEstablished;

        static bool CoordinatesAndHeadingConfirmed;

        static bool RoverHasMoved;

        static List<string> Headings = new List<string>() { Constants.NORTH, Constants.WEST, Constants.SOUTH, Constants.EAST };

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

            while (GridEstablished)
            {
                while (!CoordinatesAndHeadingConfirmed)
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
                while (!RoverHasMoved)
                {
                    try
                    {
                        MoveRover();
                    }
                    catch (IndexOutOfRangeException ex)
                    {
                        Console.WriteLine(ex.Message);
                        Console.ReadLine();
                        Environment.Exit(0);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }

        static void EstablishGrid()
        {
            Console.WriteLine(Constants.GRID_ESTABLISHMENT_INSTRUCTIONS);
            var stringArray = Console.ReadLine().Split(" ");
            var coordinates = RemoveWhitespaceChars(stringArray);
            ValidateInput(coordinates, Constants.GRID_ESTABLISHMENT_STEP);
            Grid.EasternGridBound = Convert.ToInt32(coordinates[0]);
            Grid.NorthernGridBound = Convert.ToInt32(coordinates[1]);
            GridEstablished = true;
        }

        static void ConfirmCoordinatesAndHeading()
        {
            Console.WriteLine(Constants.ROVER_CONFIRMATION_INSTRUCTIONS);
            var stringArray = Console.ReadLine().Split(" ");
            var coordinatesAndHeading = RemoveWhitespaceChars(stringArray);
            ValidateInput(coordinatesAndHeading, Constants.ROVER_COORDINATE_CONFIRMATION_STEP);
            Rover = new Rover();
            var eastCoordinate = Convert.ToInt32(coordinatesAndHeading[0]);
            var northCoordinate = Convert.ToInt32(coordinatesAndHeading[1]);
            if (eastCoordinate > Grid.EasternGridBound || northCoordinate > Grid.NorthernGridBound)
            {
                throw new Exception(Constants.COORDINATES_OUTSIDE_BOUNDS);
            }
            Rover.EastCoordinate = eastCoordinate;
            Rover.NorthCoordinate = northCoordinate;
            Rover.Heading = coordinatesAndHeading[2];
            CoordinatesAndHeadingConfirmed = true;
            RoverHasMoved = false;
        }

        static void MoveRover()
        {
            Console.WriteLine(Constants.ROVER_MOVE_INSTRUCTIONS);
            var charArray = Console.ReadLine().ToCharArray();
            var stringArray = charArray
                .ToList()
                .Select(c => c.ToString())
                .ToArray();
            var turnMoveInstructions = RemoveWhitespaceChars(stringArray);
            ValidateInput(turnMoveInstructions, Constants.ROVER_MOVE_STEP);
            var newHeadingIndex = new int();
            foreach (var instruction in turnMoveInstructions)
            {
                var currentHeadingIndex = Headings.IndexOf(Rover.Heading);
                switch (instruction)
                {
                    case Constants.LEFT:
                        currentHeadingIndex++;
                        newHeadingIndex = currentHeadingIndex > 3 ? 0 : currentHeadingIndex;
                        Rover.Heading = Headings[newHeadingIndex];
                        break;
                    case Constants.RIGHT:
                        currentHeadingIndex--;
                        newHeadingIndex = currentHeadingIndex < 0 ? 3 : currentHeadingIndex;
                        Rover.Heading = Headings[newHeadingIndex];
                        break;
                    case Constants.MOVE:
                        switch (Rover.Heading)
                        {
                            case Constants.NORTH:
                                Rover.NorthCoordinate++;
                                break;
                            case Constants.WEST:
                                Rover.EastCoordinate--;
                                break;
                            case Constants.SOUTH:
                                Rover.NorthCoordinate--;
                                break;
                            case Constants.EAST:
                                Rover.EastCoordinate++;
                                break;
                        }
                        if (Rover.NorthCoordinate > Grid.NorthernGridBound || 
                            Rover.NorthCoordinate < 0 ||
                            Rover.EastCoordinate > Grid.EasternGridBound ||
                            Rover.EastCoordinate < 0)
                        {
                            throw new IndexOutOfRangeException(Constants.ROVER_LOST_MESSAGE);
                        }
                        break;
                }
            }
            RoverHasMoved = true;
            CoordinatesAndHeadingConfirmed = false;
            Console.WriteLine($"Position updated to {Rover.EastCoordinate} {Rover.NorthCoordinate} {Rover.Heading}");
        }

        static void ValidateInput(List<string> inputArray, int step)
        {
            if (!inputArray.Any())
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
                    ValidateCoordinate(inputArray[0]);
                    ValidateCoordinate(inputArray[1]);
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

                    ValidateCoordinate(inputArray[0]);
                    ValidateCoordinate(inputArray[1]);
                    
                    var heading = inputArray[2];
                    if (!Headings.Contains(heading.ToString()))
                    {
                        throw new Exception(Constants.HEADING_INVALID);
                    }

                    break;
                case Constants.ROVER_MOVE_STEP:
                    foreach (var value in inputArray)
                    {
                        var valueString = value.ToString();
                        if (valueString != Constants.LEFT && valueString != Constants.RIGHT && valueString != Constants.MOVE)
                        {
                            throw new Exception(Constants.INSTRUCTION_INVALID);
                        }
                    }
                    break;
            }
        }

        static void ValidateCoordinate(string input)
        {
            var outInt = new int();
            if (int.TryParse(input, out outInt))
            {
                if (outInt < 1)
                {
                    throw new Exception(Constants.POSITIVE_INTEGERS_ONLY);
                }
            }
            else
            {
                throw new Exception(Constants.POSITIVE_INTEGERS_ONLY);
            }
        }

        static List<string> RemoveWhitespaceChars(string[] stringArray)
        {
            return stringArray
                .ToList()
                .Where(c => c != string.Empty)
                .ToList();
        }
    }
}
