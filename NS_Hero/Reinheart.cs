using Overwatch.NS_Team;
namespace Overwatch.NS_Hero
{
    internal class Reinheart : Hero
    {
        public bool isDefending;

        public Reinheart(EHeroType heroType, string userName, int hp, int atk, int def, ETeamType teamType)
            : base(heroType, userName, hp, atk, def, teamType) { }

        public override void Skill1()
        {
            if (isDefending)
            {
                Speak("방벽 닫기");
                isDefending = false;
            }

            Speak("으아아~메가메~~하!!!(돌진)");
            FightManager.Instance.Attack(this, atk);
        }
        public override void Skill2()
        {
            if (isDefending)
            {
                Speak("방벽 닫기");
                isDefending = false;
            }

            Speak("화염 강타!");
            FightManager.Instance.Attack(this, atk);
        }
        public override void Ultimate()
        {
            if (isDefending)
            {
                Speak("방벽 닫기");
                isDefending = false;
            }

            Speak("망치~~~!! 나가신다!!!");
            FightManager.Instance.Attack(this, atk);
        }
        public override void Shot1()
        {
            if (isDefending)
            {
                Speak("방벽 닫기");
                isDefending = false;
            }

            Speak("슈욱~ 쾅[기본 공격]");
            FightManager.Instance.Attack(this, atk);
        }
        public override void Shot2()
        {
            isDefending = !isDefending;

            string msg = isDefending ? "위잉..[방벽 펼치기]" : "철컥..[방벽 닫기]";
            Speak(msg);
        }
        public override void OnHit() => Speak("아 우리팀 뭐함~~ 힐좀");
        public override void OnHeal() => Speak("아 역시 절 챙겨주는 유일한 힐러님 ㅠㅠ");
        public override void OnNaNo() => Speak($"|{"=".PadRight(5, '=')} 아무도 날 막을 수!! 읎다!!!!! {"=".PadRight(5, '=')}|");
        public override void OnDie() => Speak("아메가메 꿱");
        public override void Win() => Speak("으하하하하하!! 독일의 기술은 세계 제일!");
    }
}
