using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zork
{
    internal class Program
    {
        private static readonly string[,] rooms = { { "Rocky Trail", "South of House", "Canyon View" }, { "Forest", "West of House", "Behind House" }, { "Dense WOods", "north of hosue", "Clearing" } };
    
        private static (int x,int y) location;

        private static string CurrentRoom { get { return rooms[location.x, location.y]; } }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to zork");
            //string inputString=Console.ReadLine();
            Commands command = Commands.UNKOWN;
            while (command!= Commands.QUIT)
            {
                Console.Write("> ");
                command = ToCommand(Console.ReadLine().Trim());
                string outputString=null;
                

                switch (command)
                {
                    case Commands.LOOK:
                        break;
                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.WEST:
                    case Commands.EAST:
                        if (!Move(command))
                        {
                            outputString = "The way is Shut!";
                        }
                        break;
                    case Commands.UNKOWN:
                        outputString = "Unknown Command";
                        break;
                }
                
                Console.WriteLine(outputString);
            }
        }

        private static bool Move(Commands command)
        {
            bool isValidMove = true;
           
            switch (command)
            {
                case Commands.NORTH when location.x< rooms.GetLength(0):
                    location.x++;
                    break;
                case Commands.SOUTH when location.x > 0:
                    location.x--;
                    break;
                case Commands.WEST when location.y >0:
                    location.y--;
                    break;
                case Commands.EAST when location.y < rooms.GetLength(1):
                    location.y++;
                    break;
                default:
                    isValidMove = false;
                    break;

            }

            return isValidMove;


        }
        private static Commands ToCommand(string commandString)
        {
            commandString=commandString.Trim();
            Commands command;

            if(Enum.TryParse<Commands>(commandString,true,out command))
            {
                return command;
            }
            else
            {
                return Commands.UNKOWN;
            }
           
        }

        private static bool IsDirection(Commands command)=> Directions.Contains(command);

        private static readonly HashSet<Commands> Directions = new HashSet<Commands>{Commands.NORTH,Commands. SOUTH,Commands.EAST,Commands.WEST};
    }
}
