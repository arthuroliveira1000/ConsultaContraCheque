using CWI.ContraCheque.Dominio;
using CWI.ContraCheque.Importador;
using CWI.ContraCheque.Web.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace CWI.ContraCheque.Web.Controllers
{

    public class ImportacaoContraChequeController : Controller
    {
        private static string mensagemBag = "";
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: ImportacaoContraCheque

        private ApplicationUserManager _userManager;

        public ImportacaoContraChequeController()
        {
        }

        public ImportacaoContraChequeController(ApplicationUserManager userManager)
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

        private bool VerificaUsuarioAdmin()
        {
            var userId = User.Identity.GetUserId();
            if (userId != null) {
            var user = UserManager.FindById(userId);
            return user.Roles.Any(x => x.RoleId == "1");
            }
            return false;
        }


        public ActionResult Index()
        {
            if (!VerificaUsuarioAdmin())
            {
                return RedirectToAction("Login2", "Account");
            }   
            ViewBag.Message = mensagemBag;
            mensagemBag = "";
            return View();
        }
        public ActionResult Importa(HttpPostedFileBase file, string competencia)
        {
            if (file == null)
            {
                mensagemBag = "Selecione algum arquivo!";
            }
            else if (!file.FileName.Substring(file.FileName.LastIndexOf(".")).Equals(".txt"))
            {
                mensagemBag = "Arquivo não está no formato correto!";
            }
            else
            {                
                var path = Path.Combine(Server.MapPath("~/Arquivos_Temporarios/"), "importa.txt");
                file.SaveAs(path);
                ModelState.Clear();
                mensagemBag = "Arquivo importado com sucesso!";
                System.IO.DirectoryInfo downloadedMessageInfo = new DirectoryInfo(Server.MapPath("~/Arquivos_Temporarios/"));

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

                foreach (FileInfo arquivo in downloadedMessageInfo.GetFiles())
                {
                    arquivo.Delete();
                }
            }
            return (Redirect("Index"));
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
                                             where contaBanco.Competencia == competencia
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