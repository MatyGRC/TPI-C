using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TPIntegrador.Enums;
using TPIntegrador.Objectos.Mapa;

namespace TPIntegrador.Objectos.Robots
{
    internal class Operador
    {
        protected string UID;
        public Estado estadoOperador;
        protected Daño dañoOperador = Daño.NINGUNO;
        protected Bateria bateria;
        protected double cargaMaxima;
        protected double cargaActual;
        protected double velocidad;
        protected Localizacion localizacionActual;

        public Operador(string UID, Estado estadoOperador, Bateria bateria, double cargaMaxima, double cargaActual, double velocidad, Localizacion localizacionActual)
        {
            this.UID = UID;
            this.estadoOperador = estadoOperador;
            this.bateria = bateria;
            this.cargaMaxima = cargaMaxima;
            this.cargaActual = cargaActual;
            this.velocidad = velocidad;
            this.localizacionActual = localizacionActual;
        }

        public string GetUID() { return UID; }

        public Localizacion GetLocalizacion() { return localizacionActual; }

        public Bateria GetBateria() { return bateria; }

        public double GetCarga() { return cargaActual; }

        public Estado GetEstado()
        {
            estadoOperador = cargaActual > 0 ? Estado.ENMOVIMIENTO : (bateria.getBateriaActual() > 0 ? Estado.STANDBY : Estado.APAGADO);
            return estadoOperador;
        }

        public void SetEstado(Estado nuevoEstado)
        {
            estadoOperador = nuevoEstado;
        }

        public virtual void MoverTerreno(Localizacion[,] terreno, int coorX, int coorY) { }
        public virtual void MoverTerreno(Cuartel, int coordenadaX, int coordenadaY) { }

        public void transferirCargaBateria(Operador otroOperador)
        {
            if (localizacionActual == otroOperador.GetLocalizacion() && dañoOperador != Daño.PUERTO_BATERIA_DESCONECTADO)
            {
                double miliAmperACargar = bateria.getBateriaActual();
                otroOperador.GetBateria().CargarBateria();
                bateria.ConsumirBateria(bateria.getBateriaActual()); 
                estadoOperador = Estado.APAGADO;
            }
        }


        public void TransferirCarga(double cargaTransporte, Localizacion[,] terreno, int coorX, int coorY)
        {
            Localizacion localizacionDestino = terreno[coorX, coorY];
            if (cargaTransporte <= cargaMaxima && localizacionDestino != localizacionActual && dañoOperador != Daño.SERVO_ATASCADO)
            {
                this.MoverTerreno(terreno, coorX, coorY);
                Console.WriteLine("La carga ha sido transferida");
            }
            else
            {
                Console.WriteLine("No es posible mover la carga actual");
            }
        }

        public void RecibirCarga(double cargaTransporte)
        {
            if (dañoOperador != Daño.SERVO_ATASCADO)
            {
                cargaActual += cargaTransporte;
            }
        }

        void VolverCuartelTransferirCargaFisica(Localizacion[,] terreno, int coorX, int coorY)
        {
            this.MoverTerreno(terreno, coorX, coorY); //, "Cuartel General");
            if (this.cargaActual > 0 && dañoOperador != Daño.SERVO_ATASCADO)
            {
                if (terreno[coorX, coorY] is Cuartel cuartel)
                {
                    Console.WriteLine($"El operador {this.UID} está transfiriendo toda la carga física al Cuartel General.");
                    cuartel.RecibirCarga(this);
                    this.cargaActual = 0;
                }
                else
                {
                    Console.WriteLine("Error: La ubicacion no es un Cuartel.");
                }
            }
        }
        
        void VolverCuartelCargarBateria(Localizacion[,] terreno, int coorX, int coorY)
        {

            this.MoverTerreno(terreno, coorX, coorY);
            if (dañoOperador != Daño.PUERTO_BATERIA_DESCONECTADO)
            {
                bateria.CargarBateria();
                Console.WriteLine($"Cuartel General ha cargado la batería del Operador {UID}.");
            }

        }

        public virtual void CambiarBateria() { }

        public void CheckearTerreno()
        {
            switch (localizacionActual.Tipo)
            {
                case TipoLocalizacion.Vertedero:
                    Random random = new Random();
                    int randomNumber = random.Next(1, 101);
                    if (randomNumber <= 5)
                    {
                        Console.WriteLine("Vertedero provocó daños al operador");
                        estadoOperador = Estado.DAÑADO;
                        CalcularDaño(randomNumber);
                    }
                    break;
                case TipoLocalizacion.VertederoElectronico:
                    Console.WriteLine("Vertedero Electronico, se reduce la bateria maxima");
                    estadoOperador = Estado.DAÑADO;
                    bateria.ReducirBateriaMaxima();
                    break;
                default:
                    Console.WriteLine("Terreno seguro");
                    break;
            }
        }

        public void CalcularDaño(int ID)
        {
            dañoOperador = ID switch
            {
                1 => Daño.MOTOR_COMPROMETIDO,
                2 => Daño.SERVO_ATASCADO,
                3 => Daño.BATERIA_PERFORADA,
                4 => Daño.PUERTO_BATERIA_DESCONECTADO,
                5 => Daño.PINTURA_RAYADA,
                _ => Daño.NINGUNO
            };
        }

        public void RepararOperador()
        {
            if (dañoOperador == Daño.MOTOR_COMPROMETIDO)
            {
                velocidad *= 2;
            }
            if (bateria.bateriaDañada)
            {
                CambiarBateria();
            }
            dañoOperador = Daño.NINGUNO;
        }
    }

}

