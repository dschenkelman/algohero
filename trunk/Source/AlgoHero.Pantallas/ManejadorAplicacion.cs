using AlgoHero.Juego.Core;
using AlgoHero.Juego.Intefaces;
using AlgoHero.Pantallas.MenuPrincipal;
using AlgoHero.Files;
using AlgoHero.Pantallas.Interfaces;
using AlgoHero.Pantallas.PlayerCancion;
using AlgoHero.MusicEntities.Servicios;

namespace AlgoHero.Pantallas
{
    public class ManejadorAplicacion
    {
        public void Iniciar()
        {
            ProveedorCancionXml proveedorCancionesDirectorio = new ProveedorCancionXml();
            
            IProveedorNiveles proveedorNiveles = new ProveedorNiveles();

            VentanaPrincipal ventanaPrincipal = new VentanaPrincipal();

            IManejadorVentanaPrincipal manejadorVentanaPrincipal = new ManejadorVentanaPrincipal(ventanaPrincipal);

            IMenuPrincipalViewModel menuPrincipalViewModel = 
                new MenuPrincipalViewModel(proveedorCancionesDirectorio, proveedorNiveles, manejadorVentanaPrincipal);
            VistaMenuPrincipal menuPrincipal = new VistaMenuPrincipal(menuPrincipalViewModel);

            IVistaPlayerCancion vistaPlayerCancion = new VistaPlayerCancion();
            IPlayerCancionViewModel playerCancionViewModel = 
                new PlayerCancionViewModel(vistaPlayerCancion, manejadorVentanaPrincipal, proveedorCancionesDirectorio, new CalculadorDuracionNotas());

            menuPrincipalViewModel.EmpezarCancionLlamado += playerCancionViewModel.EmpezarCancion;

            manejadorVentanaPrincipal.CambiarContenido(menuPrincipal);

            ventanaPrincipal.Closing += this.CerrandoVentanaPrincipal;

            ventanaPrincipal.Show();

            this.AplicacionCorriendo = true;
        }

        public void CerrandoVentanaPrincipal(object sender, System.ComponentModel.CancelEventArgs e)
        {
            this.AplicacionCorriendo = false;
        }

        public bool AplicacionCorriendo
        {
            get; private set;
        }
    }
}
