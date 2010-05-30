using System;

namespace AlgoHero.MusicEntities.Core
{
    public class Cancion
    {
        public Cancion(string nombre, string autor)
        {
            this.Nombre = nombre;
            this.Autor = autor;
        }

        public string Nombre { get; private set; }
        public string Autor { get; private set; }
        public Partitura Partitura{ get; set; }
    }
}
