using System;
using AlgoHero.MusicEntities.Enums;

namespace AlgoHero.MusicEntities.Core
{
    public class Nota
    {
        public Nota(Tono tono, FiguraMusical figura)
        {
            this.Tono = tono;
            this.Figura = figura;
        }

        public Tono Tono
        {
            get; private set;
        }

        public FiguraMusical Figura
        {
            get; private set;
        }
    }
}
