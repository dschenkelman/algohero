using System.Collections.Generic;

namespace AlgoHero.Player.Interfaces
{
    public interface IControladorTeclas
    {
        int CantidadTeclas{ get; }

        Tecla ObtenerTecla(int index);

        IEnumerable<Tecla> ObtenerTeclas();
    }
}
