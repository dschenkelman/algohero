using System;
using AlgoHero.MusicEntities.Enums;

namespace AlgoHero.Files
{
    public class ConvertidorStringAEntidadesMusicales
    {
        public static Tono ConvertirATono(string tono)
        {
            switch (tono.ToLower())
            {
                case "do":
                    return Tono.Do;
                case "do#":
                    return Tono.DoSostenido;
                case "re":
                    return Tono.Re;
                case "re#":
                    return Tono.ReSostenido;
                case "mi":
                    return Tono.Mi;
                case "fa":
                    return Tono.Fa;
                case "fa#":
                    return Tono.FaSostenido;
                case "sol":
                    return Tono.Sol;
                case "la":
                    return Tono.La;
                case "la#":
                    return Tono.LaSostenido;
                case "si":
                    return Tono.Si;
                default:
                    throw new ArgumentException();
            }
        }

        public static FiguraMusical ConvertirAFigura(string figura)
        {
            switch (figura.ToLower())
            {
                case "redonda":
                    return FiguraMusical.Redonda;
                case "blanca":
                    return FiguraMusical.Blanca;
                case "negra":
                    return FiguraMusical.Negra;
                case "corchea":
                    return FiguraMusical.Corchea;
                case "semicorchea":
                    return FiguraMusical.Semicorchea;
                default:
                    throw new ArgumentException();

            }
        }
    }
}
