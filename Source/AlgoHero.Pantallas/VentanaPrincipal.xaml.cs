using System.Windows;
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

        public object ObtenerContenido()
        {
            return this.Contenido.Content;
        }

        public void CambiarContenido(object control)
        {
            this.Contenido.Content = control;
        }
    }
}
