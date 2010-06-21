using System;

namespace AlgoHero.PuntuacionJuego
{
    public class Puntuacion
    {
        private string nivel;

        /* Constructor. Crea una nueva puntuacion con un nivel*/
        public Puntuacion(string nivelJuego)
        {
            this.Multiplicador = 1;
            this.PuntosAcumulados = 0;
            this.RachaDeNotasAcertadas = 0;
            this.nivel = nivelJuego;
        }

        /* Este metodo aumenta la racha de notas acertadas, los puntos, y si corresponde, el multiplicador, dado que se llama
         * cuando se cierta una nota*/
        public void AcertarNota()
        {
            this.RachaDeNotasAcertadas++;
            this.PuntosAcumulados += this.Multiplicador;
            //si el mult es uno un punto por nota, si es dos, dos... etc
            if (this.VerificarCambioMultiplicador())
            {
                this.Multiplicador++;
            }
        }

        /* Este metodo vuelve la racha de notas acertadas a 0, el multiplicador a 1, y mantiene los puntos, ya que se erro
         * la nota */
        public void ErrarNota()
        {
            this.Multiplicador = 1;
            this.RachaDeNotasAcertadas = 0;
        }

        /* Este metodo verifica si con la cantidad de notas acertadas de forma consecutiva, aumenta o no el multiplicador */
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