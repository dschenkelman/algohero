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

namespace AlgoHero.Pantallas.PlayerCancion
{
    public class PlayerCancionViewModel : IPlayerCancionViewModel
    {
        private readonly IVistaPlayerCancion vistaPlayerCancion;
        private readonly IManejadorVentanaPrincipal manejadorVentanaPrincipal;
        private readonly IProveedorCancion proveedorCancion;
        private readonly ICalculadorDuracionNotas calculadorDuracionNotas;
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
            this.controladorTeclas = new ControladorTeclas(mapeoTecladoEntidadesEntrada);
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

        public void EmpezarCancion(object sender, EmpezarCancionLlamadoEventArgs args)
        {
            GuardarDatosRecibidos(args);

            MostrarVentanaPlayer();

            CalcularIntervaloActualizacion();

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
        }

       
        #region MetodosPrivados
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
        #endregion


    }
}
