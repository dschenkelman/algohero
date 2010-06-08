using System.Collections.Generic;

namespace AlgoHero.Interface
{
    public interface IControladorTeclas
    {
        int CantidadTeclas{ get; }

        ITecla ObtenerTecla(int index);

        IEnumerable<ITecla> ObtenerTeclas();

        ITecla ObtenerTecla(EntidadEntrada entidadEntrada);
    }
}