namespace WebApplication1.Models.ViewModels
{
    public class RequestViewModel2
    {

        public IEnumerable<Military> Militaries { get; set; }
        public IEnumerable<Rank> Ranks { get; set; }
        public IEnumerable<CategoriesOfRank> Categories { get; set; }
        public int? RankId { get; set; }
        public int? CategoryID { get; set; }
        public ListOfPartsOfMilitaryDistrict PartsOfMilitaryDistricts { get; set; }
        public string? responce { get; set; }
    }
}
