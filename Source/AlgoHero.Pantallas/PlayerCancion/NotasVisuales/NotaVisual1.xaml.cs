using System;
using System.Windows;
using System.Windows.Controls;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Pantallas.PlayerCancion.NotasVisuales
{
    /// <summary>
    /// Interaction logic for NotaVisual1.xaml
    /// </summary>
    public partial class NotaVisual1 : UserControl, INotaVisual
    {
        public NotaVisual1(Nota nota)
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
            Canvas.SetLeft(this, 10);
            Canvas.SetRight(this, 10);
        }
    }
}
