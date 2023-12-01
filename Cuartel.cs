internal class Cuartel : Localizacion {
      List <Operador> operadores;
      double carga;
              
      public Cuartel(List <Operador> operadores, double carga, TipoLocalizacion tipo, int x, int y): base(tipo, x, y)
      {
        this.operadores = operadores;
        this.carga = carga;
        this.Tipo = tipo;
        this.CoordenadaX = x;
        this.CoordenadaY = y;
        inicializarOperadorM8();
        inicializarOperadorK9();
        inicializarOperadorUAV();
    }

    private void inicializarOperadorM8()
    {
        string uid = "M8-1"; // primer m8
        Estado estadoOperador = Estado.STANDBY; 
        Bateria bateria = new Bateria(12250, 0);
        double cargaMaxima = 250;
        double cargaActual = 0; 
        double velocidad = 10; 
        Localizacion localizacionActual = this; // La localización inicial es el cuartel

        M8 operadorM8 = new M8(uid, estadoOperador, bateria, cargaMaxima, cargaActual, velocidad, localizacionActual);

        operadores.Add(operadorM8);
    }

    private void inicializarOperadorK9()
    {
        string uid = "K9-1"; // primer k9
        Estado estadoOperador = Estado.STANDBY;
        Bateria bateria = new Bateria(6500, 0);
        double cargaMaxima = 40;
        double cargaActual = 0;
        double velocidad = 15;
        Localizacion localizacionActual = this; // La localización inicial es el cuartel

        M8 operadorM8 = new M8(uid, estadoOperador, bateria, cargaMaxima, cargaActual, velocidad, localizacionActual);

        operadores.Add(operadorM8);
    }

    private void inicializarOperadorUAV()
    {
        string uid = "UAV-1"; // primer uav
        Estado estadoOperador = Estado.STANDBY;
        Bateria bateria = new Bateria(4000, 0);
        double cargaMaxima = 5;
        double cargaActual = 0;
        double velocidad = 20;
        Localizacion localizacionActual = this; // La localización inicial es el cuartel

        M8 operadorM8 = new M8(uid, estadoOperador, bateria, cargaMaxima, cargaActual, velocidad, localizacionActual);

        operadores.Add(operadorM8);
    }



    public void listarEstados(){
        foreach (Operador o in operadores){
            Console.WriteLine(o.GetEstado());
        }
      }

      public void listarEstadosLocalizacion(int coorX, int coorY){
        foreach (Operador o in operadores){
            if(o.GetLocalizacion().CoordenadaX == coorX && o.GetLocalizacion().CoordenadaY == coorY){
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
        oOperador.MoverTerreno(terreno,coorX,coorY);
        if (oOperador.GetLocalizacion() == terreno[coorX,coorY] && oOperador.GetEstado() != Estado.APAGADO)
        {
            Console.WriteLine($"El operador {oOperador.GetUID()} ha sido enviado a la nueva localizacion.");
        }
        else
        {
            Console.WriteLine($"No hay suficiente bater�a para enviar el operador {oOperador.GetUID()} a la localizacion destino.");
        }
    }

    public void RetornoACuartel(Operador oOperador, Localizacion[,] terreno)
    {
        oOperador.MoverTerreno(terreno, CoordenadaX, CoordenadaY);
        if (oOperador.GetLocalizacion() == this && oOperador.GetEstado() != Estado.APAGADO)
        {
            Console.WriteLine($"El operador {oOperador.GetUID()} ha retornado al cuartel.");
        }
        else
        {
            Console.WriteLine($"No hay suficiente bater�a para que el operador {oOperador.GetUID()} retorne al cuartel.");
        }
    }

    public void CambiarEstadoAStandby(Operador oOperador)
    {
        oOperador.SetEstado(Estado.STANDBY);
        Console.WriteLine($"El operador {oOperador.GetUID} ha cambiado su estado a STANDBY.");
    }


    
    /*public void CargarBateria(Operador operador, double miliAmper)
    {
        operador.GetBateria() += miliAmper;
        Console.WriteLine($"Cuartel General ha cargado {miliAmper} mAh de batería para el Operador {operador.GetUID()}.");
    }*/
    public void RecibirCarga(Operador operador)
    {
        carga += operador.GetCarga();
        Console.WriteLine($"Cuartel General ha recibido toda la carga física del Operador {operador.GetUID()}.");
    }
    

    public void agregarOperador(Operador oOperador){
        operadores.Add(oOperador);
      }

     public void removerOperador(Operador oOperador) {
      operadores.Remove(oOperador);
    }

    public void RetornarOperadoresDañados(Localizacion[,] terreno){
        foreach (Operador o in operadores){
            if(o.estadoOperador == Estado.DAÑADO){
                RetornoACuartel(o,terreno);
                o.RepararOperador();
            }
        }
    }
}
