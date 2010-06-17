using System;
using System.Collections.Generic;
using AlgoHero.Juego.Intefaces;
using System.Collections.ObjectModel;

namespace AlgoHero.Juego.Core
{
    public class ProveedorNiveles : IProveedorNiveles
    {
        private ObservableCollection<Nivel> niveles;

        public ProveedorNiveles()
        {
            Nivel facil = new Nivel("Facil", null);
            Nivel medio = new Nivel("Medio", null);
            Nivel dificil = new Nivel("Dificil", null);
            this.niveles = new ObservableCollection<Nivel>();

            this.niveles.Add(facil);
            this.niveles.Add(medio);
            this.niveles.Add(dificil);
        }

        public ObservableCollection<Nivel> ObtenerNiveles()
        {
            return this.niveles;
        }

        #region Miembros de IProveedorNiveles

        ObservableCollection<Nivel> IProveedorNiveles.ObtenerNiveles()
        {
            throw new System.NotImplementedException();
        }

        #endregion
    }
}
