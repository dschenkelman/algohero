using System;
using AlgoHero.Entities.Properties;

namespace AlgoHero.Entities
{
    public class TiempoCancion
    {
        public TiempoCancion(double duracionCompas, int cantidadBlancas)
        {
            if (duracionCompas <= 0)
            {
                throw new ArgumentException(Resources.DuracionCompasIncorrecta);
            }

            if (cantidadBlancas <= 0)
            {
                throw new ArgumentException(Resources.DuracionCompasIncorrecta);
            }

            this.DuracionCompas = duracionCompas;
            this.CantidadBlancas = cantidadBlancas;
        }

        public double DuracionCompas
        {
            get; private set;
        }

        public int CantidadBlancas
        {
            get; private set;
        }
    }
}
