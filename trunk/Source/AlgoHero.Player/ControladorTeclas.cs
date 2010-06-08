using System;
using System.Collections.Generic;
using System.Linq;
using AlgoHero.Interface;

namespace AlgoHero.Player
{
    public class ControladorTeclas : IControladorTeclas
    {
        private readonly IMapeoTecladoEntidadesEntrada mapeoTecladoEntidadesEntrada;
        private List<ITecla> teclas;

        public ControladorTeclas(IMapeoTecladoEntidadesEntrada mapeoTecladoEntidadesEntrada)
        {
            this.mapeoTecladoEntidadesEntrada = mapeoTecladoEntidadesEntrada;
            this.teclas = new List<ITecla>();

            foreach(EntidadEntrada entidad in mapeoTecladoEntidadesEntrada.ObtenerEntidadesEntrada())
            {
                this.teclas.Add(new Tecla(entidad));
            }
        }

        public int CantidadTeclas
        {
            get { return this.teclas.Count; }
        }

        public ITecla ObtenerTecla(int codigoEntidadEntrada)
        {
            return this.teclas.First(t => t.EntidadEntrada.Codigo == codigoEntidadEntrada);
        }

        public IEnumerable<ITecla> ObtenerTeclas()
        {
            return this.teclas;
        }
    }
}
