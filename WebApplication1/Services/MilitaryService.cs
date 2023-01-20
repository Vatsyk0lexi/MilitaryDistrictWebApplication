using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public class MilitaryService : IMilitaryService
    {
        private readonly IMilitaryRepository militaryRepository;
        private readonly IMilitaryBaseRepository militaryBaseRepository;
        public MilitaryService(IMilitaryRepository militaryRepository, IMilitaryBaseRepository militaryBaseRepository)
        {
            this.militaryRepository = militaryRepository;
            this.militaryBaseRepository = militaryBaseRepository;
        }

        public async Task<IEnumerable<Military>> GetAllMilitaries()
        {
            return await militaryRepository.GetAllMilitaries();
        }

        public async Task<RequestViewModel2> GetAllPartsOfMilitaryDistrictAndAllRanksOfCategory()
        {
            return new RequestViewModel2()
            {
                PartsOfMilitaryDistricts = new ListOfPartsOfMilitaryDistrict()
                {
                    Armies = await militaryBaseRepository.GetAllArmiesFromPartsOfMilitaryDistrict(),
                    Corps = await militaryBaseRepository.GetAllCorpsFromPartsOfMilitaryDistrict(),
                    Divisions = await militaryBaseRepository.GetAllDivisionsFromPartsOfMilitaryDistrict(),
                    MilitaryBases = await militaryBaseRepository.GetAllMilitaryBases()
                },
                Ranks = await militaryRepository.GetAllRanks(),
                Categories = await militaryRepository.GetAllCategoriesOfRank()
            };
        }

        public async Task<RequestViewModel_4> GetChainOfCommanders(int? MilitaryID, int? CommanderID)
        {

            if (MilitaryID != null)
            {
                var selectedMilitary = await militaryRepository.GetMilitaryByID(MilitaryID);
                var commanders = await militaryRepository.GetChainOfCommadersToMilitary(MilitaryID);
                return new RequestViewModel_4() { SelectedMilitary = selectedMilitary, Commanders = commanders };
            }
            else
            {
                var commander = await militaryRepository.GetCommanderByID(CommanderID);
                var commanders = await militaryRepository.GetChainOfCommadersToCommander(CommanderID);
                var selectedMilitary = new Military() { Id = commander.Id, FullName = commander.Fullname, Rank = commander.Rank };
                return new RequestViewModel_4() { SelectedMilitary = selectedMilitary, Commanders = commanders };
            }

        }

        public async Task<RequestViewModel_11> GetItemsForElevenRequest()
        {
            return new RequestViewModel_11()
            {
                Specialities = await militaryRepository.GetListOfSpecialities(),
                PartsOfMilitaryDistricts = (await militaryBaseRepository.GetAllPartsOfMilitaryDistrictAndAllCategoryAndKindOfEquipment()).PartsOfMilitaryDistricts,
                PartsOfMilitaryBase = await militaryBaseRepository.GetAllPartsOfMilitaryBases()
            };
        }

        public async Task<RequestViewModel_6> GetItemsForSixRequest()
        {
            var model = await militaryBaseRepository.GetAllPartsOfMilitaryDistrictAndAllCategoryAndKindOfEquipment();
            return model;
        }

        public async Task<RequestViewModel_11> GetMilitariesArPartOfMilitaryDistictBySpecialityID(int SpecialityID, string? PartsOfMil)
        {
            var model = new RequestViewModel_11();
            if (PartsOfMil.Contains("Всі"))
            {
                model.SpecialtiesOfMilitaries = await militaryRepository.GetAllSpecialitiesBySpecialityID(SpecialityID);
            }
            else
            {
                var ID = int.Parse(PartsOfMil.Substring(PartsOfMil.IndexOf('|') + 1));
                if (PartsOfMil.Contains("Армія"))
                {
                    model.SpecialtiesOfMilitaries = await militaryRepository.GetSpecialitiesOfArmyBySpecialityID(ID,SpecialityID);
                }
                if (PartsOfMil.Contains("Корпус"))
                {
                    model.SpecialtiesOfMilitaries = await militaryRepository.GetSpecialitiesOfCorpBySpecialityID(ID, SpecialityID);
                }
                if (PartsOfMil.Contains("Дивізія"))
                {
                    model.SpecialtiesOfMilitaries = await militaryRepository.GetSpecialitiesOfDivisionBySpecialityID(ID, SpecialityID);
                }
                if (PartsOfMil.Contains("ВЧ"))
                {
                    model.SpecialtiesOfMilitaries = await militaryRepository.GetSpecialitiesOfMilitaryBaseBySpecialityID(ID, SpecialityID);
                }
                if (PartsOfMil.Contains("Рота"))
                {
                    model.SpecialtiesOfMilitaries = await militaryRepository.GetSpecialitiesOfRotaBySpecialityID(ID, SpecialityID);
                }
                if (PartsOfMil.Contains("Взвод"))
                {
                    model.SpecialtiesOfMilitaries = await militaryRepository.GetSpecialitiesOfPlatoonBySpecialityID(ID, SpecialityID);
                }
                if (PartsOfMil.Contains("Відділення"))
                {
                    model.SpecialtiesOfMilitaries = await militaryRepository.GetSpecialitiesOfDepartamentBySpecialityID(ID, SpecialityID);
                }
            }
            return model;
        }

        public async Task<RequestViewModel2> GetMilitariesByCategoryAtPartOfMilitaryDistrict(string[] parameters)
        {
            var viewModel = new RequestViewModel2();
            var rank = parameters[1];
            var categoryID = int.Parse(parameters[2]);
            var PartsOfMil = parameters[0];
            var RankId = rank.Length > 0 ? int.Parse(rank) : default(int);
            if (PartsOfMil.Contains("Всі"))
            {
                if (rank.Length == 0)
                {
                    var militaries = new List<Military>();
                    militaries.AddRange(await militaryRepository.GetAllMilitariesOfRankAndCategory(RankId, categoryID));
                    await AddCommandersToMilitaries(militaries, categoryID, RankId);
                    viewModel.Militaries = militaries;
                    viewModel.responce = $"{viewModel.Militaries?.FirstOrDefault()?.Rank?.Category.Name} Західного військового округу";
                    return viewModel;
                }
                else
                {
                    var militaries = new List<Military>();
                    militaries.AddRange(await militaryRepository.GetAllMilitariesOfRankAndCategory(RankId, categoryID));
                    await AddCommandersToMilitaries(militaries, categoryID, RankId);
                    viewModel.Militaries = militaries;
                    viewModel.responce = $"{viewModel.Militaries?.FirstOrDefault()?.Rank?.Name}и Західного військового округу";
                    return viewModel;
                }
            }
            else
            {
                var ID = int.Parse(PartsOfMil.Substring(PartsOfMil.IndexOf('|') + 1));
                if (PartsOfMil.Contains("Армія"))
                {
                    viewModel.Militaries = await militaryRepository.GetMilitariesByCategoryIdAtArmy(categoryID, ID, RankId);
                }
                if (PartsOfMil.Contains("Корпус"))
                {
                    viewModel.Militaries = await militaryRepository.GetMilitariesByCategoryIdAtCorp(categoryID, ID, RankId);
                }
                if (PartsOfMil.Contains("Дивізія"))
                {
                    viewModel.Militaries = await militaryRepository.GetMilitariesByCategoryIdAtDivision(categoryID, ID, RankId);
                }
                if (PartsOfMil.Contains("ВЧ"))
                {
                    viewModel.Militaries = await militaryRepository.GetMilitariesByCategoryIdAtMilitaryBase(categoryID, ID, RankId);
                }
            }
            if (viewModel.Militaries.Count() < 1)
            {
                viewModel.responce = $"Військово службовців даного звання в підрозділі '{(PartsOfMil.Substring(0, PartsOfMil.IndexOf('|'))).Trim()}' НЕ ЗНАЙДЕНО";
            }
            else
            {
                if (rank.Length > 0)
                {
                    viewModel.responce = $"{viewModel.Militaries?.FirstOrDefault()?.Rank?.Name}и підрозділу '{(PartsOfMil.Substring(0, PartsOfMil.IndexOf('|'))).Trim()}'";
                }
                else
                {
                    viewModel.responce = $"{viewModel.Militaries?.FirstOrDefault()?.Rank?.Category?.Name} підрозділу '{(PartsOfMil.Substring(0, PartsOfMil.IndexOf('|'))).Trim()}'";
                }
            }
            return viewModel;
        }

        public async Task<RequestViewModel_10> GetSpecialitiesOfPartsOfMilDistricy(string? PartsOfMil, int amount = 5)
        {
            var model = new RequestViewModel_10();
            if (PartsOfMil.Contains("Всі"))
            {
                model.SpecialtiesAndAmount = await militaryRepository.GetAllSpecialitiesMoreThanAmount(amount);
            }
            else
            {
                var ID = int.Parse(PartsOfMil.Substring(PartsOfMil.IndexOf('|') + 1));
                if (PartsOfMil.Contains("Армія"))
                {
                    model.SpecialtiesAndAmount = await militaryRepository.GetSpecialitiesOfArmyMoreThanAmount(ID, amount);
                }
                if (PartsOfMil.Contains("Корпус"))
                {
                    model.SpecialtiesAndAmount = await militaryRepository.GetSpecialitiesOfCorpMoreThanAmount(ID, amount);
                }
                if (PartsOfMil.Contains("Дивізія"))
                {
                    model.SpecialtiesAndAmount = await militaryRepository.GetSpecialitiesOfDivisionMoreThanAmount(ID, amount);
                }
                if (PartsOfMil.Contains("ВЧ"))
                {
                    model.SpecialtiesAndAmount = await militaryRepository.GetSpecialitiesOfMilitaryBaseMoreThanAmount(ID, amount);
                }
            }
            return model;
        }

        public async Task<RequestViewModel_6> ResponceForSixRequest(int CategoryOfMilitaryEquipmentId, int KindOfMilitaryEquipmentID, string PartsOfMil)
        {
            var model = new RequestViewModel_6();
            if (PartsOfMil.Contains("Всі"))
            {
                model.MilitaryEquipmentsInMilitaryBases = await militaryBaseRepository.GetAllMilitaryEquipments(CategoryOfMilitaryEquipmentId, KindOfMilitaryEquipmentID);
            }
            else
            {
                var ID = int.Parse(PartsOfMil.Substring(PartsOfMil.IndexOf('|') + 1));
                if (PartsOfMil.Contains("Армія"))
                {
                    model.MilitaryEquipmentsInMilitaryBases = await militaryBaseRepository.GetMilitaryEquipmentsOfArmyByCategoryAndKindId(ID, CategoryOfMilitaryEquipmentId, KindOfMilitaryEquipmentID);
                }
                if (PartsOfMil.Contains("Корпус"))
                {
                    model.MilitaryEquipmentsInMilitaryBases = await militaryBaseRepository.GetMilitaryEquipmentsOfCorpByCategoryAndKindId(ID, CategoryOfMilitaryEquipmentId, KindOfMilitaryEquipmentID);
                }
                if (PartsOfMil.Contains("Дивізія"))
                {
                    model.MilitaryEquipmentsInMilitaryBases = await militaryBaseRepository.GetMilitaryEquipmentsOfDivisionByCategoryAndKindId(ID, CategoryOfMilitaryEquipmentId, KindOfMilitaryEquipmentID);
                }
                if (PartsOfMil.Contains("ВЧ"))
                {
                    model.MilitaryEquipmentsInMilitaryBases = await militaryBaseRepository.GetMilitaryEquipmentsOfMilBaseByCategoryAndKindId(ID, CategoryOfMilitaryEquipmentId, KindOfMilitaryEquipmentID);
                }
            }
            return model;
        }

        private async Task AddCommandersToMilitaries(List<Military> militaries, int categoryID, int? RankId)
        {
            var commanders = await militaryRepository.GetAllCommanders();

            foreach (var x in commanders.Where(x => x.Rank.CategoryId == categoryID && x.RankId == (RankId > 0 ? RankId : x.RankId)))
            {
                militaries.Add(new Military() { Id = x.Id, FullName = $"{x.Fullname} (командир)", Rank = x.Rank });
            }
            militaries.OrderBy(x => x.RankId);
        }
    }
}
