//#define USE_ANIMATION

using System;
using System.Threading;
using System.Collections.Generic;

using Overwatch.NS_Team;
using Overwatch.NS_Hero;

namespace Overwatch
{
    internal class GameManager : ISpeakable
    {
        public static GameManager Instance;
        private readonly Dictionary<ETeamType, Team> teams = new Dictionary<ETeamType, Team>();

        static GameManager()
        {
            Instance = new GameManager();
        }

        public static void Sleep(int millisecondsTimeout)
        {
#if USE_ANIMATION
            Thread.Sleep(millisecondsTimeout);
#endif
        }
        public void Speak(string msg) => Console.WriteLine($"<System> : {msg}");

        public void StartGame()
        {
            Speak("게임을 시작합니다!");
            Console.WriteLine();
            Sleep(3000);
        }

        public void SelectHero()
        {
            Speak("영웅을 선택해주세요");
            Console.WriteLine();
            Sleep(1000);

            foreach (var team in teams)
            {
                team.Value.SelectHero();
                Console.WriteLine();
            }
        }
        public void AddTeam(Team team)
        {
            teams.Add(team.teamType, team);
            Speak($"\"{team.name}\" 팀을 추가했습니다.");
            Sleep(2000);
        }

        public void Fight()
        {
            FightManager.Instance.SetTeams(teams);
            FightManager.Instance.Fight();
        }

        public void Win(Team team)
        {
            Speak($"{team.name} 팀 승리!!");
            Sleep(2000);

            team.Win();
            Console.WriteLine();

            List<HeroDTO> winner = team.GetTeam();
            foreach (var hero in winner)
            {
                Speak($"승자 {hero.userName}[{hero.heroType}]");
                Sleep(500);
            }
            Console.WriteLine();
        }

        public void EndGame()
        {
            foreach (var team in teams)
            {
                team.Value.ClearSelectedHero();
            }
            Speak("게임을 종료합니다!");
        }
    }
}
