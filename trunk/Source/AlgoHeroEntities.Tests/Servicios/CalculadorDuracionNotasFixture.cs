using AlgoHero.Interface.Enums;
using AlgoHero.MusicEntities.Core;
using AlgoHero.MusicEntities.Servicios;

namespace AlgoHeroMusic.Entities.Tests.Servicios
{
    using NUnit.Framework;
    
    [TestFixture]
    public class CalculadorDuracionNotasFixture
    {
       [Test]
        public void CalcularDuracionNotaDevuelveValorCorrecto()
       {
           var calculador = new CalculadorDuracionNotas();
           var tiempoCancion = new TiempoCancion(4, 2);
           var nota = new Nota(Tono.Do, FiguraMusical.Redonda);
           double segundos = calculador.CalcularDuracion(tiempoCancion, nota);
           Assert.AreEqual(4, segundos);
       }

       [Test]
       public void CalcularDuracionNotaPasandoFiguraDevuelveValorCorrecto()
       {
           var calculador = new CalculadorDuracionNotas();
           var tiempoCancion = new TiempoCancion(4, 2);
           double segundos = calculador.CalcularDuracion(tiempoCancion, FiguraMusical.Semicorchea);
           Assert.AreEqual(0.25, segundos);
       }

    }
}
