using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Diagnostics;
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
        public ActionResult Index(string keyword, Nullable<DateTime> fecha)
        {
            if (!String.IsNullOrEmpty(keyword))//Permite buscar por fecha o por contenido
            {
                var diario = from s in db.Diario select s;
                diario = diario.Where(s => s.Contenido.Contains(keyword));
                return View(diario.ToList());
            }
            else if (fecha != null)
            {
                var diario = from s in db.Diario select s;
                diario = diario.Where(s => s.Fecha.Date.Equals(fecha));
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
            var fechasEventos = db.ListaEventoes.Where(s => s.FechaEvento.Equals(DateTime.Today));
            ViewBag.ListaEventoID = new SelectList(fechasEventos, "ListaEventoID", "Titulo");
            //Tener ListaEventoID y Titulo en lugar de ambos ListaEventoID permite seleccion de eventos
            //relacionados al diario mostrando solo el titulo de la fecha actual pero maneja el ID
            return View();
        }

        // POST: Diarios/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "DiarioID,ListaEventoID,Fecha,Contenido")] Diario diario)
        {
            if (ModelState.IsValid)
            {
                db.Diario.Add(diario);
                if(diario.ListaEventoID!=null)//Modifica la tabla para mostrar los eventos asociados a calendario
                {
                    var eventos = from s in db.ListaEventoes select s;
                    eventos = eventos.Where(s => s.FechaEvento.Equals(DateTime.Today));
                    foreach (var evento in eventos)
                    {
                        foreach (var id in diario.ListaEventoID)
                        {
                            if (evento.IDEvento.Equals(id))
                            {
                                evento.IDDiario = diario.DiarioID;
                                Debug.WriteLine("Acaaaa");
                            }
                        }
                    }
                }
                Debug.WriteLine(diario.Fecha.GetType());
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ListaEventoID = new SelectList(db.ListaEventoes, "ListaEventoID", "ListaEventoID", diario.ListaEventoID);
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
            ViewBag.ListaEventoID = new SelectList(db.ListaEventoes, "ListaEventoID", "ListaEventoID", diario.ListaEventoID);
            return View(diario);
        }

        // POST: Diarios/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "DiarioID,ListaEventoID,Fecha,Contenido")] Diario diario)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diario).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListaEventoID = new SelectList(db.ListaEventoes, "ListaEventoID", "ListaEventoID", diario.ListaEventoID);
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
