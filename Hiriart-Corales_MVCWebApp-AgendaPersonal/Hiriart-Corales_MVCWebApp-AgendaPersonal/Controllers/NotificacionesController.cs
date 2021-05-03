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
    public class NotificacionesController : Controller
    {
        private AgendaPersonalCF_Hiriart_Corales db = new AgendaPersonalCF_Hiriart_Corales();

        // GET: Notificaciones
        public ActionResult Index(Nullable<DateTime> hora)
        {
            if (hora != null)
            {
                var notificacion = from s in db.Notificacion select s;
                notificacion = notificacion.Where(s => s.Hora.TimeOfDay.Equals(hora));
                return View(notificacion.ToList());
            }
            else
            {
                return View(db.Notificacion.ToList());
            }
        }

        // GET: Notificaciones/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notificacion notificacion = db.Notificacion.Find(id);
            if (notificacion == null)
            {
                return HttpNotFound();
            }
            return View(notificacion);
        }

        // GET: Notificaciones/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Notificaciones/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "NotificacionID,Titulo,Hora")] Notificacion notificacion)
        {
            if (ModelState.IsValid)
            {
                db.Notificacion.Add(notificacion);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(notificacion);
        }

        // GET: Notificaciones/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notificacion notificacion = db.Notificacion.Find(id);
            if (notificacion == null)
            {
                return HttpNotFound();
            }
            return View(notificacion);
        }

        // POST: Notificaciones/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "NotificacionID,Titulo,Hora")] Notificacion notificacion)
        {
            if (ModelState.IsValid)
            {
                db.Entry(notificacion).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(notificacion);
        }

        // GET: Notificaciones/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Notificacion notificacion = db.Notificacion.Find(id);
            if (notificacion == null)
            {
                return HttpNotFound();
            }
            return View(notificacion);
        }

        // POST: Notificaciones/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Notificacion notificacion = db.Notificacion.Find(id);
            var eventos = db.Evento.Where(s => s.NotificacionID == id);
            foreach(var evento in eventos)
            {
                evento.Notificacion = null;
            }        
            db.Notificacion.Remove(notificacion);
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
