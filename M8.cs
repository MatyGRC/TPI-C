  internal class M8 : Operador {
        public M8() {
            bateria = new Bateria (12250,0);
            carga = 250;
        }

        public override void MoverTerreno(Localizacion[,] terreno, int coorX, int coorY){

        }
        
        public override void CambiarBateria(){
            Bateria nuevaBateria = new Bateria (12250, 0);
            bateria = nuevaBateria;
        }
    }
