using System;
using AlgoHero.Juego.Intefaces;

namespace AlgoHero.Juego.Core
{
    public class Nivel
    {
        private readonly IEstrategiaNivel estrategiaNivel;

        public Nivel(string descripcion, IEstrategiaNivel estrategiaNivel)
        {
            this.Descripcion = descripcion;
            this.estrategiaNivel = estrategiaNivel;
        }

        public string Descripcion
        {
            get; private set;
        }
    }
}
