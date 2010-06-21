using NUnit.Framework;
using AlgoHero.Pantallas.PlayerCancion.Utilitarios;
using System.Threading;
namespace AlgoHero.Pantallas.Tests
{
    [TestFixture]
    public class TimerDePrecisionTests
    {
        [Test]
        [Ignore]
        public void CrearTimerConIntervaloDeVeinticincoMilisegundosAvisaCadaVeinticincoMilisegundos()
        {
            TimerDePrecision timerDePrecision = new TimerDePrecision(25);

            int cantidadNotificaciones = 0;

            timerDePrecision.IntervaloConsumido += delegate { cantidadNotificaciones++; };

            timerDePrecision.Comenzar();

            Thread.Sleep(2000);

            timerDePrecision.Detener();

            Assert.AreEqual(0, timerDePrecision.Errores.Count);
            //no es exacto el tiempo, por eso el assert hace esto.
            Assert.GreaterOrEqual(cantidadNotificaciones, 72);
            Assert.LessOrEqual(cantidadNotificaciones, 88);
        }

        [Test]
        [Ignore]
        public void CrearTimerConIntervaloDeVeinticincoMilisegundosAvisaCadaCincuentaMilisegundos()
        {
            TimerDePrecision timerDePrecision = new TimerDePrecision(50);

            int cantidadNotificaciones = 0;

            timerDePrecision.IntervaloConsumido += delegate { cantidadNotificaciones++; };

            timerDePrecision.Comenzar();

            //Thread.Sleep(2000);
            Thread.Sleep(2000);

         
            Assert.AreEqual(0, timerDePrecision.Errores.Count);
            //no es exacto el tiempo, por eso el assert hace esto.
            Assert.GreaterOrEqual(cantidadNotificaciones, 32);
            Assert.LessOrEqual(cantidadNotificaciones, 48);
        }
    }
}
