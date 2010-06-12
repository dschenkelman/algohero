using System.Collections.Generic;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Files.Interfaces
{
    public interface IProveedorCancionesDirectorio
    {
        IEnumerable<Cancion> ObtenerCancionesDirectorio(string path);
    }
}
