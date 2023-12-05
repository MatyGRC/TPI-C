using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPIntegrador.Enums;
using TPIntegrador.Objectos.Mapa;

namespace TPIntegrador.Objectos.Robots
{
    internal class M8 : Operador
    {
        public M8(string UID, Estado estadoOperador, Bateria bateria, double cargaMaxima, double cargaActual, double velocidad, Localizacion localizacionActual)
        : base(UID, estadoOperador, bateria, cargaMaxima, cargaActual, velocidad, localizacionActual)
        {
            bateria = new Bateria(12250, 0);
            cargaMaxima = 250;
        }

        public override void MoverTerreno(Localizacion[,] terreno, int coorX, int coorY)
        {
            double porcentaje = cargaMaxima * 0.1;
            double cargaPorcentual = cargaActual;
            if (dañoOperador == Daño.MOTOR_COMPROMETIDO)
            {
                velocidad = velocidad / 2;
            }
            double velocidadActual = velocidad;
            while (cargaPorcentual - porcentaje < 0)
            {
                cargaPorcentual -= porcentaje;
                velocidadActual -= velocidad * 0.05;
            }

            double kilometros = localizacionActual.calcularDistanciaViaje(coorX, coorY);
            double miliAmper = (kilometros / velocidadActual) * 1000;

            double bateriaNecesaria = miliAmper * kilometros;
            if (bateria.getBateriaActual() < bateriaNecesaria)
            {
                Console.WriteLine("No es posible llegar al destino");
                return;
            }

            int nuevaCoordenadaX = localizacionActual.CoordenadaX;
            int nuevaCoordenadaY = localizacionActual.CoordenadaY;

            while (nuevaCoordenadaX != coorX || nuevaCoordenadaY != coorY)
            {
                MoverEnDireccion(terreno, ref nuevaCoordenadaX, ref nuevaCoordenadaY, coorX, coorY);
                CheckearTerreno();
            }
        }

        private void MoverEnDireccion(Localizacion[,] terreno, ref int x, ref int y, int destinoX, int destinoY)
        {
            if (x < destinoX && PuedeMoverse(terreno, x + 1, y))
            {
                x++;
            }
            else if (x > destinoX && PuedeMoverse(terreno, x - 1, y))
            {
                x--;
            }

            if (y < destinoY && PuedeMoverse(terreno, x, y + 1))
            {
                y++;
            }
            else if (y > destinoY && PuedeMoverse(terreno, x, y - 1))
            {
                y--;
            }

            VerificarLagosEnDiagonales(terreno, ref x, ref y);
        }

        private bool PuedeMoverse(Localizacion[,] terreno, int x, int y)
        {
            return EsCoordenadaValida(x, y, terreno) && terreno[x, y].Tipo != TipoLocalizacion.Lago;
        }

        private void VerificarLagosEnDiagonales(Localizacion[,] terreno, ref int x, ref int y)
        {
            if (terreno[x, y].Tipo == TipoLocalizacion.Lago &&
                terreno[x + 1, y].Tipo != TipoLocalizacion.Lago)
            {
                x++;
            }
            else if (terreno[x - 1, y].Tipo != TipoLocalizacion.Lago)
            {
                x--;
            }

            if (terreno[x, y + 1].Tipo != TipoLocalizacion.Lago)
            {
                y++;
            }
            else if (terreno[x, y - 1].Tipo != TipoLocalizacion.Lago)
            {
                y--;
            }
        }

        private bool EsCoordenadaValida(int x, int y, Localizacion[,] terreno)
        {
            return x >= 0 && x < terreno.GetLength(0) && y >= 0 && y < terreno.GetLength(1);
        }

        public override void CambiarBateria()
        {
            Bateria nuevaBateria = new Bateria(12250, 0);
            bateria = nuevaBateria;
        }
    }
}
