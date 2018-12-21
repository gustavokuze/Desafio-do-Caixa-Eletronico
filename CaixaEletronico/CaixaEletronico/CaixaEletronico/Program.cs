using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaEletronico
{
    class Program
    {
        private static ATM caixa = new ATM();

        private static void Main(string[] args)
        {
            caixa.Saldo = Utils.GerarSaldoAleatorio();
            Menu();
        }

        private static void Menu()
        {
            Console.WriteLine(" --|---- Atendimento automatico ------|-- ");
            Console.WriteLine(@"   |  Escolha um dos servicos abaixo  | ");
            Console.WriteLine(@"   v                                  v ");
            Console.WriteLine(" 1- Sacar");
            Console.WriteLine(" 2- Ver saldo");
            Console.Write(" 3- Sair\n\n>>> ");
            try
            {
                int entry = Convert.ToInt32(Console.ReadLine());
                RunService(entry);
            }
            catch (FormatException ex)
            {
                Console.Clear();
                Console.Write($"Entrada inválida: {ex.Message}\n\n");
                Menu();
            }
        }

        private static void RunService(int entry)
        {
            switch (entry)
            {
                case 3:
                    Environment.Exit(0);
                    break;
                case 2: //mostrar saldo
                    Console.Clear();
                    Console.WriteLine($"Seu saldo atual é: {caixa.Saldo}\n\n");
                    Menu();
                    break;
                case 1: //sacar
                    Sacar();
                    break;
                default: // entrada inválida
                    Console.Clear();
                    Menu();
                    break;
            }
        }

        private static void Sacar()
        {
            Console.Clear();
            caixa.renovaSaque();
            Console.WriteLine("Digite o valor que deseja sacar: ");
            try
            {
                int valor = Convert.ToInt32(Console.ReadLine());

                if ((valor < 5)) {
                    Console.Clear();
                    Console.Write($"Para saques com valores menores do que 5 reais, visite o caixa\n");
                    Menu();
                } else if ((valor > 1000)) {
                    Console.Clear();
                    Console.Write($"Para saques com valores maiores do que 1000 reais, visite o caixa\n");
                    Menu();
                }

                if (valor >= caixa.Saldo)
                {
                    Console.Clear();
                    Console.Write($"Erro: Saldo insuficiente!\n\n");
                    Menu();
                }

                var saqueResultado = caixa.Sacar(valor);


                for (int i = 0; i < saqueResultado.HistoricoResto.Count; i++)
                {
                    Console.Write($"{saqueResultado.NotasUsadasLog[i]}; Restou: {saqueResultado.HistoricoResto[i]}\n\n");
                }
                int somaSaque = saqueResultado.NotasUsadas.Sum();
                Console.WriteLine($"Soma das notas usadas: {somaSaque}");
                caixa.Saldo -= somaSaque;
                Menu();
            }
            catch (FormatException ex)
            {
                Console.Clear();
                Console.Write($"Valor inválido. Por favor entre com um valor inteiro em reais, sem pontos, virgulas ou qualquer tipo de simbolo.\n\n");
                Menu();
            }
        }
    }
}
