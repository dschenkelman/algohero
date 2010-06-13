using System.Windows;

namespace AlgoHero.Pantallas
{
    /// <summary>
    /// Interaction logic for VentanaPrincipal.xaml
    /// </summary>
    public partial class VentanaPrincipal : Window
    {
        public VentanaPrincipal(VentanaPrincipalViewModel model)
        {
            this.DataContext = model;
            InitializeComponent();
        }
    }
}
