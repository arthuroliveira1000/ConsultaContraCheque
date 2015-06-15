using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CWI.ContraCheque.Dominio;
using CWI.ContraCheque.Web.Models;

namespace CWI.ContraCheque.Web.Controllers
{
    public class Colaborador_ContaController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Colaborador_Conta
        public ActionResult Index()
        {
            return View(db.Colaborador_Conta.ToList());
        }

        // GET: Colaborador_Conta/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colaborador_Conta colaborador_Conta = db.Colaborador_Conta.Find(id);
            if (colaborador_Conta == null)
            {
                return HttpNotFound();
            }
            return View(colaborador_Conta);
        }

        // GET: Colaborador_Conta/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Colaborador_Conta/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Competencia,DataImportacao,Ocorrencia,CodigoConta,CodigoColaborador")] Colaborador_Conta colaborador_Conta)
        {
            if (ModelState.IsValid)
            {
                db.Colaborador_Conta.Add(colaborador_Conta);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(colaborador_Conta);
        }

        // GET: Colaborador_Conta/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colaborador_Conta colaborador_Conta = db.Colaborador_Conta.Find(id);
            if (colaborador_Conta == null)
            {
                return HttpNotFound();
            }
            return View(colaborador_Conta);
        }

        // POST: Colaborador_Conta/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Competencia,DataImportacao,Ocorrencia,CodigoConta,CodigoColaborador")] Colaborador_Conta colaborador_Conta)
        {
            if (ModelState.IsValid)
            {
                db.Entry(colaborador_Conta).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(colaborador_Conta);
        }

        // GET: Colaborador_Conta/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Colaborador_Conta colaborador_Conta = db.Colaborador_Conta.Find(id);
            if (colaborador_Conta == null)
            {
                return HttpNotFound();
            }
            return View(colaborador_Conta);
        }

        // POST: Colaborador_Conta/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Colaborador_Conta colaborador_Conta = db.Colaborador_Conta.Find(id);
            db.Colaborador_Conta.Remove(colaborador_Conta);
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
