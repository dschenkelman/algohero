using AlgoHero.MusicEntities.Enums;
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
            double tiempoBlanca = tiempoCancion.DuracionCompas / tiempoCancion.CantidadBlancas;
            
            switch (nota.Figura)
            {
                case FiguraMusical.Redonda:
                    return tiempoBlanca * 2;
                case FiguraMusical.Blanca:
                    return tiempoBlanca;
                case FiguraMusical.Negra:
                    return tiempoBlanca / 2;
                case FiguraMusical.Corchea:
                    return tiempoBlanca / 4;
                case FiguraMusical.Semicorchea:
                    return tiempoBlanca / 8;
                case FiguraMusical.Fusa:
                    return tiempoBlanca / 16;
                case FiguraMusical.Semifusa:
                    return tiempoBlanca / 32;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
    }
}
