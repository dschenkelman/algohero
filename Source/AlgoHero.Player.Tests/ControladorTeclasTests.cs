using AlgoHero.MusicEntities.Core;
using NUnit.Framework;
using AlgoHero.MusicEntities.Enums;

namespace AlgoHero.Player.Tests
{
    [TestFixture]
    public class ControladorTeclasTests
    {
        

        private static Cancion ObtenerCancionMock()
        {
            Cancion cancion = new Cancion("Mock", "Autor Mock");
            TiempoCancion tiempo = new TiempoCancion(4, 2);
            Partitura partitura = new Partitura(tiempo);
            //Do 16, La 10, Re 8, Si 6, Mi 4, Fa 2, Sol 1
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 16, Tono.Do, FiguraMusical.Semicorchea));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 8, Tono.Re, FiguraMusical.Corchea));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 4, Tono.Mi, FiguraMusical.Negra));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 2, Tono.Fa, FiguraMusical.Blanca));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 1, Tono.Sol, FiguraMusical.Redonda));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 2, Tono.La, FiguraMusical.Blanca));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 4, Tono.Si, FiguraMusical.Negra));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 2, Tono.Si, FiguraMusical.Blanca));
            partitura.AgregarCompas(ObtenerMockCompas(tiempo, 8, Tono.La, FiguraMusical.Corchea));

            cancion.Partitura = partitura;
            return cancion;
        }

        private static Compas ObtenerMockCompas(TiempoCancion tiempo, int cantidad, Tono tono, FiguraMusical figura)
        {
            Compas compas = new Compas(tiempo);
            for (int i = 0; i < cantidad; i++)
            {
                compas.AgregarNota(new Nota(tono, figura));
            }
            return compas;
        }
    }
}
