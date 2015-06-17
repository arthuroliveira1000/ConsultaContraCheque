using CWI.ContraCheque.Dominio;
using CWI.ContraCheque.Importador;
using CWI.ContraCheque.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CWI.ContraCheque.Web.Controllers
{
    public class ImportacaoContraChequeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ImportacaoContraCheque
        public ActionResult Index()
        {
            return View();
        }

        public void Importa([Bind(Include = "nome")] string competencia)
        {
            Dictionary<string, List<object>> dadosImportacao = new Dictionary<string, List<object>>();           
            //Isso aqui ainda está uma gambeta
            string data = "07/" + competencia;
            DateTime comp = DateTime.Parse(data);
            //Final da gambeta
            ImportaContraCheque importa = new ImportaContraCheque(comp);            
            dadosImportacao = importa.LerTxt();

            List<object> saida;
            List<Conta> contas;
            List<Colaborador_Conta> colaborador_conta;
            dadosImportacao.TryGetValue("contas", out saida);            
            contas = saida.Cast<Conta>().ToList();

            dadosImportacao.TryGetValue("colaborador_contas", out saida);
            colaborador_conta = saida.Cast<Colaborador_Conta>().ToList();
            InsereOuAtualizaContas(contas);
            InsereOuAtualizaColaborador_Contas(colaborador_conta);
        }

        private void InsereOuAtualizaContas(List<Conta> contas)
        {
            //por enquanto só está inserindo
            InsereContas(contas);
        }
        private void InsereOuAtualizaColaborador_Contas(List<Colaborador_Conta> colaborador_contas)
        {
            //por enquanto só está inserindo
            InsereColaborador_Contas(colaborador_contas);
        }
        private void InsereContas(List<Conta> contas)
        {
            foreach (Conta conta in contas)
            {
                db.Contas.Add(conta);
            }
            db.SaveChanges();
        }
        private void InsereColaborador_Contas(List<Colaborador_Conta> colaborador_contas)
        {
            foreach (Colaborador_Conta c_c in colaborador_contas)
            {
                db.Colaborador_Conta.Add(c_c);
            }
            db.SaveChanges();
        }
    }
}