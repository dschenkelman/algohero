using AlgoHero.Player;
using System.Windows.Input;
using AlgoHero.Interface;

namespace AlgoHero.Player.Interfaces
{
    interface IMapeoTecladoEntidadesEntrada
    {
        EntidadEntrada ObtenerEntidadEntrada(Key key);
    }
}
