namespace Overwatch.NS_Team
{
    internal class RedTeam : Team
    {
        public RedTeam(string name, ETeamType teamType) : base(name, teamType) { }

        public override void SelectHero()
        {
            AddHero("Esca", GetRandHero());
            GameManager.Sleep(4000);
            AddHero("Miro", GetRandHero());
            GameManager.Sleep(2000);
            AddHero("Ryujehong", GetRandHero());
            GameManager.Sleep(1000);
        }
    }
}
