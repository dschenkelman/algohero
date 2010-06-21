using System.Windows.Controls;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Pantallas.PlayerCancion.NotasVisuales
{
    /// <summary>
    /// Interaction logic for NotaVisual4.xaml
    /// </summary>
    public partial class NotaVisual4 : UserControl, INotaVisual
    {
        public NotaVisual4(Nota nota)
        {
            this.NotaRelacionada = nota;
            InitializeComponent();
        }

        public Nota NotaRelacionada
        {
            get; private set;
        }

        public void AgregarACanvas(Canvas canvas)
        {
            NotaVisual.AgregarACanvas(canvas, this);
        }

        public void Actualizar()
        {
            NotaVisual.Actualizar(this);
        }

        public bool PuedeBorrarse()
        {
            return NotaVisual.PuedeBorrarse(this);
        }

        public bool HayQuePresionar()
        {
            return NotaVisual.HayQuePresionar(this);
        }

    }
}
