using System.Collections.Generic;

namespace AlgoHero.Interface
{
    public interface IControladorTeclas
    {
        int CantidadTeclas{ get; }

        IEnumerable<ITecla> ObtenerTeclas();

        ITecla ObtenerTecla(int codigoEntidadEntrada);
    }
}