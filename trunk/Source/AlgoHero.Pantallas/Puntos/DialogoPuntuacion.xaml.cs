using System.Windows;

namespace AlgoHero.Pantallas.Puntos
{
    /// <summary>
    /// Interaction logic for DialogoPuntuacion.xaml
    /// </summary>
    public partial class DialogoPuntuacion : Window
    {
        public DialogoPuntuacion(Juego.Puntos.Puntuacion puntuacion)
        {
            this.DataContext = puntuacion;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}