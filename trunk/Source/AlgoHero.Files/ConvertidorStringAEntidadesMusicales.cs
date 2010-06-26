using System;
using AlgoHero.Interface.Enums;

namespace AlgoHero.Files
{
    public class ConvertidorStringAEntidadesMusicales
    {
        /*Recibe una cadena con una representacion de un tono como string y devuelve su tono relacionado. 
         Si la cadena no esta relacionada a ningun tono lanza una excpecion ArgumentException.*/
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
                case "sol#":
                    return Tono.SolSostenido;
                case "la":
                    return Tono.La;
                case "la#":
                    return Tono.LaSostenido;
                case "si":
                    return Tono.Si;
                case "silencio":
                    return Tono.Silencio;
                default:
                    throw new ArgumentException();
            }
        }

        /*Recibe una cadena con una representacion de un figura como string y devuelve su figura relacionado. 
         Si la cadena no esta relacionada a ninguna figura lanza una excpecion ArgumentException.*/
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
                case "fusa":
                    return FiguraMusical.Corchea;
                case "semifusa":
                    return FiguraMusical.Semicorchea;
                default:
                    throw new ArgumentException();

            }
        }
    }
}
