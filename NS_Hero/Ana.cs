using System;
using System.Diagnostics;

using Overwatch.NS_Team;

namespace Overwatch.NS_Hero
{
    internal class Ana : Hero
    {
        private bool isZoom;
        private bool usedNano;

        public Ana(EHeroType heroType, string userName, int hp, int atk, int def, ETeamType teamType)
            : base(heroType, userName, hp, atk, def, teamType) { }

        public override void Skill1()
        {
            if (isZoom) Shot1();
            else Fight();
        }
        public override void Skill2()
        {
            if (isZoom) Shot1();
            else
            {
                Speak("쨍 푸쉬이익!![생체 수류탄]");
                FightManager.Instance.Attack(this, atk);
                FightManager.Instance.Heal(this, atk / 3);
            }
        }
        public override void Ultimate()
        {
            if (isZoom && !usedNano)
            {
                isZoom = false;
                Speak("줌을 끔");
            }

            if (!usedNano)
            {
                usedNano = true;
                Team ourTeam = FightManager.Instance.GetTeams()[teamType];
                Hero ourHero;

                if (ourTeam.heroes.Count == 1)
                {
                    Speak("왜 우리팀 다 죽었냐.. 셀프 나노다!!");
                    ourHero = this;
                }
                else
                {
                    int target;

                    while (true)
                    {
                        target = Rand.rand.Next(ourTeam.heroes.Count);
                        ourHero = ourTeam.heroes[target];
                        if (ourHero != this && !ourHero.IsDie) break;
                    }   
                    Speak($"{ourHero.heroType}, 넌 강해졌다. 돌격해!");
                }
                ourHero.atk = (int)(ourHero.atk * 1.5);
                ourHero.OnNaNo();
            }
            else
            {
                Fight();
            }
        }
        public override void Shot1()
        {
            string msg = isZoom ? "춉![기본 공격]" : "푸슉![기본 공격]";
            Speak(msg);

            FightManager.Instance.Heal(this, atk);
        }
        public override void Shot2()
        {
            isZoom = !isZoom;
            Speak(isZoom ? "줌을 킴" : "줌을 끔");
        }
        public override void OnHit() => Speak("힐러 좀 지켜줘 ㅡㅡ");
        public override void OnHeal() => Debug.Assert(false);
        public override void OnNaNo() => Speak($"|{"=".PadRight(5, '=')} 난 강해졌다. 돌격한다 {"=".PadRight(5, '=')}|");
        public override void OnDie() => Speak("으아아아아------");
        public override void Win() => Speak("이게 아-놔지");
    }
}
