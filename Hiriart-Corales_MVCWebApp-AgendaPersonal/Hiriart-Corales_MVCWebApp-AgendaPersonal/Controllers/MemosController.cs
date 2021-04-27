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
    public class MemosController : Controller
    {
        private AgendaPersonalCF_Hiriart_Corales db = new AgendaPersonalCF_Hiriart_Corales();

        // GET: Memos
        public ActionResult Index(string Titulo)
        {

            if (!String.IsNullOrEmpty(Titulo))
            {
                var memo = from s in db.Memo select s;
                memo = memo.Where(s => s.Contenido.Contains(Titulo));
                memo.Include(m => m.Evento);
                return View(memo.ToList());
            }
            else
            {
                var memo = db.Memo.Include(m => m.Evento);
                return View(memo.ToList());
            }         
        }

        // GET: Memos/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Memo memo = db.Memo.Find(id);
            if (memo == null)
            {
                return HttpNotFound();
            }
            return View(memo);
        }

        // GET: Memos/Create
        public ActionResult Create()
        {
            ViewBag.EventoID = new SelectList(db.Evento, "EventoID", "Titulo");
            return View();
        }

        // POST: Memos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MemoID,EventoID,Contenido")] Memo memo)
        {
            if (ModelState.IsValid)
            {
                db.Memo.Add(memo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EventoID = new SelectList(db.Evento, "EventoID", "Contenido", memo.EventoID);
            return View(memo);
        }

        // GET: Memos/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Memo memo = db.Memo.Find(id);
            if (memo == null)
            {
                return HttpNotFound();
            }
            ViewBag.EventoID = new SelectList(db.Evento, "EventoID", "Contenido", memo.EventoID);
            return View(memo);
        }

        // POST: Memos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MemoID,EventoID,Contenido")] Memo memo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(memo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EventoID = new SelectList(db.Evento, "EventoID", "Contenido", memo.EventoID);
            return View(memo);
        }

        // GET: Memos/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Memo memo = db.Memo.Find(id);
            if (memo == null)
            {
                return HttpNotFound();
            }
            return View(memo);
        }

        // POST: Memos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Memo memo = db.Memo.Find(id);
            db.Memo.Remove(memo);
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
