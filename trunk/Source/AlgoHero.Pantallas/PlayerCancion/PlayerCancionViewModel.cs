using AlgoHero.Files.Interfaces;
using AlgoHero.Juego.Core;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Pantallas.Eventos;
using AlgoHero.Pantallas.Interfaces;

namespace AlgoHero.Pantallas.PlayerCancion
{
    public class PlayerCancionViewModel : IPlayerCancionViewModel
    {
        private readonly IVistaPlayerCancion vistaPlayerCancion;
        private readonly IManejadorVentanaPrincipal manejadorVentanaPrincipal;
        private readonly IProveedorCancion proveedorCancion;

        public PlayerCancionViewModel(IVistaPlayerCancion vistaPlayerCancion, IManejadorVentanaPrincipal manejadorVentanaPrincipal, IProveedorCancion proveedorCancion)
        {
            this.vistaPlayerCancion = vistaPlayerCancion;
            this.manejadorVentanaPrincipal = manejadorVentanaPrincipal;
            this.proveedorCancion = proveedorCancion;
        }

        public void EmpezarCancion(object sender, EmpezarCancionLlamadoEventArgs args)
        {
            this.CancionActual = proveedorCancion.ObtenerCancionConPartitura(args.Cancion.PathPartitura);
            this.NivelActual = args.Nivel;

            this.manejadorVentanaPrincipal.CambiarContenido(this.vistaPlayerCancion);
        }

        public Cancion CancionActual
        {
            get; private set;
        }

        public Nivel NivelActual
        {
            get; private set;
        }
    }
}
