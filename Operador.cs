    internal abstract class Operador {
    public string UID;
    Estado estadoOperador;
    public double bateria;
    public double bateriaActual;
    public double carga;
    public double cargaActual;
    public double velocidad;
    public string localizacion;

    /*public Operador(string UID, string estadoOperador, int bateria, int bateriaActual, double cargaMaxima, double cargaActual, double velocidad, string localizacion){
        this.UID = UID;
        this.estadoOperador = estadoOperador;
        this.bateria = bateria;
        this.bateriaActual = bateriaActual;
        this.carga = cargaMaxima;
        this.cargaActual = cargaActual;
        this.velocidad = velocidad;
        this.localizacion = localizacion;
    }*/

    public string GetLocalidad(){return localizacion;}
    
    public double GetBateria(){return bateria;}

    public Estado GetEstado(){
        if(bateria > 0 ){
            estadoOperador = Estado.ENCENDIDO;
        } else { estadoOperador = Estado.APAGADO; }
    }

    public bool MoverLocalidad(double kilometros, string localizacionDestino){
        bool desplazamiento = false;
        double porcentaje = carga * 0.1;
        double cargaActual = carga;
        double velocidadActual = velocidad;
        while(cargaActual < 0){
            cargaActual -= porcentaje;
            velocidadActual -= velocidad * 0.05;
        }
        double tiempo = kilometros / velocidadActual;
        bateria -= tiempo * 1000;  //Cada hora de consumo son 1000 miliAmperios, entonces multiplico la cantidad de horas por mil    
        if (bateria > 0){ //Pregunto si tiene suficiente bateria para llegar al destino
            localizacion = localizacionDestino;
            desplazamiento = true;
        }
        return desplazamiento;
    }

    public void transferirCarga(Operador oOperador){
        if(localizacion == oOperador.GetLocalidad()){
            oOperador.GetBateria() += bateria;
            bateria = 0;
            estadoOperador = Estado.APAGADO;
        }
    }

    public void EnviarALocalizacion(string nuevaUbicacion)
    {
        if (MoverLocalidad(0, nuevaUbicacion) && GetEstado() != Estado.APAGADO)
        {
            Console.WriteLine($"El operador {UID} ha sido enviado a la ubicación {nuevaUbicacion}.");
        }
        else
        {
            Console.WriteLine($"No hay suficiente batería para enviar el operador {UID} a la ubicación {nuevaUbicacion}.");
        }
    }

    public void RetornoACuartel()
    {
        if (MoverLocalidad(0, "Cuartel") && GetEstado() != Estado.APAGADO)
        {
            Console.WriteLine($"El operador {UID} ha retornado al cuartel.");
        }
        else
        {
            Console.WriteLine($"No hay suficiente batería para que el operador {UID} retorne al cuartel.");
        }
    }

    public void CambiarEstadoAStandby()
    {
        estadoOperador = Estado.APAGADO;
        Console.WriteLine($"El operador {UID} ha cambiado su estado a STANDBY.");
    }
    public void TransferirCarga(double cargaTransporte, string localizacionDestino)
    {
        /*ver funcion de movimiento (punto 1) y agregarlo al if o antes del if*/
        if (cargaTransporte >= carga && localizacionDestino != localizacion)
        {
            localizacionDestino = localizacion;
            Console.WriteLine($"La carga ha sido transferida {localizacionDestino}");
        }
        else
        {
            Console.WriteLine("No es posible mover la carga actual");
        }
    }

    public void RecibirCarga(double cargaTransporte)
    {
        carga += cargaTransporte;
    }

    void VolverCuartelTransferirCargaFisica(Cuartel cuartel)
    {
        double distanciaLocalizacion = CalcularDistanciaACuartel();
        this.MoverLocalidad(distanciaLocalizacion, "Cuartel General");
        if (this.carga > 0)
        {
            Console.WriteLine($"Operador {this.UID} está transfiriendo toda la carga física al Cuartel General.")            Cuarteles.RecibirCarga(this);
            this.carga = 0;
        }
    }
    void VolverCuartelCargarBateria(Cuartel cuartel)
    {
        double distanciaLocalizacion = CalcularDistanciaACuartel();
        this.MoverLocalidad(distanciaLocalizacion, "Cuartel General");
        if (this.bateriaActual < this.bateria)
        {
            double cantidadACargar = this.bateria - this.bateriaActual;
            Cuartel.CargarBateria(this, cantidadACargar);
            this.bateriaActual = this.bateria;
        }
    }
    private double CalcularDistanciaACuartel()
    {
        return localizacionDestino;
    }
}

