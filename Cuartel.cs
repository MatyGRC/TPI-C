using TPI;
internal class Cuartel {
      List <Operador> operadores;

      public Cuartel(List <Operador> operadores){
        this.operadores = operadores;
      }

      public void agregarOperador(Operador oOperador){
        operadores.Add(oOperador);
      }

      public void listarEstados(){
        foreach (Operador o in operadores){
            Console.WriteLine(o.GetEstado()); //No sé si deberia usar un console acá
        }
      }

      public void listarEstadosLocalizacion(string localizacion){
        foreach (Operador o in operadores){
            if(o.GetLocalidad() == localizacion){
                Console.WriteLine(o.GetEstado());
            }
        }
      }

    public Operador SeleccionarOperadorPorUID(string uid)
    {
        Operador operadorSeleccionado = operadores.Find(o => o.UID == uid);

        if (operadorSeleccionado == null)
        {
            Console.WriteLine($"No se encontró un operador con el UID {uid} en el cuartel.");
        }

        return operadorSeleccionado;
    }

    void RecibirCarga(Operador operador)
    {
        Console.WriteLine($"Cuartel General ha recibido toda la carga física del Operador {operador.UID}.");
    }

    public void CargarBateria(Operador operador, double miliAmper)
    {
        operador.GetBateria() += miliAmper;
        Console.WriteLine($"Cuartel General ha cargado {miliAmper} mAh de batería para el Operador {operador.UID}.");
    }

}
