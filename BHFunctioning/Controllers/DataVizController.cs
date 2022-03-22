﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using BHFunctioning.Models;
using BHFunctioning.Data;
using BHFunctioning.Models;
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
        public ActionResult Create(HealthData obj)
        {
            System.Diagnostics.Debug.WriteLine("Medical: " + obj.Medical);
            System.Diagnostics.Debug.WriteLine("ChildDX: "+obj.ChildDx);
            System.Diagnostics.Debug.WriteLine("Selfharm: "+obj.Selfharm);
            System.Diagnostics.Debug.WriteLine("Sofas: "+ obj.Sofas);
            System.Diagnostics.Debug.WriteLine("ClinicalStage: "+obj.ClinicalStage);
            System.Diagnostics.Debug.WriteLine("Circadian: " + obj.Circadian);
            System.Diagnostics.Debug.WriteLine("Tripartite: "+obj.Tripartite);
            System.Diagnostics.Debug.WriteLine("Psychosis: "+obj.Psychosis);
            System.Diagnostics.Debug.WriteLine("NEET: "+ obj.NEET);

            //var _id = _db.Healthdata.SingleOrDefault(
            //    a => a.Medical == obj.Medical && 
            //    a.ChildDx == obj.ChildDx && 
            //    a.Selfharm == obj.Selfharm &&
            //    a.Sofas == obj.Sofas &&
            //    a.ClinicalStage == obj.ClinicalStage &&
            //    a.Circadian == obj.Circadian &&
            //    a.Tripartite == obj.Tripartite &&
            //    a.Psychosis == obj.Psychosis &&
            //    a.NEET == obj.NEET);


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
