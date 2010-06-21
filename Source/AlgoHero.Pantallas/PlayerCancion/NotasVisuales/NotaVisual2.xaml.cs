using System.Windows.Controls;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Pantallas.PlayerCancion.NotasVisuales
{
    /// <summary>
    /// Interaction logic for NotaVisual2.xaml
    /// </summary>
    public partial class NotaVisual2 : UserControl, INotaVisual
    {
        public NotaVisual2(Nota nota)
        {
            this.NotaRelacionada = nota;
            InitializeComponent();
        }


        public Nota NotaRelacionada
        {
            get;
            private set;
        }
        
        public void AgregarACanvas(Canvas canvas)
        {
            canvas.Children.Add(this);
            Canvas.SetTop(this, 0);
            Canvas.SetLeft(this, 10);
            Canvas.SetRight(this, 10);
        }

        public void Actualizar()
        {
            double alturaAnterior = Canvas.GetTop(this);
            Canvas.SetTop(this, alturaAnterior + this.ActualHeight / 5);
        }

    }
}
