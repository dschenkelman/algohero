using AlgoHero.Interface;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Juego.Intefaces
{
    public interface IEstrategiaNivel
    {
        Nota ObtenerSiguienteNota();
        bool EsFinalCancion();
        void AsignarTonos(IControladorTeclas controlador);
    }
}