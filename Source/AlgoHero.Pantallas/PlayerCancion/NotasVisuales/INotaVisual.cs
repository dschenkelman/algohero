using System.Windows.Controls;
using AlgoHero.MusicEntities.Core;
namespace AlgoHero.Pantallas.PlayerCancion.NotasVisuales
{
    public interface INotaVisual
    {
        Nota NotaRelacionada { get; }
        void AgregarACanvas(Canvas canvas);
    }
}
