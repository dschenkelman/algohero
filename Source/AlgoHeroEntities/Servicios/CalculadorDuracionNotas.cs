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

            return (tiempoNegra * this.RelacionDeFigura(figura));

        }
        
        public double RelacionDeFigura(FiguraMusical figura)
        {
            switch (figura)
            {
                case FiguraMusical.Redonda:
                    return 4.0;
                case FiguraMusical.Blanca:
                    return 2.0;
                case FiguraMusical.Negra:
                    return 1.0;
                case FiguraMusical.Corchea:
                    return 0.5;
                case FiguraMusical.Semicorchea:
                    return 0.25;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public double RelacionDeFigura(Nota nota)
        {
            return this.RelacionDeFigura(nota.Figura);
        }
    }
}
