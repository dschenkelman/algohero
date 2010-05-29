using AlgoHero.MusicEntities.Core;

namespace AlgoHero.MusicEntities.Servicios.Interfaces
{
    public interface ICalculadorDuracionNotas
    {
        double CalcularDuracion(TiempoCancion tiempoCancion, Nota nota);
    }
}
