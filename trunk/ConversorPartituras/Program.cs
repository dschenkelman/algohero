using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Traductor
{
    class Program
    {
        static void Main(string[] args)
        {
            ElTraductor et = new ElTraductor();
            Console.WriteLine("Iniciando...");
            string path;
            try
            {
                path = args[0];
            }
            catch 
            {
                Console.WriteLine("No se detecto ningun archivo por linea de comandos.");
                Console.Write("Por favor ingrese uno(ruta absoluta): ");
                path = Console.ReadLine();
            }
            
            Console.WriteLine("Se convertira el archivo: \n"+path);
            while (true)
            {
                try
                {
                    et.ObtenerCancion(path);
                    Console.WriteLine();
                    Console.WriteLine("Exitos! (cualquier tecla para salir)");
                    Console.ReadKey();
                    return;
                }
                catch
                {
                    Console.WriteLine();
                    Console.WriteLine("Error! (cualquier tecla para reintentar; killsignal para salir)");
                    Console.ReadKey();
                }
            }

        }
    }
}
