namespace Overwatch.NS_Team
{
    internal class BlueTeam : Team
    {
        public BlueTeam(string name, ETeamType teamType) : base(name, teamType) { }

        public override void SelectHero()
        {
            AddHero("나노하나", GetRandHero());
            GameManager.Sleep(1000);
            AddHero("카이저", GetRandHero());
            GameManager.Sleep(3000);
            AddHero("쪼낙", GetRandHero());
            GameManager.Sleep(1000);
        }
    }
}
