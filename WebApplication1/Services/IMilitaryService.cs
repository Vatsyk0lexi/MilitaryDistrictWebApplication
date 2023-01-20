using Azure;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Services
{
    public interface IMilitaryService
    {
        Task<IEnumerable<Military>> GetAllMilitaries();
        Task<RequestViewModel2> GetAllPartsOfMilitaryDistrictAndAllRanksOfCategory();
        Task<RequestViewModel2> GetMilitariesByCategoryAtPartOfMilitaryDistrict(string[] parameters);
        Task<RequestViewModel_4> GetChainOfCommanders(int? MilitaryID, int? CommanderID);
        Task<RequestViewModel_6> GetItemsForSixRequest();
        Task<RequestViewModel_11> GetItemsForElevenRequest();
        Task<RequestViewModel_6> ResponceForSixRequest(int CategoryOfMilitaryEquipmentId, int KindOfMilitaryEquipmentID, string responce);
        Task<RequestViewModel_11> GetMilitariesArPartOfMilitaryDistictBySpecialityID(int SpecialityID,  string? responce);
        Task<RequestViewModel_10> GetSpecialitiesOfPartsOfMilDistricy(string? responce, int amount = 5);
    }
}
