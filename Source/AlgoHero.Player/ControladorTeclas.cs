using System;
using System.Collections.Generic;
using AlgoHero.Interface;
using AlgoHero.Player.Interfaces;

namespace AlgoHero.Player
{
    public class ControladorTeclas : IControladorTeclas
    {
        public int CantidadTeclas
        {
            get { throw new NotImplementedException(); }
        }

        public Tecla ObtenerTecla(int index)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Tecla> ObtenerTeclas()
        {
            throw new NotImplementedException();
        }

        public Tecla ObtenerTecla(EntidadEntrada entidadEntrada)
        {
            throw new NotImplementedException();
        }
    }
}
