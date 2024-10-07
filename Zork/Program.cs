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
        private static readonly Room[,] rooms = 
        { 
            {new Room("Rocky Trail") , new Room("South of House"), new Room("Canyon View") }, 
            { new Room("Forest"), new Room("West of House"), new Room("Behind House")    }, 
            {new Room("Dense Woods"), new Room("North of House"), new Room("Clearing") }
        };
    
        private static (int x,int y) location;

        private static Room CurrentRoom { get { return rooms[location.x, location.y]; } }
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to zork");
            InitializeRoomDescriptions();
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
                        outputString = CurrentRoom.Description;
                        break;
                    case Commands.NORTH:
                    case Commands.SOUTH:
                    case Commands.WEST:
                    case Commands.EAST:
                        if (Move(command))
                        {
                           outputString=CurrentRoom.ToString()+"\n"+ CurrentRoom.Description;
                        }
                        else
                        {
                            outputString = "The way is Shut!";
                        }
                        break;
                    case Commands.UNKOWN:
                        outputString = "Unknown Command";
                        break;
                }
                
                Console.WriteLine(outputString);
                //Console.WriteLine();
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
        private static void InitializeRoomDescriptions()
        {
            var roomMap=new Dictionary<string, Room>();
            foreach(Room room in rooms)
            {
                roomMap[room.Name] = room;
            }
            roomMap["Rocky Trail"].Description = "You are on a rock-strwn trail.";
            roomMap["South of House"].Description = "You are facing the south side of a white house. there is no door here, and all the windows are barred.";
            roomMap["Canyon View"].Description = "You are at the top of ther Great Canyon on its south wall.";
            roomMap["Forest"].Description = "This is a forest, with tree in all directions around you";
            roomMap["West of House"].Description ="This is an open fiewl west of a white house, with a boarded front door." ;
            roomMap["Behind House"].Description = "you are behind the white house. In One corner of the house there is a small window which is slightly ajar.";
            roomMap["Dense Woods"].Description = "This is a dimly list forest, with large trees all around. to the east, there appears to be sunlight.";
            roomMap["North of House"].Description = "You are facing the north side of a white house. There is no door here all the windows are barred.";
            roomMap["Clearing"].Description = "You are in a clearing, with a forest surround you on the west and south.";

        }
    }
}
