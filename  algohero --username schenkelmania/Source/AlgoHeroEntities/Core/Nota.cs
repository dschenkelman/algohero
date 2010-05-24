using AlgoHero.MusicEntities.Servicios.Interfaces;

namespace AlgoHero.MusicEntities.Core
{
    using AlgoHero.MusicEntities.Enums;
    using AlgoHero.MusicEntities.Servicios;

    public class Nota
    {
        private ICalculadorDuracionNotas calculadorDuracion;

        public Nota(Tono tono, FiguraMusical figura)
        {
            this.Tono = tono;
            this.Figura = figura;
            this.calculadorDuracion = new CalculadorDuracionNotas();
        }

        #region Properties
        public Tono Tono
        {
            get; private set;
        }

        public FiguraMusical Figura
        {
            get; private set;
        }
        #endregion


        public double CalcularDuracion(TiempoCancion tiempoCancion)
        {
            return this.calculadorDuracion.CalcularDuracion(tiempoCancion, this);
        }
    }
}
