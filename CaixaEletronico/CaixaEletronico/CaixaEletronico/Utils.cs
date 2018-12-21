using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaixaEletronico
{
    public class Utils
    {
        public static int GerarSaldoAleatorio()
        {
            Random rand = new Random();
            return rand.Next(0, 1000000);
        }
    }
}
