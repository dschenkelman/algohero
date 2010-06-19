using System;

namespace AlgoHero.PuntuacionJuego
{
    public class Puntuacion
    {
        private string nivel;

        public Puntuacion(string nivelJuego)
        {
            this.Multiplicador = 1;
            this.PuntosAcumulados = 0;
            this.RachaDeNotasAcertadas = 0;
            this.nivel = nivelJuego;
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
            switch (this.nivel.ToLower())
            {
                case "facil":
                    return this.RachaDeNotasAcertadas % 5 == 0;
                case "medio":
                    return this.RachaDeNotasAcertadas % 10 == 0;
                case "dificil":
                    return this.RachaDeNotasAcertadas % 15 == 0;
                default:   
                    throw new ArgumentException();
            }
        }

        public int Multiplicador { get; private set; }
        public int RachaDeNotasAcertadas { get; private set; }
        public int PuntosAcumulados { get; private set; }
    }
}
