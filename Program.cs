using System;
using Overwatch.NS_Team;

namespace Overwatch
{
    public class Program
    {
        public static void Main()
        {
            GameManager.Instance.StartGame();
            
            Team redTeam = new RedTeam(name: "루나틱 안녕", ETeamType.RED_TEAM);
            Team blueTeam = new BlueTeam(name: "세계 최강", ETeamType.BLUE_TEAM);
            Console.WriteLine();
            
            GameManager.Instance.AddTeam(redTeam);
            GameManager.Instance.AddTeam(blueTeam);
            Console.WriteLine();
            
            GameManager.Instance.SelectHero();
            
            GameManager.Instance.Fight();
            
            GameManager.Instance.EndGame();

            Console.ReadLine();
        }
    }
}