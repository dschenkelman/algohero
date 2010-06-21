using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using AlgoHero.PuntuacionJuego;

namespace PuntuacionVista
{
    public class PuntuacionViewModel : INotifyPropertyChanged
    {
        private int multiplicador;
        private int puntosAcumulados;
        private int racha;
        private Puntuacion puntuacionJuego;

        public PuntuacionViewModel(Puntuacion puntuacionJuego)
        {
            this.puntuacionJuego = puntuacionJuego;
            this.multiplicador = puntuacionJuego.Multiplicador;
            this.puntosAcumulados = puntuacionJuego.PuntosAcumulados;
            this.racha = puntuacionJuego.RachaDeNotasAcertadas;
        }

        public int Multiplicador
        {
            get
            {
                return this.multiplicador;
            }

            set
            {
                if(this.multiplicador != this.puntuacionJuego.Multiplicador)
                {
                    this.multiplicador = this.puntuacionJuego.Multiplicador;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("Multiplicador"));
                }
            }
        }

        public int RachaDeNotasAcertadas
        {
            get
            {
                return this.racha;
            }

            set
            {
                if(this.racha != this.puntuacionJuego.RachaDeNotasAcertadas)
                {
                    this.racha = this.puntuacionJuego.RachaDeNotasAcertadas;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("RachaDeNotasAcertadas"));
                }
            }
        }

        public int PuntosAcumulados
        {
            get
            {
                return this.puntosAcumulados;
            }

            set
            {
                if(this.puntosAcumulados != this.puntuacionJuego.PuntosAcumulados)
                {
                    this.puntosAcumulados = this.puntuacionJuego.PuntosAcumulados;
                    this.PropertyChanged(this, new PropertyChangedEventArgs("PuntosAcumulados"));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

    }
}
