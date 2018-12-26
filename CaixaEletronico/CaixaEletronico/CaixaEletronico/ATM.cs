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
        private const int dois = 2, cinco = 5, dez = 10, vinte = 20, cinquenta = 50;
        private Saque saque = new Saque();
        public List<int> NotasDisponiveis { get; private set; } = new List<int>() {
            cinquenta, vinte, vinte, vinte, vinte, vinte, vinte, vinte, vinte, vinte, vinte
        };

        public void RenovaSaque()
        {
            saque = new Saque();
        }

        public Saque Sacar(int valor)
        {
            int resto = 0;
            if ((valor % 2) != 0)
            {
                resto = DescontaValor(valor, cinco, "Foi usada uma nota de 5");
            }
            else if (valor >= cinquenta)
            {
                /*
                    ao invés de fazer assim, deveria criar uma função "geraTrocado" 
                    que retorna uma lista com o valor usando as notas disponíveis


                    posso dentro do método criar uma cópia da lista de notas disponíveis 
                    e ir removendo as notas nela mesma, pra não interferir na lista real



                 */

                if (NotaDisponivel(cinquenta))
                {
                    resto = DescontaValor(valor, cinquenta, $"Foi usada uma nota de {cinquenta}");
                }
                else if (NotasDisponiveis.Count(x => x == vinte) >= 2 && NotaDisponivel(dez))
                {
                    resto = DescontaValores(valor, new List<int>() { vinte, vinte, dez }, "Foram usadas 2 notas de 20 e uma de 10");
                }
                else if (NotasDisponiveis.Count(x => x == dez) >= 5)
                {
                    resto = DescontaValor(valor, 50, dez);
                }
                else if (NotasDisponiveis.Count(x => x == cinco) >= 10)
                {
                    resto = DescontaValor(valor, 50, cinco);
                }
                else if (NotasDisponiveis.Count(x => x == dois) >= 25)
                {
                    resto = DescontaValor(valor, 50, dois);
                }
                else
                {
                    return saque; //preciso rever isso
                }

            }
            else if (valor >= 40)
            {
                resto = DescontaValor(valor, vinte, $"Foi usada uma nota de {vinte}");
            }
            else if (valor >= 30)
            {
                resto = DescontaValor(valor, vinte, $"Foi usada uma nota de {vinte}");
            }
            else if (valor >= vinte)
            {
                resto = DescontaValor(valor, vinte, $"Foi usada uma nota de {vinte}");
            }
            else if (valor >= dez)
            {
                resto = DescontaValor(valor, dez, $"Foi usada uma nota de {dez}");

            }
            else if (valor >= cinco)
            {
                if (valor % 10 == 8)
                {
                    resto = DescontaValor(valor, 8, dois);
                }
                else if (valor % 10 == 6)
                {
                    resto = DescontaValor(valor, 6, dois);
                }
                else
                {
                    DescontaValor(valor, cinco, $"Foi usada uma nota de {cinco}");
                }
            }
            else if (valor >= dois)
            {
                resto = DescontaValor(valor, dois, $"Foi usada uma nota de {dois}");
            }

            if (valor == 0)
                return saque;
            return Sacar(resto);
        }

        private int DescontaValor(int valor, int nota, string logMsg)
        {
            int resto = valor - nota;
            saque.HistoricoResto.Add(resto);
            saque.NotasUsadas.Add(nota);
            saque.NotasUsadasLog.Add(logMsg);
            return resto;
        }

        private int DescontaValor(int valor, int desconto, int nota)
        {
            int resto = valor - desconto;
            saque.HistoricoResto.Add(resto);
            for (int i = 0; i < desconto / nota; i++) saque.NotasUsadas.Add(nota);
            saque.NotasUsadasLog.Add($"Foram usadas {desconto / nota} notas de {nota}");
            return resto;
        }

        private int DescontaValores(int valor, List<int> notas, string logMsg)
        {
            int resto = valor - notas.Sum();
            saque.HistoricoResto.Add(resto);
            saque.NotasUsadas.AddRange(notas);
            saque.NotasUsadasLog.Add(logMsg);
            return resto;
        }

        public bool PossivelSacar(int valor)
        {
            if (NotasDisponiveis.Sum() > valor)
            {
                if (valor % 2 != 0)
                {
                    if (!NotaDisponivel(cinco))
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