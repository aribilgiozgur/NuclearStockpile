using NuclearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NuclearProject.Controllers
{
    public class WarheadController : Controller
    {
   
        //
        // GET: /Warhead/
        
        public ActionResult Index()
        {
                WarheadType w = new WarheadType();
                List<WarheadType> warheads = w.GetAll();
                return View(warheads);
        }

        public ActionResult View(int Id) {
            WarheadType w = new WarheadType(Id);
            return View(w);
        }

        public ActionResult Edit(int Id) {
            // Auth control!!! 
            
            WarheadType w = new WarheadType(Id);
            return View(w);
        }

        [HttpPost]
        public ActionResult Edit(WarheadType wt) {
            wt.UpdateWarhead();
            return RedirectToAction("Index");
        }

        public ActionResult Insert() {
            return View();
        }

        [HttpPost]
        public ActionResult Insert(FormCollection col) {
            String warheadName = col.Get("WarheadName");
            WarheadType w = new WarheadType();
            w.WarheadTypeName = warheadName;            
            ViewBag.insertStatus = w.InsertWarhead();
            return View();        
        }

    }
}
