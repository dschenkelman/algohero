using System;
using System.Collections.Generic;
using System.Windows.Controls;
using AlgoHero.Interface;
using AlgoHero.MusicEntities.Core;
using AlgoHero.Pantallas.Interfaces;
using AlgoHero.Pantallas.PlayerCancion.NotasVisuales;
using System.Windows.Threading;

namespace AlgoHero.Pantallas.PlayerCancion
{
    /// <summary>
    /// Interaction logic for VistaPlayerCancion.xaml
    /// </summary>
    public partial class VistaPlayerCancion : UserControl, IVistaPlayerCancion
    {
        private List<INotaVisual> notasEntrada1;
        private List<INotaVisual> notasEntrada2;
        private List<INotaVisual> notasEntrada3;
        private List<INotaVisual> notasEntrada4;
        private List<List<INotaVisual>> listaNotasEntrada;

        public VistaPlayerCancion()
        {
            this.notasEntrada1 = new List<INotaVisual>();
            this.notasEntrada2 = new List<INotaVisual>();
            this.notasEntrada3 = new List<INotaVisual>();
            this.notasEntrada4 = new List<INotaVisual>();
            this.listaNotasEntrada = new List<List<INotaVisual>>();
            this.listaNotasEntrada.Add(this.notasEntrada1);
            this.listaNotasEntrada.Add(this.notasEntrada2);
            this.listaNotasEntrada.Add(this.notasEntrada3);
            this.listaNotasEntrada.Add(this.notasEntrada4);
            InitializeComponent();
        }

        public delegate void ConvocarNotaVisualInterno(Nota nota, ITecla tecla);

        public void AgregarNotaVisual(Nota nota, IEnumerable<ITecla> teclas)
        {
            foreach (var tecla in teclas)
            {
                Dispatcher.BeginInvoke(new ConvocarNotaVisualInterno(this.AgregarNotaVisualInterno), DispatcherPriority.Send, nota, tecla);
            }
        }

        private void AgregarNotaVisualInterno(Nota nota, ITecla tecla)
        {
            INotaVisual notaVisual = ObtenerNotaVisualDeTecla(nota, tecla);
            this.listaNotasEntrada[tecla.EntidadEntrada.Codigo - 1].Add(notaVisual);
            Canvas canvasDeNota = this.ObtenerCanvasDeTecla(tecla);

            notaVisual.AgregarACanvas(canvasDeNota);
        }

        private static INotaVisual ObtenerNotaVisualDeTecla(Nota nota, ITecla tecla)
        {
            switch (tecla.EntidadEntrada.Codigo)
            {
                case 1:
                    return new NotaVisual1(nota);
                case 2:
                    return new NotaVisual2(nota);
                case 3:
                    return new NotaVisual3(nota);
                case 4:
                    return new NotaVisual4(nota);
                default:
                    throw new ArgumentException();
            }
        }

        private Canvas ObtenerCanvasDeTecla(ITecla tecla)
        {
            switch (tecla.EntidadEntrada.Codigo)
            {
                case 1:
                    return this.Entrada1;
                case 2:
                    return this.Entrada2;
                case 3:
                    return this.Entrada3;
                case 4:
                    return this.Entrada4;
                default:
                    throw new ArgumentException();
            }
        }

        #region IVistaPlayerCancion Members


        public delegate void InvocarActualizarNotaVisual();

        public void Actualizar()
        {
            Dispatcher.BeginInvoke(new InvocarActualizarNotaVisual(this.ActualizarInterno), DispatcherPriority.Send);
        }

        private void ActualizarInterno()
        {
            int cantidadEntradas = this.listaNotasEntrada.Count;
            for (int i = 0; i < cantidadEntradas; i++)
            {
                List<INotaVisual> notasEntrada = this.listaNotasEntrada[i];
                int cantidadNotas = notasEntrada.Count;
                for (int j = 0; j < cantidadNotas; j++)
                {
                    INotaVisual notaActual = notasEntrada[j];
                    notaActual.Actualizar();
                    this.UpdateLayout();
                    if (notaActual.PuedeBorrarse())
                    {
                        notasEntrada.RemoveAt(j);
                        j--;
                        cantidadNotas--;
                    }
                }
            }
        }

        #endregion
    }
}
