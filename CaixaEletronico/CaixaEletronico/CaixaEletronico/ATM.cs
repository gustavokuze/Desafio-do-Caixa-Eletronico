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
            bool precisaDeAjuste = (valor % 10) == 3 || (valor % 10) == 8;

            if (valor >= cinquenta)
            {
                resto = (precisaDeAjuste) ?
                   resto = AjustaDesconto50(valor) : DescontaValor(valor, cinquenta, $"Foi usada uma nota de {cinquenta}");
            }
            else if (valor >= 40)
            {
                resto = (precisaDeAjuste) ?
                    resto = AjustaDesconto40(valor) : DescontaValor(valor, vinte, $"Foi usada uma nota de {vinte}");
            }
            else if (valor >= 30)
            {
                resto = (precisaDeAjuste) ?
                    resto = AjustaDesconto30(valor) : DescontaValor(valor, vinte, $"Foi usada uma nota de {vinte}");
            }
            else if (valor >= vinte)
            {
                resto = (precisaDeAjuste) ?
                    resto = AjustaDesconto20(valor) : DescontaValor(valor, vinte, $"Foi usada uma nota de {vinte}");
            }
            else if (valor >= dez)
            {
                resto = (precisaDeAjuste) ?
                      resto = AjustaDesconto10(valor) : DescontaValor(valor, dez, $"Foi usada uma nota de {dez}");

            }
            else if (valor >= cinco)
            {
                resto = ((valor % 10) == 8) ?
                      resto = Desconta8(valor) : DescontaValor(valor, cinco, $"Foi usada uma nota de {cinco}");
                
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

        private int AjustaDesconto50(int valor)
        {
            string logMsg = "Foram usadas duas notas de 20, uma de 5 e 4 notas de 2";
            var listaNotas = new List<int>() {
                vinte, vinte, cinco, dois, dois, dois, dois
            };
            //if ((valor % 10) == 8)
            if ((valor > 53))
            {
                listaNotas.Remove(cinco);
                listaNotas.Remove(vinte);
                listaNotas.Remove(vinte);
                listaNotas.Add(cinquenta);
                logMsg = "Foram usadas uma nota de 50 e 4 notas de 2";
            }
            int resto = DescontaValores(valor, listaNotas, logMsg);
            return resto;
        }

        private int AjustaDesconto40(int valor)
        {
            string logMsg = "Foram usadas uma nota de 20, uma de 10, uma de 5 e 4 notas de 2";
            var listaNotas = new List<int>() {
                vinte, dez, cinco, dois, dois, dois, dois
            };
            if ((valor % 10) == 8)
            {
                listaNotas.Remove(dez);
                listaNotas.Remove(cinco);
                listaNotas.Add(vinte);
                logMsg = "Foram usadas duas notas de 20 e 4 notas de 2";
            }
            int resto = DescontaValores(valor, listaNotas, logMsg);
            return resto;
        }

        private int AjustaDesconto30(int valor)
        {
            string logMsg = "Foram usadas uma nota de 20, uma de 5 e 4 notas de 2";
            var listaNotas = new List<int>() {
                vinte, cinco, dois, dois, dois, dois
            };
            if ((valor % 10) == 8)
            {
                listaNotas.Remove(cinco);
                listaNotas.Add(dez);
                logMsg = "Foram usadas uma nota de 20, uma de 10 e 4 notas de 2";
            }
            int resto = DescontaValores(valor, listaNotas, logMsg);
            return resto;
        }


        private int AjustaDesconto20(int valor)
        {
            string logMsg = "Foram usadas uma nota de 10, uma de 5 e 4 notas de 2";
            var listaNotas = new List<int>() {
                dez, cinco, dois, dois, dois, dois
            };
            if ((valor % 10) == 8)
            {
                listaNotas.Remove(dez);
                listaNotas.Remove(cinco);
                listaNotas.Add(vinte);
                logMsg = "Foram usadas uma nota de vinte e 4 notas de 2";
            }
            int resto = DescontaValores(valor, listaNotas, logMsg);
            return resto;
        }

        private int AjustaDesconto10(int valor)
        {
            string logMsg = "uma nota de 5 e 4 notas de 2";
            var listaNotas = new List<int>() {
                cinco, dois, dois, dois, dois
            };
            if ((valor % 10) == 8)
            {
                listaNotas.Remove(cinco);
                listaNotas.Add(dez);
                logMsg = "Foram usadas uma nota de 10 e 4 notas de 2";
            }
            int resto = DescontaValores(valor, listaNotas, logMsg);
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


        private int Desconta6(int valor)
        {
            int resto = valor - 6;
            saque.HistoricoResto.Add(resto);
            for (int i = 0; i < 3; i++) { saque.NotasUsadas.Add(dois); }
            saque.NotasUsadasLog.Add("Foram usadas 3 notas de 2");
            return resto;
        }


        private int Desconta8(int valor)
        {
            int resto = valor - 8;
            saque.HistoricoResto.Add(resto);
            for (int i = 0; i < 4; i++) { saque.NotasUsadas.Add(dois); }
            saque.NotasUsadasLog.Add("Foram usadas 4 notas de 2");
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