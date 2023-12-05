using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPIntegrador.Enums;
using TPIntegrador.Objectos.Robots;

namespace TPIntegrador.Objectos.Mapa
{
    internal class Terreno
    {
        private const int TamanoTerreno = 100; // En kilómetros cuadrados
        private Localizacion[,] terreno = new Localizacion[TamanoTerreno, TamanoTerreno]; //100*100
        private Random random = new Random(); //para elegir la zona
                                              //limites de tipo de terreno, cuarteles 3 y reciclajes 5
        private int cuartelesActuales = 0;
        private int sitiosReciclajeActuales = 0;

        private List<Cuartel> cuartelesEnTerreno = new List<Cuartel>();

        internal List<Cuartel> getCuarteles()
        {
            return cuartelesEnTerreno;
        }

        public Terreno()
        {
            GenerarTerreno();
        }

        private void GenerarTerreno()
        {
            for (int i = 0; i < TamanoTerreno; i++)
            {
                for (int j = 0; j < TamanoTerreno; j++)
                {
                    terreno[i, j] = GenerarLocalizacionAleatoria(i, j);
                }
            }
        }

        private Localizacion GenerarLocalizacionAleatoria(int coordenadaX, int coordenadaY)
        {
            Localizacion localizacion = null;
            TipoLocalizacion tipo = (TipoLocalizacion)random.Next(Enum.GetNames(typeof(TipoLocalizacion)).Length);

            while (localizacion == null)
            {
                if (tipo == TipoLocalizacion.Cuartel && cuartelesActuales < 3)
                {
                    cuartelesActuales++;
                    Cuartel nuevoCuartel = new Cuartel(new List<Operador>(), 0, tipo, coordenadaX, coordenadaY);
                    cuartelesEnTerreno.Add(nuevoCuartel);
                    localizacion = nuevoCuartel;
                }
                else if (tipo == TipoLocalizacion.SitioReciclaje && sitiosReciclajeActuales < 5)
                {
                    sitiosReciclajeActuales++;
                    localizacion = new Localizacion(tipo, coordenadaX, coordenadaY);
                }
                else if (tipo != TipoLocalizacion.Cuartel && tipo != TipoLocalizacion.SitioReciclaje)
                {
                    localizacion = new Localizacion(tipo, coordenadaX, coordenadaY);
                }
                else
                {
                    tipo = (TipoLocalizacion)random.Next(Enum.GetNames(typeof(TipoLocalizacion)).Length); // Generar un nuevo tipo si no se pudo crear una localización
                }
            }

            return localizacion;
        }

        public void calcularVertederoCercano()
        {

        }
        public void MostrarTerreno()
        {
            for (int i = 0; i < TamanoTerreno; i++)
            {
                for (int j = 0; j < TamanoTerreno; j++)
                {
                    Console.Write($"{terreno[i, j].Tipo.ToString()} ");
                }
                Console.WriteLine();
            }
        }

    }
}

