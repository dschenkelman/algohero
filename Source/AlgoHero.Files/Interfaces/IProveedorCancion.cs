using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Files.Interfaces
{
    public interface IProveedorCancion
    {
        Cancion ObtenerCancion(string path);
    }
}
