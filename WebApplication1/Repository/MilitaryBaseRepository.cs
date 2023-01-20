using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using System.Collections.Generic;
using WebApplication1.Models.ViewModels;
using System;

namespace WebApplication1.Repository
{
    public class MilitaryBaseRepository : IMilitaryBaseRepository
    {
        private readonly MilitaryDistrictContext DBcontext;
        public MilitaryBaseRepository(MilitaryDistrictContext dBcontext)
        {
            DBcontext = dBcontext;

        }

        public async Task<IEnumerable<Army>> GetAllArmiesFromPartsOfMilitaryDistrict()
        {
            var result = from a in DBcontext.PartsOfMilitaryDistricts
                         join arm in DBcontext.Armies on a.Id equals arm.PartsOfMilDistrId
                         where arm.PartsOfMilDistrId == a.Id
                         select arm;
            return result.ToList();
        }
        public async Task<IEnumerable<MilitaryBase>> GetAllMilitaryBases()
        {
            return await DBcontext.MilitaryBases
                    .ToListAsync();
        }
        public async Task<IEnumerable<Corps>> GetAllCorpsFromPartsOfMilitaryDistrict()
        {
            var result = from a in DBcontext.PartsOfMilitaryDistricts
                         join corp in DBcontext.Corps on a.Id equals corp.PartsOfMilDistrId
                         where corp.PartsOfMilDistrId == a.Id
                         select corp;
            return result.ToList();
        }

        public async Task<IEnumerable<Division>> GetAllDivisionsFromPartsOfMilitaryDistrict()
        {
            var result = from a in DBcontext.PartsOfMilitaryDistricts
                         join div in DBcontext.Divisions on a.Id equals div.PartsOfMilDistrId
                         where div.PartsOfMilDistrId == a.Id
                         select div;
            return result.ToList();
        }

        public async Task<IEnumerable<MilitaryDistrict>> GetAllMilitaryDistricts() => await DBcontext.MilitaryDistricts.ToListAsync();




        public async Task<IEnumerable<MilitaryBase>> GetMilitaryBasesOfArmyByID(int? ArmyID)
        {

            var BrigadesOfSelectedArmy = await DBcontext
                                                .Brigades
                                                .Include(x => x.Division)
                                                .ThenInclude(x => x.Corp)
                                                .Where(x => x.Division.Corp.ArmyId == ArmyID)
                                                .ToListAsync();
            var DivisionsOfSelectedArmy = await DBcontext
                                                .Divisions
                                                .Include(x => x.Corp)
                                                .Where(x => x.Corp.ArmyId == ArmyID)
                                                .ToListAsync();

            List<MilitaryBase> militaryBases = new List<MilitaryBase>();
            foreach (var br in BrigadesOfSelectedArmy)
            {
                militaryBases.AddRange(await DBcontext.MilitaryBases
                    .Where(x => x.SubjectId == br.PartsOfMilDistrId)
                    .Include(x => x.Commander)
                    .Include(x => x.PlacesOfDeployment)
                    .ToListAsync());
            }
            foreach (var division in DivisionsOfSelectedArmy)
            {
                militaryBases.AddRange(await DBcontext.MilitaryBases
                    .Where(x => x.SubjectId == division.PartsOfMilDistrId)
                    .Include(x => x.Commander)
                    .Include(x => x.PlacesOfDeployment)
                    .ToListAsync());
            }

            return militaryBases;

        }

        public async Task<IEnumerable<MilitaryBase>> GetMilitaryBasesOfCorpByID(int? CorpID)
        {
            var corp = await DBcontext.Corps
                .Include(x => x.Divisions)
                    .ThenInclude(x => x.Brigades)
                .Where(x => x.Id == CorpID)
                .FirstOrDefaultAsync();
            var divisionOfCorp = corp.Divisions;
            var brigadesOfCorp = divisionOfCorp.Select(x => x.Brigades).ToList();
            List<MilitaryBase> militaryBases = new List<MilitaryBase>();
            foreach (var br in brigadesOfCorp)
            {
                foreach (var item in br)
                {
                    militaryBases.AddRange(await DBcontext.MilitaryBases
                    .Where(x => x.SubjectId == item.PartsOfMilDistrId)
                    .Include(x => x.Commander)
                    .Include(x => x.PlacesOfDeployment)
                    .ToListAsync());
                }

            }
            foreach (var division in divisionOfCorp)
            {
                militaryBases.AddRange(await DBcontext.MilitaryBases
                    .Where(x => x.SubjectId == division.PartsOfMilDistrId)
                    .Include(x => x.Commander)
                    .Include(x => x.PlacesOfDeployment)
                    .ToListAsync());
            }
            return militaryBases;
        }

        public async Task<IEnumerable<MilitaryBase>> GetMilitaryBasesOfDivisionByID(int? DivisionID)
        {
            var division = await DBcontext.Divisions.Include(x => x.Brigades).Where(x => x.Id == DivisionID).FirstOrDefaultAsync();
            var BrigadesInDiv = division.Brigades;
            List<MilitaryBase> militaryBases = new List<MilitaryBase>();
            foreach (var item in BrigadesInDiv)
            {
                militaryBases.AddRange(await DBcontext.MilitaryBases
                .Where(x => x.SubjectId == item.PartsOfMilDistrId)
                .Include(x => x.Commander)
                .Include(x => x.PlacesOfDeployment)
                .ToListAsync());
            }

            militaryBases.AddRange(await DBcontext.MilitaryBases
                .Where(x => x.SubjectId == division.PartsOfMilDistrId)
                .Include(x => x.Commander)
                .Include(x => x.PlacesOfDeployment)
                .ToListAsync());

            return militaryBases;
        }

        public async Task<IEnumerable<MilitaryBase>> GetMilitaryBasesOfMilitaryDistrictByID(int? MilitaryDistrictID = 1)
        {
            return await DBcontext.MilitaryBases
                        .Include(x => x.PlacesOfDeployment)
                        .Include(x => x.Commander)
                         .Include(x => x.Subject)
                             .Where(x => x.Subject.MilitaryDistrictId == MilitaryDistrictID)
                             .Include(x => x.Subject.Brigade)
                             .ToListAsync();
        }

        public async Task<IEnumerable<MilitaryBase>> GetMBByPlacesOfDeploymentID(int? Id)
        {
            return await DBcontext.MilitaryBases
                .Include(x => x.Commander)
                .Include(x => x.PlacesOfDeployment)
                .Where(x => x.PlacesOfDeploymentId == Id)
                .OrderBy(x => x.PlacesOfDeploymentId)
                .ToListAsync();

        }
        public async Task<IEnumerable<PlacesOfDeployment>> GetAllPlacesOfDeployment()
        {
            return await DBcontext.PlacesOfDeployments.ToListAsync();
        }

        public async Task<MilitaryDistrict> GetMilitaryDistrictById(int? MilitaryDistrictID)
        {
            return await DBcontext.MilitaryDistricts.Include(x => x.Commander).ThenInclude(x => x.Rank).ThenInclude(x => x.Category).Where(x => x.Id == MilitaryDistrictID).FirstOrDefaultAsync();
        }

        public async Task<Brigade?> GetBrigadeByPartsOfMilitaryDistrictId(int? SubjectID)
        {
            var brigade = await DBcontext.Brigades
                .Include(x => x.Commander)
                .ThenInclude(x => x.Rank)
                        .ThenInclude(x => x.Category)
                .Include(x => x.Division)
                    .ThenInclude(x => x.Corp)
                        .ThenInclude(x => x.Army)
                .Include(x => x.Division.Commander)
                    .ThenInclude(x => x.Rank)
                        .ThenInclude(x => x.Category)
                .Include(x => x.Division.Corp.Commander)
                    .ThenInclude(x => x.Rank)
                        .ThenInclude(x => x.Category)
                .Include(x => x.Division.Corp.Army.Commander)
                    .ThenInclude(x => x.Rank)
                        .ThenInclude(x => x.Category)
                .Where(x => x.PartsOfMilDistrId == SubjectID)
                .FirstOrDefaultAsync();
            return brigade == null ? null : brigade;
        }

        public async Task<PartsOfMilitaryDistrict> GetPartsOfMilitaryDistrictById(int? PartosOfMilitaryDistrictID)
        {
            return await DBcontext.PartsOfMilitaryDistricts
                .Include(x => x.Brigade)
                .Where(x => x.Id == PartosOfMilitaryDistrictID)
                .FirstOrDefaultAsync();
        }

        public async Task<RequestViewModel_6> GetAllPartsOfMilitaryDistrictAndAllCategoryAndKindOfEquipment()
        {
            return new RequestViewModel_6()
            {
                CategoriesOfMilitaryEquipment = await GetAllCategoriesOfEquipment(),
                KindsOfMilitaryEquipment = await GetAllKindOfEquipment(),
                PartsOfMilitaryDistricts = new ListOfPartsOfMilitaryDistrict()
                {
                    Armies = await GetAllArmiesFromPartsOfMilitaryDistrict(),
                    Corps = await GetAllCorpsFromPartsOfMilitaryDistrict(),
                    Divisions = await GetAllDivisionsFromPartsOfMilitaryDistrict(),
                    MilitaryBases = await GetAllMilitaryBases()
                }
            };

        }

        public async Task<List<KindOfMilitaryEquipment>> GetAllKindOfEquipment()
        {
            return await DBcontext.KindOfMilitaryEquipments
                .ToListAsync();
        }

        public async Task<List<CategoryOfMilitaryEquipment>> GetAllCategoriesOfEquipment()
        {
            return await DBcontext.CategoryOfMilitaryEquipments
                .ToListAsync();
        }

        public async Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetAllMilitaryEquipments(int CategoryOfMilitaryEquipmentId, int KindOfMilitaryEquipmentID)
        {
            var model = new List<MilitaryEquipmentsInMilitaryBase>();
            var militaryBases = await GetAllMilitaryBases();
            foreach (var mb in militaryBases)
            {
                model.AddRange(await GetMilitaryEquipmentsOfMilBaseByCategoryAndKindId(mb.Id, CategoryOfMilitaryEquipmentId, KindOfMilitaryEquipmentID));
            }
            model.OrderBy(x => x.MilitaryEquipmentsId);
            return model;
        }
        public async Task<IEnumerable<MilitaryEquipment>> GetListOFMilitaryEquipments()
        {
            return await DBcontext.MilitaryEquipments
                .Include(x => x.CategoryOfMilitaryEquipment)
                .OrderBy(x => x.CategoryOfMilitaryEquipment.Id)
                    .ToListAsync();
        }

        public async Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentInMilitaryBasesByCategoryAndKindIdAndAmount(int? CategoryOfMilitaryEquipment, int KindOfMilitaryequipment, int amount)
        {
            IEnumerable<MilitaryEquipmentsInMilitaryBase> militaryEquipmentsOfAllMb = await GetAllMilitaryEquipments((int)CategoryOfMilitaryEquipment, KindOfMilitaryequipment);
            var model = new List<MilitaryEquipmentsInMilitaryBase>();
            foreach (var item in militaryEquipmentsOfAllMb)
            {
                if (item.Amount > amount)
                {
                    model.Add(item);
                }
            }
            return model;
        }

        public async Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentsByKindId(int KindOfMilitaryEquipmentID)
        {
            return await DBcontext.MilitaryEquipmentsInMilitaryBases
                .Include(x => x.MilitaryBase)
                .Include(x => x.MilitaryEquipments)
                    .ThenInclude(x => x.CategoryOfMilitaryEquipment)
                .Include(x => x.MilitaryEquipments.KindOfMilitaryEquipment)
                .Where(x => x.MilitaryEquipments.KindOfMilitaryEquipmentId == KindOfMilitaryEquipmentID)
                .OrderBy(x => x.MilitaryEquipments.Id)
                    .ToListAsync();
        }

        public async Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentsByCategoryId(int CategoryOfMilitaryEquipmentId)
        {
            return await DBcontext.MilitaryEquipmentsInMilitaryBases
               .Include(x => x.MilitaryBase)
               .Include(x => x.MilitaryEquipments)
                   .ThenInclude(x => x.CategoryOfMilitaryEquipment)
               .Include(x => x.MilitaryEquipments.KindOfMilitaryEquipment)
               .Where(x => x.MilitaryEquipments.CategoryOfMilitaryEquipmentId == CategoryOfMilitaryEquipmentId)
               .OrderBy(x => x.MilitaryEquipments.Id)
                   .ToListAsync();
        }

        public async Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentsOfArmyByCategoryAndKindId(int ArmyID, int CategoryOfMilitaryEquipmentId, int KindOfMilitaryEquipmentID)
        {
            var MilitaryBasesOfArmy = await GetMilitaryBasesOfArmyByID(ArmyID);
            List<MilitaryEquipmentsInMilitaryBase> equipments = new List<MilitaryEquipmentsInMilitaryBase>();
            foreach (var mb in MilitaryBasesOfArmy)
            {
                equipments.AddRange(await GetMilitaryEquipmentsOfMilBaseByCategoryAndKindId(mb.Id, CategoryOfMilitaryEquipmentId, KindOfMilitaryEquipmentID));
            }
            return equipments;
        }

        public async Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentsOfCorpByCategoryAndKindId(int CorpID, int CategoryOfMilitaryEquipmentId, int KindOfMilitaryEquipmentID)
        {
            var MilitaryBasesOfCorp = await GetMilitaryBasesOfCorpByID(CorpID);
            List<MilitaryEquipmentsInMilitaryBase> equipments = new List<MilitaryEquipmentsInMilitaryBase>();
            foreach (var mb in MilitaryBasesOfCorp)
            {
                equipments.AddRange(await GetMilitaryEquipmentsOfMilBaseByCategoryAndKindId(mb.Id, CategoryOfMilitaryEquipmentId, KindOfMilitaryEquipmentID));
            }
            return equipments;
        }

        public async Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentsOfDivisionByCategoryAndKindId(int DivisionID, int CategoryOfMilitaryEquipmentId, int KindOfMilitaryEquipmentID)
        {
            var MilitaryBasesOfDivision = await GetMilitaryBasesOfDivisionByID(DivisionID);
            List<MilitaryEquipmentsInMilitaryBase> equipments = new List<MilitaryEquipmentsInMilitaryBase>();
            foreach (var mb in MilitaryBasesOfDivision)
            {
                equipments.AddRange(await GetMilitaryEquipmentsOfMilBaseByCategoryAndKindId(mb.Id, CategoryOfMilitaryEquipmentId, KindOfMilitaryEquipmentID));
            }
            return equipments;
        }


        public async Task<IEnumerable<MilitaryEquipmentsInMilitaryBase>> GetMilitaryEquipmentsOfMilBaseByCategoryAndKindId(int MbID, int CategoryOfMilitaryEquipmentId, int KindOfMilitaryEquipmentID)
        {
            var equipments = await DBcontext.MilitaryEquipmentsInMilitaryBases
               .Include(x => x.MilitaryBase)
               .Include(x => x.MilitaryEquipments)
                   .ThenInclude(x => x.CategoryOfMilitaryEquipment)
               .Include(x => x.MilitaryEquipments.KindOfMilitaryEquipment)
               .Where(x => x.MilitaryBaseId == MbID)
               .Where(x => x.MilitaryEquipments.KindOfMilitaryEquipmentId == (KindOfMilitaryEquipmentID > 0 ? KindOfMilitaryEquipmentID : x.MilitaryEquipments.KindOfMilitaryEquipmentId))
               .Where(x => x.MilitaryEquipments.CategoryOfMilitaryEquipmentId == (CategoryOfMilitaryEquipmentId > 0 ? CategoryOfMilitaryEquipmentId : x.MilitaryEquipments.CategoryOfMilitaryEquipmentId))
               .OrderBy(x => x.MilitaryEquipments.Id)
               .ToListAsync();
            var model = FormatEquipmentColumn(equipments);
            return model;
        }

        private List<MilitaryEquipmentsInMilitaryBase> FormatEquipmentColumn(List<MilitaryEquipmentsInMilitaryBase> MilitaryEquipments)
        {
            List<int> UnicKey = new List<int>();
            List<int> IndexForDeleting = new List<int>();
            foreach (var equipment in MilitaryEquipments)
            {
                if (UnicKey.Contains(equipment.MilitaryEquipmentsId))
                {
                    var firstElement = MilitaryEquipments.Find(x => x.MilitaryEquipmentsId == equipment.MilitaryEquipmentsId);
                    firstElement.Amount += equipment.Amount;
                    IndexForDeleting.Add(MilitaryEquipments.IndexOf(equipment));
                }
                else
                {
                    UnicKey.Add(equipment.MilitaryEquipmentsId);
                }
            }
            for (int i = 0; i < IndexForDeleting.Count(); i++)
            {
                MilitaryEquipments.RemoveAt(IndexForDeleting.ElementAt(i) - i);
            }
            return MilitaryEquipments;
        }
        private List<MilitaryWeaponsInMilitaryBase> FormatWeaponColumn(List<MilitaryWeaponsInMilitaryBase> MilitaryWeapons)
        {
            List<int> UnicKey = new List<int>();
            List<int> IndexForDeleting = new List<int>();
            foreach (var weapons in MilitaryWeapons)
            {
                if (UnicKey.Contains(weapons.MilitaryWeaponId))
                {
                    var firstElement = MilitaryWeapons.Find(x => x.MilitaryWeaponId == weapons.MilitaryWeaponId);
                    firstElement.Amount += weapons.Amount;
                    IndexForDeleting.Add(MilitaryWeapons.IndexOf(weapons));
                }
                else
                {
                    UnicKey.Add(weapons.MilitaryWeaponId);
                }
            }
            for (int i = 0; i < IndexForDeleting.Count(); i++)
            {
                MilitaryWeapons.RemoveAt(IndexForDeleting.ElementAt(i) - i);
            }
            return MilitaryWeapons;
        }

        public async Task<IEnumerable<BuldingsInMilitaryBase>> GetBuldingsInMilitaryBase(int? MilitaryBaseId, bool IsForDislocation)
        {
            return await DBcontext.BuldingsInMilitaryBases
                    .Include(x => x.Bulding)
                    .Include(x => x.MilitaryBase)
                    .Where(x => x.MilitaryBaseId == MilitaryBaseId)
                    .Where(x => x.Bulding.ForAccommodation == IsForDislocation)
                    .ToListAsync();
        }

        public async Task<IEnumerable<MilitaryWeaponsInMilitaryBase>> GetAllMilitaryWeapons(int KindOfMilitaryWeaponID)
        {
            var militaryBases = await GetAllMilitaryBases();
            List<MilitaryWeaponsInMilitaryBase> weapons = new List<MilitaryWeaponsInMilitaryBase>();
            foreach (var mb in militaryBases)
            {
                weapons.AddRange(await GetMilitaryWeaponsOfMilitaryBaseByKindId(mb.Id, KindOfMilitaryWeaponID));
            }
            return weapons;
        }

        public async Task<IEnumerable<MilitaryWeaponsInMilitaryBase>> GetMilitaryWeaponsOfArmyByKindId(int ArmyID, int KindOfMilitaryWeaponID)
        {
            var MilitaryBasesOfArmy = await GetMilitaryBasesOfArmyByID(ArmyID);
            List<MilitaryWeaponsInMilitaryBase> weapons = new List<MilitaryWeaponsInMilitaryBase>();
            foreach (var mb in MilitaryBasesOfArmy)
            {
                weapons.AddRange(await GetMilitaryWeaponsOfMilitaryBaseByKindId(mb.Id, KindOfMilitaryWeaponID));
            }
            return weapons;
        }

        public async Task<IEnumerable<MilitaryWeaponsInMilitaryBase>> GetMilitaryWeaponsOfCorpByKindId(int CorpID, int KindOfMilitaryWeaponID)
        {

            var MilitaryBasesOfCorp = await GetMilitaryBasesOfCorpByID(CorpID);
            List<MilitaryWeaponsInMilitaryBase> weapons = new List<MilitaryWeaponsInMilitaryBase>();
            foreach (var mb in MilitaryBasesOfCorp)
            {
                weapons.AddRange(await GetMilitaryWeaponsOfMilitaryBaseByKindId(mb.Id, KindOfMilitaryWeaponID));
            }
            return weapons;

        }

        public async Task<IEnumerable<MilitaryWeaponsInMilitaryBase>> GetMilitaryWeaponsOfDivisionByKindId(int DivisionID, int KindOfMilitaryWeaponID)
        {
            var MilitaryBasesOfDivision = await GetMilitaryBasesOfDivisionByID(DivisionID);
            List<MilitaryWeaponsInMilitaryBase> weapons = new List<MilitaryWeaponsInMilitaryBase>();
            foreach (var mb in MilitaryBasesOfDivision)
            {
                weapons.AddRange(await GetMilitaryWeaponsOfMilitaryBaseByKindId(mb.Id, KindOfMilitaryWeaponID));
            }
            return weapons;
        }

        public async Task<IEnumerable<MilitaryWeaponsInMilitaryBase>> GetMilitaryWeaponsOfMilitaryBaseByKindId(int MbID, int KindOfMilitaryWeaponID)
        {
            var weapons = await DBcontext.MilitaryWeaponsInMilitaryBases
               .Include(x => x.MilitaryBase)
               .Include(x => x.MilitaryWeapon)
                   .ThenInclude(x => x.KindOfMilitaryWeapon)
               .Where(x => x.MilitaryBaseId == MbID)
               .Where(x => x.MilitaryWeapon.KindOfMilitaryWeaponId == (KindOfMilitaryWeaponID > 0 ? KindOfMilitaryWeaponID : x.MilitaryWeapon.KindOfMilitaryWeaponId))
               .OrderBy(x => x.MilitaryWeaponId)
               .ToListAsync();

            var model = FormatWeaponColumn(weapons);
            return model.OrderBy(x => x.MilitaryWeapon.KindOfMilitaryWeaponId);
        }

        public async Task<List<KindOfMilitaryWeapon>> GetAllKindOfWeapons()
        {
            return await DBcontext.KindOfMilitaryWeapons.ToListAsync();
        }

        public Task<IEnumerable<KindOfMilitaryWeapon>> GetListOFKindOfMilitaryWeapons()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<MilitaryWeaponsInMilitaryBase>> GetMilitaryWeaponInMilitaryBasesByKindIdAndAmount(int? KindOfMilitaryWeaponID, int amount)
        {
            IEnumerable<MilitaryWeaponsInMilitaryBase> militaryWeaponsOfAllMb = await GetAllMilitaryWeapons((int)KindOfMilitaryWeaponID);
            var model = new List<MilitaryWeaponsInMilitaryBase>();
            foreach (var item in militaryWeaponsOfAllMb)
            {
                if (item.Amount > amount)
                {
                    model.Add(item);
                }
            }
            return model;
        }

        public async Task<ListOfPartsOfMilitaryBase> GetAllPartsOfMilitaryBases()
        {

            var AllRotas = await GetAllRotas();
            var AllPlatoons = await GetAllPlatoons();
            var AllDepartaments = await GetAllDepartaments();

            return new ListOfPartsOfMilitaryBase() { Departaments = AllDepartaments, Platoons = AllPlatoons, Rotas = AllRotas };
        }

        private async Task<List<Rotum>> GetAllRotas()
        {
            return await DBcontext.Rota
                .ToListAsync();
        }

        private async Task<List<Platoon>> GetAllPlatoons()
        {
            return await DBcontext.Platoons
             .ToListAsync();
        }
        private async Task<List<Departament>> GetAllDepartaments()
        {
            return await DBcontext.Departaments
             .ToListAsync();
        }

        public async Task<(Army, int)> GetArmyByMaxOrMinAmountOfMilitaryBases(bool MaxOrMin)
        {
            var AllArmies = await GetAllArmiesFromPartsOfMilitaryDistrict();
            var Army = AllArmies.FirstOrDefault();
            var militarybasesOfArmy = await GetMilitaryBasesOfArmyByID(Army.Id);
            int maxOrMinValue = militarybasesOfArmy.Count();
            for (int i = 1; i < AllArmies.Count(); i++)
            {
                int num = (await GetMilitaryBasesOfArmyByID(AllArmies.ElementAt(i).Id)).Count();
                if (maxOrMinValue > num != MaxOrMin)
                {
                    maxOrMinValue = num;
                    Army = AllArmies.ElementAt(i);
                }
            }
            return (Army,maxOrMinValue);
        }

        public async Task<(Corps, int)> GetCorpByMaxOrMinAmountOfMilitaryBases(bool MaxOrMin)
        {
            var AllCorps= await GetAllCorpsFromPartsOfMilitaryDistrict();
            var Corp = AllCorps.FirstOrDefault();
            var militarybasesOfCorp = await GetMilitaryBasesOfCorpByID(Corp.Id);
            int maxOrMinValue = militarybasesOfCorp.Count();
            for (int i = 1; i < AllCorps.Count(); i++)
            {
                int num = (await GetMilitaryBasesOfCorpByID(AllCorps.ElementAt(i).Id)).Count();
                if (maxOrMinValue > num != MaxOrMin)
                {
                    maxOrMinValue = num;
                    Corp = AllCorps.ElementAt(i);
                }
            }
            return (Corp, maxOrMinValue);
        }

        public async Task<(Division, int)> GetDivisionByMaxOrMinAmountOfMilitaryBases(bool MaxOrMin)
        {
            var AllDivisions = await GetAllDivisionsFromPartsOfMilitaryDistrict();
            var Division = AllDivisions.FirstOrDefault();
            var militarybasesOfDiv = await GetMilitaryBasesOfDivisionByID(Division.Id);
            int maxOrMinValue = militarybasesOfDiv.Count();
            for (int i = 1; i < AllDivisions.Count(); i++)
            {
                int num = (await GetMilitaryBasesOfDivisionByID(AllDivisions.ElementAt(i).Id)).Count();
                if (maxOrMinValue > num != MaxOrMin)
                {
                    maxOrMinValue = num;
                    Division = AllDivisions.ElementAt(i);
                }
            }
            return (Division, maxOrMinValue);
        }
    }
}
