using System.Windows.Controls;

namespace AlgoHero.Pantallas.MenuPrincipal
{
    /// <summary>
    /// Interaction logic for VistaMenuPrincipal.xaml
    /// </summary>
    public partial class VistaMenuPrincipal : UserControl
    {
        public VistaMenuPrincipal(MenuPrincipalViewModel model)
        {
            this.DataContext = model;
            InitializeComponent();
        }
    }
}
