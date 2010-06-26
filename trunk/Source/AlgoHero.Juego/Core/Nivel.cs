using System;
using AlgoHero.Juego.Intefaces;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Interface;

namespace AlgoHero.Juego.Core
{
    public class Nivel
    {
        private readonly IEstrategiaNivel estrategiaNivel;
        private Cancion cancion;

        /* Constructor. Recibe una descripcion y una IEstrategia Nivel, y crea el nivel.*/
        public Nivel(string descripcion, IEstrategiaNivel estrategiaNivel)
        {
            this.Descripcion = descripcion;
            this.estrategiaNivel = estrategiaNivel;
            this.cancion = null;
        }

        /* Este metodo recibe una Cancion por parametro y se la asigna al parametor cancion y tambien a la estrategia del nivel. */
        public void AsignarCancion(Cancion cancion)
        {
            this.cancion = cancion;
            this.estrategiaNivel.AsignarCancion(cancion);
        }

        public string Descripcion
        {
            get; private set;
        }

        public bool EsFinalCancion
        {
            get
            {
                return this.estrategiaNivel.EsFinalCancion();
            }
        }

        /* Este metodo devuelve la siguiente nota.*/
        public Nota ObtenerSiguienteNota()
        {
            return this.estrategiaNivel.ObtenerSiguienteNota();
        }

        /* Este metodo recibe un IControladorTeclas y asigna los tonos a las teclas indicadas. */
        public void AsignarTeclas(IControladorTeclas controladorTeclas)
        {
            this.estrategiaNivel.AsignarTonos(controladorTeclas);
        }
    }
}
