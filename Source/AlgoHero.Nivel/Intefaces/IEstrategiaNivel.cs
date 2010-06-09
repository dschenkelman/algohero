using System;
using System.Collections.Generic;
using AlgoHero.Interface.Enums;
using AlgoHero.Interface;
using AlgoHero.MusicEntities.Core;

namespace AlgoHero.Nivel.Intefaces
{
    public interface IEstrategiaNivel
    {
        Nota ObtenerSiguienteNota();
        bool EsFinalCancion();
        void AsignarTonos(IControladorTeclas controlador);
    }
}