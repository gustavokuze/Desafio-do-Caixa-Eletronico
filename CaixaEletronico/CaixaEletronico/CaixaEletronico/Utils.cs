using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaEletronico
{
    public class Utils
    {
        public static int GerarSaldoAleatorio()
        {
            Random rand = new Random();
            return rand.Next(0, 1000000);
        }


        public static List<int> GerarTrocado(int valor, List<int> notasDisponiveis, List<int> retorno = null)
        {
            int restante = valor;
            if (retorno == null) retorno = new List<int>();
            if (restante >= Notas.Cinquenta && notasDisponiveis.Contains(Notas.Cinquenta))
            {
                restante -= Notas.Cinquenta;
                retorno.Add(Notas.Cinquenta);
            }
            else if (restante >= Notas.Vinte && notasDisponiveis.Contains(Notas.Vinte))
            {
                restante -= Notas.Vinte;
                retorno.Add(Notas.Vinte);
            }
            else if (restante >= Notas.Dez && notasDisponiveis.Contains(Notas.Dez))
            {
                restante -= Notas.Dez;
                retorno.Add(Notas.Dez);
            }
            else if (restante >= Notas.Cinco && notasDisponiveis.Contains(Notas.Cinco))
            {
                restante -= Notas.Cinco;
                retorno.Add(Notas.Cinco);
            }
            else if (restante >= Notas.Dois && notasDisponiveis.Contains(Notas.Dois))
            {
                restante -= Notas.Dois;
                retorno.Add(Notas.Dois);
            }

            if (restante >= 0)
            {
                return GerarTrocado(restante, notasDisponiveis, retorno);
            }
            else
            {
                return retorno;
            }
        }

    }
}
