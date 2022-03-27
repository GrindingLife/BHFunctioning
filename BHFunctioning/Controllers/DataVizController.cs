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

        // GET: DataVizController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: DataVizController/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> GenerateHealthData()
        {
            Random rand = new();
            for (int i = 1; i < 1000; i++)
            {
                string str = i.ToString();
                HealthData tmp = new();
                tmp.Id = str;
                tmp.Id = i.ToString();
                tmp.NEET = rand.Next() % 2;
                tmp.Selfharm = rand.Next() % 2;
                tmp.Psychosis = rand.Next() % 2;
                tmp.Medical = rand.Next() % 2;
                tmp.ChildDx = rand.Next() % 2;
                tmp.Circadian = rand.Next() % 2;
                tmp.Tripartite = rand.Next() % 4 + 1;
                tmp.ClinicalStage = rand.Next() % 3 + 1;
                tmp.Sofas = rand.Next() % 12 + 1;

                _db.HealthData.Add(tmp);
                await _db.SaveChangesAsync();               

            }
            return RedirectToAction(nameof(Index));
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

            return RedirectToAction(nameof(Index));

        }

        //[HttpGet]
        //public JsonResult HealthChart(string id)
        //{
        //    var healthList = _db.HealthData.();
        //    return Json(healthList);
        //}

        [HttpGet]
        public ActionResult VisualiseData()
        {
            var id = TempData["id"];
            if (id != null)
            {
                //List<HealthDataFuture> DataList = new();
                //var HealthDatas = _db.HealthDataFuture;
                //foreach (HealthDataFuture data in HealthDatas)
                //{
                //    if (data.Id == id.ToString())
                //    {
                //        DataList.Add(data);
                //    }
                //}


                return View(id);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AjaxMethod(string id)
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
                            {
                            "Months", "SOFAS"
                            });
            //FOR SOME REASON IT KEEPS READING THE FIRST DATABASE ENTRY ONLY SO THERE ALL THE ENTRIES ARE THE SAME VALUE
            var HealthDatas = _db.HealthDataFuture;
            foreach (HealthDataFuture data in HealthDatas)
            {
                if (data.Id == id)
                {
                    chartData.Add(new object[]
                        {
                            data.Month, data.Sofas
                        });
                }
            }

            return Json(chartData);
        }

        // POST: DataVizController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(HealthData obj)
        {
            //System.Diagnostics.Debug.WriteLine("Medical: " + obj.Medical);
            //System.Diagnostics.Debug.WriteLine("ChildDX: " + obj.ChildDx);
            //System.Diagnostics.Debug.WriteLine("Selfharm: " + obj.Selfharm);
            //System.Diagnostics.Debug.WriteLine("Sofas: " + obj.Sofas);
            //System.Diagnostics.Debug.WriteLine("ClinicalStage: " + obj.ClinicalStage);
            //System.Diagnostics.Debug.WriteLine("Circadian: " + obj.Circadian);
            //System.Diagnostics.Debug.WriteLine("Tripartite: " + obj.Tripartite);
            //System.Diagnostics.Debug.WriteLine("Psychosis: " + obj.Psychosis);
            //System.Diagnostics.Debug.WriteLine("NEET: " + obj.NEET);

            
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
            //data.Medical == obj.Medical &&
            //data.ChildDx == obj.ChildDx &&
            //data.Selfharm == obj.Selfharm &&
            //data.Sofas == obj.Sofas &&
            //data.ClinicalStage == obj.ClinicalStage &&
            //data.Circadian == obj.Circadian &&
            //data.Tripartite == obj.Tripartite &&
            //data.Psychosis == obj.Psychosis &&
            //data.NEET == obj.NEET &&
            //data.Sofas == obj.Sofas
            TempData["id"] = tmp.Id;
            if (tmp != null) {                 
                return RedirectToAction("VisualiseData");
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

        // GET: DataVizController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: DataVizController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: DataVizController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: DataVizController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
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
