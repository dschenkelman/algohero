using System;
using System.Diagnostics;
using System.Threading;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace AlgoHero.Pantallas.PlayerCancion.Utilitarios
{
    public class TimerDePrecision
    {
        private readonly int intervaloMilisegundos;
        private Thread threadProgreso;
        private Stopwatch relojPrecision;

        public List<Exception> Errores;

        public TimerDePrecision(int intervaloMilisegundos)
        {
            this.intervaloMilisegundos = intervaloMilisegundos;
            this.relojPrecision = new Stopwatch();
            this.Errores = new List<Exception>();
        }

        public event EventHandler<IntervaloConsumidoEventArgs> IntervaloConsumido;

        public void Comenzar()
        {
            this.relojPrecision.Start();
            threadProgreso = new Thread(this.MantenerCorriendo);
            threadProgreso.Start();
        }

        public int apariciones;

        private void MantenerCorriendo()
        {
             while (this.relojPrecision.IsRunning)
             {
                try
                {
                    this.apariciones++;
                    int milisegundosAdelantados = this.intervaloMilisegundos -
                                                  (int) Math.Ceiling(0.12*this.intervaloMilisegundos);
                    if (this.relojPrecision.ElapsedMilliseconds > milisegundosAdelantados)
                    {
                        IntervaloConsumido(this, new IntervaloConsumidoEventArgs());
                        this.relojPrecision = Stopwatch.StartNew();
                    }
                }
                catch (InvalidComObjectException e)
                {
                    this.Errores.Add(e);
                }
            }
            
        }

        public void Detener()
        {
            this.relojPrecision.Stop();
            threadProgreso.Interrupt();
            this.threadProgreso = null;
        }
    }

    
}
