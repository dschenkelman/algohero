using System;
using System.Collections.Generic;
using System.Linq;
using AlgoHero.Interface;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Juego.Entrada
{
    public class ControladorTeclas : IControladorTeclas
    {
        private readonly IMapeoTecladoEntidadesEntrada mapeoTecladoEntidadesEntrada;
        private List<ITecla> teclas;

        /* Constructor. Crea un nuevo controlador de teclas. */
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

        /* Este metodo recibe un codigo de entidad entrada y devuelve una tecla asociada. */
        public ITecla ObtenerTecla(int codigoEntidadEntrada)
        {
            ITecla tecla =  this.teclas.FirstOrDefault(t => t.EntidadEntrada.Codigo == codigoEntidadEntrada);
            if (tecla == null)
            {
                throw new ArgumentException();
            }

            return tecla;
        }

        /* Este metodo devuelve una coleccion de las teclas. */
        public IEnumerable<ITecla> ObtenerTeclas()
        {
            return this.teclas;
        }

        public IEnumerable<ITecla> ObtenerTeclas(Nota nota)
        {
            throw new NotImplementedException();
        }
    }
}