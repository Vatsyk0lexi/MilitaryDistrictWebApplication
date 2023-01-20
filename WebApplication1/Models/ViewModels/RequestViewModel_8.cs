namespace WebApplication1.Models.ViewModels
{
    public class RequestViewModel_8
    {
        public int KindOfMilitaryEquipmentID { get; set; }
        public IEnumerable<MilitaryEquipmentsInMilitaryBase> MilitaryEquipmentsInMilitaryBases { get; set; }
        public IEnumerable<CategoryOfMilitaryEquipment> KindsOfMilitaryEquipment { get; set; }
    }
}
