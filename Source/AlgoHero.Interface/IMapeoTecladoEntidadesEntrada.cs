using System.Collections.ObjectModel;
using System.Windows.Input;

namespace AlgoHero.Interface
{
    public interface IMapeoTecladoEntidadesEntrada
    {
        EntidadEntrada ObtenerEntidadEntrada(Key key);
        ReadOnlyCollection<EntidadEntrada> ObtenerEntidadesEntrada();
    }
}