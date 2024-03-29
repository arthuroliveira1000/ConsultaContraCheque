﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWI.ContraCheque.Dominio
{
    //Colaborador_Conta é uma classe que faz o link entre Colaborador e Conta (possuí algumas informações desse relacionamento)
    public class Colaborador_Conta : Entidade
    {
        //Competencia é o período que equivale
        public DateTime Competencia { get; set; }
        //Talvez não será exibido em nenhum momento na view, porém será fundamental para a gente, se a DataImportacao for equivalente ao mesmo mês
        // A gente sobreescreve
        public DateTime DataImportacao { get; set; }
        //exemplo de dados em Ocorrencia = ME, eu sei que este campo participa do relacionamento entre Conta e colaborador
        //porém nem eu e nem o André sabemos o que ele é ainda, segunda teremos a resposta. Mas ele está certo aqui.
        public string Ocorrencia { get; set; }
        //Fk de Conta, virou string pois receberemos dados por exemplo: T03
        public string CodigoConta { get; set; }
        //Fk de Colaborador
        public long CodigoColaborador { get; set; }

        public Colaborador_Conta()
        {
            DataImportacao = DateTime.Now;
        }
    }
}
