using System.Collections.Generic;
using AlgoHero.Interface;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Pantallas.Interfaces
{
    public interface IVistaPlayerCancion
    {
        void AgregarNotaVisual(Nota nota, IEnumerable<ITecla> teclas);
        void Actualizar();
        void AsignarDataContext(IPlayerCancionViewModel model);
        bool TieneNotaAPresionar(EntidadEntrada entrada);
    }
}
