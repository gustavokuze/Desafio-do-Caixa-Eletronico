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
                resto = ((valor % 10) == 3) ?
                   resto = Desconta53(valor) : DescontaValor(valor, cinquenta, $"Foi usada uma nota de {cinquenta}");
            }
            else if (valor >= 40)
            {
                resto = ((valor % 10) == 3) ?
                    resto = Desconta43(valor) : DescontaValor(valor, vinte, $"Foi usada uma nota de {vinte}");
            }
            else if (valor >= 30)
            {
                resto = ((valor % 10) == 3) ?
                    resto = Desconta33(valor) : DescontaValor(valor, vinte, $"Foi usada uma nota de {vinte}");
            }
            else if (valor >= vinte)
            {
                resto = ((valor % 10) == 3)? 
                    resto = Desconta23(valor) : DescontaValor(valor, vinte, $"Foi usada uma nota de {vinte}");
            }
            else if (valor >= dez)
            {
              resto = ((valor % 10) == 3) ?
                    resto = Desconta13(valor) : DescontaValor(valor, dez, $"Foi usada uma nota de {dez}");

            }
            else if (valor >= cinco)
            {
                resto = DescontaValor(valor, cinco, $"Foi usada uma nota de {cinco}");
            }
            else if (valor >= dois)
            {
                resto = DescontaValor(valor, dois, $"Foi usada uma nota de {dois}");
            }
            else if (valor == 1)
            {
                resto = valor += saque.NotasUsadas.Last();
                RemoveUltimoValorHistorico();

                resto = Desconta6(valor);
            }

            if (valor == 0)
                return saque;
            return Sacar(resto);
        }

        private int Desconta53(int valor)
        {
            var listaNotas = new List<int>() {
                vinte, vinte, cinco, dois, dois, dois, dois
            };
            int resto = DescontaValores(valor, listaNotas, "Foram usadas duas nota de 20, uma de 5 e 4 notas de 2");
            return resto;
        }
         
        private int Desconta33(int valor)
        {
            var listaNotas = new List<int>() {
                vinte, cinco, dois, dois, dois, dois
            };
            int resto = DescontaValores(valor, listaNotas, "Foram usadas uma nota de 20, uma de 5 e 4 notas de 2");
            return resto;
        }

        private int Desconta43(int valor)
        {
            var listaNotas = new List<int>() {
                vinte, dez, cinco, dois, dois, dois, dois
            };
            int resto = DescontaValores(valor, listaNotas, "Foram usadas uma nota de 20, uma de 10, uma de 5 e 4 notas de 2");
            return resto;
        }

        private int DescontaValor(int valor, int nota, string logMsg)
        {
            int resto = valor - nota;
            saque.HistoricoResto.Add(resto);
            saque.NotasUsadas.Add(nota);
            saque.NotasUsadasLog.Add(logMsg);
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

        private int Desconta23(int valor)
        {
            var listaNotas = new List<int>() {
                dez, cinco, dois, dois, dois, dois
            };
            int resto = DescontaValores(valor, listaNotas, "Foram usadas uma nota de 10, uma de 5 e 4 notas de 2");
            return resto;
        }

        private int Desconta13(int valor)
        {
            var listaNotas = new List<int>() {
                cinco, dois, dois, dois, dois
            };
            int resto = DescontaValores(valor, listaNotas, "Foram usadas uma nota de 5 e 4 notas de 2");
            return resto;
        }

        private int Desconta6(int valor)
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
