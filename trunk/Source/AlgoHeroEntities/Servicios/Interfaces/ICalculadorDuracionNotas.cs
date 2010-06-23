using AlgoHero.MusicEntities.Core;
using AlgoHero.Interface.Enums;

namespace AlgoHero.MusicEntities.Servicios.Interfaces
{
    public interface ICalculadorDuracionNotas
    {
        double CalcularDuracion(TiempoCancion tiempoCancion, Nota nota);
        double CalcularDuracion(TiempoCancion tiempoCancion, FiguraMusical figuraMusical);
        double RelacionDeFigura(FiguraMusical figura);
        double RelacionDeFigura(Nota nota);
    }
}
