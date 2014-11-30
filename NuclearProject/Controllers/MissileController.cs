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
            List<Missile> missiles = Missile.GetMissiles();         
            return View(missiles);

        }

        public ActionResult Insert() {
            WarheadType wt = new WarheadType();
            List<WarheadType> warheads = wt.GetAll();
            return View(warheads);
        }

        [HttpPost]
        public ActionResult Insert(FormCollection col) {
            List<String> paramArray = new List<string>();
            for (int i = 0; i < col.Count; i++)
            {
                paramArray.Add(col.Get(i));
            }
            Missile m = new Missile(paramArray);         
            m.InsertMissile();
            return RedirectToAction("Index");

        }

        public ActionResult View(int id) {
            Missile m = new Missile(id);            
            return View(m);
        }

        public ActionResult Edit(int id)
        {
            Session["MissileId"] = id;
            Missile m = new Missile(id);
            WarheadType wt = new WarheadType();
            ViewBag.Warheads = wt.GetAll();
            return View(m);
        }

        [HttpPost]
        public ActionResult Edit(FormCollection col)
        {
            int id = int.Parse(Session["MissileId"].ToString());
            List<String> paramArray = new List<string>();
            for (int i = 0; i < col.Count; i++)
            {
                paramArray.Add(col.Get(i));
            }

            Missile m = new Missile(paramArray);
            m.MissileId = id;
            m.Save();
            return RedirectToAction("Index");
        }

            [HttpPost]
            public ActionResult Delete(FormCollection col) {                        
                int  id = int.Parse(col.Get("id"));            
                Missile m = new Missile(id);
                m.Delete();
                return RedirectToAction("Index");

            }


    }
}
