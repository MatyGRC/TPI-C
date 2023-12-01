internal class Localidad{
    string nombre;
    double distanciaACuartel;
 
    public Localidad(string nombre, double distanciaACuartel) {
        this.nombre = nombre;
        this.distanciaACuartel = distanciaACuartel;
    }
    public double getDistancia() {return distanciaACuartel;}

    public string getNombre() {return nombre;}

    public double calcularDistanciaViaje(Localidad oLocalidad){
        return Math.Abs(distanciaACuartel - oLocalidad.getDistancia());
    }
}