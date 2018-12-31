using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaEletronico
{
    public class ATM
    {
        public int Saldo { get; set; } = 0;
        private Saque saque = new Saque();

        public List<int> NotasDisponiveis { get; set; } = new List<int>();
        
        public ATM()
        {
            //numero inicial de cada nota
            int notas50 = (5);
            int notas20 = (10);
            int notas10 = (15);
            int notas5 = (10);
            int notas2 = (25);
            for (int i = 0; i < notas50; i++)
            {
                NotasDisponiveis.Add(Notas.Cinquenta);
            }
            for (int i = 0; i < notas20; i++)
            {
                NotasDisponiveis.Add(Notas.Vinte);
            }
            for (int i = 0; i < notas10; i++)
            {
                NotasDisponiveis.Add(Notas.Dez);
            }
            for (int i = 0; i < notas5; i++)
            {
                NotasDisponiveis.Add(Notas.Cinco);
            }
            for (int i = 0; i < notas2; i++)
            {
                NotasDisponiveis.Add(Notas.Dois);
            }
        }

        public void RenovaSaque()
        {
            saque = new Saque();
        }

        public Saque Sacar(int valor)
        {
            var trocado = Utils.CalculaTrocado(valor, NotasDisponiveis);
            if (trocado.Count > 0)
            {
                DescontaValores(valor, trocado);
                if (Program.IsTestEnv)
                {
                    Console.WriteLine("-(( Valores descontados com sucesso, baseados no retorno da função CalculaTroco ))-");
                    MostraCedulasDisponiveis();                }
                    
                return saque;
            }
            if (Program.IsTestEnv)
                Console.WriteLine("-(( Erro: A função CalculaTroco retornou vazio ))-");
            return new Saque();
        }


        public void MostraCedulasDisponiveis()
        {
            Console.WriteLine($"-(( Cedulas disponiveis: 50 = { NotasDisponiveis.Where(x => x == 50).Count() }; 20 = { NotasDisponiveis.Where(x => x == 20).Count() }; 10 = { NotasDisponiveis.Where(x => x == 10).Count() }; 5 = { NotasDisponiveis.Where(x => x == 5).Count() }; 2 = { NotasDisponiveis.Where(x => x == 2).Count() } ))-");
        }

        private int DescontaValores(int valor, List<int> notas)
        {
            int resto = valor;
            notas.ForEach(nota =>
            {
                resto -= nota;
                saque.HistoricoResto.Add(resto);
                saque.NotasUsadas.Add(nota);
                saque.NotasUsadasLog.Add($"Foi usada uma nota de {nota}; Restou: {resto}");
                NotasDisponiveis.Remove(nota);
            });
            
            return resto;
        }

        public bool SaquePossivelParaValor(int valor)
        {
            if (NotasDisponiveis.Sum() > valor)
            {
                if (valor % 2 != 0)
                {
                    if (!NotaDisponivel(Notas.Cinco))
                    {
                        return false;
                    }
                }
                return true;
            }

            return false;
        }

        private bool NotaDisponivel(int nota)
        {
            return NotasDisponiveis.Contains(nota);
        }

    }
}
