using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWI.ContraCheque.Dominio
{
    public class Colaborador : Entidade
    {
        //Este CodigoColaborador não será a PK da Tabela no banco, acho que até poderia ser
        //mas elá é o código que vem da importação do txt, a FK em Conta apontará para CodigoColaborador.
        public long CodigoColaborador { get; set; }
        public string NomeColaborador { get; set; }
        //TipoSalario, acho que poderá ser do tipo CHAR, mas pode ser que em outro arquivo de importação venha TipoSalario = MM        
        public string TipoSalario { get; set; }
        //Exemplo de dado em CentroCusto = ADMINISTRAÇÃO CWICG
        public string CentroCusto { get; set; }
        public DateTime DataAdmissao { get; set; }
    }
}
