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
        
        /// <summary>
        /// Calcula quais notas dentre as disponíveis devem ser usadas
        /// </summary>
        /// <param name="valor">O valor que deve ser trocada</param>
        /// <returns>Caso tudo tenha ocorrido bem, a lista das cédulas que devem ser usadas para retirar esse valor.
        /// Do contrário, retorna uma lista vazia</returns>
        public static List<int> CalculaTrocado(int valor, List<int> notasDisponiveis)
        {
            int restante = valor;
            List<int> notasAbaixoValor = notasDisponiveis.Where(x => x <= valor).OrderByDescending(x => x).ToList();
            List<int> notasUsadas = new List<int>();

            while (restante != 0)
            {
                if (restante > 0)
                {
                    var nota = (valor % 10 == 0) ? notasAbaixoValor.FirstOrDefault() : notasAbaixoValor.Where(x => x == 5).FirstOrDefault();

                    if (nota > 0)
                    {
                        restante -= nota;
                        notasUsadas.Add(notasAbaixoValor.First());
                        notasAbaixoValor.Remove(notasAbaixoValor.First());
                    }
                    else
                    {
                        restante = 0;
                        notasUsadas.Clear();
                    }

                    if (restante == 0)
                    {
                        return notasUsadas;
                    }
                }
                else if (restante < 0)
                {
                    int notaErrada = notasUsadas.Max();
                    restante += notaErrada;
                    notasUsadas.Remove(notaErrada); //reverte a ultima nota adicionada
                }
            }

            if (notasUsadas.Count > 0)
                return notasUsadas;
            return new List<int>();
        }

    }
}
