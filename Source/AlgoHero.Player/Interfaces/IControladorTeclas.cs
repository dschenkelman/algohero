using System.Collections.Generic;
using AlgoHero.Interface;

namespace AlgoHero.Player.Interfaces
{
    public interface IControladorTeclas
    {
        int CantidadTeclas{ get; }

        Tecla ObtenerTecla(int index);

        IEnumerable<Tecla> ObtenerTeclas();

        Tecla ObtenerTecla(EntidadEntrada entidadEntrada);
    }
}
