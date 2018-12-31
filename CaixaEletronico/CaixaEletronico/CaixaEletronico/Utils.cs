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
            if (Program.IsTestEnv)
                Console.WriteLine("-(( Entrou na função CalculaTroco ))-");
            int restante = valor;
            List<int> notasAbaixoValor = notasDisponiveis.Where(x => x <= valor).OrderByDescending(x => x).ToList();
            List<int> notasUsadas = new List<int>();

            while (restante != 0)
            {
                if (restante > 1 && restante != 3)
                {
                    var nota = (restante % 2 == 0) ? notasAbaixoValor.FirstOrDefault() : notasAbaixoValor.Where(x => x == 5).FirstOrDefault();
                    if (Program.IsTestEnv)
                        Console.WriteLine($"-(( Nota escolhida: {nota} ))-");
                    if (nota > 0)
                    {
                        restante -= nota;
                        notasUsadas.Add(nota);
                        notasAbaixoValor = notasDisponiveis.Where(x => x <= valor).OrderByDescending(x => x).ToList();
                        notasUsadas.ForEach(n => { if (notasAbaixoValor.Contains(n)) notasAbaixoValor.Remove(n); });
                        notasAbaixoValor.Remove(nota);
                    }
                    else
                    {
                        if (Program.IsTestEnv)
                            Console.WriteLine($"-(( Erro, a nota escolhida era menor ou igual a 0 ))-");
                        restante = 0;
                        notasUsadas.Clear();
                    }

                    if (restante == 0)
                    {
                        return notasUsadas;
                    }
                }
                else if (restante < 0 || restante == 3 || restante == 1)
                {
                    bool existeMenor = notasAbaixoValor.OrderByDescending(x => x).Last() < notasUsadas.Last();
                    int notaErrada = (existeMenor) ? notasUsadas.Last() : notasUsadas.Max();
                    if (Program.IsTestEnv)
                        Console.WriteLine($"-(( Revertendo nota [{notaErrada}] , o restante era menor que 1 ou era 3 ))-");
                    restante += notaErrada;
                    notasAbaixoValor = notasDisponiveis.Where(x => x <= restante).OrderByDescending(x => x).ToList();
                    notasAbaixoValor.RemoveAll(x => x == notaErrada);
                    notasUsadas.Remove(notaErrada);
                }
            }

            if (notasUsadas.Count > 0)
                return notasUsadas;
            return new List<int>();
        }

    }
}
