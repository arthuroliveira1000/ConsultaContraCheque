using CWI.ContraCheque.Dominio;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWI.ContraCheque.Importador
{
    public class ImportaContraCheque
    {        
        private DateTime Competencia {get; set;}
        private string caminhoArquivo;
        private List<object> ListaContas = new List<object>();
        private List<object> ListaColaborador_Conta = new List<object>();

        public ImportaContraCheque(DateTime competencia)
        {          
            Competencia = competencia;
            caminhoArquivo = AppDomain.CurrentDomain.BaseDirectory;
            caminhoArquivo += "Arquivos_Temporarios\\importa.txt";
        }

        public Dictionary<string, List<object>> LerTxt()
        {
            string linha;
            StreamReader reader = new StreamReader(caminhoArquivo);
            while ((linha = reader.ReadLine()) != null)
            {
                ProcessaLinha(linha);
            }
            reader.Close();

            Dictionary<string, List<object>> mapRetorno = new Dictionary<string, List<object>>();
            mapRetorno.Add("contas", ListaContas);
            mapRetorno.Add("colaborador_contas", ListaColaborador_Conta);
            
            return mapRetorno;
        }
        private void ProcessaLinha(string linha)
        {
            string codigoColaborador = "";
            string codigoConta = "";
            string valorReferencia = "";
            string valorBase = "";
            string valorTotal = "";
            string tipo = "";
            string descricao = "";
            string ocorrencia = "";

            //controla a quantidade de letras 
            int c = 1;
            foreach (char letra in linha)
            {
                if (c <= 6)
                {
                    codigoColaborador += letra;
                }
                else if (c >= 59 && c <= 64)
                {
                    codigoConta += letra;
                }
                else if (c >= 65 && c <= 82)
                {
                    valorReferencia += letra;
                }
                else if (c >= 83 && c <= 96)
                {
                    valorBase += letra;
                }
                else if (c >= 97 && c <= 108)
                {
                    valorTotal += letra;
                }
                else if (c == 109)
                {
                    tipo += letra;
                }
                else if (c >= 110 && c <= 151)
                {
                    descricao += letra;
                }
                else if (c >= 152 && c <= 153)
                {
                    ocorrencia += letra;
                }
                c++;
            }                
            Conta conta = ConverteParaConta(codigoConta, valorReferencia, valorBase, valorTotal, tipo, descricao);           
            Colaborador_Conta colaborador_conta = ConverteParaColaborador_Conta(codigoColaborador, codigoConta, ocorrencia);
            ListaContas.Add(conta);
            ListaColaborador_Conta.Add(colaborador_conta);
        }
        private Conta ConverteParaConta(string codigoConta, string valorReferencia, string valorBase, string valorTotal, string tipo, string descricao)
        {
            codigoConta = codigoConta.Trim();
            valorReferencia = valorReferencia.Trim();
            valorBase = valorBase.Trim();
            valorTotal = valorTotal.Trim();
            tipo = tipo.Trim();
            descricao = descricao.Trim();

            Conta conta = new Conta();
            conta.CodigoConta = codigoConta;
            conta.ValorReferencia = ConverteDecimal(valorReferencia);
            conta.Base = ConverteDecimal(valorBase);
            conta.Total = ConverteDecimal(valorTotal);
            conta.Tipo = tipo;
            conta.Descricao = descricao;
            conta.Competencia = Competencia;
            return conta;
        }
        private Colaborador_Conta ConverteParaColaborador_Conta(string codigoColaborador, string codigoConta, string ocorrencia)
        {
            codigoColaborador = codigoColaborador.Trim();
            codigoConta = codigoConta.Trim();
            ocorrencia = ocorrencia.TrimStart();
            ocorrencia = ocorrencia.TrimEnd();
            
            Colaborador_Conta colaborador_conta = new Colaborador_Conta();
            colaborador_conta.CodigoColaborador = Convert.ToInt64(codigoColaborador);
            colaborador_conta.CodigoConta = codigoConta;
            colaborador_conta.Ocorrencia = ocorrencia;
            colaborador_conta.DataImportacao = DateTime.Now;
            colaborador_conta.Competencia = Competencia;

            return colaborador_conta;            
        }
        //Converte para decimal, se o valor da string for empty retorna null
        private static Decimal? ConverteDecimal(string valor)
        {
            Decimal? retornar = Convert.ToDecimal(valor == string.Empty ? null : valor);
            return retornar == 0 ? null : retornar;
        }
    }
}
