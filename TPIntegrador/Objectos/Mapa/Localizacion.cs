using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPIntegrador.Enums;

namespace TPIntegrador.Objectos.Mapa
{
    internal class Localizacion
    {
        public TipoLocalizacion Tipo { get; set; } // se generan solos los getters y setters
        public int CoordenadaX { get; set; }
        public int CoordenadaY { get; set; }

        public Localizacion(TipoLocalizacion tipo, int x, int y)
        {
            Tipo = tipo;
            CoordenadaX = x;
            CoordenadaY = y;
        }
        public double calcularDistanciaViaje(int coordenadaXDestino, int coordenadaYDestino)
        {
            double distancia = Math.Abs(coordenadaXDestino - CoordenadaX) + Math.Abs(coordenadaYDestino - CoordenadaY);
            return distancia;
        }

        public Localizacion buscarVertedero(Localizacion[,] terreno){
            int distanciaMinima = 0;
            Localizacion vertederoCercano;
             for (int i = 0; i < terreno.GetLength(0); i++)
            {
                for (int j = 0; j < terreno.GetLength(1); j++)
                {
                    if(terreno[i,j].Tipo == TipoLocalizacion.Vertedero){
                        int coorX = terreno[i,j].CoordenadaX;
                        int coorY = terreno[i,j].CoordenadaY;
                        double distancia = this.calcularDistanciaViaje(coorX,coorY);
                        if(distancia < distanciaMinima && distancia > 0){
                            distanciaMinima = distancia;
                            vertederoCercano = terreno[i,j];
                        }
                    }
                }
            }
            return vertederoCercano;
        }
    }
}
