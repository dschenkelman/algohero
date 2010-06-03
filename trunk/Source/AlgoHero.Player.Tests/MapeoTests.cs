using System;
using System.Collections.Generic;
using NUnit.Framework;
using System.Windows.Input;

namespace AlgoHero.Player.Tests
{
    [TestFixture]
    public class MapeoTests
    {

        [Test]
        public void ObtenerEntidadMapeoFuncionaCorrectamente()
        {
            Mapeo map = new Mapeo();
            EntidadEntrada entEntradaUno = map.ObtenerEntidadEntrada(Key.A);
            EntidadEntrada entEntradaDos = map.ObtenerEntidadEntrada(Key.S);
            EntidadEntrada entEntradaTres = map.ObtenerEntidadEntrada(Key.K);
            EntidadEntrada entEntradaCuatro = map.ObtenerEntidadEntrada(Key.L);
            Assert.AreEqual(entEntradaUno.getCodigo(), 1);
            Assert.AreEqual(entEntradaDos.getCodigo(), 2);
            Assert.AreEqual(entEntradaTres.getCodigo(), 3);
            Assert.AreEqual(entEntradaCuatro.getCodigo(), 4);
        }

        [Test]
        public void ObtenerEntidadMapeoConKeyIncorrectaDevuelveNull()
        {
            Mapeo map = new Mapeo();
            Assert.AreEqual(map.ObtenerEntidadEntrada(Key.G), null);
        }

        [Test]
        public void ObtenerDiccionarioEntidadesFuncionaCorrectamente()
        {
            Mapeo map = new Mapeo();
            Dictionary<Key, EntidadEntrada> diccionario = new Dictionary<Key, EntidadEntrada>();
            diccionario.Add(Key.A, new EntidadEntrada(1));
            diccionario.Add(Key.S, new EntidadEntrada(2));
            diccionario.Add(Key.K, new EntidadEntrada(3));
            diccionario.Add(Key.L, new EntidadEntrada(4));

            foreach (Key key in map.getDiccionarioEntidades().Keys)
            {
                Assert.Contains(key, diccionario.Keys);
                EntidadEntrada entEntrada;
                EntidadEntrada entEntradaDos;
                Assert.AreEqual(diccionario.TryGetValue(key, out entEntrada), map.getDiccionarioEntidades().TryGetValue(key, out entEntradaDos));
            }
        }


    }
}
