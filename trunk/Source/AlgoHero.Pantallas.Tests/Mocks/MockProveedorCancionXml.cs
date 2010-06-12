using System;
using System.Collections.Generic;
using AlgoHero.Files.Interfaces;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Pantallas.Tests.Mocks
{
    public class MockProveedorCancionXml : IProveedorCancion
    {
        public Cancion ObtenerCancionSinPartitura(string path)
        {
            throw new NotImplementedException();
        }

        public Cancion ObtenerCancionConPartitura(string path)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Cancion> ObtenerCancionesDirectorio(string path)
        {
            List<Cancion> canciones = new List<Cancion>();
            canciones.Add(new Cancion("We will rock you", "Queen"));
            canciones.Add(new Cancion("Jijiji", "Los redondos"));
            canciones.Add(new Cancion("Hotel California", "Eagles"));
            return canciones;
        }
    }
}
