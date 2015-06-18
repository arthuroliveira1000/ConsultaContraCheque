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

            string data = "01/" + competencia;
            DateTime comp = DateTime.Parse(data);

            ImportaContraCheque importa = new ImportaContraCheque(comp);
            dadosImportacao = importa.LerTxt();

            List<object> saida;
            List<Conta> contas;
            List<Colaborador_Conta> colaborador_conta;
            dadosImportacao.TryGetValue("contas", out saida);
            contas = saida.Cast<Conta>().ToList();
            dadosImportacao.TryGetValue("colaborador_contas", out saida);
            colaborador_conta = saida.Cast<Colaborador_Conta>().ToList();

            InsereOuAtualiza(comp, colaborador_conta, contas);
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
        private void InsereOuAtualiza(DateTime competencia, List<Colaborador_Conta> colaborador_contas, List<Conta> contas)
        {
            int mes = competencia.Month;
            int ano = competencia.Year;
            long codigoColaborador = colaborador_contas.First().CodigoColaborador;   
            //Pega os existentes
            var contasEcolaborador_contas = (from contas_colaborador in db.Colaborador_Conta.ToList()
                        where contas_colaborador.CodigoColaborador == codigoColaborador
                        where contas_colaborador.Competencia.Month == mes
                        where contas_colaborador.Competencia.Year == ano
                        join contaBanco in db.Contas on contas_colaborador.CodigoConta equals contaBanco.CodigoConta
                        select new
                        {
                            contas_colaborador,
                            contaBanco
                        }).ToList();                                                 

            //Se cair no if existe contas e atualiza
            if (contasEcolaborador_contas.Count > 0)
            {
                var contasE = contasEcolaborador_contas.Select(x => x.contaBanco);
                var colaboradorEContas = contasEcolaborador_contas.Select(x => x.contas_colaborador);
                db.Contas.RemoveRange(contasE);
                db.Colaborador_Conta.RemoveRange(colaboradorEContas);
                db.SaveChanges();
                InsereContas(contas);
                InsereColaborador_Contas(colaborador_contas);
            }
            //se cair no else, é novo. Então só insere
            else
            {
                InsereContas(contas);
                InsereColaborador_Contas(colaborador_contas);
            }
        }
    }
}