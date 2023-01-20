namespace WebApplication1.Models.ViewModels
{
    public class ListOfPartsOfMilitaryDistrict
    {
        public IEnumerable<Army> Armies { get; set; }
        public IEnumerable<Corps> Corps { get; set; }
        public IEnumerable<Division> Divisions { get; set; }
        public IEnumerable<MilitaryBase> MilitaryBases { get; set; }
    }
}
