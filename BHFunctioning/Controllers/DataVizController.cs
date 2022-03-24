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

        // POST: DataVizController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(HealthData obj)
        {
            System.Diagnostics.Debug.WriteLine("Medical: " + obj.Medical);
            System.Diagnostics.Debug.WriteLine("ChildDX: " + obj.ChildDx);
            System.Diagnostics.Debug.WriteLine("Selfharm: " + obj.Selfharm);
            System.Diagnostics.Debug.WriteLine("Sofas: " + obj.Sofas);
            System.Diagnostics.Debug.WriteLine("ClinicalStage: " + obj.ClinicalStage);
            System.Diagnostics.Debug.WriteLine("Circadian: " + obj.Circadian);
            System.Diagnostics.Debug.WriteLine("Tripartite: " + obj.Tripartite);
            System.Diagnostics.Debug.WriteLine("Psychosis: " + obj.Psychosis);
            System.Diagnostics.Debug.WriteLine("NEET: " + obj.NEET);

            var iid = _db.HealthData.Select(
                a => a.Medical == obj.Medical &&
                a.ChildDx == obj.ChildDx &&
                a.Selfharm == obj.Selfharm &&
                a.Sofas == obj.Sofas &&
                a.ClinicalStage == obj.ClinicalStage &&
                a.Circadian == obj.Circadian &&
                a.Tripartite == obj.Tripartite &&
                a.Psychosis == obj.Psychosis &&
                a.NEET > 5);
            var objs = _db.HealthData.SingleOrDefault(
                a => a.Medical == obj.Medical &&
                a.ChildDx == obj.ChildDx &&
                a.Selfharm == obj.Selfharm &&
                a.Sofas == obj.Sofas &&
                a.ClinicalStage == obj.ClinicalStage &&
                a.Circadian == obj.Circadian &&
                a.Tripartite == obj.Tripartite &&
                a.Psychosis == obj.Psychosis &&
                a.NEET == obj.NEET);
            //System.Diagnostics.Debug.WriteLine("Medical: " + objs.Medical);
            //System.Diagnostics.Debug.WriteLine("ChildDX: " + objs.ChildDx);
            //System.Diagnostics.Debug.WriteLine("Selfharm: " + objs.Selfharm);
            //System.Diagnostics.Debug.WriteLine("Sofas: " + objs.Sofas);
            //System.Diagnostics.Debug.WriteLine("ClinicalStage: " + objs.ClinicalStage);
            //System.Diagnostics.Debug.WriteLine("Circadian: " + objs.Circadian);
            //System.Diagnostics.Debug.WriteLine("Tripartite: " + objs.Tripartite);
            //System.Diagnostics.Debug.WriteLine("Psychosis: " + objs.Psychosis);
            //System.Diagnostics.Debug.WriteLine("NEET: " + objs.NEET);
            Random rand = new();
            for(int i = 1; i < 1000; i++)
            {
                HealthData tmp = new();
                tmp.Id = i.ToString();
                tmp.NEET = rand.Next(0, 1);
                tmp.Selfharm = rand.Next(0, 1);
                tmp.Psychosis = rand.Next(0, 1);
                tmp.Medical = rand.Next(0,1);
                tmp.ChildDx = rand.Next(0, 1);
                tmp.Circadian = rand.Next(0, 1);
                tmp.Tripartite = rand.Next(1, 4);
                tmp.ClinicalStage = rand.Next(1, 3);
                tmp.Sofas = rand.Next(1, 12);
                var res = await _db.HealthData.AddAsync(tmp);
                
                
                if(res == null)
                {
                    Console.WriteLine("Error");
                    return View("Error");

                }

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
