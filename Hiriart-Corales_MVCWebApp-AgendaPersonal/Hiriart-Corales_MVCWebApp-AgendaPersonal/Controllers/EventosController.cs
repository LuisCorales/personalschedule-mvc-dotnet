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
    public class EventosController : Controller
    {
        private AgendaPersonalCF_Hiriart_Corales db = new AgendaPersonalCF_Hiriart_Corales();

        // GET: Eventos
        public ActionResult Index(string titulo)
        {
            if (!String.IsNullOrEmpty(titulo))
            {
                var evento = from s in db.Evento select s;
                evento = evento.Where(s => s.Titulo.Contains(titulo));
                evento.Include(e => e.ListaContacto).Include(e => e.Memo).Include(e => e.Notificacion);
                return View(evento.ToList());
            }
            else
            {
                var eventoes = db.Evento.Include(e => e.ListaContacto).Include(e => e.Memo).Include(e => e.Notificacion);
                return View(eventoes.ToList());
            }          
        }

        // GET: Eventos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // GET: Eventos/Create
        public ActionResult Create()
        {
            ViewBag.ListaContactoID = new SelectList(db.ListaContactoes, "ListaContactoID", "ListaContactoID");
            ViewBag.MemoID = new SelectList(db.Memo, "MemoID", "Contenido");
            ViewBag.NotificacionID = new SelectList(db.Notificacion, "NotificacionID", "Titulo");
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventoID,NotificacionID,MemoID,ListaContactoID,Inicio,Fin,Titulo,Descripcion,Ubicacion,EsSerie,Dias")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Evento.Add(evento);
                ListaEvento listaEvento = new ListaEvento();
                listaEvento.ListaEventoID = evento.EventoID;
                listaEvento.IDDiario = null;
                listaEvento.IDEvento = evento.EventoID;
                db.ListaEventoes.Add(listaEvento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ListaContactoID = new SelectList(db.ListaContactoes, "ListaContactoID", "ListaContactoID", evento.ListaContactoID);
            ViewBag.MemoID = new SelectList(db.Memo, "MemoID", "Contenido", evento.MemoID);
            ViewBag.NotificacionID = new SelectList(db.Notificacion, "NotificacionID", "Titulo", evento.NotificacionID);
            return View(evento);
        }

        // GET: Eventos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            ViewBag.ListaContactoID = new SelectList(db.ListaContactoes, "ListaContactoID", "ListaContactoID", evento.ListaContactoID);
            ViewBag.MemoID = new SelectList(db.Memo, "MemoID", "Contenido", evento.MemoID);
            ViewBag.NotificacionID = new SelectList(db.Notificacion, "NotificacionID", "Titulo", evento.NotificacionID);
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventoID,NotificacionID,MemoID,ListaContactoID,Inicio,Fin,Titulo,Descripcion,Ubicacion,EsSerie,Dias")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ListaContactoID = new SelectList(db.ListaContactoes, "ListaContactoID", "ListaContactoID", evento.ListaContactoID);
            ViewBag.MemoID = new SelectList(db.Memo, "MemoID", "Contenido", evento.MemoID);
            ViewBag.NotificacionID = new SelectList(db.Notificacion, "NotificacionID", "Titulo", evento.NotificacionID);
            return View(evento);
        }

        // GET: Eventos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Evento evento = db.Evento.Find(id);
            if (evento == null)
            {
                return HttpNotFound();
            }
            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evento evento = db.Evento.Find(id);
            db.Evento.Remove(evento);
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
