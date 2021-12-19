using System;
using System.Diagnostics;

using Overwatch.NS_Team;
namespace Overwatch.NS_Hero
{
    internal abstract class Hero : ISpeakable
    {
        public EHeroType heroType;
        public EInputType inputType;
        public string userName;
        public int maxHP;
        public int hp;
        public int atk;
        public int def;
        public ETeamType teamType;

        private bool isDie;
        public bool IsDie
        {
            get
            {
                return isDie;
            }
            set
            {
                isDie = value;
                if (isDie)
                {
                    FightManager.Instance.CheckDefeat(teamType);
                }
            }
        }

        public Hero(EHeroType heroType, string userName, int hp, int atk, int def, ETeamType teamType)
        {
            this.heroType = heroType;
            this.userName = userName;
            this.hp = hp;
            maxHP = this.hp;
            this.atk = atk;
            this.def = def;
            this.teamType = teamType;
        }

        public void Speak(string msg)
        {
            Console.WriteLine($"{userName}({heroType}) : {msg}");
            GameManager.Sleep(1500 + Rand.rand.Next(2) * 1000);
        }

        public abstract void Skill1();
        public abstract void Skill2();
        public abstract void Ultimate();
        public abstract void Shot1();
        public abstract void Shot2();
        public abstract void OnHit();
        public abstract void OnHeal();
        public abstract void OnNaNo();
        public abstract void OnDie();
        public void Fight()
        {
            inputType = (EInputType)Rand.rand.Next(1, (int)EInputType.INPUT_NUM);
            switch (inputType)
            {
                case EInputType.SHIFT: Skill1(); break;
                case EInputType.E: Skill2(); break;
                case EInputType.Q: Ultimate(); break;
                case EInputType.LEFT_CLICK: Shot1(); break;
                case EInputType.RIGHT_CLICK: Shot2(); break;
                default: Debug.Assert(false); break;
            }

            GameManager.Sleep(500 + Rand.rand.Next(3) * 1000);
        }
        public abstract void Win();
    }
}