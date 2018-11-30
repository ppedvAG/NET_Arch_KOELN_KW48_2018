using Autofac;
using ppedv.TastyMoon.DomainModel;
using ppedv.TastyMoon.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ppedv.TastyMoon.UI.Web.Controllers
{
    public class RezeptController : Controller
    {

        Core core = MvcApplication.Container.Resolve<Core>();

        // GET: Rezept
        public ActionResult Index()
        {

            return View(core.UnitOfWork.RezeptRepo.Query().ToList());
        }

        // GET: Rezept/Details/5
        public ActionResult Details(int id)
        {
            return View(core.UnitOfWork.RezeptRepo.GetById(id));
        }

        // GET: Rezept/Create
        public ActionResult Create()
        {
            return View(new Rezept() { Name = "NEU" });
        }

        // POST: Rezept/Create
        [HttpPost]
        public ActionResult Create(Rezept rezept)
        {
            try
            {
                core.UnitOfWork.RezeptRepo.Add(rezept);
                core.UnitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rezept/Edit/5
        public ActionResult Edit(int id)
        {
            return View(core.UnitOfWork.RezeptRepo.GetById(id));
        }

        // POST: Rezept/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, Rezept rezept)
        {
            try
            {
                core.UnitOfWork.RezeptRepo.Update(rezept);
                core.UnitOfWork.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Rezept/Delete/5
        public ActionResult Delete(int id)
        {
            return View(core.UnitOfWork.RezeptRepo.GetById(id));
        }

        // POST: Rezept/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                var loaded = core.UnitOfWork.RezeptRepo.GetById(id);
                if (loaded != null)
                {
                    core.UnitOfWork.RezeptRepo.Delete(loaded);
                    core.UnitOfWork.Save();
                }

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
