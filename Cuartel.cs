using TPI;
internal class Cuartel {
      List <Operador> operadores;
      Localidad cuartelGeneral;
      double carga;
      public Cuartel(List <Operador> operadores, double carga){
        this.operadores = operadores;
        this.carga = carga;
        cuartelGeneral = new Localidad ("CuartelGeneral" ,0);
      }

      public void listarEstados(){
        foreach (Operador o in operadores){
            Console.WriteLine(o.GetEstado()); //No sé si deberia usar un console acá
        }
      }

      public void listarEstadosLocalizacion(string localizacion){
        foreach (Operador o in operadores){
            if(o.GetLocalidad().getNombre() == localizacion){
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
    public void EnviarALocalizacion(Operador oOperador, Localidad nuevaUbicacion)
    {
        oOperador.MoverLocalidad(nuevaUbicacion);
        if (oOperador.GetLocalidad() == nuevaUbicacion && oOperador.GetEstado() != Estado.APAGADO)
        {
            Console.WriteLine($"El operador {oOperador.GetUID()} ha sido enviado a la ubicaci�n {nuevaUbicacion.getNombre()}.");
        }
        else
        {
            Console.WriteLine($"No hay suficiente bater�a para enviar el operador {oOperador.GetUID()} a la ubicaci�n {nuevaUbicacion.getNombre()}.");
        }
    }

    public void RetornoACuartel(Operador oOperador)
    {
        oOperador.MoverLocalidad(this.cuartelGeneral);
        if (oOperador.GetLocalidad() == cuartelGeneral && oOperador.GetEstado() != Estado.APAGADO)
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
        oOperador.GetEstado() = Estado.STANDBY;
        Console.WriteLine($"El operador {UID} ha cambiado su estado a STANDBY.");
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
}
