using System.Collections.Generic;

namespace AlgoHero.Interface
{
    public interface IManagerTeclas<T>
    {
        int CantidadTeclas{ get; }

        T ObtenerTecla(int index);

        IEnumerable<T> ObtenerTeclas();
    }
}
