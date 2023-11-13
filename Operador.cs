    internal abstract class Operador {
    string UID;
    Estado estadoOperador;
    //public double bateria;
    Bateria bateria;
    double cargaMaxima;
    double cargaActual;
    double velocidad;
    Localidad localizacion;//string localizacion;

    public Operador(string UID, Estado estadoOperador, Bateria bateria, double cargaMaxima, double cargaActual, double velocidad, Localidad localizacion){
        this.UID = UID;
        this.estadoOperador = estadoOperador;
        this.bateria = bateria;
        this.bateriaActual = bateriaActual;
        this.cargaMaxima = cargaMaxima;
        this.cargaActual = cargaActual;
        this.velocidad = velocidad;
        this.localizacion = localizacion;
    }

    public string GetUID() {return UID;}

    public Localidad GetLocalidad(){return localizacion;}
    
    public Bateria GetBateria(){return bateria;}

    public double GetCarga(){return cargaActual;}

    public Estado GetEstado(){
        if(cargaActual > 0){
            estadoOperador = Estado.ENMOVIMIENTO;
        } else if(bateria > 0) {
            estadoOperador = Estado.STANDBY;
        } else { estadoOperador = Estado.APAGADO; }
        return estadoOperador;
    }

    public void MoverLocalidad(Localidad localizacionDestino){
        //bool desplazamiento = false;
        double porcentaje = cargaMaxima * 0.1;
        double cargaPorcentual = cargaActual;
        double velocidadActual = velocidad;
        while(cargaPorcentual-porcentaje < 0){
            cargaPorcentual -= porcentaje;
            velocidadActual -= velocidad * 0.05;
        }
        double kilometros = localizacion.calcularDistanciaViaje(localizacionDestino);
        double miliAmper = (kilometros / velocidadActual) * 1000; //Cada hora de consumo son 1000 miliAmperios, entonces multiplico la cantidad de horas por mil    
        bateria.ConsumirBateria(miliAmper);           //bateria -= tiempo * 1000;  
        if (bateria.getBateriaActual() > 0){ //Pregunto si tiene suficiente bateria para llegar al destino
            localizacion = localizacionDestino;
            //desplazamiento = true;
        }
        //return desplazamiento;
    }

    public void transferirCargaBateria(Operador oOperador){
        if(localizacion == oOperador.GetLocalidad()){
            double miliAmperACargar = bateria.getBateriaActual();
            oOperador.GetBateria() += miliAmperACargar();
            bateria.ConsumirBateria(bateria.getBateriaActual()); //Descargo toda la bateria
            estadoOperador = Estado.APAGADO;
        }
    }

   
    public void TransferirCarga(double cargaTransporte, Localidad localizacionDestino)
    {
        /*ver funcion de movimiento (punto 1) y agregarlo al if o antes del if*/
        if (cargaTransporte <= cargaMaxima && localizacionDestino.getNombre() != localizacion.getNombre())
        {
            this.MoverLocalidad(localizacionDestino)
            Console.WriteLine($"La carga ha sido transferida {localizacionDestino.getNombre()}");
        }
        else
        {
            Console.WriteLine("No es posible mover la carga actual");
        }
    }

    public void RecibirCarga(double cargaTransporte)
    {
        cargaActual += cargaTransporte;
    }

    void VolverCuartelTransferirCargaFisica(Cuartel cuartel, Localidad cuartelGeneral)
    {
        //double distanciaLocalizacion = CalcularDistanciaACuartel();
        this.MoverLocalidad(cuartelGeneral); //, "Cuartel General");
        if (this.cargaActual > 0)
        {
            Console.WriteLine($"Operador {this.UID} est� transfiriendo toda la carga f�sica al Cuartel General.")            
            cuartel.RecibirCarga(this);
            this.cargaActual = 0;
        }
    }
    void VolverCuartelCargarBateria(Cuartel cuartel, Localidad cuartelGeneral)
    {
        //double distanciaLocalizacion = CalcularDistanciaACuartel();
        this.MoverLocalidad(cuartelGeneral);
        bateria.CargarBateria();
        Console.WriteLine($"Cuartel General ha cargado la batería del Operador {UID}.");
        /*if (this.bateriaActual < this.bateria)
        {
            double cantidadACargar = this.bateria - this.bateriaActual;
            Cuartel.CargarBateria(this, cantidadACargar);
            this.bateriaActual = this.bateria;
        }*/
    }


    /*private double CalcularDistanciaACuartel()
    {
        return localizacionDestino;
    }*/
}

