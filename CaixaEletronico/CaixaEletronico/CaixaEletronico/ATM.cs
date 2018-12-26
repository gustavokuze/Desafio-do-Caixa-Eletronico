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

        public void RenovaSaque()
        {
            saque = new Saque();
        }

        public Saque Sacar(int valor)
        {
            int resto = 0;
            if ((valor % 2) != 0 && ((valor) != 8 || valor == 5))
            {
                resto = DescontaValor(valor, cinco, "Foi usada uma nota de 5");
            }
            else if (valor >= cinquenta)
            {
                resto = DescontaValor(valor, cinquenta, $"Foi usada uma nota de {cinquenta}");
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
            for (int i = 0; i < desconto / nota; i++)  saque.NotasUsadas.Add(nota); 
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
    }
}