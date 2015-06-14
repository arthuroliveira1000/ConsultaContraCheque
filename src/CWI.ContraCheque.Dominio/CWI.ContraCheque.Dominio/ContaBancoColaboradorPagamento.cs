using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWI.ContraCheque.Dominio
{
    //Conta onde será efetuado o pagamento para o colaborador
    public class ContaBancoColaboradorPagamento : Entidade
    {
        public string NomeBanco { get; set; }
        public string Agencia { get; set; }
        public string Conta { get; set; }
        //FK ligada com o ID de colaborador, aqui vamos usar o ID para o vinculo
        public long IdColaborador { get; set; }
    }
}
