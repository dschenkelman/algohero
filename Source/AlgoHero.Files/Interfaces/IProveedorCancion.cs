using System.Collections.Generic;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Files.Interfaces
{
    public interface IProveedorCancion
    {
        Cancion ObtenerCancionSinPartitura(string path);
        Cancion ObtenerCancionConPartitura(string path);
    }
}
