using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BHFunctioning.Models;
using BHFunctioning.Data;
using BHFunctioning.Models;
using System.Linq;

namespace BHFunctioning.Controllers
{
    public class DataVizController : Controller
    {
        private readonly ApplicationDbContext _db;
        public DataVizController(ApplicationDbContext db)
        {
            _db = db;

        }
        // GET: DataVizController
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> RemoveHealthData()
        {

            List<HealthData> lis = new();
            lis = _db.HealthData.ToList();
            foreach (HealthData ele in lis)
            {
                _db.HealthData.Remove(ele);
                await _db.SaveChangesAsync();
            }

            //List<HealthDataFuture> list = new();
            //list = _db.HealthDataFuture.ToList();
            //foreach (HealthDataFuture ele in list)
            //{
            //    _db.HealthDataFuture.Remove(ele);
            //    await _db.SaveChangesAsync();
            //}

            return RedirectToAction(nameof(Index));

        }

        [HttpPost]
        public ActionResult AddCSV()
        {
            CsvRead r = new();
            r.readToDB(_db);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public ActionResult VisualiseData()
        {
            var id = TempData["id"];
            if (id != null)
            {
                return View(id);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AjaxMethod(bool NEET, bool Selfharm, bool Psychosis, bool Medical, bool ChildDx, bool Circadian, int Tripartite, int ClinicalStage, int SOFAS)
        {

            var temp = _db.HealthData.FirstOrDefault(
                a => a.Medical == Medical &&
                a.ChildDx == ChildDx &&
                a.Selfharm == Selfharm &&
                a.Sofas == SOFAS &&
                a.ClinicalStage == ClinicalStage &&
                a.Circadian == Circadian &&
                a.Tripartite == Tripartite &&
                a.Psychosis == Psychosis &&
                a.NEET == NEET);

            if(temp == null)
            {
                return RedirectToAction("Index");
            }

            var id = temp.Id;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
                            {
                            "Months", "SOFAS"
                            });
            chartData.Add(new object[]
                            {
                            0, SOFAS
                            });
            List<HealthDataFuture> DataList6 = _db.HealthDataFuture.Where(a => a.HealthDataFK == id && a.Month == 6).OrderByDescending(b => b.Month).ToList();
            List<HealthDataFuture> DataList12 = _db.HealthDataFuture.Where(a => a.HealthDataFK == id && a.Month == 12).OrderByDescending(b => b.Month).ToList();

            int avgSofas6 = 0;
            int avgSofas12 = 0;

            foreach(HealthDataFuture tmp in DataList6)
            {
                avgSofas6 += tmp.Sofas;
            }
            avgSofas6 = avgSofas6 / DataList6.Count;
            chartData.Add(new object[]
                        {
                            6, avgSofas6
                        });
            foreach (HealthDataFuture tmp in DataList12)
            {
                avgSofas12 += tmp.Sofas;
            }
            avgSofas12 = avgSofas12 / DataList12.Count;
            chartData.Add(new object[]
                        {
                            12, avgSofas12
                        });

            return Json(chartData);
        }

        // POST: DataVizController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Index(HealthData obj)
        {

            var tmp = _db.HealthData.FirstOrDefault(
                a => a.Medical == obj.Medical &&
                a.ChildDx == obj.ChildDx &&
                a.Selfharm == obj.Selfharm &&
                a.Sofas == obj.Sofas &&
                a.ClinicalStage == obj.ClinicalStage &&
                a.Circadian == obj.Circadian &&
                a.Tripartite == obj.Tripartite &&
                a.Psychosis == obj.Psychosis &&
                a.NEET == obj.NEET);

            
            if (tmp != null)
            {
                TempData["id"] = tmp.Id;
                return View(obj);
            }
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

       
    }
}
