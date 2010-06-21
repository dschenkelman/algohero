using System;
using System.Windows.Controls;
using System.Windows;

namespace AlgoHero.Pantallas.PlayerCancion.NotasVisuales
{
    public static class NotaVisual
    {
        //Factor de velocidad de movimiento de notas. 
        public const int FACTOR_VELOCIDAD = 5;
        
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
            Canvas.SetTop(notaVisual, alturaAnterior + notaVisual.ActualHeight / FACTOR_VELOCIDAD);
        }

        public static bool PuedeBorrarse(FrameworkElement notaVisual)
        {
            Canvas canvasPadre = notaVisual.Parent as Canvas;
            if (canvasPadre != null)
            {
                double alturaActual = Canvas.GetTop(notaVisual);
                if (alturaActual > canvasPadre.ActualHeight)
                {
                    return true;   
                }
                return false;
            }
            throw new InvalidOperationException();
        }

        public static void Borrar(FrameworkElement notaVisual)
        {
            Canvas canvasPadre = notaVisual.Parent as Canvas;
            if (canvasPadre != null)
            {
                canvasPadre.Children.Remove(notaVisual);
            }
            throw new InvalidOperationException();
        }
    }
}
