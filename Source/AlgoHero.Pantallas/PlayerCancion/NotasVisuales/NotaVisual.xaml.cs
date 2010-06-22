using System;
using System.Windows.Controls;
using System.Windows.Media;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Pantallas.PlayerCancion.NotasVisuales
{
    /// <summary>
    /// Interaction logic for NotaVisual.xaml
    /// </summary>
    public partial class NotaVisual : UserControl, INotaVisual
    {

        // Factor de velocidad de movimiento de notas. Cuanto menor es van mas rapido
        public const int FACTOR_VELOCIDAD = 5;

        // Factor de error. Cuanto menor es mayor precision se requiere.
        public const int FACTOR_ERROR = 20;
        
        public NotaVisual(Nota nota, Color colorNota)
        {
            this.NotaRelacionada = nota;
            this.DataContext = colorNota;
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
            Canvas.SetTop(this, alturaAnterior + this.ActualHeight / FACTOR_VELOCIDAD);
        }

        public bool PuedeBorrarse()
        {
            Canvas canvasPadre = this.Parent as Canvas;
            if (canvasPadre != null)
            {
                double alturaActual = Canvas.GetTop(this);
                if (alturaActual > canvasPadre.ActualHeight)
                {
                    return true;
                }
                return false;
            }
            throw new InvalidOperationException();
        }

        public bool HayQuePresionar()
        {
            Canvas canvasPadre = this.Parent as Canvas;
            if (canvasPadre != null)
            {
                double alturaActual = Canvas.GetTop(this);

                double alturaDesdeBase = canvasPadre.ActualHeight - this.ActualHeight;

                double diferencia = Math.Abs(alturaActual - alturaDesdeBase);

                if (diferencia < FACTOR_ERROR)
                {
                    return true;
                }
                return false;
            }
            throw new InvalidOperationException();
        }
    }
}
