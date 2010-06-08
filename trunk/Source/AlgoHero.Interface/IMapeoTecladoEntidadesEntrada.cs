using System.Windows.Input;
using AlgoHero.Interface;

namespace AlgoHero.Player.Interfaces
{
    public interface IMapeoTecladoEntidadesEntrada
    {
        EntidadEntrada ObtenerEntidadEntrada(Key key);
    }
}
