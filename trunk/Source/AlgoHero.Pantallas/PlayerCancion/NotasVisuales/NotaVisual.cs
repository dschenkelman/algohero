using System.Windows.Controls;
using System.Windows;

namespace AlgoHero.Pantallas.PlayerCancion.NotasVisuales
{
    public static class NotaVisual
    {
        public static void AgregarACanvas(Canvas canvas, FrameworkElement notaVisual)
        {
            canvas.Children.Add(notaVisual);
            Canvas.SetTop(notaVisual, 0);
            Canvas.SetLeft(notaVisual, 10);
            Canvas.SetRight(notaVisual, 10);
        }

        public static void Actualizar(FrameworkElement notaVisual)
        {
            double alturaAnterior = Canvas.GetTop(notaVisual);
            Canvas.SetTop(notaVisual, alturaAnterior + notaVisual.ActualHeight / 5);
        }
    }
}
