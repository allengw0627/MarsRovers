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

        static List<char> Headings = new List<char>() { Constants.NORTH, Constants.WEST, Constants.SOUTH, Constants.EAST };

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
            var coordinates = Console.ReadLine()
                .ToCharArray();
            ValidateInput(coordinates, Constants.GRID_ESTABLISHMENT_STEP);
            Grid.EasternGridBound = Convert.ToInt32(coordinates[0]);
            Grid.NorthernGridBound = Convert.ToInt32(coordinates[1]);
            GridEstablished = true;
        }

        static void ConfirmCoordinatesAndHeading()
        {
            Console.WriteLine(Constants.ROVER_CONFIRMATION_INSTRUCTIONS);
            var coordinatesAndHeading = Console.ReadLine()
                .ToCharArray();
            ValidateInput(coordinatesAndHeading, Constants.ROVER_COORDINATE_CONFIRMATION_STEP);
            Rover = new Rover();
            Rover.EastCoordinate = Convert.ToInt32(coordinatesAndHeading[0]);
            Rover.NorthCoordinate = Convert.ToInt32(coordinatesAndHeading[1]);
            Rover.Heading = coordinatesAndHeading[2];
            CoordinatesAndHeadingConfirmed = true;
        }

        static void MoveRover()
        {
            Console.WriteLine(Constants.ROVER_MOVE_INSTRUCTIONS);
            var turnMoveInstructions = Console.ReadLine()
                .ToCharArray();
            ValidateInput(turnMoveInstructions, Constants.ROVER_MOVE_STEP);
            foreach (var instruction in turnMoveInstructions)
            {
                var currentHeadingIndex = Headings.IndexOf(Rover.Heading);
                var newHeadingIndex = new int();
                switch (instruction)
                {
                    case Constants.LEFT:
                        newHeadingIndex = currentHeadingIndex++;
                        newHeadingIndex = newHeadingIndex > 3 ? 0 : newHeadingIndex;
                        Rover.Heading = Headings[newHeadingIndex];
                        break;
                    case Constants.RIGHT:
                        newHeadingIndex = currentHeadingIndex--;
                        newHeadingIndex = newHeadingIndex < 0 ? 3 : newHeadingIndex;
                        Rover.Heading = Headings[newHeadingIndex];
                        break;
                    case Constants.MOVE:
                        switch (Rover.Heading)
                        {
                            case Constants.NORTH:
                                Rover.NorthCoordinate = Rover.NorthCoordinate++;
                                break;
                            case Constants.WEST:
                                Rover.EastCoordinate = Rover.EastCoordinate--;
                                break;
                            case Constants.SOUTH:
                                Rover.NorthCoordinate = Rover.NorthCoordinate--;
                                break;
                            case Constants.EAST:
                                Rover.EastCoordinate = Rover.EastCoordinate++;
                                break;
                        }
                        break;
                }
            }
            RoverHasMoved = true;
            CoordinatesAndHeadingConfirmed = false;
            Console.WriteLine($"{Rover.EastCoordinate} {Rover.NorthCoordinate} {Rover.Heading}");
        }

        static void ValidateInput(char[] input, int step)
        {
            var inputArray = input.ToList();
            if (char.IsWhiteSpace(inputArray[0]) && inputArray.Count() == 1)
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
                    if (!Headings.Contains(heading))
                    {
                        throw new Exception(Constants.HEADING_INVALID);
                    }

                    break;
                case Constants.ROVER_MOVE_STEP:
                    foreach (var value in inputArray)
                    {
                        if (value != Constants.LEFT && value != Constants.RIGHT && value != Constants.MOVE)
                        {
                            throw new Exception(Constants.INSTRUCTION_INVALID);
                        }
                    }
                    break;
            }
        }

        static void ValidateCoordinate(char number)
        {
            if (!char.IsNumber(number) || number < 1)
            {
                throw new Exception(Constants.POSITIVE_INTEGERS_ONLY);
            }
        }
    }
}
