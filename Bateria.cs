internal class Bateria{
    double bateriaMaxima, bateriaActual;
    bool bateriaDañada = false;
    public Bateria(double bateriaMaxima, double bateriaActual){
        this.bateriaActual=bateriaActual;
        this.bateriaMaxima = bateriaMaxima;
    }

    public double getBateriaActual() { return bateriaActual; }
    public void CargarBateria(){
        bateriaActual = bateriaMaxima;
    }

    public void ConsumirBateria(double miliAmper){
        if(miliAmper - bateriaActual > 0){
            bateriaActual -= miliAmper;
        }else{
            bateriaActual = 0;
        }
    }

    public void ReducirBateriaMaxima(){
        bateriaMaxima *= 0.2;
        bateriaDañada = true;
    }
}