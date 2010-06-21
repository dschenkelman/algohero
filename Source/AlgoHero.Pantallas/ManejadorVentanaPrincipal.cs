﻿using System;
using System.Windows.Controls;
using AlgoHero.Pantallas.Interfaces;

namespace AlgoHero.Pantallas
{
    public class ManejadorVentanaPrincipal : IManejadorVentanaPrincipal
    {
        private readonly IVentanaPrincipal ventanaPrincipal;

        public ManejadorVentanaPrincipal(IVentanaPrincipal ventanaPrincipal)
        {
            this.ventanaPrincipal = ventanaPrincipal;
        }

        public void CambiarContenido(object control)
        {
            this.ventanaPrincipal.CambiarContenido(control);
        }
    }
}