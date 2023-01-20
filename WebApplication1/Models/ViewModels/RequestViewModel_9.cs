namespace WebApplication1.Models.ViewModels
{
    public class RequestViewModel_9
    {
        public List<KindOfMilitaryWeapon> KindOfMilitaryWeapons { get; set; }
        public int KindOfMilitaryWeaponID { get; set; }

        public IEnumerable<MilitaryWeaponsInMilitaryBase> MilitaryWeaponsInMilitaryBases { get; set; }
        public ListOfPartsOfMilitaryDistrict PartsOfMilitaryDistricts { get; set; }
        public string? responce { get; set; }
    }
}
