using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AlgoHero.Juego.Intefaces
{
    interface IReproductorMusica
    {
        void ReproducirCancion(string path);
        void DetenerReproduccion();
    }
}
