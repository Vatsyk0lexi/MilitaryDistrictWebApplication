namespace WebApplication1.Models.ViewModels
{
    public class RequestViewModel
    {
        //всіх частин військового округу, зазначеної армії, дивізії, корпусу та їх командирів
        public IEnumerable<MilitaryBase> MilitaryBases { get; set; }
        public ListOfPartsOfMilitaryDistrict PartsOfMilitaryDistricts { get; set; }
        public string? responce { get; set; }

    }
}
