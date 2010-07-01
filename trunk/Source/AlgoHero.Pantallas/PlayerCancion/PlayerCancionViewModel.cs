using System;
using System.Collections.Generic;
using System.Linq;
using AlgoHero.Files.Interfaces;
using AlgoHero.Interface;
using AlgoHero.Interface.Enums;
using AlgoHero.Juego.Core;
using AlgoHero.Juego.Intefaces;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Pantallas.Eventos;
using AlgoHero.Pantallas.Interfaces;
using AlgoHero.MusicEntities.Servicios.Interfaces;
using AlgoHero.Juego.Entrada;
using System.Timers;
using System.Windows.Input;
using System.ComponentModel;
using AlgoHero.Juego.Puntos;
using AlgoHero.Pantallas.Puntos;
using System.Windows;
using System.Windows.Threading;
using System.IO;

namespace AlgoHero.Pantallas.PlayerCancion
{
    public class PlayerCancionViewModel : IPlayerCancionViewModel, INotifyPropertyChanged
    {
        private bool estaActiva;
        private readonly IVistaPlayerCancion vistaPlayerCancion;
        private readonly IManejadorVentanaPrincipal manejadorVentanaPrincipal;
        private readonly IProveedorCancion proveedorCancion;
        private readonly ICalculadorDuracionNotas calculadorDuracionNotas;
        private readonly IMapeoTecladoEntidadesEntrada mapeoTecladoEntidadesEntrada;
        private readonly IReproductorMusica reproductorMusica;
        private decimal intervaloActualizacion;
        private decimal segundosProximaNota;
        private Timer timer;
        private readonly IControladorTeclas controladorTeclas;

        /* Constructor. */
        public PlayerCancionViewModel(IVistaPlayerCancion vistaPlayerCancion,
            IManejadorVentanaPrincipal manejadorVentanaPrincipal,
            IProveedorCancion proveedorCancion,
            ICalculadorDuracionNotas calculadorDuracionNotas,
            IMapeoTecladoEntidadesEntrada mapeoTecladoEntidadesEntrada,
            IReproductorMusica reproductorMusica)
        {
            this.vistaPlayerCancion = vistaPlayerCancion;
            this.manejadorVentanaPrincipal = manejadorVentanaPrincipal;
            this.proveedorCancion = proveedorCancion;
            this.calculadorDuracionNotas = calculadorDuracionNotas;
            this.mapeoTecladoEntidadesEntrada = mapeoTecladoEntidadesEntrada;
            this.reproductorMusica = reproductorMusica;
            this.controladorTeclas = new ControladorTeclas(mapeoTecladoEntidadesEntrada);
            this.vistaPlayerCancion.AsignarDataContext(this);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        
        public void TeclaApretada(object sender, KeyEventArgs eventArgs)
        {
            if(estaActiva)
            {
                TeclaApretadaConVentanaActiva(eventArgs.Key);   
            }
        }

        public void TeclaApretadaConVentanaActiva(Key tecla)
        {
            EntidadEntrada entidad = this.mapeoTecladoEntidadesEntrada.ObtenerEntidadEntrada(tecla);

            //si la tecla esta mapeada a una entidad de entrada
            if (entidad != null)
            {
                bool presionadoCorrecto = this.vistaPlayerCancion.TieneNotaAPresionar(entidad);
                if (presionadoCorrecto)
                {
                    this.PuntuacionCancion.AcertarNota();
                }
                else
                {
                    this.PuntuacionCancion.ErrarNota();
                }

                PublicarCambioPuntuacionCancion();
            }
        }

        /* En caso de modificarse la puntuacion, este metodo actualiza la vista.*/
        private void PublicarCambioPuntuacionCancion()
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("PuntuacionCancion"));
            }
        }

        public Puntuacion PuntuacionCancion
        {
            get;
            set;
        }

        public Cancion CancionActual
        {
            get;
            private set;
        }

        public Nivel NivelActual
        {
            get;
            private set;
        }

        public event EventHandler<EventArgs> CancionTerminada;

        /* Este metodo da comienzo a la cancion. */
        public void EmpezarCancion(object sender, EmpezarCancionLlamadoEventArgs args)
        {
            ActivarVista(true);
            
            GuardarDatosRecibidos(args);

            MostrarVentanaPlayer();

            CalcularIntervaloActualizacion();

            IniciarPuntuacion();

            EmpezarCiclo();

            ReproducirCancion();
        }

        
        /* Este metodo actualiza el estado de la vista. */
        public void ActualizarEstado(object sender, ElapsedEventArgs e)
        {
            this.vistaPlayerCancion.Actualizar();

            this.segundosProximaNota -= this.intervaloActualizacion;

            if (!this.NivelActual.EsFinalCancion && this.segundosProximaNota <= this.intervaloActualizacion)
            {
                Nota nota = this.NivelActual.ObtenerSiguienteNota();
                this.segundosProximaNota = (decimal) nota.CalcularTiempoProximaNota(this.CancionActual.Partitura.TiempoCancion);

                IEnumerable<ITecla> teclasRelacionadas = ObtenerTeclasRelacionadas(nota);

                this.vistaPlayerCancion.AgregarNotaVisual(nota, teclasRelacionadas);
            }

            if (this.NivelActual.EsFinalCancion && !this.vistaPlayerCancion.TieneNotasAMostrar())
            {
                TerminarCancion();
            }
        }

        #region MetodosPrivados

        /* Este metodo termina la cancion, reiniciando el timer, mostrando los puntos y cambiando la vista. */
        private void TerminarCancion()
        {
            ReiniciarTimer();
            this.ActivarVista(false);
            MostrarDialogoPuntuacion();
            DetenerCancion();
            PublicarCancionTerminada();
        }

        private void PublicarCancionTerminada()
        {
            if (this.CancionTerminada != null)
            {
                this.CancionTerminada(this, new EventArgs());
            }
        }

        /* Este metodo reinicia del timer. */
        private void ReiniciarTimer()
        {
            this.timer.Stop();
            this.timer = null;
        }

        /* Este metodo muestra el dialogo dep untuacion al finalizar el juego */
        private void MostrarDialogoPuntuacion()
        {
            if (Application.Current != null)
            {
                Application.Current.Dispatcher.BeginInvoke(
                    new InvocadorMostrarPuntuacionInterno(this.MostrarPuntuacionInterno),
                    DispatcherPriority.Send);
            }
        }

        public delegate void InvocadorMostrarPuntuacionInterno();

        /* Este metodo muestra el dialogo de puntuacion. */
        private void MostrarPuntuacionInterno()
        {

            DialogoPuntuacion dialogoPuntuacion = new DialogoPuntuacion(this.PuntuacionCancion);
            dialogoPuntuacion.ShowDialog();
        }

        /* Devuelve las teclas relacionadas a una nota. */
        private IEnumerable<ITecla> ObtenerTeclasRelacionadas(Nota nota)
        {
            IEnumerable<ITecla> teclas = this.controladorTeclas.ObtenerTeclas();

            List<ITecla> teclasAUsar = new List<ITecla>();

            foreach (Tono tono in nota.Tonos)
            {
                Tono tonoLocal = tono;
                teclasAUsar.AddRange(teclas.Where(t => t.ObtenerTonosAsociados().Contains(tonoLocal)));
            }

            return teclasAUsar;
        }
        
        /* Este metodo comienza el ciclo del juego. */
        private void EmpezarCiclo()
        {
            this.timer = new Timer((double) (this.intervaloActualizacion * 1000));
            this.timer.Elapsed += ActualizarEstado;
            this.timer.Start();
        }

        /* Este metodo calcula el intervalo de actualizacion en segundos */
        private void CalcularIntervaloActualizacion()
        {
            double tiempoSemifusa = this.calculadorDuracionNotas.CalcularDuracion
                (this.CancionActual.Partitura.TiempoCancion,FiguraMusical.Semifusa);

            //en segundos
            this.intervaloActualizacion = (decimal) (tiempoSemifusa/3.0);
            this.segundosProximaNota = this.intervaloActualizacion;
        }

        /* Este metodo cambia el contenido de la ventana principal. */
        private void MostrarVentanaPlayer()
        {
            this.manejadorVentanaPrincipal.CambiarContenido(this.vistaPlayerCancion);
        }

        private void GuardarDatosRecibidos(EmpezarCancionLlamadoEventArgs args)
        {
            this.CancionActual = proveedorCancion.ObtenerCancionConPartitura(args.Cancion.PathPartitura);
            this.NivelActual = args.Nivel;
            this.NivelActual.AsignarCancion(this.CancionActual);
            this.NivelActual.AsignarTeclas(this.controladorTeclas);
        }

        /* Este metodo activa la vista. */
        private void ActivarVista(bool estado)
        {
            this.estaActiva = estado;
        }

        /* Este metodo crea un nuevo objeto Puntuacion y lo publica en la vista. */
        private void IniciarPuntuacion()
        {
            this.PuntuacionCancion = new Puntuacion();
            this.PublicarCambioPuntuacionCancion();
        }

        /*Comienza a reproducir la musica de la cancion seleccionada*/
        private void ReproducirCancion()
        {
            string pathCompleto = Path.Combine(Environment.CurrentDirectory, this.CancionActual.PathArchivoMusica);
            this.reproductorMusica.ReproducirCancion(pathCompleto);
        }

        private void DetenerCancion()
        {
            this.reproductorMusica.DetenerReproduccion();
        }

        #endregion
    }

}
