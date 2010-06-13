using System.Collections.ObjectModel;
using AlgoHero.Juego.Core;
using AlgoHero.Juego.Intefaces;
using AlgoHero.Pantallas.MenuPrincipal;
using AlgoHero.Files.Interfaces;
using AlgoHero.Files;
using AlgoHero.Pantallas.Interfaces;
using AlgoHero.Pantallas.PlayerCancion;

namespace AlgoHero.Pantallas
{
    public class InicializadorAplicacion
    {
        public void Iniciar()
        {
            IProveedorCancionesDirectorio proveedorCancionesDirectorio = new ProveedorCancionXml();
            //Cambiar para usar el proveedor de verdad
            IProveedorNiveles proveedorNiveles = new MockProveedorNiveles();

            VentanaPrincipal ventanaPrincipal = new VentanaPrincipal();

            IManejadorVentanaPrincipal manejadorVentanaPrincipal = new ManejadorVentanaPrincipal(ventanaPrincipal);

            IMenuPrincipalViewModel menuPrincipalViewModel = 
                new MenuPrincipalViewModel(proveedorCancionesDirectorio, proveedorNiveles, manejadorVentanaPrincipal);
            VistaMenuPrincipal menuPrincipal = new VistaMenuPrincipal(menuPrincipalViewModel);

            //IPlayerCancionViewModel playerCancionViewModel = new PlayerCancionViewModel();

            manejadorVentanaPrincipal.CambiarContenido(menuPrincipal);
            ventanaPrincipal.Show();
        }

        /*Crear un nuevo proveedor niveles y borrar esta clase.*/
        private class MockProveedorNiveles : IProveedorNiveles
        {
            private ObservableCollection<Nivel> niveles;

            public MockProveedorNiveles()
            {
                Nivel facil = new Nivel("Facil", null);
                Nivel medio = new Nivel("Medio", null);
                Nivel dificil = new Nivel("Dificil", null);
                niveles = new ObservableCollection<Nivel>();

                this.niveles.Add(facil);
                this.niveles.Add(medio);
                this.niveles.Add(dificil);
            }

            public ObservableCollection<Nivel> ObtenerNiveles()
            {
                return this.niveles;
            }
        }
    }
}
