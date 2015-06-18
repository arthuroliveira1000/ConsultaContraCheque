using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWI.ContraCheque.Dominio
{
    public class Conta : Entidade
    {
        //CodigoConta mesmo caso do CodigoColaborador, virou string pois receberemos dados = "T03"
        public string CodigoConta { get; set; }
        public DateTime Competencia { get; set; }
        public string Descricao { get; set; }
        public string Tipo { get; set; }
        public decimal? Total { get; set; }
        public decimal? Base { get; set; }
        public decimal? ValorReferencia { get; set; }
    }
}
