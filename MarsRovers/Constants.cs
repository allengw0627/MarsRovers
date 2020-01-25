using System;
using System.Collections.Generic;
using System.Text;

namespace MarsRovers
{
    class Constants
    {
        #region Messages
        public const string WELCOME_MESSAGE = "Welcome to NASA's Rover CLI!";
        public const string GRID_ESTABLISHMENT_INSTRUCTIONS = "Please enter your exploration grid bounds separated by a space.";
        public const string ROVER_CONFIRMATION_INSTRUCTIONS = "Please confirm the coordinates and heading of the rover, separated by spaces.";
        public const string COORDINATES_OUTSIDE_BOUNDS = "Cannot establish coordinates outside the grid bounds.";
        public const string ROVER_MOVE_INSTRUCTIONS = "Please enter your turn/move instructions.";
        public const string INPUT_REQUIRED = "Sorry, input required.";
        public const string NORTHERN_BOUND_REQUIRED = "Sorry, a northern grid bound is required.";
        public const string POSITIVE_INTEGERS_ONLY = "Sorry, both coordinates must be positive integers.";
        public const string NORTH_COORDINATE_REQUIRED = "Sorry, a north coordinate is required.";
        public const string HEADING_REQUIRED = "Sorry, a heading is required.";
        public const string HEADING_INVALID = "Sorry, heading is invalid.  Acceptable headings are N, S, E, and W.";
        public const string INSTRUCTION_INVALID = "Sorry, instructions contain invalid character(s).  Acceptable instructions are L, R, and M.";
        public const string ROVER_LOST_MESSAGE = "Rover has been lost!  Current coordinates are outside the grid bounds.";
        #endregion

        #region Headings
        public const string NORTH = "N";
        public const string SOUTH = "S";
        public const string EAST = "E";
        public const string WEST = "W";
        #endregion

        #region Instructions
        public const string LEFT = "L";
        public const string RIGHT = "R";
        public const string MOVE = "M";
        #endregion

        #region Steps
        public const int GRID_ESTABLISHMENT_STEP = 1;
        public const int ROVER_COORDINATE_CONFIRMATION_STEP = 2;
        public const int ROVER_MOVE_STEP = 3;
        #endregion
    }
}
