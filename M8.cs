  internal class M8 : Operador {
        public M8(string UID, Estado estadoOperador, Bateria bateria, double cargaMaxima, double cargaActual, double velocidad, Localizacion localizacionActual) 
        : base(UID, estadoOperador, bateria, cargaMaxima, cargaActual, velocidad, localizacionActual) {
            bateria = new Bateria (12250,0);
            cargaMaxima = 250;
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
        
            int coordenadaX = localizacionActual.CoordenadaX;
            int coordenadaY = localizacionActual.CoordenadaY;
            while(localizacionActual.CoordenadaX != coorX && localizacionActual.CoordenadaY != coorY){
                if(localizacionActual.CoordenadaX < coorX && terreno[coordenadaX++,coordenadaY].Tipo != TipoLocalizacion.Lago){
                      localizacionActual = terreno[coordenadaX++,coordenadaY];
                      bateria.ConsumirBateria(miliAmper);   
                      CheckearTerreno();
                }else if (terreno[coordenadaX--,coordenadaY].Tipo != TipoLocalizacion.Lago){
                  localizacionActual = terreno[coordenadaX--,coordenadaY];
                  bateria.ConsumirBateria(miliAmper);   
                  CheckearTerreno();
                }

                if(localizacionActual.CoordenadaY < coorY && terreno[coordenadaX,coordenadaY++].Tipo != TipoLocalizacion.Lago){
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
            Bateria nuevaBateria = new Bateria (12250, 0);
            bateria = nuevaBateria;
        }
    }
