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

        public string Nombre { get; set; }
        public string Autor { get; set; }
    }
}
