using System.Collections.ObjectModel;
using AlgoHero.Interface.Enums;

namespace AlgoHero.Interface
{
    public interface ITecla
    {
        void AgregarTonoAsociado(Tono tono);
        Tono ObtenerTono(int index);
        void ResetearTonosAsignados();
        ReadOnlyCollection<Tono> ObtenerTonosAsociados();
        int CantidadTonos { get; }
        EntidadEntrada EntidadEntrada { get; }
    }
}
