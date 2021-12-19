using System;
using System.Collections.Generic;

using Overwatch.NS_Team;
using Overwatch.NS_Hero;
namespace Overwatch
{
    internal class FightManager : ISpeakable
    {
        public static FightManager Instance;
        private Dictionary<ETeamType, Team> teams;

        static FightManager()
        {
            Instance = new FightManager();
        }

        public void Speak(string msg)
        {
            Console.WriteLine($"<System> : {msg}");
            GameManager.Sleep(1500 + Rand.rand.Next(3) * 1000);
        }
        public Dictionary<ETeamType, Team> GetTeams() => teams;
        public void SetTeams(Dictionary<ETeamType, Team> teams)
        {
            this.teams = teams;
        }

        public void Fight()
        {
            ETeamType atkTeamType = (ETeamType)(1 + Rand.rand.Next(2));

            Speak($"\"{teams[atkTeamType].name}\" 팀의 선공!{"=".PadRight(10, '=')}>");
            teams[atkTeamType].Fight();

            while (true)
            {
                atkTeamType = atkTeamType == ETeamType.RED_TEAM ?
                    ETeamType.BLUE_TEAM :
                    ETeamType.RED_TEAM;

                if (teams[atkTeamType].isDefeat)
                {
                    break;
                }
                teams[atkTeamType].Fight();
            }

            Team winnerTeam = teams[ETeamType.RED_TEAM].isDefeat ?
                teams[ETeamType.BLUE_TEAM] :
                teams[ETeamType.RED_TEAM];

            GameManager.Instance.Win(winnerTeam);
        }
        public void Attack(Hero hero, int dmg)
        {
            Team enemyTeam = hero.teamType == ETeamType.RED_TEAM ?
                teams[ETeamType.BLUE_TEAM] :
                teams[ETeamType.RED_TEAM];

            int target;
            Hero enemy;

            if (enemyTeam.isDefeat)
            {
                Speak("더 이상 공격할 적이 없습니다.");
                return;
            }

            while (true)
            {
                target = Rand.rand.Next(enemyTeam.heroes.Count);
                enemy = enemyTeam.heroes[target];
                if (!enemy.IsDie) break;
            }

            if (enemy is Reinheart)
            {
                if (((Reinheart)enemy).isDefending)
                {
                    Speak("라인하르트의 방벽으로 인해 공격력이 절반이 됩니다.");
                    dmg /= 2;
                }
            }

            Speak($"{hero.userName}[{hero.heroType}](이)가 {enemy.userName}[{enemy.heroType}]에게 " +
                $"\"{dmg}\" 데미지를 입혔습니다.");

            enemy.hp -= dmg;
            if (enemy.hp < 0) enemy.hp = 0;

            Speak($"{enemy.userName}[{enemy.heroType}] 남은 체력 < {enemy.hp} >");
            if (enemy.hp <= 0)
            {
                enemy.IsDie = true;
                enemy.OnDie();
                Speak($"{hero.userName}[{hero.heroType}](킬) => {enemy.userName}[{enemy.heroType}]\n");
            }
            else
            {
                enemy.OnHit();
                Console.WriteLine();
            }
        }

        public void Heal(Hero hero, int heal)
        {
            Team ourTeam = hero.teamType == ETeamType.RED_TEAM ?
                teams[ETeamType.RED_TEAM] :
                teams[ETeamType.BLUE_TEAM];

            if (ourTeam.heroes.Count == 1)
            {
                Speak("힐을 줄 상대가 없으므로 공격합니다.");
                Attack(hero, dmg: heal);
                return;
            }

            Hero ourHero = null;

            for (int i = 0; i < ourTeam.heroes.Count; i++)
            {
                if (ourTeam.heroes[i] == hero) continue;
                if (ourTeam.heroes[i].IsDie) continue;

                bool giveHeal = Rand.rand.Next(2) == 1;
                if (giveHeal)
                {
                    ourHero = ourTeam.heroes[i];
                    break;
                }

                if (ourHero == null && i == ourTeam.heroes.Count - 1)
                {
                    ourHero = ourTeam.heroes[i];
                }
            }

            if (ourHero == null)
            {
                Speak("힐을 줄 상대가 없으므로 공격합니다.");
                Attack(hero, dmg: heal);
                return;
            }

            if (ourHero.hp == ourHero.maxHP)
            {
                Speak($"{ourHero.userName}[{ourHero.heroType}]는 이미 풀피입니다. 멍청한 {hero.userName}[{hero.heroType}]..");
                return;
            }
            else
            {
                Speak($"{hero.userName}[{hero.heroType}](이)가 {ourHero.userName}[{ourHero.heroType}]를 " +
                $"\"{heal}\" 만큼 치유했습니다.");
            }

            ourHero.hp += heal;
            if (ourHero.hp > ourHero.maxHP) ourHero.hp = ourHero.maxHP;

            Speak($"{ourHero.userName}[{ourHero.heroType}] 현재 체력 < {ourHero.hp} >");
            ourHero.OnHeal();
            Console.WriteLine();
        }

        public void CheckDefeat(ETeamType teamType)
        {
            foreach (var hero in teams[teamType].heroes)
                if (!hero.IsDie) return;

            teams[teamType].isDefeat = true;
        }
    }
}
