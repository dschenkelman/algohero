using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoHero.Player
{
    public class EntidadEntrada
    {

        public EntidadEntrada(int codigo)
        {
            this.Codigo = codigo;
        }

        public int Codigo { get; private set; }

    }
}
