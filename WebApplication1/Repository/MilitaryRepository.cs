using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;

namespace WebApplication1.Repository
{
    public class MilitaryRepository : IMilitaryRepository
    {
        private readonly MilitaryDistrictContext DbContext;
        private readonly IMilitaryBaseRepository militaryBaseRepository;
        public MilitaryRepository(MilitaryDistrictContext dbContext, IMilitaryBaseRepository militaryBaseRepository)
        {
            DbContext = dbContext;
            this.militaryBaseRepository = militaryBaseRepository;
        }

        public async Task<IEnumerable<CategoriesOfRank>> GetAllCategoriesOfRank()
        {
            return await DbContext.CategoriesOfRanks.ToListAsync();
        }

        public async Task<IEnumerable<Commander>> GetAllCommanders()
        {
            var commanders = new List<Commander>();
            return await DbContext.Commanders
                .Include(x => x.Rank).ThenInclude(x => x.Category)
                .OrderBy(x => x.RankId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Military>> GetAllMilitaries()
        {
            var militaries = await DbContext.Militaries
                .Include(x => x.Rank)
                .ThenInclude(x => x.Category)
                .ToListAsync();
            return militaries.OrderBy(x => x.RankId);
        }

        public async Task<IEnumerable<Military>> GetAllMilitariesOfRankAndCategory(int? RankId, int? CategoryID)
        {
            return await DbContext.Militaries
                .Include(x => x.Rank)
                    .ThenInclude(x => x.Category)
                .Where(x => x.RankId == (RankId>0? RankId:x.RankId) && x.Rank.CategoryId == CategoryID)
                .OrderBy(x => x.RankId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Rank>> GetAllRanks()
        {
            return await DbContext.Ranks
                .OrderBy(x => x.CategoryId)
                     .ToListAsync();
        }

        public async Task<Commander> GetCommanderOfDivisionById(int? DivisionId)
        {
            return await DbContext.Divisions.Where(x => x.Id == DivisionId)
             .Include(x => x.Commander)
                .ThenInclude(x => x.Rank)
            .Select(x => x.Commander).FirstOrDefaultAsync();
        }

        public async Task<Commander> GetCommanderOfCorpById(int? CorpId)
        {
            return await DbContext.Corps.Where(x => x.Id == CorpId)
             .Include(x => x.Commander)
                .ThenInclude(x => x.Rank)
            .Select(x => x.Commander).FirstOrDefaultAsync();
        }
        public async Task<Commander> GetCommanderOfArmyById(int? ArmyId)
        {
            return await DbContext.Armies.Where(x => x.Id == ArmyId)
             .Include(x => x.Commander)
                .ThenInclude(x => x.Rank)
            .Select(x => x.Commander).FirstOrDefaultAsync();
        }

        public async Task<Rotum> GetRotaByMilitaryBaseId(int? MilitaryBaseId)
        {
            return await DbContext.Rota
                .Include(x => x.Commander)
                    .ThenInclude(x => x.Rank)
                    .ThenInclude(x => x.Category)
                .Where(x => x.MilitaryBaseId == MilitaryBaseId)
                .FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<Platoon>> GetPlatoonsByMilitaryBaseId(int? MilitaryBaseId)
        {
            return await DbContext.Platoons
                .Include(x => x.Commander)
                    .ThenInclude(x => x.Rank)
                    .ThenInclude(x => x.Category)
                .Where(x => x.MilitaryBaseId == MilitaryBaseId).ToListAsync();
        }
        public async Task<IEnumerable<Departament>> GetDepartamentsByMilitaryBaseId(int? MilitaryBaseId)
        {
            return await DbContext.Departaments
                .Include(x => x.Commander)
                    .ThenInclude(x => x.Rank)
                    .ThenInclude(x => x.Category)
                .Where(x => x.MilitaryBaseId == MilitaryBaseId).ToListAsync();
        }
        public async Task<IEnumerable<Commander>> GetCommandersFromMB(int? MilitaryBaseId)
        {
            List<Commander> commanders = new List<Commander>();
            var MIlitaryBase = await DbContext.MilitaryBases
                .Include(x => x.Commander)
                    .ThenInclude(x => x.Rank)
                        .ThenInclude(x => x.Category)
                .Where(x => x.Id == MilitaryBaseId).FirstOrDefaultAsync();
            commanders.Add(MIlitaryBase.Commander);
            var RotaOfMb = await GetRotaByMilitaryBaseId(MilitaryBaseId);
            var PlatoonsOfMb = await GetPlatoonsByMilitaryBaseId(MilitaryBaseId);
            var DepartamentsOfMb = await GetDepartamentsByMilitaryBaseId(MilitaryBaseId);
            commanders.Add(RotaOfMb.Commander);
            commanders.AddRange(PlatoonsOfMb.Select(x => x.Commander));
            commanders.AddRange(DepartamentsOfMb.Select(x => x.Commander));
            return commanders.OrderBy(x => x.RankId);
        }

        public async Task<IEnumerable<Military>> GetMilitariesByCategoryId(int? id)
        {
            return await DbContext.Militaries
                .Include(x => x.Rank)
                .ThenInclude(x => x.Category)
                .Where(x => x.Rank.CategoryId == id)
                .OrderBy(x => x.RankId)
                .ToListAsync();
        }

        public async Task<IEnumerable<Military>> GetMilitariesByCategoryIdAtArmy(int? Categoryid, int? ArmyId, int? RankId)
        {
            List<Military> militaries = new List<Military>();
            var corpsInArmy = DbContext.Corps.Where(x => x.ArmyId == ArmyId).Select(x => x.Id).ToList();
            foreach (var CorpId in corpsInArmy)
            {
                militaries.AddRange(await GetMilitariesByCategoryIdAtCorp(Categoryid, CorpId, RankId));
            }
            var commanderOfArmy = await GetCommanderOfArmyById(ArmyId);
            if (RankId > 0 ? commanderOfArmy.RankId == RankId : true)
            {
                militaries.Add(new Military() { Id = commanderOfArmy.Id, FullName = $"{commanderOfArmy.Fullname} (командир армії)", Rank = commanderOfArmy.Rank });
            }
            return militaries.OrderBy(x => x.RankId);
        }

        public async Task<IEnumerable<Military>> GetMilitariesByCategoryIdAtCorp(int? Categoryid, int? CorpId, int? RankId)
        {
            List<Military> militaries = new List<Military>();
            var divisionsIDInCorp = DbContext.Divisions.Where(x => x.CorpId == CorpId).Select(x => x.Id).ToList();
            foreach (var DivisionID in divisionsIDInCorp)
            {
                militaries.AddRange(await GetMilitariesByCategoryIdAtDivision(Categoryid, DivisionID, RankId));
            }
            var commanderOfCorp = await GetCommanderOfCorpById(CorpId);
            if (RankId > 0 ? commanderOfCorp.RankId == RankId : true)
            {
                militaries.Add(new Military() { Id = commanderOfCorp.Id, FullName = $"{commanderOfCorp.Fullname} (командир корпусу)", Rank = commanderOfCorp.Rank });
            }
            militaries.OrderBy(x => x.RankId);
            return militaries;
        }

        public async Task<IEnumerable<Military>> GetMilitariesByCategoryIdAtDivision(int? Categoryid, int? DivisionId, int? RankId)
        {
            List<Military> militaries = new List<Military>();
            var mbOfDivision = await militaryBaseRepository.GetMilitaryBasesOfDivisionByID(DivisionId);
            List<int> MilitaryBaseIDs = mbOfDivision.Select(x => x.Id).ToList();
            var commanderOfDivision = await GetCommanderOfDivisionById(DivisionId);
            if (RankId > 0 ? commanderOfDivision.RankId == RankId : true)
            {
                militaries.Add(new Military() { Id = commanderOfDivision.Id, FullName = $"{commanderOfDivision.Fullname} (командир дивізії)", Rank = commanderOfDivision.Rank });
            }

            foreach (var id in MilitaryBaseIDs)
            {
                militaries.AddRange(await GetMilitariesByCategoryIdAtMilitaryBase(Categoryid, id, RankId));
            }
            return militaries.OrderBy(x => x.RankId);


        }

        public async Task<IEnumerable<Military>> GetMilitariesByCategoryIdAtMilitaryBase(int? Categoryid, int? MilitaryBaseId, int? RankId)
        {

            List<Military> militaries = new List<Military>();
            var militariesFromAllDepartamentInMB = await DbContext.Militaries
                .Include(x => x.Departament)
                    .ThenInclude(x => x.MilitaryBase)
                .Where(x => x.Departament.MilitaryBaseId == MilitaryBaseId)
                .Include(x => x.Rank)
                    .ThenInclude(x => x.Category)
                .Where(x => x.Rank.CategoryId == Categoryid)
                .Where(x => x.RankId == (RankId > 0 ? RankId : x.RankId))
                .OrderBy(x => x.RankId)
                .ToListAsync();
            var commandersFromMB = await GetCommandersFromMB(MilitaryBaseId);
            var Commanders = commandersFromMB.ToList().Where(x => x.RankId == (RankId > 0 ? RankId : x.RankId) && x.Rank.CategoryId == Categoryid).OrderBy(x => x.RankId);
            militaries.AddRange(militariesFromAllDepartamentInMB);
            foreach (var x in Commanders)
            {
                militaries.Add(new Military() { Id = x.Id, FullName = $"{x.Fullname} (командир)", Rank = x.Rank });
            }
            return militaries.OrderBy(x => x.RankId);
        }

        public async Task<IEnumerable<Commander>> GetChainOfCommadersToMilitary(int? MilitaryID)
        {
            List<Commander> commanders = new List<Commander>();
            var military = await DbContext.Militaries
                .Include(x => x.Departament)
                    .ThenInclude(x => x.Platoon)
                        .ThenInclude(x => x.Rota)
                            .ThenInclude(x => x.MilitaryBase)
                .Include(x => x.Departament.Commander)
                    .ThenInclude(x => x.Rank)
                        .ThenInclude(x => x.Category)
                .Include(x => x.Departament.Platoon.Commander)
                .ThenInclude(x => x.Rank)
                        .ThenInclude(x => x.Category)
                .Include(x => x.Departament.Platoon.Rota.Commander)
                .ThenInclude(x => x.Rank)
                        .ThenInclude(x => x.Category)
                .Include(x => x.Departament.MilitaryBase.Commander)
                .ThenInclude(x => x.Rank)
                        .ThenInclude(x => x.Category)
                .Where(x => x.Id == MilitaryID)
                .FirstOrDefaultAsync();
            var commanderOfDepartament = military.Departament.Commander;
            commanderOfDepartament.Fullname += $" (командир {military.Departament.Name})";
            var commanderOfPlatoon = military.Departament.Platoon.Commander;
            commanderOfPlatoon.Fullname += $" (командир {military.Departament.Platoon.Name})";
            var commanderOfRota = military.Departament.Platoon.Rota.Commander;
            commanderOfRota.Fullname += $" (командир {military.Departament.Platoon.Rota.Name})";
            var commanderOfMB = military.Departament.MilitaryBase.Commander;
            commanderOfMB.Fullname += $" (командир В\\ч {military.Departament.MilitaryBase.Name})";
            commanders.Add(commanderOfDepartament);
            commanders.Add(commanderOfPlatoon);
            commanders.Add(commanderOfRota);
            commanders.Add(commanderOfMB);

            var brigadeOfMB = await militaryBaseRepository.GetBrigadeByPartsOfMilitaryDistrictId(military.Departament.MilitaryBase.SubjectId);
            if (brigadeOfMB != null)
            {
                var commanderOfBrigade = brigadeOfMB.Commander;
                commanderOfBrigade.Fullname += $" (командир {brigadeOfMB.Name})";
                commanders.Add(commanderOfBrigade);

                var commanderOfDivision = brigadeOfMB.Division.Commander;
                commanderOfDivision.Fullname += $" (командир {brigadeOfMB.Division.Name})";
                commanders.Add(commanderOfDivision);

                var commanderOfCorp = brigadeOfMB.Division.Corp.Commander;
                commanderOfCorp.Fullname += $" (командир {brigadeOfMB.Division.Corp.Name})";
                commanders.Add(commanderOfCorp);

                var commanderOfArmy = brigadeOfMB.Division.Corp.Army.Commander;
                commanderOfArmy.Fullname += $" (командир {brigadeOfMB.Division.Corp.Army.Name})";
                commanders.Add(commanderOfArmy);
            }
            else
            {

            }
            var commanderOfMilitaryDistrict = (await militaryBaseRepository.GetMilitaryDistrictById(1)).Commander;
            commanderOfMilitaryDistrict.Fullname += $" (командир Західного військового округу)";
            commanders.Add(commanderOfMilitaryDistrict);
            return commanders;
        }

        public async Task<IEnumerable<Commander>> GetChainOfCommadersToCommander(int? CommanderID)
        {
            // дізнатися якої структури цей командир. Дізнатися чи комусь підпорядк. дана структура, командир - кому підпорядк-...
            List<Commander> commanders = new List<Commander>();


            return commanders;
        }

        public async Task<Military> GetMilitaryByID(int? MilitaryID)
        {
            return await DbContext.Militaries
                .Include(x => x.Rank)
                    .ThenInclude(x => x.Category)
                .Where(x => x.Id == MilitaryID)
                .FirstOrDefaultAsync();
        }

        public async Task<Commander> GetCommanderByID(int? CommanderID)
        {
            return await DbContext.Commanders
                .Include(x => x.Rank)
                    .ThenInclude(x => x.Category)
                .Where(x => x.Id == CommanderID)
                .FirstOrDefaultAsync();
        }
        private List<SpecialtyAndAmount> FormatSpecialtytColumn(List<SpecialtyAndAmount> specialities)
        {
            List<int> UnicKey = new List<int>();
            List<int> IndexForDeleting = new List<int>();
            foreach (var specialty in specialities)
            {
                if (UnicKey.Contains(specialty.Specialty.Id))
                {
                    var firstElement = specialities.Find(x => x.Specialty.Id == specialty.Specialty.Id);
                    firstElement.Amount += specialty.Amount;
                    IndexForDeleting.Add(specialities.IndexOf(specialty));
                }
                else
                {
                    UnicKey.Add(specialty.Specialty.Id);
                }
            }
            for (int i = 0; i < IndexForDeleting.Count(); i++)
            {
                specialities.RemoveAt(IndexForDeleting.ElementAt(i) - i);
            }
            return specialities;
        }
        public async Task<List<SpecialtyAndAmount>> GetSpecialitiesOfMilitaryBaseMoreThanAmount(int ID, int amount)
        {
            var model = new List<SpecialtyAndAmount>();
            var specialitiesInMB = await DbContext.SpecialtyOfMilitaries
                .Include(x => x.Military)
                    .ThenInclude(x => x.Departament)
                 .Include(x => x.Specialty)
                 .Where(x => x.Military.Departament.MilitaryBaseId == ID)
                 .ToListAsync();
            Dictionary<int, int> SpecialityIdAndAmount = new Dictionary<int, int>();
            foreach (var item in specialitiesInMB.Select(x => x.SpecialtyId).Distinct().ToList())
            {
                SpecialityIdAndAmount.Add(item, 0);
            }

            foreach (var specialtyOfMilitary in specialitiesInMB)
            {
                if (SpecialityIdAndAmount.ContainsKey(specialtyOfMilitary.SpecialtyId))
                {
                    SpecialityIdAndAmount[specialtyOfMilitary.SpecialtyId] += 1;
                }
            }
            foreach (var item in SpecialityIdAndAmount)
            {
                if (item.Value > amount)
                {
                    model.Add(new SpecialtyAndAmount()
                    {
                        Specialty = await GetSpecialtyByID(item.Key),
                        Amount = item.Value
                    });
                }
            }
            return model;
        }

        private async Task<Specialty> GetSpecialtyByID(int specialtyId)
        {
            return await DbContext.Specialties.Where(x => x.Id == specialtyId).FirstOrDefaultAsync();
        }

        public async Task<List<SpecialtyAndAmount>> GetAllSpecialitiesMoreThanAmount(int amount)
        {
            var model = new List<SpecialtyAndAmount>();
            var militaryBases = await militaryBaseRepository.GetAllMilitaryBases();
            foreach (var mb in militaryBases)
            {
                model.AddRange(await GetSpecialitiesOfMilitaryBaseMoreThanAmount(mb.Id, amount));
            }
            return FormatSpecialtytColumn(model);
        }

        public async Task<List<SpecialtyAndAmount>> GetSpecialitiesOfDivisionMoreThanAmount(int ID, int amount)
        {
            var model = new List<SpecialtyAndAmount>();
            var militaryBases = await militaryBaseRepository.GetMilitaryBasesOfDivisionByID(ID);
            foreach (var mb in militaryBases)
            {
                model.AddRange(await GetSpecialitiesOfMilitaryBaseMoreThanAmount(mb.Id, amount));
            }
            return model;
        }

        public async Task<List<SpecialtyAndAmount>> GetSpecialitiesOfCorpMoreThanAmount(int ID, int amount)
        {
            var model = new List<SpecialtyAndAmount>();
            var militaryBases = await militaryBaseRepository.GetMilitaryBasesOfCorpByID(ID);
            foreach (var mb in militaryBases)
            {
                model.AddRange(await GetSpecialitiesOfMilitaryBaseMoreThanAmount(mb.Id, amount));
            }
            return model;
        }

        public async Task<List<SpecialtyAndAmount>> GetSpecialitiesOfArmyMoreThanAmount(int ID, int amount)
        {
            var model = new List<SpecialtyAndAmount>();
            var militaryBases = await militaryBaseRepository.GetMilitaryBasesOfArmyByID(ID);
            foreach (var mb in militaryBases)
            {
                model.AddRange(await GetSpecialitiesOfMilitaryBaseMoreThanAmount(mb.Id, amount));
            }
            return model;
        }

        public async Task<List<Specialty>> GetListOfSpecialities()
        {
            return await DbContext.Specialties.ToListAsync();
        }

        public async Task<List<SpecialtyOfMilitary>> GetAllSpecialitiesBySpecialityID(int SpecialityID)
        {
            return await DbContext.SpecialtyOfMilitaries
                .Include(x => x.Specialty)
                .Include(x => x.Military)
                    .ThenInclude(x => x.Departament)
                .Where(x=>x.SpecialtyId == SpecialityID)
                .ToListAsync();
        }

        public async Task<List<SpecialtyOfMilitary>> GetSpecialitiesOfArmyBySpecialityID(int ID, int SpecialityID)
        {
            var model = new List<SpecialtyOfMilitary>();
            var militaryBases = await militaryBaseRepository.GetMilitaryBasesOfArmyByID(ID);
            foreach (var mb in militaryBases)
            {
                model.AddRange(await GetSpecialitiesOfMilitaryBaseBySpecialityID(mb.Id, SpecialityID));
            }
            return model;
        }

        public async Task<List<SpecialtyOfMilitary>> GetSpecialitiesOfCorpBySpecialityID(int ID, int SpecialityID)
        {
            var model = new List<SpecialtyOfMilitary>();
            var militaryBases = await militaryBaseRepository.GetMilitaryBasesOfCorpByID(ID);
            foreach (var mb in militaryBases)
            {
                model.AddRange(await GetSpecialitiesOfMilitaryBaseBySpecialityID(mb.Id, SpecialityID));
            }
            return model;
        }

        public async Task<List<SpecialtyOfMilitary>> GetSpecialitiesOfDivisionBySpecialityID(int ID, int SpecialityID)
        {
            var model = new List<SpecialtyOfMilitary>();
            var militaryBases = await militaryBaseRepository.GetMilitaryBasesOfDivisionByID(ID);
            foreach (var mb in militaryBases)
            {
                model.AddRange(await GetSpecialitiesOfMilitaryBaseBySpecialityID(mb.Id, SpecialityID));
            }
            return model;
        }

        public async Task<List<SpecialtyOfMilitary>> GetSpecialitiesOfMilitaryBaseBySpecialityID(int ID, int SpecialityID)
        {
            return await DbContext.SpecialtyOfMilitaries
                .Include(x => x.Specialty)
                .Include(x => x.Military)
                    .ThenInclude(x => x.Departament)
                .Where(x=>x.Military.Departament.MilitaryBaseId == ID)
                .Where(x => x.SpecialtyId == SpecialityID)
                .ToListAsync();
        }

        public async Task<List<SpecialtyOfMilitary>> GetSpecialitiesOfRotaBySpecialityID(int ID, int SpecialityID)
        {
            return await DbContext.SpecialtyOfMilitaries
                .Include(x => x.Specialty)
                .Include(x => x.Military)
                    .ThenInclude(x => x.Departament)
                        .ThenInclude(x=>x.Platoon)
                            .ThenInclude(x=>x.Rota)
                .Where(x => x.Military.Departament.Platoon.RotaId == ID)
                .Where(x => x.SpecialtyId == SpecialityID)
                .ToListAsync();
        }

        public async Task<List<SpecialtyOfMilitary>> GetSpecialitiesOfDepartamentBySpecialityID(int ID, int SpecialityID)
        {
            return await DbContext.SpecialtyOfMilitaries
                .Include(x => x.Specialty)
                .Include(x => x.Military)
                    .ThenInclude(x => x.Departament)
                .Where(x => x.Military.DepartamentId == ID)
                .Where(x => x.SpecialtyId == SpecialityID)
                .ToListAsync();
        }

        public async Task<List<SpecialtyOfMilitary>> GetSpecialitiesOfPlatoonBySpecialityID(int ID, int SpecialityID)
        {
            return await DbContext.SpecialtyOfMilitaries
                .Include(x => x.Specialty)
                .Include(x => x.Military)
                    .ThenInclude(x => x.Departament)
                .Where(x => x.Military.Departament.PlatoonId == ID)
                .Where(x => x.SpecialtyId == SpecialityID)
                .ToListAsync();
        }
    }
}
