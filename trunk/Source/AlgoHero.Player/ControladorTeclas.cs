using System;
using System.Collections.Generic;
using AlgoHero.Interface;

namespace AlgoHero.Player
{
    public class ControladorTeclas : IControladorTeclas
    {
        private readonly IMapeoTecladoEntidadesEntrada mapeoTecladoEntidadesEntrada;
        private List<Tecla> teclas;

        public ControladorTeclas(IMapeoTecladoEntidadesEntrada mapeoTecladoEntidadesEntrada)
        {
            this.mapeoTecladoEntidadesEntrada = mapeoTecladoEntidadesEntrada;
            this.teclas = new List<Tecla>();

            foreach(EntidadEntrada entidad in mapeoTecladoEntidadesEntrada.ObtenerEntidadesEntrada())
            {
                this.teclas.Add(new Tecla(entidad));
            }
        }

        public int CantidadTeclas
        {
            get { return this.teclas.Count; }
        }

        public ITecla ObtenerTecla(int index)
        {
            return this.teclas[index];
        }

        public IEnumerable<ITecla> ObtenerTeclas()
        {
            throw new NotImplementedException();
        }

        public ITecla ObtenerTecla(EntidadEntrada entidadEntrada)
        {
            throw new NotImplementedException();
        }
    }
}
