namespace WebApplication1.Models.ViewModels
{
    public class RequestViewModel_6
    {
        public int CategoryOfMilitaryEquipmentId { get; set; }
        public List<CategoryOfMilitaryEquipment> CategoriesOfMilitaryEquipment { get; set; }
        public int KindOfMilitaryEquipmentID { get; set; }
        public List<KindOfMilitaryEquipment> KindsOfMilitaryEquipment { get; set; }

        public IEnumerable<MilitaryEquipmentsInMilitaryBase> MilitaryEquipmentsInMilitaryBases { get; set; }
        public ListOfPartsOfMilitaryDistrict PartsOfMilitaryDistricts { get; set; }
        public string? responce { get; set; }
    }
}
