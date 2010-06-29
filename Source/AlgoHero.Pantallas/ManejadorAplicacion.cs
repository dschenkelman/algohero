﻿using AlgoHero.Juego.Core;
using AlgoHero.Juego.Intefaces;
using AlgoHero.Pantallas.MenuPrincipal;
using AlgoHero.Files;
using AlgoHero.Pantallas.Interfaces;
using AlgoHero.Pantallas.PlayerCancion;
using AlgoHero.MusicEntities.Servicios;
using AlgoHero.Juego.Entrada;
using AlgoHero.Juego.Reproductor;

namespace AlgoHero.Pantallas
{
    public class ManejadorAplicacion
    {
        /* Inicia la aplicacion. */
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
                new PlayerCancionViewModel(vistaPlayerCancion, manejadorVentanaPrincipal, proveedorCancionesDirectorio, new CalculadorDuracionNotas(), new MapeoTecladoEntidadesEntrada(), new ReproductorMusica());

            menuPrincipalViewModel.EmpezarCancionLlamado += playerCancionViewModel.EmpezarCancion;
            playerCancionViewModel.CancionTerminada += menuPrincipalViewModel.CancionTermino;

            manejadorVentanaPrincipal.CambiarContenido(menuPrincipal);

            ventanaPrincipal.KeyDown += playerCancionViewModel.TeclaApretada;
            ventanaPrincipal.Closing += this.CerrandoVentanaPrincipal;

            ventanaPrincipal.Show();

            this.AplicacionCorriendo = true;
        }

        /* Este metodo cierra la ventana principal, establece la propiedad AplicacionCorriendo a false */
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