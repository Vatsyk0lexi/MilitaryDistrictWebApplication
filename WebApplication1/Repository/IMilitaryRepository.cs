using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Repository
{
    public interface IMilitaryRepository
    {
        Task<IEnumerable<Military>> GetAllMilitaries();
        Task<IEnumerable<Commander>> GetAllCommanders();
        Task<IEnumerable<CategoriesOfRank>> GetAllCategoriesOfRank();
        Task<Commander> GetCommanderOfDivisionById(int? DivisionId);
        Task<Commander> GetCommanderOfCorpById(int? CorpId);
        Task<Commander> GetCommanderOfArmyById(int? ArmyId);
        Task<IEnumerable<Military>> GetAllMilitariesOfRankAndCategory(int? RankId, int? CategoryID);
        Task<IEnumerable<Military>> GetMilitariesByCategoryId(int? id);
        Task<IEnumerable<Military>> GetMilitariesByCategoryIdAtArmy(int? Categoryid, int? ArmyId, int? RankId);
        Task<IEnumerable<Military>> GetMilitariesByCategoryIdAtCorp(int? Categoryid, int? CorpId, int? RankId);
        Task<IEnumerable<Military>> GetMilitariesByCategoryIdAtDivision(int? Categoryid, int? DivisionId, int? RankId);
        Task<IEnumerable<Military>> GetMilitariesByCategoryIdAtMilitaryBase(int? Categoryid, int? MilitaryBaseId, int? RankId);
        Task<IEnumerable<Rank>> GetAllRanks();
        Task<IEnumerable<Commander>> GetCommandersFromMB(int? MilitaryBaseId);
        Task<IEnumerable<Commander>> GetChainOfCommadersToMilitary(int? MilitaryID);
        Task<IEnumerable<Commander>> GetChainOfCommadersToCommander(int? CommanderID);
        Task<Military> GetMilitaryByID(int? MilitaryID);
        Task<Commander> GetCommanderByID(int? CommanderID);
        Task<IEnumerable<Platoon>> GetPlatoonsByMilitaryBaseId(int? MilitaryBaseId);
        Task<IEnumerable<Departament>> GetDepartamentsByMilitaryBaseId(int? MilitaryBaseId);
        Task<Rotum> GetRotaByMilitaryBaseId(int? MilitaryBaseId);
        Task<List<SpecialtyAndAmount>> GetSpecialitiesOfMilitaryBaseMoreThanAmount(int ID, int amount);
        Task<List<SpecialtyAndAmount>> GetAllSpecialitiesMoreThanAmount(int amount);
        Task<List<SpecialtyAndAmount>> GetSpecialitiesOfDivisionMoreThanAmount(int ID, int amount);
        Task<List<SpecialtyAndAmount>> GetSpecialitiesOfCorpMoreThanAmount(int ID, int amount);
        Task<List<SpecialtyAndAmount>> GetSpecialitiesOfArmyMoreThanAmount(int ID, int amount);
        Task<List<Specialty>> GetListOfSpecialities();
        Task<List<SpecialtyOfMilitary>> GetAllSpecialitiesBySpecialityID(int SpecialityID);
        Task<List<SpecialtyOfMilitary>> GetSpecialitiesOfArmyBySpecialityID(int ID, int SpecialityID);
        Task<List<SpecialtyOfMilitary>> GetSpecialitiesOfCorpBySpecialityID(int ID, int SpecialityID);
        Task<List<SpecialtyOfMilitary>> GetSpecialitiesOfDivisionBySpecialityID(int ID, int SpecialityID);
        Task<List<SpecialtyOfMilitary>> GetSpecialitiesOfMilitaryBaseBySpecialityID(int ID, int SpecialityID);
        Task<List<SpecialtyOfMilitary>> GetSpecialitiesOfRotaBySpecialityID(int ID, int SpecialityID);
        Task<List<SpecialtyOfMilitary>> GetSpecialitiesOfDepartamentBySpecialityID(int ID, int SpecialityID);
        Task<List<SpecialtyOfMilitary>> GetSpecialitiesOfPlatoonBySpecialityID(int ID, int SpecialityID);
    }
}
