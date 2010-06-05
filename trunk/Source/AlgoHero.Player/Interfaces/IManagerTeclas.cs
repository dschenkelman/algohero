using System.Collections.Generic;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Player.Interfaces
{
    public interface IManagerTeclas
    {
        int CantidadTeclas{ get; }

        Tecla ObtenerTecla(int index);

        IEnumerable<Tecla> ObtenerTeclas();
    }
}
