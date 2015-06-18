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
    public class ContraChequeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ContraCheque
        public ActionResult Index()
        {
            //ESSA É UMA BASE PARA DESENVOLVER O CONTRA CHEQUE, POR ENQUANTO ID FIXO, COMPETENCIA FIXA. (PARA TESTE)                       
            ContraChequeExibicao contraChequeExibicao = new ContraChequeExibicao();
            string data = "01/07/2014";
            DateTime comp = DateTime.Parse(data);
            int codigoColaborador = 1895;
            //id variavel local
            IEnumerable<Colaborador> cola = (from colaborador in db.Colaboradors.ToList()
                                               where colaborador.CodigoColaborador == codigoColaborador
                                               select new Colaborador()
                                               {
                                                   Id = colaborador.Id,
                                                   NomeColaborador = colaborador.NomeColaborador,
                                                   CodigoColaborador = colaborador.CodigoColaborador,
                                                   CentroCusto = colaborador.CentroCusto,
                                                   DataAdmissao = colaborador.DataAdmissao,
                                                   TipoSalario = colaborador.TipoSalario
                                               });
            if (cola.ToList().Count != 0)
            {
                contraChequeExibicao.Colaborador = cola.ToList().First();
                contraChequeExibicao.BancoColaborador = db.ContaBancoColaboradorPagamentoes.FirstOrDefault(banco => banco.IdColaborador == contraChequeExibicao.Colaborador.Id);

                var contasEcolaborador_contas = (from contas_colaborador in db.Colaborador_Conta.ToList()
                                                 where contas_colaborador.CodigoColaborador == codigoColaborador
                                                 where contas_colaborador.Competencia == comp
                                                 join contaBanco in db.Contas on contas_colaborador.CodigoConta equals contaBanco.CodigoConta
                                                 where contaBanco.Competencia == comp
                                                 select new
                                                 {
                                                     contas_colaborador,
                                                     contaBanco
                                                 }).ToList();
                if (contasEcolaborador_contas.Count > 0)
                {
                    var contasE = contasEcolaborador_contas.Select(x => x.contaBanco);
                    var colaboradorEContas = contasEcolaborador_contas.Select(x => x.contas_colaborador);
                    contraChequeExibicao.Contas = contasE.ToList();
                    List<Colaborador_Conta> colConta = colaboradorEContas.ToList();
                    
                    if (colConta.Count != 0)
                    {
                        contraChequeExibicao.Competencia = colConta[0].Competencia;
                        contraChequeExibicao.DataImportacao = colConta[0].DataImportacao;
                        contraChequeExibicao.Ocorrencia = colConta[0].Ocorrencia;
                    }
                }
                return View(contraChequeExibicao);
            }
            return View(new ContraChequeExibicao());
        }
    }
}