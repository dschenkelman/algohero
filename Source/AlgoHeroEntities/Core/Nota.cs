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
        private List<Tono> tonos;

        public Nota(List<Tono> tonos, FiguraMusical figura)
        {
            this.tonos = tonos;
            this.Figura = figura;
            this.calculadorDuracion = new CalculadorDuracionNotas();
        }

        public Nota(Tono tono, FiguraMusical figura)
        {
            this.tonos = new List<Tono>(){tono};
            this.Figura = figura;
            this.calculadorDuracion = new CalculadorDuracionNotas();
        }

        #region Properties
        
        public FiguraMusical Figura
        {
            get;
            private set;
        }

        public ReadOnlyCollection<Tono> Tonos
        {
            get { return this.tonos.AsReadOnly(); }
        }

        #endregion
        

        public double CalcularTiempoProximaNota(TiempoCancion tiempoCancion)
        {
            return this.calculadorDuracion.CalcularDuracion(tiempoCancion, this);
        }
    }
}
