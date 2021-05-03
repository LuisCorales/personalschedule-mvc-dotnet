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
            var contactos = db.ListaContactoes.Where(s => s.IDEvento == id);
            //Lo anterior es: contactos = contactos.Where(s => s.IDEvento==id); 
            ViewBag.ListaContactoID = new SelectList(contactos, "ListaContactoID", "NombreApellido");
            return View(evento);
        }

        // GET: Eventos/Create
        public ActionResult Create()
        {
            ViewBag.ListaContactoID = new SelectList(db.ListaContactoes, "ListaContactoID", "NombreApellido");
            ViewBag.MemoID = new SelectList(db.Memo, "MemoID", "Contenido");
            ViewBag.NotificacionID = new SelectList(db.Notificacion, "NotificacionID", "Titulo");
            return View();
        }

        // POST: Eventos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventoID,NotificacionID,MemoID,ListaContactoID,Fecha,Inicio,Fin,Titulo,Descripcion,Ubicacion,EsSerie,Dias")] Evento evento)
        {           
            if (ModelState.IsValid)
            {
                db.Evento.Add(evento);
                db.SaveChanges();
                ListaEvento listaEvento = new ListaEvento();//Crea y llena un anetrada de lista de eventos
                listaEvento.ListaEventoID = evento.EventoID;
                listaEvento.IDDiario = null;
                listaEvento.Titulo = evento.Titulo;
                listaEvento.FechaEvento = evento.Fecha;
                db.ListaEventoes.Add(listaEvento);//Aniade una una entrada lista de eventos cuando se crea une vento
                db.SaveChanges();
                if (evento.ListaContactoID != null)//Modifica la tabla para mostrar los eventos asociados a calendario
                {
                    var contactos = from s in db.ListaContactoes select s;                 
                    foreach (var contacto in contactos)
                    {
                        foreach (var id in evento.ListaContactoID)
                        {
                            if (contacto.ListaContactoID == id)
                            {
                                contacto.IDEvento = evento.EventoID;
                            }
                        }
                    }
                }
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
            ViewBag.ListaContactoID = new SelectList(db.ListaContactoes, "ListaContactoID", "NombreApellido");
            ViewBag.MemoID = new SelectList(db.Memo, "MemoID", "Contenido", evento.MemoID);
            ViewBag.NotificacionID = new SelectList(db.Notificacion, "NotificacionID", "Titulo", evento.NotificacionID);
            return View(evento);
        }

        // POST: Eventos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventoID,NotificacionID,MemoID,ListaContactoID,Fecha,Inicio,Fin,Titulo,Descripcion,Ubicacion,EsSerie,Dias")] Evento evento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(evento).State = EntityState.Modified;
                db.SaveChanges();
                if (evento.ListaContactoID != null)//Modifica la tabla para mostrar los eventos asociados a calendario
                {
                    var contactos = from s in db.ListaContactoes select s;
                    foreach (var contacto in contactos)
                    {
                        foreach (var id in evento.ListaContactoID)
                        {
                            if (contacto.ListaContactoID == id)
                            {
                                contacto.IDEvento = evento.EventoID;
                            }
                        }
                    }
                }
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
            var contactos = db.ListaContactoes.Where(s => s.IDEvento == id);
            //Lo anterior es: contactos = contactos.Where(s => s.IDEvento==id); 
            ViewBag.ListaContactoID = new SelectList(contactos, "ListaContactoID", "NombreApellido");
            return View(evento);
        }

        // POST: Eventos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Evento evento = db.Evento.Find(id);
            ListaEvento listaEvento = db.ListaEventoes.Find(id);//Encuentra la entrada de lista correcta             
            db.Evento.Remove(evento);
            db.ListaEventoes.Remove(listaEvento);
            //(En desarrollo) Encuentra los memos y notificaciones que tengan relacion a este evento y les borra
            //db.Memo.Remove(evento.Memo);
            //db.Notificacion.Remove(evento.Notificacion);
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
