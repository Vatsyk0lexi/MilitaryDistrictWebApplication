namespace WebApplication1.Models.ViewModels
{
    public class RequestViewModel_7
    {
        public int MilitaryBaseId { get; set; }
        public bool IsForDislocation { get; set; }
        public List<MilitaryBase> MilitaryBases { get; set; }
        public List<BuldingsInMilitaryBase> BuldingsInMilitaryBases { get; set; }
    }
}
