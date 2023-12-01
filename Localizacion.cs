class Localizacion
{
    public TipoLocalizacion Tipo { get; set; } //se generan solos los getters y setters
    int coordenadaX { get; set; }
    int coordenadaY {get; set;}
    public Localizacion(TipoLocalizacion tipo, int X, int Y)
    {
        Tipo = tipo;
        coordenadaX = X;
        coordenadaY = Y;
    }
    public double calcularDistanciaViaje(int coordenadaXDestino, int coordenadaYDestino){
        double distancia = Math.Abs(coordenadaXDestino-coordenadaX) + Math.Abs(coordenadaYDestino-coordenadaY);
        return distancia;
    }
}