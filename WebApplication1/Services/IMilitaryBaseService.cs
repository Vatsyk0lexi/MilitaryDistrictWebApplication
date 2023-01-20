using Azure;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Services
{
    public interface IMilitaryBaseService
    {
        Task<RequestViewModel> GetAllPartsOfMilitaryDistrict();
        Task<IEnumerable<PlacesOfDeployment>> GetAllPlacesOfDeployment();
        Task<IEnumerable<MilitaryBase>> GetMBByPlacesOfDeploymentID(int? Id);
        Task<RequestViewModel> ResponceForFirstRequest(string? responce);
        Task<IEnumerable<MilitaryBase>> GetAllMilitariBases();
        Task<IEnumerable<MilitaryEquipment>> GetListOFMilitaryEquipments();
        Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentInMilitaryBases(int? MilitaryEquipmentId, int KindOfMilitaryequipment = 1, int amount = 5);
        Task<IEnumerable<MilitaryWeaponsInMilitaryBase>> GetMilitaryWeaponsInMilitaryBases(int? KindOfMilitaryWeaponID, int amount = 10);
        Task<IEnumerable<BuldingsInMilitaryBase>> GetBuldingsInMilitaryBase(int? MilitaryBaseId, bool IsForDislocation);
        Task<IEnumerable<CategoryOfMilitaryEquipment>> GetListOFKindsOfMilitaryEquipments();
        Task<IEnumerable<KindOfMilitaryWeapon>> GetListOFKindsOfMilitaryWeapons();
        Task<RequestViewModel_9> ResponceForNineRequest(int KindOfMilitaryWeaponID, string? responce);
        Task<RequestViewModel_9> GetItemsForNineRequest();

        Task<RequestViewModel_13> GetResponceFor13Request(string responce, bool MaxOrMin);
    }
}
