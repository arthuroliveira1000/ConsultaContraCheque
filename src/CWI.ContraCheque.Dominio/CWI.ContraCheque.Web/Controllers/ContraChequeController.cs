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
            long id = 1;
            
            ContraChequeExibicao contraChequeExibicao = new ContraChequeExibicao();
            //id variavel local
            contraChequeExibicao.Colaborador = db.Colaboradors.Find(id);
            contraChequeExibicao.BancoColaborador = db.ContaBancoColaboradorPagamentoes.FirstOrDefault(banco => banco.IdColaborador == id);

            IEnumerable<Conta> cont = from contaColaborador in db.Colaborador_Conta.ToList()
                                     where contaColaborador.CodigoColaborador == 1895
                                     where contaColaborador.Competencia.Month == 7 && contaColaborador.Competencia.Year == 2014
                                     join c in db.Contas on contaColaborador.CodigoConta equals c.CodigoConta
                                     select new Conta()
                                     {
                                         Id = c.Id,
                                         CodigoConta = c.CodigoConta,
                                         Base = c.Base,
                                         Descricao = c.Descricao,
                                         Tipo = c.Tipo,
                                         Total = c.Total,
                                         ValorReferencia = c.ValorReferencia
                                     };
            contraChequeExibicao.Contas = cont.ToList();

            IEnumerable<Colaborador_Conta> res = from contaColaborador in db.Colaborador_Conta.ToList()
                                     where contaColaborador.CodigoColaborador == 1895
                                     where contaColaborador.Competencia.Month == 7 && contaColaborador.Competencia.Year == 2014
                                     join c in db.Contas on contaColaborador.CodigoConta equals c.CodigoConta
                                     select new Colaborador_Conta()
                                     {
                                         Id = contaColaborador.Id,
                                         Competencia = contaColaborador.Competencia,
                                         DataImportacao = contaColaborador.DataImportacao,
                                         Ocorrencia = contaColaborador.Ocorrencia
                                     };
            List<Colaborador_Conta> colConta = res.ToList();
            //contraChequeExibicao.Competencia = colConta[0].Competencia;
            //contraChequeExibicao.DataImportacao = colConta[0].DataImportacao;
            //contraChequeExibicao.Ocorrencia = colConta[0].Ocorrencia;

            return View(contraChequeExibicao);
        }
    }
}