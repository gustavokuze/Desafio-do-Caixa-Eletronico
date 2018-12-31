using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaEletronico
{
    class Program
    {
        public static int Ini { get; set; } = 119; //valores padrão para os cenários dos emails
        public static int Fin { get; set; } = 120;
        public static bool IsTestEnv { get; set; } = false;

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
            Console.WriteLine(" 3- Teste");
            Console.Write(" 4 - Sair\n\n>>> ");
            try
            {
                caixa.MostraCedulasDisponiveis();
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
                case 4:
                    Environment.Exit(0);
                    break;
                case 3:
                    RunTest();
                    Menu();
                    break;
                case 2: //mostrar saldo
                    Console.Clear();
                    Console.WriteLine($"Seu saldo atual é: {caixa.Saldo}\n\n");
                    Menu();
                    break;
                case 1: //sacar
                    IsTestEnv = false;
                    Sacar();
                    break;
                default: // entrada inválida
                    Console.Clear();
                    Menu();
                    break;
            }
        }

        private static void RunTest()
        {
            IsTestEnv = true;

            Console.WriteLine("Digite o valor de inicio do teste: ");
            Ini = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Digite o valor final do teste: ");
            Fin = Convert.ToInt32(Console.ReadLine());


            for (int i = Ini; i <= Fin; i++)
            {
                Sacar(i);
            }
            Console.WriteLine($"O teste correu de {Ini} a {Fin}");
            Console.ReadKey();
        }

        private static void Sacar(int iTeste = 0)
        {
            if (!IsTestEnv)
                Console.Clear();

            caixa.RenovaSaque();
            if (!IsTestEnv)
                Console.WriteLine("Digite o valor que deseja sacar: ");
            try
            {
                int valor = iTeste;
                if (!IsTestEnv)
                    valor = Convert.ToInt32(Console.ReadLine());
                if (IsTestEnv)
                    Console.WriteLine($"-----Iniciando saque {valor}-----");
                var verificacaoOk = VerificacaoInicial(valor);

                if (verificacaoOk)
                {
                    var saqueResultado = caixa.Sacar(valor);

                    if (saqueResultado.EstaValido())
                    {
                        Console.Write($"\nValor solicitado: {valor}\n");
                        for (int i = 0; i < saqueResultado.HistoricoResto.Count; i++)
                        {
                            Console.Write($"{saqueResultado.NotasUsadasLog[i]}\n\n");
                        }
                        int somaSaque = saqueResultado.NotasUsadas.Sum();
                        Console.WriteLine($"Soma das notas usadas: {somaSaque}");
                        caixa.Saldo -= somaSaque;
                        if (!IsTestEnv)
                            Menu();
                    }
                    else
                    {
                        if (!IsTestEnv)
                            MsgCedulasInsuficientes();
                        else
                            Console.WriteLine($"Mensagem de Cedulas insuficientes. NotasUsadas: {saqueResultado.NotasUsadas.Count}; NotasUsadasLog: {saqueResultado.NotasUsadasLog.Count}; HistoricoResto: {saqueResultado.HistoricoResto.Count}");
                    }
                }
                else
                {
                    Console.WriteLine($"Verificação Inicial falhou com o valor {valor}");
                }

            }
            catch (FormatException ex)
            {
                Console.Clear();
                Console.Write($"Valor inválido. Por favor entre com um valor inteiro em reais, sem pontos, virgulas ou qualquer tipo de simbolo.\n\n");
                Menu();
            }
            if (IsTestEnv)
                Console.WriteLine("____Fim saque____\n");
        }

        private static bool VerificacaoInicial(int valor)
        {
            if (valor >= caixa.Saldo)
            {
                MsgSaldoInsuficiente();
                return false;
            }

            if (valor == 3 || valor == 1)
            {
                MsgMoedas();
                return false;
            }

            if (!caixa.SaquePossivelParaValor(valor))
            {
                MsgCedulasInsuficientes();
                return false;
            }
            return true;
        }

        private static void MsgSaldoInsuficiente()
        {
            if (!IsTestEnv)
                Console.Clear();
            Console.Write($"Erro: Saldo insuficiente!\n\n");
            if (!IsTestEnv)
                Menu();
        }

        private static void MsgMoedas()
        {
            if (!IsTestEnv)
                Console.Clear();
            Console.Write($"Erro: Para o saque de moedas, visite o caixa!\n\n");
            if (!IsTestEnv)
                Menu();
        }

        private static void MsgCedulasInsuficientes()
        {
            if (!IsTestEnv)
                Console.Clear();
            Console.Write($"Erro: Cédulas insuficientes para realizar o saque!\n\n");
            if (!IsTestEnv)
                Menu();
        }
    }
}
