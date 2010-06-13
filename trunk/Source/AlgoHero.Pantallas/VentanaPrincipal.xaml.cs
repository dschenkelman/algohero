using System;
using System.Windows;
using System.Windows.Controls;
using AlgoHero.Pantallas.Interfaces;

namespace AlgoHero.Pantallas
{
    /// <summary>
    /// Interaction logic for VentanaPrincipal.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window, IVentanaPrincipal
    {
        public VentanaPrincipal()
        {
            InitializeComponent();
        }

        public Control ObtenerContenido()
        {
            return (Control)this.Contenido.Content;
        }

        public void CambiarContenido(Control control)
        {
            this.Contenido.Content = control;
        }
    }
}
