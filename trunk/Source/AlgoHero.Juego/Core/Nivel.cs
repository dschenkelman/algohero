using System;
using AlgoHero.Juego.Intefaces;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Juego.Core
{
    public class Nivel
    {
        private readonly IEstrategiaNivel estrategiaNivel;
        private Cancion cancion;

        public Nivel(string descripcion, IEstrategiaNivel estrategiaNivel)
        {
            this.Descripcion = descripcion;
            this.estrategiaNivel = estrategiaNivel;
            this.cancion = null;
        }

        public void AsignarCancion(Cancion cancion)
        {
            this.cancion = cancion;
            this.estrategiaNivel.AsignarCancion(cancion);
        }

        public string Descripcion
        {
            get; private set;
        }
    }
}
