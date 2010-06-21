using System.Timers;
using AlgoHero.Juego.Core;
using AlgoHero.Pantallas.Eventos;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Pantallas.PlayerCancion.Utilitarios;

namespace AlgoHero.Pantallas.Interfaces
{
    public interface IPlayerCancionViewModel
    {
        void EmpezarCancion(object sender, EmpezarCancionLlamadoEventArgs args);
        void ActualizarEstado(object sender, IntervaloConsumidoEventArgs e);
        Cancion CancionActual { get; }
        Nivel NivelActual { get; }
    }
}
