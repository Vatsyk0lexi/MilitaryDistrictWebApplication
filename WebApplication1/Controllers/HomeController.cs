using Azure;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using WebApplication1.Models;
using WebApplication1.Models.ViewModels;
using WebApplication1.Services;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMilitaryBaseService militaryBaseService;
        private readonly IMilitaryService militaryService;

        public HomeController(ILogger<HomeController> logger, IMilitaryBaseService militaryBaseService, IMilitaryService militaryService)
        {
            _logger = logger;
            this.militaryBaseService = militaryBaseService;
            this.militaryService = militaryService;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Request_1(string? responce)
        {

            if (responce != null)
            {
                var model = await militaryBaseService.ResponceForFirstRequest(responce);
                ViewBag.Head = model.responce;
                return View(model);
            }
            else
            {
                var model = await militaryBaseService.GetAllPartsOfMilitaryDistrict();
                ViewBag.Head = "Отримати перелік всіх частин військового округу, зазначеної армії, дивізії, корпусу та їх\r\nкомандирів.";
                return View(model);
            }

        }

        [HttpGet]
        public async Task<IActionResult> Request_2(string? responce, int? RankId, int? CategoryID)
        {

            if (responce != null)
            {
                responce = $"{responce}_{RankId}_{CategoryID}";
                var parameters = responce.Split('_');
                var model = await militaryService.GetMilitariesByCategoryAtPartOfMilitaryDistrict(parameters);
                ViewBag.Head = model.responce;
                return View(model);
            }
            else
            {
                var model = await militaryService.GetAllPartsOfMilitaryDistrictAndAllRanksOfCategory();
                ViewBag.Head = "Отримати дані по офіцерському складу в цілому і по офіцерському складу зазначеного звання\r\nвсіх частин військового округу, окремої армії, дивізії, корпусу, військової частини";
                return View(model);
            }

        }
        [HttpGet]
        public IActionResult Request_3(string? responce, int? RankId, int? CategoryID)
        {
            return RedirectToAction("Request_2", new { responce, RankId, CategoryID });
        }
        [HttpGet]
        public async Task<IActionResult> Request_4(int? MilitaryID, int? CommanderID)
        {
            if (MilitaryID != null)
            {
                var model = await militaryService.GetChainOfCommanders(MilitaryID, CommanderID);
                ViewBag.Head = $"Отримати ланцюжок підпорядкованості знизу доверху для '{model.SelectedMilitary.FullName}'";
                return View(model);
            }
            else
            {
                return RedirectToAction("Request_2");
            }
            
        }
        [HttpGet]
        public async Task<IActionResult> Request_5(int? Id)
        {
            if (Id == null)
            {
                ViewBag.Head = "Отримати перелік місць дислокації всіх частин військового округу, окремої армії, дивізії, корпуси, військової частини ";
                var placesOfDeployment = await militaryBaseService.GetAllPlacesOfDeployment();
                return View(placesOfDeployment);
            }
            var militaryBasesOfSelectedPlacesOfDeployment = await militaryBaseService.GetMBByPlacesOfDeploymentID(Id);
            return View("Request_1", new RequestViewModel() { MilitaryBases = militaryBasesOfSelectedPlacesOfDeployment });
        }
        [HttpGet]
        public async Task<IActionResult> Request_6(int CategoryOfMilitaryEquipmentId, int KindOfMilitaryEquipmentID, string? responce)
        {
            if (responce != null)
            {
                var model = await militaryService.ResponceForSixRequest(CategoryOfMilitaryEquipmentId, KindOfMilitaryEquipmentID, responce);
                model.MilitaryEquipmentsInMilitaryBases.OrderBy(x => x.MilitaryEquipments.Id);
                return View(model);
            }
            else
            {
                ViewBag.Head = "Отримати дані про наявність бойової техніки в цілому та з урахуванням зазначеної категорії або виду в усіх частинах військового округу, окремої армії, дивізії, корпусі, військовій частині";
                var model = await militaryService.GetItemsForSixRequest();
                return View(model);
            }

        }
        public async Task<IActionResult> Request_7(int? MilitaryBaseId, bool IsForDislocation)
        {
            if (MilitaryBaseId == null)
            {
                ViewBag.Head = "Отримати перелік споруд зазначеної військової частини, перелік споруд, де дислоковано більше одного підрозділу, де не дислоковано жодного підрозділу";
                var model = new RequestViewModel_7() { MilitaryBases = (List<MilitaryBase>)await militaryBaseService.GetAllMilitariBases() };
                return View(model);
            }
            else
            {
                var model = new RequestViewModel_7() { BuldingsInMilitaryBases = (List<BuldingsInMilitaryBase>)await militaryBaseService.GetBuldingsInMilitaryBase(MilitaryBaseId, IsForDislocation) };
                return View(model);
            }

        }

        public async Task<IActionResult> Request_8(int? KindOfMilitaryEquipmentID, bool IsMoreThanFive)
        {
            if (KindOfMilitaryEquipmentID == null)
            {
                ViewBag.Head = "Отримати перелік військових частин, в яких число одиниць зазначеного виду бойової техніки більше 5";
                var model = new RequestViewModel_8() { KindsOfMilitaryEquipment = await militaryBaseService.GetListOFKindsOfMilitaryEquipments() };
                return View(model);
            }
            else
            {
                var model = new RequestViewModel_8() { MilitaryEquipmentsInMilitaryBases = await militaryBaseService.GetMilitaryEquipmentInMilitaryBases(KindOfMilitaryEquipmentID) };
                return View(model);
            }
        }

        public async Task<IActionResult> Request_9(int KindOfMilitaryWeaponID, string? responce)
        {
            if (responce != null)
            {
                var model = await militaryBaseService.ResponceForNineRequest(KindOfMilitaryWeaponID, responce);
                return View(model);
            }
            else
            {
                ViewBag.Head = "Отримати дані про наявність озброєння в цілому і з урахуванням зазначеного виду в усіх частинах військового округу, окремої армії, дивізії, корпусі, військовій частині";
                var model = await militaryBaseService.GetItemsForNineRequest();
                return View(model);
            }

        }

        public async Task<IActionResult> Request_10(string? responce)
        {
            if (responce != null)
            {
                var model = await militaryService.GetSpecialitiesOfPartsOfMilDistricy(responce);
                return View(model);
            }
            else
            {
                ViewBag.Head = "Отримати перелік військових спеціальностей, за якими в окрузі, окремої армії, дивізії, корпусі, військової частини більше п'яти фахівців";
                var model = new RequestViewModel_10() { PartsOfMilitaryDistricts = (await militaryBaseService.GetItemsForNineRequest()).PartsOfMilitaryDistricts };
                return View(model);
            }

        }
        public async Task<IActionResult> Request_11(int SpecialityID, string? responce)
        {
            if (responce != null)
            {
                var model = await militaryService.GetMilitariesArPartOfMilitaryDistictBySpecialityID(SpecialityID, responce);
                return View(model);
            }
            else
            {
                ViewBag.Head = "Отримати перелік військовослужбовців зазначеної спеціальності в окрузі, окремої армії, дивізії, корпусі, військової частини, у зазначеному підрозділі деякої військової частини";
                var model = await militaryService.GetItemsForElevenRequest();
                return View(model);
            }

        }
        public async Task<IActionResult> Request_12(int? KindOfMilitaryWeaponID, bool IsMoreThanTen)
        {
            if (KindOfMilitaryWeaponID == null)
            {
                ViewBag.Head = "Отримати перелік військових частин, в яких число одиниць зазначеного виду озброєння більше 10";
                var model = new RequestViewModel_12() { KindOfMilitaryWeapons = await militaryBaseService.GetListOFKindsOfMilitaryWeapons() };
                return View(model);
            }
            else
            {
                var model = new RequestViewModel_12() { MilitaryWeaponsInMilitaryBases = await militaryBaseService.GetMilitaryWeaponsInMilitaryBases(KindOfMilitaryWeaponID) };
                return View(model);
            }

        }
        public async Task<IActionResult> Request_13(string? responce, bool MaxOrMin)
        {
            if (responce == null)
            {
                ViewBag.Head = "«Отримати дані про армію, дивізії, корпусі, в які входить найбільше (найменше) військових частин.»";
                return View();
            }
            else
            {
                var model = await militaryBaseService.GetResponceFor13Request(responce, MaxOrMin);
                return View(model);
            }

        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}