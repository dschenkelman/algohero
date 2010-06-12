using System;
using System.Collections.Generic;
using AlgoHero.Files.Interfaces;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Pantallas.Tests.Mocks
{
    public class MockProveedorCanciones : IProveedorCancionesDirectorio
    {
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
