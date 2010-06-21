using AlgoHero.Files.Interfaces;
using AlgoHero.Interface.Enums;
using AlgoHero.Juego.Core;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Pantallas.Eventos;
using AlgoHero.Pantallas.Interfaces;
using AlgoHero.MusicEntities.Servicios.Interfaces;
using System.Timers;
using AlgoHero.Pantallas.PlayerCancion.Utilitarios;

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
        private TimerDePrecision timer;

        public PlayerCancionViewModel(IVistaPlayerCancion vistaPlayerCancion, 
            IManejadorVentanaPrincipal manejadorVentanaPrincipal,
            IProveedorCancion proveedorCancion, ICalculadorDuracionNotas calculadorDuracionNotas)
        {
            this.vistaPlayerCancion = vistaPlayerCancion;
            this.manejadorVentanaPrincipal = manejadorVentanaPrincipal;
            this.proveedorCancion = proveedorCancion;
            this.calculadorDuracionNotas = calculadorDuracionNotas;
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

        public void ActualizarEstado(object sender, IntervaloConsumidoEventArgs e)
        {
            this.segundosProximaNota -= this.intervaloActualizacion;

            if (!this.NivelActual.EsFinalCancion && this.segundosProximaNota == 0m)
            {
                Nota nota = this.NivelActual.ObtenerSiguienteNota();
                this.segundosProximaNota = (decimal) nota.CalcularTiempoProximaNota(this.CancionActual.Partitura.TiempoCancion);
            }
        }

        #region MetodosPrivados
        private void EmpezarCiclo()
        {
            this.timer = new TimerDePrecision((int) (this.intervaloActualizacion * 1000));
            this.timer.IntervaloConsumido += ActualizarEstado;
            this.timer.Comenzar();
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
        }
        #endregion


    }
}
