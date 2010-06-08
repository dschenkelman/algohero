using System.Collections.Generic;
using AlgoHero.Interface;

namespace AlgoHero.Player.Interfaces
{
    public interface IControladorTeclas
    {
        int CantidadTeclas{ get; }

        ITecla ObtenerTecla(int index);

        IEnumerable<ITecla> ObtenerTeclas();

        ITecla ObtenerTecla(EntidadEntrada entidadEntrada);
    }
}
