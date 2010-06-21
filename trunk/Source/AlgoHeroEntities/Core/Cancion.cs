namespace AlgoHero.MusicEntities.Core
{
    public class Cancion
    {
        /*Crea una nueva cancion asignando el nombre y autor recibido como propiedades.*/
        public Cancion(string nombre, string autor)
        {
            this.Nombre = nombre;
            this.Autor = autor;
        }

        public string Nombre { get; private set; }
        public string Autor { get; private set; }
        public Partitura Partitura{ get; set; }
        public string PathPartitura{ get; set; }
    }
}
