using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using TPIntegrador.Objectos.Mapa;

class Program
{
    static void Main()
    {
        UsarTerreno();
        Terreno terreno = new Terreno();
        List<Cuartel> cuartelesEnTerreno = terreno.getCuarteles();

        int opcion;
        int numeroCuartel;

        do
        {
            Console.WriteLine("Menú de Operadores:");
            Console.WriteLine("Seleccione un cuartel:");

            for (int i = 0; i < cuartelesEnTerreno.Count; i++)
            {
                Console.WriteLine($"{i + 1}) Cuartel {cuartelesEnTerreno[i].Numero}");
            }

            Console.WriteLine("0) Salir");

            Console.Write("Seleccione un cuartel o 0 para salir: ");
            numeroCuartel = int.Parse(Console.ReadLine());

            if (numeroCuartel == 0)
            {
                Console.WriteLine("Saliendo del programa. ¡Hasta luego!");
                break;
            }

            if (numeroCuartel < 1 || numeroCuartel > cuartelesEnTerreno.Count)
            {
                Console.WriteLine("Número de cuartel no válido. Intente de nuevo.");
                continue;
            }

            Cuartel cuartelSeleccionado = cuartelesEnTerreno[numeroCuartel - 1];

            Console.WriteLine($"Operando en Cuartel {cuartelSeleccionado.Numero}");

            Console.WriteLine("1) Listar el estado de todos los operadores.");
            Console.WriteLine("2) Listar el estado de todos los operadores en cierta localización.");
            Console.WriteLine("3) Total recall general a todos los operadores.");
            Console.WriteLine("4) Seleccionar un operador en específico:");
            Console.WriteLine("   a) Enviarlo a una localización en especial.");
            Console.WriteLine("   b) Indicar retorno a cuartel.");
            Console.WriteLine("   c) Cambiar estado a STANDBY.");
            Console.WriteLine("5) Agregar o remover operadores de la reserva.");

            Console.Write("Seleccione una opción: ");
            opcion = int.Parse(Console.ReadLine());

            switch (opcion)
            {
                case 1:
                    cuartelSeleccionado.listarEstados();
                    break;
                case 2:
                    Console.Write("Ingrese la coordenada X: ");
                    int coordenadaX = int.Parse(Console.ReadLine());

                    Console.Write("Ingrese la coordenada Y: ");
                    int coordenadaY = int.Parse(Console.ReadLine());

                    cuartelSeleccionado.listarEstadosLocalizacion(coordenadaX, coordenadaY);
                    break;
                case 3:
                    cuartelSeleccionado.totalRecall();
                    break;
                case 4:
                    // Lógica para seleccionar un operador y realizar acciones.
                    break;
                case 5:
                    // Lógica para agregar o remover operadores de la reserva.
                    break;
                case 0:
                    Console.WriteLine("Saliendo del programa.");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Intente de nuevo.");
                    break;
            } while (opcion != 0) ;

        }
    }



    static void UsarTerreno()
    {
        Console.WriteLine("Si desea cargar terreno previo, presiona x");
        string respuesta = Console.ReadLine();
        if (respuesta.ToLower() == "x")
        {
            string json = File.ReadAllText(@"./terreno.json");
            Terreno terrenoViejo = JsonSerializer.Deserialize<Terreno>(json);
        }
    }

    static void GuardarTerreno(Terreno terreno)
    {
        string fileName = "terreno.json";
        string json = JsonSerializer.Serialize(terreno);
        File.WriteAllText(fileName, json);
    }
}

