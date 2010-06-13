using System;
using AlgoHero.Pantallas.Eventos;

namespace AlgoHero.Pantallas.Interfaces
{
    public interface IMenuPrincipalViewModel
    {
        event EventHandler<EmpezarCancionLlamadoEventArgs> EmpezarCancionLlamado;
    }
}
