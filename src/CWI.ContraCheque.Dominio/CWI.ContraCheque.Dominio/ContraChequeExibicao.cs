using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWI.ContraCheque.Dominio
{
    public class ContraChequeExibicao
    {
        public Colaborador Colaborador { get; set; }
        public ContaBancoColaboradorPagamento BancoColaborador { get; set; }        
        public List<Conta> Contas { get; set; }
        public DateTime DataImportacao { get; set; }
        public DateTime Competencia { get; set; }
        public string Ocorrencia { get; set; }

    }
}
