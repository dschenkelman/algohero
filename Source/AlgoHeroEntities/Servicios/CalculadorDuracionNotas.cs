using AlgoHero.Interface.Enums;
using AlgoHero.MusicEntities.Servicios.Interfaces;

namespace AlgoHero.MusicEntities.Servicios
{
    using System;
    using AlgoHero.MusicEntities.Core;

    public class CalculadorDuracionNotas : ICalculadorDuracionNotas
    {
        /*Devuelve la duracion entre una nota y la siguiente a partir del tiempo 
         * de la cancion y la nota. Depende de la figura de la nota.*/
        public double CalcularDuracion(TiempoCancion tiempoCancion, Nota nota)
        {
            return this.CalcularDuracion(tiempoCancion, nota.Figura);
        }

        public double CalcularDuracion(TiempoCancion tiempoCancion, FiguraMusical figura)
        {
            double tiempoNegra = tiempoCancion.DuracionCompas / tiempoCancion.CantidadNegras;

            switch (figura)
            {
                case FiguraMusical.Redonda:
                    return tiempoNegra * 4;
                case FiguraMusical.Blanca:
                    return tiempoNegra * 2;
                case FiguraMusical.Negra:
                    return tiempoNegra;
                case FiguraMusical.Corchea:
                    return tiempoNegra / 2;
                case FiguraMusical.Semicorchea:
                    return tiempoNegra / 4;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
