using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Hiriart_Corales_MVCWebApp_AgendaPersonal.Models;

namespace Hiriart_Corales_MVCWebApp_AgendaPersonal.Controllers
{
    public class DiariosController : Controller
    {
        private AgendaPersonalCF_Hiriart_Corales db = new AgendaPersonalCF_Hiriart_Corales();

        // GET: Diarios
        public ActionResult Index(string Keyword)
        {
            if (!String.IsNullOrEmpty(Keyword))
            {
                var diario = from s in db.Diario select s;
                diario = diario.Where(s => s.Contenido.Contains(Keyword));
                return View(diario.ToList());
            }
            else
            {
                return View(db.Diario.ToList());
            }          
        }

        // GET: Diarios/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diario diario = db.Diario.Find(id);
            if (diario == null)
            {
                return HttpNotFound();
            }
            return View(diario);
        }

        // GET: Diarios/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Diarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiarioID,Fecha,Contenido")] Diario diario)
        {
            if (ModelState.IsValid)
            {
                db.Diario.Add(diario);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(diario);
        }

        // GET: Diarios/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diario diario = db.Diario.Find(id);
            if (diario == null)
            {
                return HttpNotFound();
            }
            return View(diario);
        }

        // POST: Diarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiarioID,Fecha,Contenido")] Diario diario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diario);
        }

        // GET: Diarios/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Diario diario = db.Diario.Find(id);
            if (diario == null)
            {
                return HttpNotFound();
            }
            return View(diario);
        }

        // POST: Diarios/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Diario diario = db.Diario.Find(id);
            foreach (var evento in diario.Evento)
            {
                evento.Serie = null;
            }         
            db.Diario.Remove(diario);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
