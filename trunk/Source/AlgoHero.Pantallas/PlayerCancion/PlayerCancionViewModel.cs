using System;
using System.Collections.Generic;
using System.Linq;
using AlgoHero.Files.Interfaces;
using AlgoHero.Interface;
using AlgoHero.Interface.Enums;
using AlgoHero.Juego.Core;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Pantallas.Eventos;
using AlgoHero.Pantallas.Interfaces;
using AlgoHero.MusicEntities.Servicios.Interfaces;
using AlgoHero.Juego.Entrada;
using System.Timers;
using System.Windows.Input;
using AlgoHero.PuntuacionJuego;
using System.ComponentModel;

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
        private decimal intervaloActualizacion;
        private decimal segundosProximaNota;
        private Timer timer;
        private readonly IControladorTeclas controladorTeclas;

        public PlayerCancionViewModel(IVistaPlayerCancion vistaPlayerCancion, 
            IManejadorVentanaPrincipal manejadorVentanaPrincipal,
            IProveedorCancion proveedorCancion, ICalculadorDuracionNotas calculadorDuracionNotas,
            IMapeoTecladoEntidadesEntrada mapeoTecladoEntidadesEntrada)
        {
            this.vistaPlayerCancion = vistaPlayerCancion;
            this.manejadorVentanaPrincipal = manejadorVentanaPrincipal;
            this.proveedorCancion = proveedorCancion;
            this.calculadorDuracionNotas = calculadorDuracionNotas;
            this.mapeoTecladoEntidadesEntrada = mapeoTecladoEntidadesEntrada;
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

                if (this.PropertyChanged != null)
                {
                    this.PropertyChanged(this, new PropertyChangedEventArgs("PuntuacionCancion"));
                }
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

        public void EmpezarCancion(object sender, EmpezarCancionLlamadoEventArgs args)
        {
            ActivarVista(true);
            
            GuardarDatosRecibidos(args);

            MostrarVentanaPlayer();

            CalcularIntervaloActualizacion();

            IniciarPuntuacion();

            EmpezarCiclo();
        }

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

        private void TerminarCancion()
        {
            this.timer.Stop();
            this.timer = null;
            this.ActivarVista(false);
            if (this.CancionTerminada != null)
            {
                this.CancionTerminada(this, new EventArgs());
            }
        }
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
        
        private void EmpezarCiclo()
        {
            this.timer = new Timer((double) (this.intervaloActualizacion * 1000));
            this.timer.Elapsed += ActualizarEstado;
            this.timer.Start();
        }

        private void CalcularIntervaloActualizacion()
        {
            double tiempoSemicorchea = this.calculadorDuracionNotas.CalcularDuracion
                (this.CancionActual.Partitura.TiempoCancion,FiguraMusical.Semicorchea);

            //en segundos
            this.intervaloActualizacion = (decimal) (tiempoSemicorchea/10);
            this.segundosProximaNota = this.intervaloActualizacion;
        }

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

        private void ActivarVista(bool estado)
        {
            this.estaActiva = estado;
        }

        private void IniciarPuntuacion()
        {
            this.PuntuacionCancion = new Puntuacion(this.NivelActual);
        }


        #endregion

    }
}
