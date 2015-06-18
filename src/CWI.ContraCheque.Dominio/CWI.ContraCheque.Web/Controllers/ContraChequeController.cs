using CWI.ContraCheque.Dominio;
using CWI.ContraCheque.Web.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace CWI.ContraCheque.Web.Controllers
{
    public class ContraChequeController : Controller
    {
        private ApplicationUserManager _userManager;

        public ContraChequeController()
        {
        }

        public ContraChequeController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ContraCheque

        private bool VerificaUsuarioComum()
        {
            var userId = User.Identity.GetUserId();
            if (userId != null) {
            var user = UserManager.FindById(userId);
            return user.Roles.Any(x => x.RoleId == "2");
            }
            return false;
        }

        public ActionResult Index()
        {
            if (!VerificaUsuarioComum())
            {
                return RedirectToAction("Login2", "Account");
            }   

            string competencia = "";
            DateTime comp;
            if (competencia.Equals(""))
            {
                competencia = "01/" + DateTime.Now.Month + "/" + DateTime.Now.Year;
                comp = DateTime.Parse(competencia);
            }
            else
            {
                competencia = "01/" + competencia;
                comp = DateTime.Parse(competencia);
            }

            //ESSA É UMA BASE PARA DESENVOLVER O CONTRA CHEQUE, POR ENQUANTO ID FIXO, COMPETENCIA FIXA. (PARA TESTE)                       
            ContraChequeExibicao contraChequeExibicao = new ContraChequeExibicao();
            string codigoUser = User.Identity.GetUserId();
            var a = db.Users.Find(codigoUser);
            long codigoc = a.CodigoColaborador;
            long codigoColaborador = codigoc;
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