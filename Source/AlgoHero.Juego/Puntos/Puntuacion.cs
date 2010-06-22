using System;
using AlgoHero.Juego.Core;

namespace AlgoHero.Juego.Puntos
{
    public class Puntuacion
    {
        /* Constructor. Crea una nueva puntuacion con un nivel*/
        public Puntuacion()
        {
            this.Multiplicador = 1;
            this.PuntosAcumulados = 0;
            this.RachaDeNotasAcertadas = 0;
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
            return this.RachaDeNotasAcertadas % 10 == 0;
        }

        public int Multiplicador { get; private set; }
        public int RachaDeNotasAcertadas { get; private set; }
        public int PuntosAcumulados { get; private set; }

        public void Reiniciar()
        {
            this.Multiplicador = 1;
            this.PuntosAcumulados = 0;
            this.RachaDeNotasAcertadas = 0;
        }
    }
}