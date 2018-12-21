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
        public void renovaSaque()
        {
            saque = new Saque();
        }

        public Saque Sacar(int valor)
        {
            int resto = 0;

            if (valor >= cinquenta)
            {
                resto = DescontaValor(valor, cinquenta);
            }
            else if (valor >= vinte)
            {
                resto = ((valor % 10) == 3)? 
                    resto = DescontaValor(valor, dez) : DescontaValor(valor, vinte);
            }
            else if (valor >= dez)
            {
                if (valor == 13)
                {
                    resto = Desconta13Pila(valor);
                }
                else
                {
                    resto = DescontaValor(valor, dez);
                }

            }
            else if (valor >= cinco)
            {
                resto = DescontaValor(valor, cinco);
            }
            else if (valor >= dois)
            {
                resto = DescontaValor(valor, dois);
            }
            else if (valor == 1)
            {
                resto = valor += saque.NotasUsadas.Last();
                RemoveUltimoValorHistorico();

                resto = Desconta6Pila(valor);
            }

            if (valor == 0)
                return saque;
            return Sacar(resto);
        }

        private int DescontaValor(int valor, int nota)
        {
            int resto = valor - nota;
            saque.HistoricoResto.Add(resto);
            saque.NotasUsadas.Add(nota);
            saque.NotasUsadasLog.Add($"Foi usada uma nota de {nota}");
            return resto;
        }

        private int Desconta13Pila(int valor)
        {
            int resto = valor - 13;
            saque.HistoricoResto.Add(resto);
            saque.NotasUsadas.Add(cinco);
            for (int i = 0; i < 4; i++) { saque.NotasUsadas.Add(dois); }
            saque.NotasUsadasLog.Add("Foi usada uma nota de 5 e 4 notas de 2");
            return resto;
        }

        private int Desconta6Pila(int valor)
        {
            int resto = valor - 6;
            saque.HistoricoResto.Add(resto);
            for (int i = 0; i < 3; i++) { saque.NotasUsadas.Add(dois); }
            saque.NotasUsadasLog.Add("Foram usadas 3 notas de 2");
            return resto;
        }

        private void RemoveUltimoValorHistorico()
        {
            saque.HistoricoResto.RemoveAt(saque.HistoricoResto.Count - 1);
            saque.NotasUsadas.RemoveAt(saque.NotasUsadas.Count - 1);
            saque.NotasUsadasLog.RemoveAt(saque.NotasUsadasLog.Count - 1);
        }

    }
}
