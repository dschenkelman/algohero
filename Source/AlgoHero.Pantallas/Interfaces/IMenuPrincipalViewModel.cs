using System;
using AlgoHero.Pantallas.Eventos;
using AlgoHero.Pantallas.MenuPrincipal;

namespace AlgoHero.Pantallas.Interfaces
{
    public interface IMenuPrincipalViewModel
    {
        event EventHandler<EmpezarCancionLlamadoEventArgs> EmpezarCancionLlamado;
        void CancionTermino(object sender, EventArgs e);
        void AsignarVista(VistaMenuPrincipal vista);
    }
}
