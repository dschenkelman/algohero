using System;
using AlgoHero.MusicEntities.Properties;

namespace AlgoHero.MusicEntities.Core
{
    public class TiempoCancion
    {
        /*Crea un nuevo TiempoCancion a partir de la duracion del compas y la cantidad de blancas por compas.*/
        public TiempoCancion(double duracionCompas, double cantidadNegras)
        {
            if (duracionCompas <= 0)
            {
                throw new ArgumentException(Resources.DuracionCompasIncorrecta);
            }

            if (cantidadNegras <= 0)
            {
                throw new ArgumentException(Resources.DuracionCompasIncorrecta);
            }

            this.DuracionCompas = duracionCompas;
            this.CantidadNegras = cantidadNegras;
        }

        public double DuracionCompas
        {
            get; private set;
        }

        public double CantidadNegras
        {
            get; private set;
        }
    }
}
