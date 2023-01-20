namespace WebApplication1.Models.ViewModels
{
    public class RequestViewModel_12
    {
        public int KindOfMilitaryWeaponID { get; set; }
        public IEnumerable<MilitaryWeaponsInMilitaryBase> MilitaryWeaponsInMilitaryBases { get; set; }
        public IEnumerable<KindOfMilitaryWeapon> KindOfMilitaryWeapons{ get; set; }
    }
}
