using RobotWars.Enum;

namespace RobotWars.Interfaces
{
    public interface ICommand
    {
         Arena CommandArena { get; set; }

         Robot CommandRobot { get; set; }

        void Execute();
        //public bool IsValid();

      //  void Create( string commandName, CommandType commondType);


    }
}
