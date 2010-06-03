using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoHero.Player
{
    public class EntidadEntrada
    {
        private int codigo;

        public EntidadEntrada(int codigo)
        {
            this.setCodigo(codigo);
        }

        private void setCodigo(int codigo)
        {
            this.codigo = codigo;
        }

        public int getCodigo()
        {
            return this.codigo;
        }

    }
}
