using AlgoHero.MusicEntities.Servicios.Interfaces;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;

namespace AlgoHero.MusicEntities.Core
{
    using AlgoHero.MusicEntities.Enums;
    using AlgoHero.MusicEntities.Servicios;

    public class Nota
    {
        private ICalculadorDuracionNotas calculadorDuracion;

        public Nota(List<Tono> tonos, FiguraMusical figura)
        {
            this.Tonos = tonos;
            this.Figura = figura;
            this.calculadorDuracion = new CalculadorDuracionNotas();
        }

        public Nota(Tono tono, FiguraMusical figura)
        {
            List<Tono> tonos = new List<Tono>();
            tonos.Add(tono);
            this.Tonos = tonos;
            this.Figura = figura;
            this.calculadorDuracion = new CalculadorDuracionNotas();
        }

        #region Properties
        private List<Tono> Tonos
        {
            get;
            set;
        }

        public FiguraMusical Figura
        {
            get;
            private set;
        }
        #endregion
        
        public ReadOnlyCollection<Tono> ObtenerTonos()
        {
            return this.Tonos.AsReadOnly();
        }

        public double CalcularTiempoProximaNota(TiempoCancion tiempoCancion)
        {
            return this.calculadorDuracion.CalcularDuracion(tiempoCancion, this);
        }
    }
}
