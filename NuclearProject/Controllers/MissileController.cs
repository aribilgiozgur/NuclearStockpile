using NuclearProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NuclearProject.Controllers
{
    public class MissileController : Controller
    {
        //
        // GET: /Missile/Index

        public ActionResult Index()
        {
            Missile m = new Missile();
            List<Missile> missiles = m.GetMissiles();         
            return View(missiles);

            // ViewModel yaratıp bunu da döndürebiliriz. 
            /*
             foreach(Missile m in missiles){
             * MissileViewModel mvm = m.getViewModel()
             * }
             */
        }

        public ActionResult Insert() {
            WarheadType wt = new WarheadType();
            List<WarheadType> warheads = wt.GetAll();
            return View(warheads);
        }

        [HttpPost]
        public ActionResult Insert(FormCollection col) {
            Missile m = new Missile();
            m.WarheadTypeId = int.Parse(col.Get("WarheadTypeId"));
            m.MissileName = col.Get("MissileName");
            m.MissileRange = double.Parse(col.Get("MissileRange"));
            m.FuelType = col.Get("FuelType");

            m.InsertMissile();
            return RedirectToAction("Index");

        }

    }
}
