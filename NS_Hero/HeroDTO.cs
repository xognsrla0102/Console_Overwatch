namespace Overwatch.NS_Hero
{
    internal class HeroDTO
    {
        public EHeroType heroType;
        public string userName;

        public HeroDTO(EHeroType heroType, string userName)
        {
            this.heroType = heroType;
            this.userName = userName;
        }
    }
}
