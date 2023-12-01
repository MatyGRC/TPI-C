  internal class K9 : Operador{
    public K9(string UID, Estado estadoOperador, Bateria bateria, double cargaMaxima, double cargaActual, double velocidad, Localizacion localizacionActual)
       : base(UID, estadoOperador, bateria, cargaMaxima, cargaActual, velocidad, localizacionActual)
    {
        this.bateria = new Bateria(6500, 0);
        this.cargaMaxima = 40;
    }

    public override void MoverTerreno(Localizacion[,] terreno, int coorX, int coorY){
           double porcentaje = cargaMaxima * 0.1;
           double cargaPorcentual = cargaActual;
            if(dañoOperador == Daño.MOTOR_COMPROMETIDO) {
            velocidad = velocidad/2;
            }
            double velocidadActual = velocidad;
            while(cargaPorcentual-porcentaje < 0){
                cargaPorcentual -= porcentaje;
                velocidadActual -= velocidad * 0.05;
             }
            double kilometros = localizacionActual.calcularDistanciaViaje(coorX,coorY);
            double miliAmper = (kilometros / velocidadActual) * 1000; //Cada hora de consumo son 1000 miliAmperios, entonces multiplico la cantidad de horas por mil    
            double bateriaNecesaria = miliAmper * kilometros;
            if(bateria.getBateriaActual() < bateriaNecesaria){
              Console.WriteLine("No es posible llegar al destino");         
            }
        
            int coordenadaX = localizacionActual.coordenadaX;
            int coordenadaY = localizacionActual.coordenadaY;
            while(localizacionActual.coordenadaX != coorX && localizacionActual.coordenadaY != coorY){
                if(localizacionActual.coordenadaX < coorX && terreno[coordenadaX++,coordenadaY].Tipo != TipoLocalizacion.Lago){
                      localizacionActual = terreno[coordenadaX++,coordenadaY];
                      bateria.ConsumirBateria(miliAmper);   
                      CheckearTerreno();
                }else if (terreno[coordenadaX--,coordenadaY].Tipo != TipoLocalizacion.Lago){
                  localizacionActual = terreno[coordenadaX--,coordenadaY];
                  bateria.ConsumirBateria(miliAmper);   
                  CheckearTerreno();
                }

                if(localizacionActual.coordenadaY < coorY && terreno[coordenadaX,coordenadaY++].Tipo != TipoLocalizacion.Lago){
                    localizacionActual = terreno[coordenadaX,coordenadaY++];
                    bateria.ConsumirBateria(miliAmper);   
                    CheckearTerreno();
                }else if(terreno[coordenadaX,coordenadaY--].Tipo != TipoLocalizacion.Lago){
                    localizacionActual = terreno[coordenadaX,coordenadaY--];
                    bateria.ConsumirBateria(miliAmper);   
                    CheckearTerreno();
                }

                //Puede ser que en direcciones verticales y horizontales tenga lagos, asi que consulto las diagonales
                //Me disculpo por este IF
                if(terreno[coordenadaX++,coordenadaY].Tipo == TipoLocalizacion.Lago &&
                terreno[coordenadaX--,coordenadaY].Tipo == TipoLocalizacion.Lago &&
                terreno[coordenadaX,coordenadaY++].Tipo == TipoLocalizacion.Lago &&
                terreno[coordenadaX,coordenadaY--].Tipo == TipoLocalizacion.Lago
                ){
                  if(terreno[coordenadaX++,coordenadaY++].Tipo != TipoLocalizacion.Lago){
                    localizacionActual = terreno[coordenadaX++,coordenadaY++];
                    bateria.ConsumirBateria(miliAmper);   
                    CheckearTerreno();
                  }else if(terreno[coordenadaX++,coordenadaY--].Tipo != TipoLocalizacion.Lago){
                    localizacionActual = terreno[coordenadaX++,coordenadaY--];
                    bateria.ConsumirBateria(miliAmper);   
                    CheckearTerreno();
                  }
                  else if(terreno[coordenadaX--,coordenadaY++].Tipo != TipoLocalizacion.Lago){
                    localizacionActual = terreno[coordenadaX++,coordenadaY--];
                    bateria.ConsumirBateria(miliAmper);   
                    CheckearTerreno();
                  }
                  else{
                    localizacionActual = terreno[coordenadaX--,coordenadaY--];
                    bateria.ConsumirBateria(miliAmper);   
                    CheckearTerreno();
                  }
                }
                
            }
    }
    public override void CambiarBateria(){
        Bateria nuevaBateria = new Bateria (6500, 0);
        bateria = nuevaBateria;
    }
  }