using System.Runtime.CompilerServices;
class Program
{

    static void Main()
    {
        List<Operadores> reserva = new List<Operadores>();
        VerMenu();
        string opcion = Console.ReadLine();
        bool salir = false;
        while (!salir)
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
                    AgregarORemover();
                    break;
                case "6":
                    Console.WriteLine("Saliendo del programa.");
                    salir = true;
                    break;
                default:
                    Console.WriteLine("Opción no válida. Por favor, ingrese un número válido.");
                    break;
            }

        }
        void AgregarORemover()
        {
            bool salir = false;

            while (!salir)
            {
                Console.WriteLine("1. Agregar operador UAV");
                Console.WriteLine("2. Agregar operador K9");
                Console.WriteLine("3. Agregar operador M8");
                Console.WriteLine("4. Remover operador");
                Console.WriteLine("5. Salir");

                string select = Console.ReadLine();
                switch (select)
                {
                    case "1":
                        AgregarOpUAV();
                        break;
                    case "2":
                        
                        break;
                    case "3":

                        break;
                    case "4":
                        RemoverOp();
                        break;
                    case "5":
                        salir = true;
                        break;
                    default:
                        Console.WriteLine("Opción no válida. Por favor, ingrese una opción válida.");
                        break;
                }
            }
         
            void AgregarOpUAV()
            {
                Console.WriteLine("Ingrese el nombre del operador: ");
                string nombreOp = Console.ReadLine();
                Operadores nombreOp = new Uav();
                reserva.Add(nombreOp);
                Console.WriteLine($"El operador '{nombreOp}' se ha agregado a la lista.");
            }

            void RemoverOp()
            {
                Console.Write("Ingrese el nombre del operador a remover: ");
                string nombreOp = Console.ReadLine();
                if (reserva.Remove(nombreOp))
                {
                    Console.WriteLine($"El operador '{nombreOp}' se ha removido de la lista.");
                }
                else
                {
                    Console.WriteLine($"El operador '{nombreOp}' no se encontró en la lista.");
                }
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
}
