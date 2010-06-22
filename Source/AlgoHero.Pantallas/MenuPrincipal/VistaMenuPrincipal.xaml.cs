using System.Windows.Controls;
using AlgoHero.Pantallas.Interfaces;

namespace AlgoHero.Pantallas.MenuPrincipal
{
    /// <summary>
    /// Interaction logic for VistaMenuPrincipal.xaml
    /// </summary>
    public partial class VistaMenuPrincipal : UserControl
    {
        public VistaMenuPrincipal(IMenuPrincipalViewModel viewModel)
        {
            this.DataContext = viewModel;
            viewModel.AsignarVista(this);
            InitializeComponent();
        }
    }
}
