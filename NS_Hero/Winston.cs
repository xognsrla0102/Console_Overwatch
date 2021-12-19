using Overwatch.NS_Team;
namespace Overwatch.NS_Hero
{
    internal class Winston : Hero
    {
        public Winston(EHeroType heroType, string userName, int hp, int atk, int def, ETeamType teamType)
            : base(heroType, userName, hp, atk, def, teamType) { }

        public override void Skill1() => Speak("슈퍼 점프~!");

        public override void Skill2() => Fight();

        public override void Ultimate() => Fight();

        public override void Shot1()
        {
            Speak("지이잉~~[기본 공격]");
            FightManager.Instance.Attack(this, atk);
        }

        public override void Shot2() => Fight();
        public override void OnHit() => Speak("(대충 탱커 피 딿는 소리...)");
        public override void OnHeal() => Speak("오! 힐 감사합니다!!");
        public override void OnNaNo() => Speak($"|{"=".PadRight(5, '=')} 피가 끓어오르는 군요! {"=".PadRight(5, '=')}|");
        public override void OnDie() => Speak("오..안녕하세요..? 여긴 병원 인가요..(꿱)");
        public override void Win() => Speak("바나나 안 먹습니다--");
    }
}
