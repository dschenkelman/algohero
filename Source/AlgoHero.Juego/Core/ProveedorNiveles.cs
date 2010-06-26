using AlgoHero.Juego.Intefaces;
using System.Collections.ObjectModel;

namespace AlgoHero.Juego.Core
{
    public class ProveedorNiveles : IProveedorNiveles
    {
        private ObservableCollection<Nivel> niveles;

        /* Constructor. Crea los niveles y los asigna a la coleccion de niveles. */
        public ProveedorNiveles()
        {
            Nivel facil = new Nivel("Facil", new EstrategiaNivelFacil());
            Nivel medio = new Nivel("Medio", new EstrategiaNivelMedio());
            Nivel dificil = new Nivel("Dificil", new EstrategiaNivelDificil());
            this.niveles = new ObservableCollection<Nivel>();

            this.niveles.Add(facil);
            this.niveles.Add(medio);
            this.niveles.Add(dificil);
        }

        /* Este metodo devuelve una coleccion con los niveles. */
        public ObservableCollection<Nivel> ObtenerNiveles()
        {
            return this.niveles;
        }

    }
}
