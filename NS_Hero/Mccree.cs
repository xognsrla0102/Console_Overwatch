using Overwatch.NS_Team;
namespace Overwatch.NS_Hero
{
    internal class Mccree : Hero
    {
        public Mccree(EHeroType heroType, string userName, int hp, int atk, int def, ETeamType teamType)
            : base(heroType, userName, hp, atk, def, teamType) { }

        public override void Skill1() => Speak("구르기!");
        public override void Skill2()
        {
            Speak("섬광탄!");
            FightManager.Instance.Attack(this, atk / 2);
        }
        public override void Ultimate() => Fight();
        public override void Shot1()
        {
            Speak("탕![기본 공격]");
            FightManager.Instance.Attack(this, atk);
        }
        public override void Shot2()
        {
            Speak("타타타타타탕!!![피스키퍼 난사]");
            FightManager.Instance.Attack(this, (int)(atk * 1.5));
        }
        public override void OnHit() => Speak("왜캐 아픔;;");
        public override void OnHeal() => Speak("이야~ 힐 달다~~");
        public override void OnNaNo() => Speak($"|{"=".PadRight(5, '=')} 앞으로 나오시지 {"=".PadRight(5, '=')}|");
        public override void OnDie() => Speak("게임을.. 진...다...으악~");
        public override void Win() => Speak("누가 장의사좀 불러~");
    }
}
