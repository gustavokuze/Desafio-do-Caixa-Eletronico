using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaEletronico
{
    public class Saque
    {
        public List<int> NotasUsadas { get; set; } = new List<int>();
        public List<string> NotasUsadasLog { get; set; } = new List<string>();
        public List<int> HistoricoResto { get; set; } = new List<int>();
        public bool EstaValido()
        {
            return (this.NotasUsadas.Count > 0 && this.NotasUsadasLog.Count > 0 && this.HistoricoResto.Count > 0);
        }
    }
}
