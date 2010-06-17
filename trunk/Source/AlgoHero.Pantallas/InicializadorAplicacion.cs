using System.Collections.ObjectModel;
using AlgoHero.Juego.Core;
using AlgoHero.Juego.Intefaces;
using AlgoHero.Pantallas.MenuPrincipal;
using AlgoHero.Files;
using AlgoHero.Pantallas.Interfaces;
using AlgoHero.Pantallas.PlayerCancion;

namespace AlgoHero.Pantallas
{
    public class InicializadorAplicacion
    {
        public void Iniciar()
        {
            ProveedorCancionXml proveedorCancionesDirectorio = new ProveedorCancionXml();
            //TODO: Cambiar para usar el proveedor de verdad
            IProveedorNiveles proveedorNiveles = new MockProveedorNiveles();

            VentanaPrincipal ventanaPrincipal = new VentanaPrincipal();

            IManejadorVentanaPrincipal manejadorVentanaPrincipal = new ManejadorVentanaPrincipal(ventanaPrincipal);

            IMenuPrincipalViewModel menuPrincipalViewModel = 
                new MenuPrincipalViewModel(proveedorCancionesDirectorio, proveedorNiveles, manejadorVentanaPrincipal);
            VistaMenuPrincipal menuPrincipal = new VistaMenuPrincipal(menuPrincipalViewModel);

            IVistaPlayerCancion vistaPlayerCancion = new VistaPlayerCancion();
            IPlayerCancionViewModel playerCancionViewModel = 
                new PlayerCancionViewModel(vistaPlayerCancion, manejadorVentanaPrincipal, proveedorCancionesDirectorio);

            menuPrincipalViewModel.EmpezarCancionLlamado += playerCancionViewModel.EmpezarCancion;

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

            #region Miembros de IProveedorNiveles

            ObservableCollection<Nivel> IProveedorNiveles.ObtenerNiveles()
            {
                throw new System.NotImplementedException();
            }

            #endregion
        }
    }
}
