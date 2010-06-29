using System.Collections.Generic;
using System.Collections.ObjectModel;
using AlgoHero.Files.Interfaces;
using AlgoHero.Juego.Core;
using AlgoHero.Juego.Intefaces;
using AlgoHero.MusicEntities.Core;
using System.IO;
using System;
using AlgoHero.Pantallas.Eventos;
using AlgoHero.Pantallas.Interfaces;
using Microsoft.Practices.Composite.Presentation.Commands;
using System.Windows;
using System.Windows.Threading;

namespace AlgoHero.Pantallas.MenuPrincipal
{
    public class MenuPrincipalViewModel : IMenuPrincipalViewModel
    {
        private readonly IProveedorCancionesDirectorio proveedorCanciones;
        private readonly IProveedorNiveles proveedorNiveles;
        private readonly IManejadorVentanaPrincipal manejadorVentanaPrincipal;

        /* Constructor. Recibe un proveedor de canciones, otro de niveles, y un manejador de ventana principal. Agrega las
         * canciones y los niveles. */
        public MenuPrincipalViewModel(IProveedorCancionesDirectorio proveedorCanciones, IProveedorNiveles proveedorNiveles,
            IManejadorVentanaPrincipal manejadorVentanaPrincipal)
        {
            this.proveedorCanciones = proveedorCanciones;
            this.proveedorNiveles = proveedorNiveles;
            this.manejadorVentanaPrincipal = manejadorVentanaPrincipal;

            this.Canciones = new ObservableCollection<Cancion>();
            this.Niveles = new ObservableCollection<Nivel>();
            this.ComandoEmpezarCancion = new DelegateCommand<object>(this.EmpezarCancion, this.PuedeEmpezarCancion);

            AgregarCancionesDeProveedor();
            AgregarNiveles();
        }

        private Cancion cancionActual;

        public Cancion CancionActual
        {
            get { return cancionActual; }
            set
            {
                if (value != cancionActual)
                {
                    cancionActual = value;
                    this.ComandoEmpezarCancion.RaiseCanExecuteChanged();
                }
            }
        }

        private Nivel nivelActual;
        private VistaMenuPrincipal vistaMenuPrincipal;

        public Nivel NivelActual
        {
            get { return nivelActual; }
            set
            {
                if (value != nivelActual)
                {
                    nivelActual = value;
                    this.ComandoEmpezarCancion.RaiseCanExecuteChanged();
                }
                
            }
        }

        public DelegateCommand<object> ComandoEmpezarCancion
        {
            get; private set;
        }

        public ObservableCollection<Cancion> Canciones
        {
            get;
            private set;
        }

        public ObservableCollection<Nivel> Niveles
        {
            get;
            private set;
        }

        /* Agrega los niveles del proveedor a la propiedad niveles. */
        private void AgregarNiveles()
        {
            this.Niveles = this.proveedorNiveles.ObtenerNiveles(); 
        }

        /* Agrega las canciones del proveedor a la lista de canciones. */
        private void AgregarCancionesDeProveedor()
        {
            //TODO: Cambiar esto para poder configurar de donde vienen las canciones
            string pathCanciones = Path.Combine(Environment.CurrentDirectory, "Canciones");
            IEnumerable<Cancion> canciones = this.proveedorCanciones.ObtenerCancionesDirectorio(pathCanciones);
            foreach (var cancion in canciones)
            {
                this.Canciones.Add(cancion);
            }
        }

        /* Devuelve true en caso de que exista un nivel actual y una cancion, es decir, pueda comenzar la cancion */
        private bool PuedeEmpezarCancion(object obj)
        {
            return (this.NivelActual != null && this.CancionActual != null);
        }

        public void EmpezarCancion(object obj)
        {
            var handler = this.EmpezarCancionLlamado;
            if (handler != null)
            {
                handler(this, new EmpezarCancionLlamadoEventArgs(this.CancionActual, this.NivelActual));
            }
        }

        public event EventHandler<EmpezarCancionLlamadoEventArgs> EmpezarCancionLlamado;
        
        public void CancionTermino(object sender, EventArgs e)
        {
            Application.Current.Dispatcher.BeginInvoke(
                new InvocarActivarVista(this.ActivarVista), 
                DispatcherPriority.SystemIdle);
        }

        public void AsignarVista(VistaMenuPrincipal vista)
        {
            this.vistaMenuPrincipal = vista;
        }

        public delegate void InvocarActivarVista();

        private void ActivarVista()
        {
            this.manejadorVentanaPrincipal.CambiarContenido(this.vistaMenuPrincipal);
        }
    }
}
