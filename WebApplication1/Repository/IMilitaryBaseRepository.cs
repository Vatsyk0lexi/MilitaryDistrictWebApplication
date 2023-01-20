using WebApplication1.Models;
using System.Collections.Generic;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Repository
{
    public interface IMilitaryBaseRepository
    {
        Task<IEnumerable<MilitaryDistrict>> GetAllMilitaryDistricts();
        Task<IEnumerable<MilitaryBase>> GetAllMilitaryBases();
        Task<IEnumerable<Army>> GetAllArmiesFromPartsOfMilitaryDistrict();
        Task<IEnumerable<Corps>> GetAllCorpsFromPartsOfMilitaryDistrict();
        Task<IEnumerable<Division>> GetAllDivisionsFromPartsOfMilitaryDistrict();
        Task<IEnumerable<MilitaryBase>> GetMilitaryBasesOfMilitaryDistrictByID(int? MilitaryDistrictID);
        Task<IEnumerable<MilitaryBase>> GetMilitaryBasesOfCorpByID(int? CorpID);
        Task<IEnumerable<MilitaryBase>> GetMilitaryBasesOfArmyByID(int? ArmyID);
        Task<IEnumerable<MilitaryBase>> GetMilitaryBasesOfDivisionByID(int? DivisionID);
        Task<MilitaryDistrict> GetMilitaryDistrictById(int? MilitaryDistrictID);
        Task<Brigade?> GetBrigadeByPartsOfMilitaryDistrictId(int? MilitaryBaseID);
        Task<PartsOfMilitaryDistrict> GetPartsOfMilitaryDistrictById(int? PartosOfMilitaryDistrictID);
        Task<IEnumerable<PlacesOfDeployment>> GetAllPlacesOfDeployment();
        Task<IEnumerable<MilitaryBase>> GetMBByPlacesOfDeploymentID(int? Id);
        Task<RequestViewModel_6> GetAllPartsOfMilitaryDistrictAndAllCategoryAndKindOfEquipment();
        Task<ListOfPartsOfMilitaryBase> GetAllPartsOfMilitaryBases();
        Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetAllMilitaryEquipments(int CategoryOfMilitaryEquipmentId, int KindOfMilitaryEquipmentID);
        Task<IEnumerable<MilitaryEquipment>> GetListOFMilitaryEquipments();
        Task<IEnumerable<KindOfMilitaryWeapon>> GetListOFKindOfMilitaryWeapons();
        Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentsByKindId(int KindOfMilitaryEquipmentID);
        Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentsByCategoryId(int CategoryOfMilitaryEquipmentId);
        Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentsOfArmyByCategoryAndKindId(int ArmyID, int CategoryOfMilitaryEquipmentId, int KindOfMilitaryEquipmentID);
        Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentsOfCorpByCategoryAndKindId(int CorpID, int CategoryOfMilitaryEquipmentId, int KindOfMilitaryEquipmentID);
        Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentsOfDivisionByCategoryAndKindId(int DivisionID, int CategoryOfMilitaryEquipmentId, int KindOfMilitaryEquipmentID);
        Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentsOfMilBaseByCategoryAndKindId(int MbID, int CategoryOfMilitaryEquipmentId, int KindOfMilitaryEquipmentID);
        Task<IEnumerable<BuldingsInMilitaryBase>> GetBuldingsInMilitaryBase(int? MilitaryBaseId, bool IsForDislocation);
        Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentInMilitaryBasesByCategoryAndKindIdAndAmount(int? CategoryOfMilitaryEquipment, int KindOfMilitaryequipment, int amount);
        Task<IEnumerable<MilitaryWeaponsInMilitaryBase>> GetMilitaryWeaponInMilitaryBasesByKindIdAndAmount(int? KindOfMilitaryWeaponID,int amount);
        Task<List<KindOfMilitaryEquipment>> GetAllKindOfEquipment();
        
        Task<List<CategoryOfMilitaryEquipment>> GetAllCategoriesOfEquipment();
        Task<IEnumerable<MilitaryWeaponsInMilitaryBase>> GetAllMilitaryWeapons(int KindOfMilitaryEquipmentID);
        Task<IEnumerable<MilitaryWeaponsInMilitaryBase>> GetMilitaryWeaponsOfArmyByKindId(int ArmyID, int KindOfMilitaryWeaponID);
        Task<IEnumerable<MilitaryWeaponsInMilitaryBase>> GetMilitaryWeaponsOfCorpByKindId(int CorpID, int KindOfMilitaryWeaponID);
        Task<IEnumerable<MilitaryWeaponsInMilitaryBase>> GetMilitaryWeaponsOfDivisionByKindId(int DivisionID, int KindOfMilitaryWeaponID);
        Task<IEnumerable<MilitaryWeaponsInMilitaryBase>> GetMilitaryWeaponsOfMilitaryBaseByKindId(int MbID, int KindOfMilitaryWeaponID);
        Task<List<KindOfMilitaryWeapon>> GetAllKindOfWeapons();
        Task<(Army, int)> GetArmyByMaxOrMinAmountOfMilitaryBases(bool MaxOrMin);
        Task<(Corps, int)> GetCorpByMaxOrMinAmountOfMilitaryBases(bool MaxOrMin);
        Task<(Division, int)> GetDivisionByMaxOrMinAmountOfMilitaryBases(bool MaxOrMin);
    }
}
