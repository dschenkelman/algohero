using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using AlgoHero.Interface;
using AlgoHero.Interface.Enums;


namespace AlgoHero.Juego.Tests.Core.Mocks
{
    public class MockTecla : ITecla
    {
        private List<Tono> tonosAsociados;
        private int Codigo;

        public MockTecla(int codigo)
        {
            this.Codigo = codigo;
            this.EntidadEntrada = new EntidadEntrada(codigo);
            this.tonosAsociados = new List<Tono>();
            this.CantidadTonos = 0;
        }

        public void AgregarTonoAsociado(Tono tono)
        {
            if (this.tonosAsociados.Contains(tono))
            {
                throw new InvalidOperationException();
            }
            this.CantidadTonos += 1;
            this.tonosAsociados.Add(tono);
        }

        public Tono ObtenerTono(int index)
        {
            return this.tonosAsociados[index];
        }

        public ReadOnlyCollection<Tono> ObtenerTonosAsociados()
        {
            return this.tonosAsociados.AsReadOnly();
        }

        public int CantidadTonos { get; private set; }
        public EntidadEntrada EntidadEntrada { get; private set; }
    }
}