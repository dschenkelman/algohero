using System.Timers;
using System.Windows.Input;
using AlgoHero.Juego.Core;
using AlgoHero.Pantallas.Eventos;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Pantallas.Interfaces
{
    public interface IPlayerCancionViewModel
    {
        void EmpezarCancion(object sender, EmpezarCancionLlamadoEventArgs args);
        void ActualizarEstado(object sender, ElapsedEventArgs e);
        void TeclaApretada(object sender, KeyEventArgs teclaApretada);
        Cancion CancionActual { get; }
        Nivel NivelActual { get; }
    }
}
