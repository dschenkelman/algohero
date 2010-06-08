using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;

namespace AlgoHero.Interface
{
    public interface ITecla<T>
    {
        void AgregarTonoAsociado(T tono);
        T ObtenerTono(int index);
        ReadOnlyCollection<T> ObtenerTonosAsociados();
    }
}
