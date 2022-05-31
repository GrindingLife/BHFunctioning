using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BHFunctioning.Models;
using BHFunctioning.Data;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace BHFunctioning.Controllers
{
    [Authorize]
    public class DataVizController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<IdentityUser> _userManager;

        public DataVizController(ApplicationDbContext db, UserManager<IdentityUser> userManager)
        {
            _db = db;

            _userManager = userManager;
        }
        // GET: DataVizController
        public ActionResult Index()
        {
            return View();
        }



        [HttpPost]
        public IActionResult GetAlert(bool NEET, bool Selfharm, bool Psychosis, bool Medical, bool ChildDx, bool Circadian, int Tripartite, int ClinicalStage, int SOFAS)
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

            if (temp == null)
            {
                ModelState.AddModelError("", "Error finding health data, returns null");
                return RedirectToAction("Index");
            }

            var ip = Request.HttpContext.Connection.RemoteIpAddress.ToString();
            InputLog userInput = new();
            userInput.NEET = NEET;
            userInput.Selfharm = Selfharm;
            userInput.Psychosis = Psychosis;
            userInput.Medical = Medical;
            userInput.ChildDx = ChildDx;
            userInput.Circadian = Circadian;
            userInput.Tripartite = Tripartite;
            userInput.ClinicalStage = ClinicalStage;
            userInput.Sofas = SOFAS;
            userInput.UserName = _userManager.GetUserName(User);
            userInput.IpAddress = ip.ToString();
            userInput.DateTime = DateTime.Now;

            _db.InputLogs.Add(userInput);
            _db.SaveChanges();


            string txt;
            if (temp.Alert == 1)
            {
                txt = "Alert! Predicting a significant (10 points) drop in SOFAS score (after 3 months) ";
            }
            else txt = "";

            return Json(txt);
        }

        [HttpPost]
        public IActionResult DensityLineChart(int sofas)
        {
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
                            {
                                "X", "Y",
                            });
            chartData.Add(new object[]
                            {
                                24, 0,
                            });
            /* Format data to graph density line chart*/
            List<DensityData> list = _db.DensityDatas.OrderBy(o => o.X).ToList();
            double sum = 0;
            foreach (var ele in list)
            {
                sum += Convert.ToDouble(ele.X);
                if (double.Parse(ele.X) > sofas)
                {
                    //create star for position to show this is user input area
                    chartData.Add(new object[]
                    {
                    Convert.ToDouble(ele.X), Convert.ToDouble(ele.Y)
                    });
                }
                else
                {
                    chartData.Add(new object[]
                    {
                    ele.X, ele.Y, null, null
                    });
                }
            }

            chartData.Add(sum / list.Count);

            return Json(chartData);
        }

        [HttpPost]
        public IActionResult PieChart(bool NEET, bool Selfharm, bool Psychosis, bool Medical, bool ChildDx, bool Circadian, int Tripartite, int ClinicalStage, int SOFAS)
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

            if (temp == null)
            {
                ModelState.AddModelError("", "Error finding health data, returns null");
                return RedirectToAction("Index");
            }

            var id = temp.Id;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
                            {
                                "Cluster", "Probability"
                            });

            chartData.Add(new object[]
                        {
                                "Constant", temp.Constant
                        });


            chartData.Add(new object[]
                        {
                                "Improvement", temp.Up
                        });
            chartData.Add(new object[]
                            {
                                "Deterioration", temp.Down
                            });

            return Json(chartData);
        }
        [HttpPost]
        public IActionResult PieChart2(bool NEET, bool Selfharm, bool Psychosis, bool Medical, bool ChildDx, bool Circadian, int Tripartite, int ClinicalStage, int SOFAS)
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

            if (temp == null)
            {
                ModelState.AddModelError("", "Error finding health data, returns null");
                return RedirectToAction("Index");
            }

            var id = temp.Id;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
                            {
                                "Threshold", "Probability"
                            });

            chartData.Add(new object[]
                        {
                                "50", temp.Threshold_50
                        });


            chartData.Add(new object[]
                        {
                                "60", temp.Threshold_60
                        });
            chartData.Add(new object[]
                            {
                                "70", temp.Threshold_70
                            });

            return Json(chartData);
        }


        [HttpPost]
        public IActionResult FutureSofasGraph(bool NEET, bool Selfharm, bool Psychosis, bool Medical, bool ChildDx, bool Circadian, int Tripartite, int ClinicalStage, int SOFAS)
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

            if (temp == null)
            {
                ModelState.AddModelError("", "Error finding health data, returns null");
                return RedirectToAction("Index");
            }

            var id = temp.Id;
            List<object> chartData = new List<object>();
            chartData.Add(new object[]
                            {
                                "Months", "Mean", "- Std. Deviation", "+ Std. Deviation"
                            });
            chartData.Add(new object[]
                            {
                                0, SOFAS*10+5, SOFAS*10+5, SOFAS*10+5
                            });

            if (temp != null)
            {
                chartData.Add(new object[]
                            {
                                3, temp.Mean, (temp.Mean - temp.StandardDeviation), (temp.Mean + temp.StandardDeviation)
                            });

            }
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
