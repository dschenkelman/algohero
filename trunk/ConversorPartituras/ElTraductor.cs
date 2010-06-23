using System;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
namespace Traductor
{
    public class ElTraductor
    {
        public void ObtenerCancion(string path)
        {
            XmlDocument documento = new XmlDocument();
            documento.Load(path);

            List<Double> segundos_blancas= this.CrearTiempoCancion(documento);
            Double segundosPorCompas = segundos_blancas[0];
            Double blancasPorCompas = segundos_blancas[1];
            
            Console.Write("Ingrese el nombre del tema: ");
            string nombre = Console.ReadLine();
            Console.Write("Ingrese el autor del tema: ");
            string autor = Console.ReadLine();

            this.ArmarCancion(segundosPorCompas, blancasPorCompas, nombre, autor, documento);
            
            Console.WriteLine("Segundos que dura un Compas: " + segundosPorCompas);
            Console.WriteLine("Blancas por Compas: " + blancasPorCompas);
            Console.WriteLine();
            
        }

        private List<Double> CrearTiempoCancion(XmlDocument documento)
        {
            XmlNode main = documento.SelectSingleNode("/xml/score-partwise/part");
            XmlNodeList compases = main.SelectNodes("./measure");

            XmlNode time = compases[0].SelectSingleNode("./attributes/time");
            string beats_str = time.SelectSingleNode("./beats").InnerText;
            string tipo_str = time.SelectSingleNode("./beat-type").InnerText;

            XmlNode tempo = compases[0].SelectSingleNode("./direction/sound");
            double tempoEnNegras = Convert.ToDouble(tempo.Attributes["tempo"].Value);
            
            double beats = Convert.ToDouble(beats_str);
            double tipo = Convert.ToDouble(tipo_str);
            
            while (tipo > 4)
            {
                tipo /= 2;
                beats /= 2;
            }
            while (tipo < 4)
            {
                tipo *= 2;
                beats *= 2;
            }
            double segundosPorCompas = ( (double)(beats * 60) ) / (double)tempoEnNegras;
            List<Double> list = new List<double>();
            list.Add(Math.Round(segundosPorCompas, 3,MidpointRounding.ToEven));
            list.Add(beats);
            return list;
        }
        
        private void ArmarCancion(double double_segundos, double double_blancas, string nombre_str, 
            string autor_str, XmlDocument origen)
        {
            XmlDocument destino = new XmlDocument();

            XmlElement root = destino.CreateElement("","xml","");
            XmlElement cancion = destino.CreateElement("", "cancion", "");
            XmlElement compases = destino.CreateElement("", "compases", "");
            XmlElement tiempo = destino.CreateElement("", "tiempo", "");

            XmlAttribute nombre = destino.CreateAttribute("nombre");
            nombre.Value = nombre_str;
            cancion.Attributes.Append(nombre);

            XmlAttribute autor = destino.CreateAttribute("autor");
            autor.Value = autor_str;
            cancion.Attributes.Append(autor);

            XmlAttribute segundos = destino.CreateAttribute("duracionCompasSegundos");
            segundos.Value = Convert.ToString(double_segundos);
            tiempo.Attributes.Append(segundos);

            XmlAttribute blancas = destino.CreateAttribute("cantidadNegras");
            blancas.Value = Convert.ToString(double_blancas);
            tiempo.Attributes.Append(blancas);

            cancion.AppendChild(tiempo);
            this.AgregarCompases(destino, compases, origen);
            cancion.AppendChild(compases);
            
            root.AppendChild(cancion);
            destino.AppendChild(root);

            try
            {
                destino.Save("C:\\"+nombre_str+".xml");
                Console.WriteLine("El archivo \"" + nombre_str + ".xml\" Fue creado en C:\\");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        private void AgregarCompases(XmlDocument destino, XmlElement compases, XmlDocument origen)
        {
            XmlNode main = origen.SelectSingleNode("/xml/score-partwise/part");
            XmlNodeList lista_compases = main.SelectNodes("./measure");
            foreach (XmlNode compas in lista_compases)
            {

                XmlElement compas_out = destino.CreateElement("", "compas", "");
                XmlElement notas_out = destino.CreateElement("", "notas", "");
                XmlNodeList notas = compas.SelectNodes("./note");
                List<XmlElement> lista_notas = new List<XmlElement>();
                foreach (XmlNode nota in notas)
                {
                    XmlAttribute forma = destino.CreateAttribute("forma");
                    string figura = nota.SelectSingleNode("./type").InnerText;
                    forma.Value = this.ConvertirFigura(figura);

                    XmlElement nota_out = destino.CreateElement("", "nota", "");
                    nota_out.Attributes.Append(forma);

                    XmlElement tono_out = destino.CreateElement("", "tono", "");
                    XmlAttribute valor_out = destino.CreateAttribute("valor");
                    try
                    {
                        //si es rest(silencio) tira excepcion porq no existe inner text para null
                        string valor = nota.SelectSingleNode("./pitch/step").InnerText;
                        if (nota.SelectSingleNode("./pitch/alter") != null)
                            valor_out.Value = this.ConvertirTono(valor, true);
                        else
                            valor_out.Value = this.ConvertirTono(valor, false);
                    } 
                    catch (Exception e)
                    {
                        valor_out.Value = this.ConvertirTono("silencio", false);
                    }
                    tono_out.Attributes.Append(valor_out);

                    if (nota.SelectSingleNode("./chord") != null)
                    {
                        int indice = lista_notas.Count - 1;
                        lista_notas[indice].AppendChild(tono_out);
                    }
                    else
                    {
                        nota_out.AppendChild(tono_out);
                        lista_notas.Add(nota_out);
                    }

                }
                foreach(XmlElement nota_o in lista_notas)
                {
                    notas_out.AppendChild(nota_o);
                }
                compas_out.AppendChild(notas_out);
                Console.Write("|");
                compases.AppendChild(compas_out);
            }
            Console.WriteLine();
        }

        private string ConvertirFigura(string figura) 
        {
            switch (figura)
            {
                case "whole":
                    return "redonda";
                case "half":
                    return "blanca";
                case "quarter":
                    return "negra";
                case "eighth":
                    return "corchea";
                case "16th":
                    return "semicorchea";
                case "32nd":
                    return "fusa";
                case "64th":
                    return "semifusa";
                default:
                    Console.WriteLine("Atencion: La figura " + figura + " NO EXISTE");
                    throw new ArgumentException();
            }
        }
        private string ConvertirTono(string tono, bool sostenido)
        {
            string cadena;
            switch (tono)
            {
                case "C":
                    cadena = "Do";
                    break;
                case "D":
                    cadena = "Re";
                    break;
                case "E":
                    cadena = "Mi";
                    break;
                case "F":
                    cadena = "Fa";
                    break;
                case "G":
                    cadena = "Sol";
                    break;
                case "A":
                    cadena = "La";
                    break;
                case "B":
                    cadena = "Si";
                    break;
                case "silencio":
                    cadena = "Silencio";
                    break;
                default:
                    Console.WriteLine("Atencion: El Tono " + tono + " NO EXISTE");
                    throw new ArgumentException();
            }
            if (sostenido) 
            {
                cadena += "#";
            }
            return cadena;
        }
        
    }
}
