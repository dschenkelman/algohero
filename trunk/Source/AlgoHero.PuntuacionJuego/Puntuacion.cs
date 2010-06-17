using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoHero.PuntuacionJuego
{
    public class Puntuacion
    {
        public Puntuacion()
        {
            this.Multiplicador = 1;
            this.PuntosAcumulados = 0;
            this.RachaDeNotasAcertadas = 0;
        }

        public void AcertarNota()
        {
            this.RachaDeNotasAcertadas++;
            this.PuntosAcumulados++; // suponiendo que cada nota acertada da 1 punto

            if (this.VerificarCambioMultiplicador())
            {
                this.Multiplicador++;
            }
        }

        public void ErrarNota()
        {
            this.Multiplicador = 1;
            this.RachaDeNotasAcertadas = 0;
        }

        private bool VerificarCambioMultiplicador()
        {
            return this.RachaDeNotasAcertadas % 10 == 0; // cada vez que acierta 10 teclas, aumenta el multiplicador (cambiar segun nivel!)
        }

        public int Multiplicador { get; private set; }
        public int RachaDeNotasAcertadas { get; private set; }
        public int PuntosAcumulados { get; private set; }
    }
}
