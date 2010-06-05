using System;
using AlgoHero.MusicEntities.Properties;

namespace AlgoHero.MusicEntities.Core
{
    public class TiempoCancion
    {
        /*Crea un nuevo TiempoCancion a partir de la duracion del compas y la cantidad de blancas por compas.*/
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
