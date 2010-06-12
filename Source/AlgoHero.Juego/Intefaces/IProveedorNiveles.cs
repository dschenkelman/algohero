using System;
using System.Collections.ObjectModel;
using AlgoHero.Juego.Core;

namespace AlgoHero.Juego.Intefaces
{
    public interface IProveedorNiveles
    {
         ObservableCollection<Nivel> ObtenerNiveles();
    }
}
