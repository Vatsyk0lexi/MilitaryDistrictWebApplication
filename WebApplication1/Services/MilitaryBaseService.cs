using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Repository;

namespace WebApplication1.Services
{
    public class MilitaryBaseService : IMilitaryBaseService
    {
        private readonly IMilitaryBaseRepository _Repo;
        public MilitaryBaseService(IMilitaryBaseRepository repo)
        {
            _Repo = repo;
        }

        public async Task<IEnumerable<MilitaryBase>> GetAllMilitariBases()
        {
            return await _Repo.GetAllMilitaryBases();
        }

        public async Task<IEnumerable<MilitaryEquipment>> GetListOFMilitaryEquipments()
        {
            var model = await _Repo.GetListOFMilitaryEquipments();
            return model;
        }

        public async Task<RequestViewModel> GetAllPartsOfMilitaryDistrict()
        {

            return new RequestViewModel()
            {
                PartsOfMilitaryDistricts = new ListOfPartsOfMilitaryDistrict()
                {
                    Armies = await _Repo.GetAllArmiesFromPartsOfMilitaryDistrict(),
                    Corps = await _Repo.GetAllCorpsFromPartsOfMilitaryDistrict(),
                    Divisions = await _Repo.GetAllDivisionsFromPartsOfMilitaryDistrict(),
                }
            };
        }

        public async Task<IEnumerable<PlacesOfDeployment>> GetAllPlacesOfDeployment()
        {
            return await _Repo.GetAllPlacesOfDeployment();
        }

        public async Task<IEnumerable<BuldingsInMilitaryBase>> GetBuldingsInMilitaryBase(int? MilitaryBaseId, bool IsForDislocation)
        {
            return await _Repo.GetBuldingsInMilitaryBase(MilitaryBaseId, IsForDislocation);
        }

        public async Task<IEnumerable<MilitaryBase>> GetMBByPlacesOfDeploymentID(int? Id)
        {
            return await _Repo.GetMBByPlacesOfDeploymentID(Id);
        }



        public async Task<RequestViewModel> ResponceForFirstRequest(string? responce)
        {
            var viewModel = new RequestViewModel();
            if (responce.Contains("Всі"))
            {
                viewModel.MilitaryBases = await _Repo.GetMilitaryBasesOfMilitaryDistrictByID(1);
                viewModel.responce = "В\\ч Західного військового округу";
                return viewModel;
            }
            else
            {
                var massive = responce.Split(' ');
                var ID = massive[massive.Length - 1];

                if (responce.Contains("Армія"))
                {
                    viewModel.MilitaryBases = await _Repo.GetMilitaryBasesOfArmyByID(int.Parse(ID));
                }
                if (responce.Contains("Корпус"))
                {
                    viewModel.MilitaryBases = await _Repo.GetMilitaryBasesOfCorpByID(int.Parse(ID));
                }
                if (responce.Contains("Дивізія"))
                {
                    viewModel.MilitaryBases = await _Repo.GetMilitaryBasesOfDivisionByID(int.Parse(ID));
                }
            }
            viewModel.responce = "В\\ч підрозділу - \"" + responce.Substring(0, responce.Length - 2) + "\"";
            return viewModel;
        }

        public async Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentInMilitaryBases(int? CategoryOfMilitaryEquipment, int KindOfMilitaryequipment = 1, int amount = 5)
        {
            return await _Repo.GetMilitaryEquipmentInMilitaryBasesByCategoryAndKindIdAndAmount(CategoryOfMilitaryEquipment,KindOfMilitaryequipment,amount);
        }

        public async Task<IEnumerable<CategoryOfMilitaryEquipment>> GetListOFKindsOfMilitaryEquipments()
        {
            return await _Repo.GetAllCategoriesOfEquipment();
        }
        public async Task<RequestViewModel_9> ResponceForNineRequest(int KindOfMilitaryWeaponID, string? PartsOfMil)
        {
            var model = new RequestViewModel_9();
            if (PartsOfMil.Contains("Всі"))
            {
                model.MilitaryWeaponsInMilitaryBases = await _Repo.GetAllMilitaryWeapons(KindOfMilitaryWeaponID);
            }
            else
            {
                var ID = int.Parse(PartsOfMil.Substring(PartsOfMil.IndexOf('|') + 1));
                if (PartsOfMil.Contains("Армія"))
                {
                    model.MilitaryWeaponsInMilitaryBases = await _Repo.GetMilitaryWeaponsOfArmyByKindId(ID, KindOfMilitaryWeaponID);
                }
                if (PartsOfMil.Contains("Корпус"))
                {
                    model.MilitaryWeaponsInMilitaryBases = await _Repo.GetMilitaryWeaponsOfCorpByKindId(ID, KindOfMilitaryWeaponID);
                }
                if (PartsOfMil.Contains("Дивізія"))
                {
                    model.MilitaryWeaponsInMilitaryBases = await _Repo.GetMilitaryWeaponsOfDivisionByKindId(ID, KindOfMilitaryWeaponID);
                }
                if (PartsOfMil.Contains("ВЧ"))
                {
                    model.MilitaryWeaponsInMilitaryBases = await _Repo.GetMilitaryWeaponsOfMilitaryBaseByKindId(ID, KindOfMilitaryWeaponID);
                }
            }
            return model;
        }

        public async Task<RequestViewModel_9> GetItemsForNineRequest()
        {
            var model = new RequestViewModel_9()
            {
                PartsOfMilitaryDistricts = (await _Repo.GetAllPartsOfMilitaryDistrictAndAllCategoryAndKindOfEquipment()).PartsOfMilitaryDistricts,
                KindOfMilitaryWeapons = await _Repo.GetAllKindOfWeapons()
            };

            return model;
        }

        public async Task<IEnumerable<MilitaryWeaponsInMilitaryBase>> GetMilitaryWeaponsInMilitaryBases(int? KindOfMilitaryWeaponID, int amount = 10)
        {
            return await _Repo.GetMilitaryWeaponInMilitaryBasesByKindIdAndAmount(KindOfMilitaryWeaponID, amount);
        }

        public async Task<IEnumerable<KindOfMilitaryWeapon>> GetListOFKindsOfMilitaryWeapons()
        {
            return await _Repo.GetAllKindOfWeapons();
        }

        public async Task<RequestViewModel_13> GetResponceFor13Request(string responce, bool MaxOrMin)
        {
            var model = new RequestViewModel_13();
            if (responce.Contains("Армія"))
            {
                var items = await _Repo.GetArmyByMaxOrMinAmountOfMilitaryBases(MaxOrMin);
                model.Army = items.Item1;
                model.Amount = items.Item2;
            }
            else if (responce.Contains("Корпус"))
            {
                var items = await _Repo.GetCorpByMaxOrMinAmountOfMilitaryBases(MaxOrMin);
                model.Corp = items.Item1;
                model.Amount = items.Item2;
            }
            else if(responce.Contains("Дивізія"))
            {
                var items = await _Repo.GetDivisionByMaxOrMinAmountOfMilitaryBases(MaxOrMin);
                model.Division = items.Item1;
                model.Amount = items.Item2;
            }
            return model;
        }
    }
}
