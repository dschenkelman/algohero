using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Windows.Input;
using AlgoHero.Interface;
using AlgoHero.Interface.Enums;
using AlgoHero.Juego.Core;
using AlgoHero.MusicEntities.Core;
using NUnit.Framework;
using AlgoHero.Pantallas.PlayerCancion;
using AlgoHero.Pantallas.Interfaces;
using AlgoHero.Files.Interfaces;
using AlgoHero.Pantallas.Eventos;
using AlgoHero.MusicEntities.Servicios.Interfaces;
using AlgoHero.Juego.Intefaces;
using System.Threading;

namespace AlgoHero.Pantallas.Tests
{
    [TestFixture]
    public class PlayerCancionViewModelTests
    {
        [Test]
        public void ConstruirViewModelSeteaDataContextDeView()
        {
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockVistaPlayerCancion vista = new MockVistaPlayerCancion();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(vista,
                new MockManejadorVentanaPrincipal(),
                proveedorCancion,
                new MockCalculadorDuracionNotas(),
                new MockMapeoTecladoEntidadesEntrada(), new MockReproductorMusica());

            Assert.IsTrue(vista.DataContextFueSeteado);
            Assert.AreEqual(vm, vista.DataContextSeteado);
        }
        
        [Test]
        public void HandlearElEventoEmpezarCancionObtieneCancionConPartitura()
        {
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(new MockVistaPlayerCancion(),
                new MockManejadorVentanaPrincipal(),
                proveedorCancion,
                new MockCalculadorDuracionNotas(),
                new MockMapeoTecladoEntidadesEntrada(), new MockReproductorMusica());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (new Cancion("Mi Cancion", "Mi Grupo") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }
                , new Nivel("Nivel Test", new MockEstrategiaNivelCancionFinita())));

            Assert.AreEqual("Cancion Recuperada", vm.CancionActual.Nombre);
        }

        [Test]
        public void HandlearElEventoEmpezarCancionGuardaNivel()
        {
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(new MockVistaPlayerCancion(),
                new MockManejadorVentanaPrincipal(),
                proveedorCancion,
                new MockCalculadorDuracionNotas(),
                new MockMapeoTecladoEntidadesEntrada(), new MockReproductorMusica());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (new Cancion("Mi Cancion", "Mi Grupo") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }
                , new Nivel("Nivel Test", new MockEstrategiaNivelCancionFinita())));

            Assert.AreEqual("Nivel Test", vm.NivelActual.Descripcion);
        }

        [Test]
        public void HandlearElEventoEmpezarCancionLlamaAMetodosNivel()
        {
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(new MockVistaPlayerCancion(),
                new MockManejadorVentanaPrincipal(),
                proveedorCancion,
                new MockCalculadorDuracionNotas(),
                new MockMapeoTecladoEntidadesEntrada(), new MockReproductorMusica());

            MockEstrategiaNivelCancionFinita estrategiaNivelCancionFinita =  new MockEstrategiaNivelCancionFinita();
            
            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (new Cancion("Mi Cancion", "Mi Grupo") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }
                , new Nivel("Nivel Test", estrategiaNivelCancionFinita)));

            Assert.IsTrue(estrategiaNivelCancionFinita.AsignarCancionLlamado);
            Assert.IsTrue(estrategiaNivelCancionFinita.AsignarTonosLlamado);
        }

        [Test]
        public void AlEmpezarCancionCambiaContenidoDeVentanaPrincipal()
        {
            IVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion,
                manejadorVentanaPrincipal,
                proveedorCancion,
                calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada(), new MockReproductorMusica());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (new Cancion("Mi Cancion", "Mi Grupo") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }
                , new Nivel("Nivel Test", new MockEstrategiaNivelCancionFinita())));

            Assert.AreEqual(vistaPlayerCancion, manejadorVentanaPrincipal.Contenido);
        }

        [Test]
        public void AlEmpezarCancionCalculaDuracionNotas()
        {
            IVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada(), new MockReproductorMusica());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (new Cancion("Mi Cancion", "Mi Grupo") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }
                , new Nivel("Nivel Test", new MockEstrategiaNivelCancionFinita())));

            Assert.IsTrue(calculadorDuracionNotas.CalcularDuracionFueLlamado);
            Assert.AreEqual(FiguraMusical.Semicorchea, calculadorDuracionNotas.FiguraLlamado);
        }

        [Test]
        public void AlEmpezarComienzaAReproducirMusica()
        {
            IVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();

            var mockReproductorMusica = new MockReproductorMusica();

            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada(), mockReproductorMusica);

            Cancion cancion = new Cancion("Mi Cancion", "Mi Grupo") {PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav"};
            
            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs
                (cancion, new Nivel("Nivel Test", new MockEstrategiaNivelCancionFinita())));

            string pathCompleto = Path.Combine(Environment.CurrentDirectory, cancion.PathArchivoMusica);
            
            Assert.IsTrue(mockReproductorMusica.ReproducirFueLlamado);
            Assert.AreEqual(pathCompleto, mockReproductorMusica.PathLlamado);
        }

        [Test]
        public void ActualizarEstadoPideSiguienteNotaSoloSiNoEstaAlFinalDeLaCancion()
        {
            MockEstrategiaNivelCancionFinita mockEstrategiaNivelCancionFinita = new MockEstrategiaNivelCancionFinita();
            Nivel nivel = new Nivel("Mock", mockEstrategiaNivelCancionFinita);
            
            IVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada(), new MockReproductorMusica());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(
                new Cancion("Jijiji", "Los redondos") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }
                , nivel));

            //loopear mucho para que se acaben las notas
            for (int i = 0; i < 1000; i++)
            {
                vm.ActualizarEstado(this, null);
            }
            
            Assert.AreEqual(10, mockEstrategiaNivelCancionFinita.LlamadosObtenerNotas);
            Assert.IsTrue(mockEstrategiaNivelCancionFinita.EsFinalCancion());
        }

        [Test]
        public void ActualizarEstadoAgregaNotaANotasVisualesDeVista()
        {
            MockEstrategiaNivelCancionFinita mockEstrategiaNivelCancionFinita = new MockEstrategiaNivelCancionFinita();
            Nivel nivel = new Nivel("Mock", mockEstrategiaNivelCancionFinita);

            MockVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada(), new MockReproductorMusica());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(
                new Cancion("Jijiji", "Los redondos") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }
                , nivel));

            //loopear mucho para que se acaben las notas
            vm.ActualizarEstado(this, null);
            

            Assert.IsTrue(vistaPlayerCancion.AgregarNotaVisualFueLlamado);
        }

        [Test]
        public void ActualizarEstadoActualizarVista()
        {
            MockEstrategiaNivelCancionFinita mockEstrategiaNivelCancionFinita = new MockEstrategiaNivelCancionFinita();
            Nivel nivel = new Nivel("Mock", mockEstrategiaNivelCancionFinita);

            MockVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada(), new MockReproductorMusica());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(
                new Cancion("Jijiji", "Los redondos") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }
                , nivel));

            //loopear mucho para que se acaben las notas
            vm.ActualizarEstado(this, null);

            Assert.IsTrue(vistaPlayerCancion.ActualizarFueLlamado);
        }

        [Test]
        public void ActualizarEstadoPublicaEventoCancionFinalizadaSiNoHayMasNotasEnPantallaYNoHayMasNotasEnCancion()
        {
            MockEstrategiaNivelSinNotas mockEstrategiaNivelCancionFinita = new MockEstrategiaNivelSinNotas();
            Nivel nivel = new Nivel("Mock", mockEstrategiaNivelCancionFinita);

            MockVistaPlayerCancionSinNotasEnVista vistaPlayerCancion = new MockVistaPlayerCancionSinNotasEnVista();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada(), new MockReproductorMusica());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(
                new Cancion("Jijiji", "Los redondos") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }
                , nivel));

            bool eventoLlamado = false;
            
            vm.CancionTerminada += delegate
                                       {
                                           eventoLlamado = true;
                                       };

            vm.ActualizarEstado(this, null);

            Assert.IsTrue(eventoLlamado);
        }

        [Test]
        public void AlTerminarLaCancionSeDetieneLaCancionSiendoReproducida()
        {
            MockEstrategiaNivelSinNotas mockEstrategiaNivelCancionFinita = new MockEstrategiaNivelSinNotas();
            Nivel nivel = new Nivel("Mock", mockEstrategiaNivelCancionFinita);

            MockVistaPlayerCancionSinNotasEnVista vistaPlayerCancion = new MockVistaPlayerCancionSinNotasEnVista();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();

            var reproductorMusica = new MockReproductorMusica();
            
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada(), reproductorMusica);

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(
                new Cancion("Jijiji", "Los redondos") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }
                , nivel));


            vm.ActualizarEstado(this, null);

            Assert.IsTrue(reproductorMusica.DetenerFueLlamado);
        }

        [Test]
        public void TeclaApretadaObtieneEntidadEntradaDeMapeo()
        {
            MockEstrategiaNivelCancionFinita mockEstrategiaNivelCancionFinita = new MockEstrategiaNivelCancionFinita();
            Nivel nivel = new Nivel("Facil", mockEstrategiaNivelCancionFinita);

            MockVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockMapeoTecladoEntidadesEntrada mapeoTecladoEntidadesEntrada = new MockMapeoTecladoEntidadesEntrada();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            
            //la marco como activa para que la llamada al método haga algo
            PlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                mapeoTecladoEntidadesEntrada, new MockReproductorMusica());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(new Cancion("Mock", "Mock") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }, nivel));
            vm.TeclaApretadaConVentanaActiva(Key.S);

            Assert.IsTrue(mapeoTecladoEntidadesEntrada.ObtenerEntidadEntradaFueLlamado);
            Assert.AreEqual(Key.S, mapeoTecladoEntidadesEntrada.TeclaLlamado);
        }

        //TODO:Correr este test
        [Test]
        [Ignore]
        public void TeclaApretadaCambiaFondoTeclaSiEstaRegistradaConEntrada()
        {
            MockEstrategiaNivelCancionFinita mockEstrategiaNivelCancionFinita = new MockEstrategiaNivelCancionFinita();
            Nivel nivel = new Nivel("Facil", mockEstrategiaNivelCancionFinita);

            MockVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockMapeoTecladoEntidadesEntrada mapeoTecladoEntidadesEntrada = new MockMapeoTecladoEntidadesEntrada();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();

            //la marco como activa para que la llamada al método haga algo
            PlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                mapeoTecladoEntidadesEntrada, new MockReproductorMusica());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(new Cancion("Mock", "Mock") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }, nivel));
            vm.TeclaApretadaConVentanaActiva(Key.S);

            Assert.IsTrue(vistaPlayerCancion.MarcarTeclaApretadaFueLlamado);
            Assert.AreEqual(1, vistaPlayerCancion.CodigoEntidadEntradaAMarcar);
        }

        [Test]
        public void TeclaApretadaAumentaPuntuacionSiAcerto()
        {
            MockEstrategiaNivelCancionFinita mockEstrategiaNivelCancionFinita = new MockEstrategiaNivelCancionFinita();
            Nivel nivel = new Nivel("Facil", mockEstrategiaNivelCancionFinita);

            MockVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockMapeoTecladoEntidadesEntrada mapeoTecladoEntidadesEntrada = new MockMapeoTecladoEntidadesEntrada();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();

            //la marco como activa para que la llamada al método haga algo
            PlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                mapeoTecladoEntidadesEntrada, new MockReproductorMusica());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(new Cancion("Mock", "Mock") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }, nivel));
            vm.TeclaApretadaConVentanaActiva(Key.S);

            Assert.AreEqual(1, vm.PuntuacionCancion.PuntosAcumulados);
            Assert.AreEqual(1, vm.PuntuacionCancion.RachaDeNotasAcertadas);
        }

        [Test]
        public void TeclaApretadaNoAumentaPuntucacionSiNoAcerto()
        {
            MockEstrategiaNivelCancionFinita mockEstrategiaNivelCancionFinita = new MockEstrategiaNivelCancionFinita();
            Nivel nivel = new Nivel("Facil", mockEstrategiaNivelCancionFinita);

            MockVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockMapeoTecladoEntidadesEntrada mapeoTecladoEntidadesEntrada = new MockMapeoTecladoEntidadesEntrada();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();

            //la marco como activa para que la llamada al método haga algo
            PlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                mapeoTecladoEntidadesEntrada, new MockReproductorMusica());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(new Cancion("Mock", "Mock") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }, nivel));
            vm.TeclaApretadaConVentanaActiva(Key.D);

            Assert.AreEqual(0, vm.PuntuacionCancion.PuntosAcumulados);
            Assert.AreEqual(0, vm.PuntuacionCancion.RachaDeNotasAcertadas);
        }

        //Hay que correr el test separado de los demas a veces.
        [Test]
        public void ActualizarEstadoPideSiguienteNotaCuandoTiempoEntreNotasEsIgualATiempoDesdeNotaAnterior()
        {
            MockEstrategiaNivelCancionInfinita mockEstrategiaNivelCancionInfinita = new MockEstrategiaNivelCancionInfinita();
            Nivel nivel = new Nivel("Mock", mockEstrategiaNivelCancionInfinita);

            IVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada(), new MockReproductorMusica());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(
                new Cancion("Jijiji", "Los redondos") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }
                , nivel));
            
            Thread.Sleep(2100);

            Assert.GreaterOrEqual(mockEstrategiaNivelCancionInfinita.LlamadosObtenerNotas , 2);
            Assert.LessOrEqual(mockEstrategiaNivelCancionInfinita.LlamadosObtenerNotas, 4);
        }
        
        //Hay que correr el test separado de los demas a veces.
        [Test]
        public void ActualizarEstadoPideSiguienteNotaCuandoTiempoEntreNotasEsIgualATiempoDesdeNotaAnterior2()
        {
            MockEstrategiaNivelCancionInfinita mockEstrategiaNivelCancionInfinita = new MockEstrategiaNivelCancionInfinita();
            Nivel nivel = new Nivel("Mock", mockEstrategiaNivelCancionInfinita);

            IVistaPlayerCancion vistaPlayerCancion = new MockVistaPlayerCancion();
            IProveedorCancion proveedorCancion = new MockProveedorCancionXml();
            MockManejadorVentanaPrincipal manejadorVentanaPrincipal = new MockManejadorVentanaPrincipal();
            MockCalculadorDuracionNotas calculadorDuracionNotas = new MockCalculadorDuracionNotas();
            IPlayerCancionViewModel vm = new PlayerCancionViewModel(
                vistaPlayerCancion, manejadorVentanaPrincipal,
                proveedorCancion, calculadorDuracionNotas,
                new MockMapeoTecladoEntidadesEntrada(), new MockReproductorMusica());

            vm.EmpezarCancion(this, new EmpezarCancionLlamadoEventArgs(
                new Cancion("Jijiji", "Los redondos") { PathPartitura = "MiPath", PathArchivoMusica = "PathCancion.wav" }
                , nivel));

            Thread.Sleep(5100);

            Assert.GreaterOrEqual(mockEstrategiaNivelCancionInfinita.LlamadosObtenerNotas, 5);
            Assert.LessOrEqual(mockEstrategiaNivelCancionInfinita.LlamadosObtenerNotas, 7);
        }

        #region Mocks
        private class MockReproductorMusica : IReproductorMusica
        {
            public void ReproducirCancion(string path)
            {
                this.PathLlamado = path;
                this.ReproducirFueLlamado = true;
            }

            public void DetenerReproduccion()
            {
                this.DetenerFueLlamado = true;
            }

            public string PathLlamado { get; set; }
            
            public bool ReproducirFueLlamado { get; set; }

            public bool DetenerFueLlamado { get; set; }
        }

        private class MockEstrategiaNivelSinNotas : IEstrategiaNivel
        {
            public Nota ObtenerSiguienteNota()
            {
                return null;
            }

            public bool EsFinalCancion()
            {
                return true;
            }

            public void AsignarTonos(IControladorTeclas controlador)
            {
                
            }

            public void AsignarCancion(Cancion cancion)
            {
                
            }
        }

        private class MockMapeoTecladoEntidadesEntrada : IMapeoTecladoEntidadesEntrada
        {
            public EntidadEntrada ObtenerEntidadEntrada(Key key)
            {
                this.ObtenerEntidadEntradaFueLlamado = true;
                this.TeclaLlamado = key;
                if (key == (Key.S))
                {
                    return new EntidadEntrada(2);
                }

                return new EntidadEntrada(1);
            }

            public ReadOnlyCollection<EntidadEntrada> ObtenerEntidadesEntrada()
            {
                return new ReadOnlyCollection<EntidadEntrada>(new List<EntidadEntrada> { new EntidadEntrada(1) });
            }

            public bool ObtenerEntidadEntradaFueLlamado
            {
                get; private set;
            }

            public Key TeclaLlamado
            {
                get; private set;
            }
        }

        private class MockEstrategiaNivelCancionInfinita : IEstrategiaNivel
        {

            public int LlamadosObtenerNotas { get; set; }

            #region IEstrategiaNivel Members

            public Nota ObtenerSiguienteNota()
            {
                LlamadosObtenerNotas++;
                return new Nota(Tono.Do, FiguraMusical.Negra);
            }

            public bool EsFinalCancion()
            {
                return false;
            }

            public void AsignarTonos(IControladorTeclas controlador)
            {
                
            }

            public void AsignarCancion(Cancion cancion)
            {

            }

            #endregion
        }

        private class MockEstrategiaNivelCancionFinita : IEstrategiaNivel
        {
            private int contador = 0;

            public int LlamadosObtenerNotas { get; set; }

            public bool AsignarTonosLlamado
            {
                get; set;
            }

            public bool AsignarCancionLlamado
            {
                get; set;
            }

            #region IEstrategiaNivel Members

            public Nota ObtenerSiguienteNota()
            {
                LlamadosObtenerNotas++;
                contador++;
                if (contador % 2 == 0)
                {
                    return new Nota(Tono.Do, FiguraMusical.Negra);
                }
                return new Nota(Tono.Re, FiguraMusical.Semicorchea);
            }

            public bool EsFinalCancion()
            {
                if (contador >= 10)
                {
                    return true;
                }

                return false;
            }

            public void AsignarTonos(IControladorTeclas controlador)
            {
                this.AsignarCancionLlamado = true;
            }

            public void AsignarCancion(Cancion cancion)
            {
                this.AsignarTonosLlamado = true;
            }

            #endregion
        }
        
        private class MockCalculadorDuracionNotas : ICalculadorDuracionNotas
        {
            public double CalcularDuracion(TiempoCancion tiempoCancion, Nota nota)
            {
                throw new NotImplementedException();
            }

            public double CalcularDuracion(TiempoCancion tiempoCancion, FiguraMusical figuraMusical)
            {
                this.CalcularDuracionFueLlamado = true;
                this.FiguraLlamado = figuraMusical;
                if (figuraMusical == FiguraMusical.Negra)
                {
                    return 1;
                }
                else if(figuraMusical == FiguraMusical.Semicorchea)
                {
                    return 0.25;
                }
                return 0;
            }

            public double RelacionDeFigura(FiguraMusical figuraMusical) 
            {
                if (figuraMusical == FiguraMusical.Negra)
                {
                    return 1;
                }
                else if (figuraMusical == FiguraMusical.Semicorchea)
                {
                    return 0.25;
                }
                return 0;
            }


            public double RelacionDeFigura(Nota nota)
            {
                FiguraMusical figuraMusical = nota.Figura;
                if (figuraMusical == FiguraMusical.Negra)
                {
                    return 1.0;
                }
                else if (figuraMusical == FiguraMusical.Semicorchea)
                {
                    return 0.25;
                }
                return 0;
            }
            

            public bool CalcularDuracionFueLlamado
            {
                get; set;
            }

            public FiguraMusical FiguraLlamado
            {
                get; set;
            }
        }
        
        private class MockVistaPlayerCancion : IVistaPlayerCancion
        {
            #region Propiedades
            public bool AgregarNotaVisualFueLlamado
            {
                get; set;
            }

            public bool ActualizarFueLlamado
            {
                get; set;
            }

            public bool DataContextFueSeteado
            {
                get; set;
            }

            public object DataContextSeteado
            {
                get; set;
            }

            public int CodigoEntidadEntradaAMarcar
            {
                get; private set;
            }

            public bool MarcarTeclaApretadaFueLlamado
            {
                get; private set;
            }
            #endregion

            public void AgregarNotaVisual(Nota nota, IEnumerable<ITecla> teclas)
            {
                this.AgregarNotaVisualFueLlamado = true;
            }

            public void Actualizar()
            {
                this.ActualizarFueLlamado = true;
            }

            public void AsignarDataContext(IPlayerCancionViewModel model)
            {
                this.DataContextFueSeteado = true;
                this.DataContextSeteado = model;
            }

            public bool TieneNotaAPresionar(EntidadEntrada entrada)
            {
                if (entrada.Codigo % 2 == 0)
                {
                    return true;
                }
                return false;
            }

            public bool TieneNotasAMostrar()
            {
                return true;
            }
        }

        private class MockVistaPlayerCancionSinNotasEnVista : IVistaPlayerCancion
        {
            #region IVistaPlayerCancion Members

            public void AgregarNotaVisual(Nota nota, IEnumerable<ITecla> teclas)
            {
            }

            public void Actualizar()
            {
            }

            public void AsignarDataContext(IPlayerCancionViewModel model)
            {
            
            }

            public bool TieneNotaAPresionar(EntidadEntrada entrada)
            {
                return false;
            }

            public bool TieneNotasAMostrar()
            {
                return false;
            }

            #endregion
        }

        private class MockProveedorCancionXml : IProveedorCancion
        {
            public Cancion ObtenerCancionSinPartitura(string path)
            {
                throw new NotImplementedException();
            }

            public Cancion ObtenerCancionConPartitura(string path)
            {
                if (path == "MiPath")
                {
                    return new Cancion("Cancion Recuperada", "Recuperada")
                               {
                                   Partitura = new Partitura(new TiempoCancion(4, 4)),
                                   PathArchivoMusica = "PathCancion.wav"
                               };
                }
                return null;
            }
        }

        private class MockManejadorVentanaPrincipal : IManejadorVentanaPrincipal
        {
            public object Contenido
            {
                get; set;
            }

            public void CambiarContenido(object control)
            {
                this.Contenido = control;
            }
        }
        #endregion
    }
}
