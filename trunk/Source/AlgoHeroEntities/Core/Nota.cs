using System.Linq;
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

        /*Crea una nueva nota con los tonos y la figura musical recibidos.*/
        public Nota(IEnumerable<Tono> tonos, FiguraMusical figura)
        {
            this.tonos = new List<Tono>(tonos);
            this.Figura = figura;
            this.calculadorDuracion = new CalculadorDuracionNotas();
        }

        /*Crea una nueva nota con el tono y la figura recibidos como parametros.*/
        public Nota(Tono tono, FiguraMusical figura)
        {
            this.tonos = new List<Tono>(){tono};
            this.Figura = figura;
            this.calculadorDuracion = new CalculadorDuracionNotas();
        }

        /*Crea una nueva nota con los tonos y la figura musical recibidos.*/
        public Nota(FiguraMusical figura, params Tono[] tonos)
        {
            this.tonos = new List<Tono>(tonos.AsEnumerable());
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
        
        /*En base al tiempo de la cancion calcula el tiempo entre la nota actual
         * (dependiendo de su Figura) y la proxima nota.*/
        public double CalcularTiempoProximaNota(TiempoCancion tiempoCancion)
        {
            return this.calculadorDuracion.CalcularDuracion(tiempoCancion, this);
        }
    }
}
