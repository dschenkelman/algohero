using System.Timers;
using AlgoHero.Juego.Core;
using AlgoHero.Pantallas.Eventos;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Pantallas.Interfaces
{
    public interface IPlayerCancionViewModel
    {
        void EmpezarCancion(object sender, EmpezarCancionLlamadoEventArgs args);
        void ActualizarEstado(object sender, ElapsedEventArgs e);
        Cancion CancionActual { get; }
        Nivel NivelActual { get; }
    }
}
