using System.Runtime.CompilerServices;
class Program
{

    static void Main()
    {
        VerMenu();
        string opcion = Console.ReadLine();
        while (opcion != "6")
        {
            switch (opcion)
            {
                case "1":
                    Console.WriteLine("Seleccionaste la opción 1.");
                    break;
                case "2":
                    Console.WriteLine("Seleccionaste la opción 2.");
                    break;
                case "3":
                    Console.WriteLine("Seleccionaste la opción 3.");
                    break;
                case "4":
                    Console.WriteLine("Seleccionaste la opción 4.");
                    break;
                case "5":
                    Console.WriteLine("Seleccionaste la opción 5.");
                    break;
                case "6":
                    Console.WriteLine("Saliendo del programa.");
                    break;
                default:
                    Console.WriteLine("Opción no válida. Por favor, ingrese un número válido.");
                    break;
            }

        }
    }
    static void VerMenu()
    {
        Console.WriteLine("Operaciones de Cuartel");
        Console.WriteLine("1. Listar el estado de todos los operadores");
        Console.WriteLine("2. Listar el estado de todos los operadores que estén en cierta localización.");
        Console.WriteLine("3. Hacer un total recall (llamado y retorno) general a todos los operadores");
        Console.WriteLine("4. Seleccionar un operador en específico");
        Console.WriteLine("5. Agregar o remover operadores de la reserva");
        Console.WriteLine("6. Salir");
    }
    enum Estado
    {
        Encendido,
        Apagado,
        EnMovimiento
    }
    void ListarEstados()
    {

    }
    void MoverOp()
    {

    }
    void TransferirCarga()
    {

    }
}

