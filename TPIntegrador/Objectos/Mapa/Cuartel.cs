using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPIntegrador.Enums;
using TPIntegrador.Objectos.Robots;

namespace TPIntegrador.Objectos.Mapa
{
    internal class Cuartel : Localizacion
    {
        List<Operador> operadores;
        double carga;
        private static int contadorCuartel = 0;
        public int Numero { get; private set; }

        public Cuartel(List<Operador> operadores, double carga, TipoLocalizacion tipo, int x, int y) : base(tipo, x, y)
        {
            Numero = ++contadorCuartel;
            this.operadores = operadores;
            this.carga = carga;
            this.Tipo = tipo;
            this.CoordenadaX = x;
            this.CoordenadaY = y;
            inicializarOperadores();
        }

        private void inicializarOperadores()
        {

            M8 operadorM8 = new M8("M8-1", Estado.STANDBY, new Bateria(12250, 0), 250, 0, 10, this);
            K9 operadorK9 = new K9("K9-1", Estado.STANDBY, new Bateria(6500, 0), 40, 0, 15, this);
            UAV operadorUAV = new UAV("UAV-1", Estado.STANDBY, new Bateria(4000, 0), 5, 0, 20, this);

            operadores.Add(operadorM8);
            operadores.Add(operadorK9);
            operadores.Add(operadorUAV);
        }

      
        public void listarEstados()
        {
            foreach (Operador o in operadores)
            {
                Console.WriteLine(o.GetEstado());
            }
        }

        public void listarEstadosLocalizacion(int coorX, int coorY)
        {
            foreach (Operador o in operadores)
            {
                if (o.GetLocalizacion().CoordenadaX == coorX && o.GetLocalizacion().CoordenadaY == coorY)
                {
                    Console.WriteLine(o.GetEstado());
                }
            }
        }

        public Operador SeleccionarOperadorPorUID(string uid)
        {
            Operador operadorSeleccionado = operadores.Find(o => o.GetUID() == uid);

            if (operadorSeleccionado == null)
            {
                Console.WriteLine($"No se encontró un operador con el UID {uid} en el cuartel.");
            }

            return operadorSeleccionado;
        }
        public void EnviarALocalizacion(Operador oOperador, Localizacion[,] terreno, int coorX, int coorY)
        {
            try
            {
                oOperador.MoverTerreno(terreno, coorX, coorY);
                if (oOperador.GetLocalizacion() == terreno[coorX, coorY] && oOperador.GetEstado() != Estado.APAGADO)
                {
                    Console.WriteLine($"El operador {oOperador.GetUID()} ha sido enviado a la nueva localizacion.");
                }
                else
                {
                    throw new Exception($"No hay suficiente batería para enviar el operador {oOperador.GetUID()} a la localizacion destino.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void totalRecall()
        {
            foreach (Operador operador in operadores){
                operador.MoverTerreno(this, CoordenadaX, CoordenadaY);
            }
        }

        public void RetornoACuartel(Operador oOperador, Localizacion[,] terreno)
        {
            try
            {
                oOperador.MoverTerreno(terreno, CoordenadaX, CoordenadaY);
                if (oOperador.GetLocalizacion() == this && oOperador.GetEstado() != Estado.APAGADO)
                {
                    Console.WriteLine($"El operador {oOperador.GetUID()} ha retornado al cuartel.");
                }
                else
                {
                    throw new Exception($"No hay suficiente batería para que el operador {oOperador.GetUID()} retorne al cuartel.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public void CambiarEstadoAStandby(Operador oOperador)
        {
            try
            {
                oOperador.SetEstado(Estado.STANDBY);
                Console.WriteLine($"El operador {oOperador.GetUID} ha cambiado su estado a STANDBY.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cambiar el estado del operador: {ex.Message}");
            }
        }

        public void CargarBateria(Operador operador, double miliAmper)
        {
            try
            {
                operador.GetBateria().CargarBateria();
                Console.WriteLine($"Cuartel General ha cargado {miliAmper} mAh de batería para el Operador {operador.GetUID()}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al cargar la batería del operador: {ex.Message}");
            }
        }

        public void RecibirCarga(Operador operador)
        {
            try
            {
                carga += operador.GetCarga();
                Console.WriteLine($"Cuartel General ha recibido toda la carga física del Operador {operador.GetUID()}.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al recibir la carga del operador: {ex.Message}");
            }
        }


        public void agregarOperador(Operador oOperador)
        {
            operadores.Add(oOperador);
        }

        public void removerOperador(Operador oOperador)
        {
            operadores.Remove(oOperador);
        }

        public void MandarVertedero(Localizacion[,] terreno){
            foreach (Operador o in operadores){
                if(o.GetEstado() == Estado.STANDBY){
                    Localizacion vertederoCercano = o.localizacionActual.buscarVertedero(terreno);
                    int coorX = vertederoCercano.CoordenadaX;
                    int coorY = vertederoCercano.CoordenadaY;
                    o.MoverTerreno(terreno,coorX,coorY);
                }
            }
        }

        public void RetornarOperadoresDañados(Localizacion[,] terreno)
        {
            foreach (Operador o in operadores)
            {
                if (o.estadoOperador == Estado.DAÑADO)
                {
                    RetornoACuartel(o, terreno);
                    o.RepararOperador();
                }
            }
        }
    }
}
