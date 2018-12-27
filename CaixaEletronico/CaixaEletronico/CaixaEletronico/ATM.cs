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

        public List<int> NotasDisponiveis { get; set; } = new List<int>() {
            Notas.Cinquenta,
            Notas.Vinte, Notas.Vinte, Notas.Vinte, Notas.Vinte,
            Notas.Vinte, Notas.Vinte, Notas.Vinte, Notas.Vinte, Notas.Vinte, Notas.Vinte
            //Notas.Dez,Notas.Cinco,Notas.Cinco,Notas.Cinco,Notas.Cinco, Notas.Dois,Notas.Dois
        };

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
                return saque;
            }

            return new Saque();
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

        private int DescontaValores(int valor, List<int> notas)
        {
            int resto = valor;
            notas.ForEach(nota =>
            {
                resto -= nota;
                saque.HistoricoResto.Add(resto);
                saque.NotasUsadas.Add(nota);
                saque.NotasUsadasLog.Add($"Foi usada uma nota de {nota}; Restou: {resto}");
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





/*
 
     
     if ((valor % 2) != 0)
            {
                resto = DescontaValor(valor, Notas.Cinco, "Foi usada uma nota de 5");
            }
            else if (valor >= Notas.Cinquenta)
            {
                var trocado = Utils.CalculaTrocado(Notas.Cinquenta, NotasDisponiveis);
                if (trocado != null && trocado.Count > 0)
                {
                    resto = DescontaValores(valor, trocado, $"Foram usadas as seguintes notas: {string.Join(", ", trocado)}");
                }
                else
                {
                    return new Saque(); //rever isto
                }

            }
            else if (valor >= 40)
            {
                resto = DescontaValor(valor, Notas.Vinte, $"Foi usada uma nota de {Notas.Vinte}");
            }
            else if (valor >= 30)
            {
                resto = DescontaValor(valor, Notas.Vinte, $"Foi usada uma nota de {Notas.Vinte}");
            }
            else if (valor >= Notas.Vinte)
            {
                resto = DescontaValor(valor, Notas.Vinte, $"Foi usada uma nota de {Notas.Vinte}");
            }
            else if (valor >= Notas.Dez)
            {
                resto = DescontaValor(valor, Notas.Dez, $"Foi usada uma nota de {Notas.Dez}");

            }
            else if (valor >= Notas.Cinco)
            {
                if (valor % 10 == 8)
                {
                    resto = DescontaValor(valor, 8, Notas.Dois);
                }
                else if (valor % 10 == 6)
                {
                    resto = DescontaValor(valor, 6, Notas.Dois);
                }
                else
                {
                    DescontaValor(valor, Notas.Cinco, $"Foi usada uma nota de {Notas.Cinco}");
                }
            }
            else if (valor >= Notas.Dois)
            {
                resto = DescontaValor(valor, Notas.Dois, $"Foi usada uma nota de {Notas.Dois}");
            }
     
     
     
            if (valor == 0)
                return saque;
     
     
     
     
     
     
     
     
     */
