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
    public class ContaBancoColaboradorPagamentoController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ContaBancoColaboradorPagamento
        public ActionResult Index()
        {
            return View(db.ContaBancoColaboradorPagamentoes.ToList());
        }

        // GET: ContaBancoColaboradorPagamento/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaBancoColaboradorPagamento contaBancoColaboradorPagamento = db.ContaBancoColaboradorPagamentoes.Find(id);
            if (contaBancoColaboradorPagamento == null)
            {
                return HttpNotFound();
            }
            return View(contaBancoColaboradorPagamento);
        }

        // GET: ContaBancoColaboradorPagamento/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ContaBancoColaboradorPagamento/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,NomeBanco,Agencia,Conta,IdColaborador")] ContaBancoColaboradorPagamento contaBancoColaboradorPagamento)
        {
            if (ModelState.IsValid)
            {
                db.ContaBancoColaboradorPagamentoes.Add(contaBancoColaboradorPagamento);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(contaBancoColaboradorPagamento);
        }

        // GET: ContaBancoColaboradorPagamento/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaBancoColaboradorPagamento contaBancoColaboradorPagamento = db.ContaBancoColaboradorPagamentoes.Find(id);
            if (contaBancoColaboradorPagamento == null)
            {
                return HttpNotFound();
            }
            return View(contaBancoColaboradorPagamento);
        }

        // POST: ContaBancoColaboradorPagamento/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,NomeBanco,Agencia,Conta,IdColaborador")] ContaBancoColaboradorPagamento contaBancoColaboradorPagamento)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contaBancoColaboradorPagamento).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(contaBancoColaboradorPagamento);
        }

        // GET: ContaBancoColaboradorPagamento/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContaBancoColaboradorPagamento contaBancoColaboradorPagamento = db.ContaBancoColaboradorPagamentoes.Find(id);
            if (contaBancoColaboradorPagamento == null)
            {
                return HttpNotFound();
            }
            return View(contaBancoColaboradorPagamento);
        }

        // POST: ContaBancoColaboradorPagamento/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ContaBancoColaboradorPagamento contaBancoColaboradorPagamento = db.ContaBancoColaboradorPagamentoes.Find(id);
            db.ContaBancoColaboradorPagamentoes.Remove(contaBancoColaboradorPagamento);
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
