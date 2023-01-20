namespace WebApplication1.Models.ViewModels
{
    public class ListOfPartsOfMilitaryBase
    {
        public IEnumerable<Rotum> Rotas { get; set; }
        public IEnumerable<Platoon> Platoons { get; set; }
        public IEnumerable<Departament> Departaments { get; set; }
    }
}