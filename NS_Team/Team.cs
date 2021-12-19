using System;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;

using Overwatch.NS_Hero;

namespace Overwatch.NS_Team
{
    internal abstract class Team : ISpeakable
    {
        public List<Hero> heroes = new List<Hero>();
        public string name;
        public ETeamType teamType;
        public bool isDefeat;

        private readonly bool[] selectedHero = new bool[(int)EHeroType.HERO_NUM];

        public Team(string name, ETeamType teamType)
        {
            this.name = name;
            this.teamType = teamType;
            Speak("팀 생성 완료.");
            GameManager.Sleep(1000);
        }

        public void Speak(string msg) => Console.WriteLine($"[{name}] : {msg}");
        public abstract void SelectHero();
        protected EHeroType GetRandHero()
        {
            int rand;

            while (true)
            {
                rand = 1 + Rand.rand.Next((int)EHeroType.HERO_NUM - 1);
                if (!selectedHero[rand])
                {
                    selectedHero[rand] = true;
                    break;
                }
            }

            return (EHeroType)rand;
        }

        private Hero AddHero(EHeroType heroName, string userName)
        {
            ETeamType teamType;
            if (this is RedTeam) teamType = ETeamType.RED_TEAM;
            else if (this is BlueTeam) teamType = ETeamType.BLUE_TEAM;
            else teamType = ETeamType.NONE;

            switch (heroName)
            {
                case EHeroType.MCCREE:
                    return new Mccree(heroName, userName, hp: 150, atk: 150, def: 100, teamType);
                case EHeroType.REINHEART:
                    return new Reinheart(heroName, userName, hp: 150, atk: 200, def: 200, teamType);
                case EHeroType.ANA:
                    return new Ana(heroName, userName, hp: 150, atk: 120, def: 100, teamType);
                case EHeroType.WINSTON:
                    return new Winston(heroName, userName, hp: 150, atk: 120, def: 200, teamType);
                default:
                    Debug.Assert(false);
                    return null;
            }
        }

        protected void AddHero(string userName, EHeroType heroName)
        {
            heroes.Add(AddHero(heroName, userName));
            Speak($"{userName}(이)가 {heroName}를 선택하였습니다.");
        }

        public List<HeroDTO> GetTeam()
        {
            List<HeroDTO> team = new List<HeroDTO>();
            foreach(var hero in heroes)
            {
                team.Add(new HeroDTO(hero.heroType, hero.userName));
            }
            return team;
        }

        public void Fight()
        {
            Speak($"공격 턴!{"=".PadRight(10, '=')}>");
            GameManager.Sleep(1000);

            foreach (var hero in heroes)
            {
                if (hero.IsDie) continue;

                bool skipFight = Rand.rand.Next(100) < 10;

                if (skipFight)
                {
                    hero.Speak(Rand.rand.Next(2) == 1 ? "대기.." : "이동 중..");
                    GameManager.Sleep(500 + Rand.rand.Next(3) * 1000);
                    continue;
                }

                hero.Fight();
            }
            Console.WriteLine();
        }

        public void Win()
        {
            foreach(var hero in heroes)
            {
                hero.Win();
                GameManager.Sleep(500);
            }
        }

        public void ClearSelectedHero() => Array.Clear(selectedHero, 0, selectedHero.Length);
    }
}
