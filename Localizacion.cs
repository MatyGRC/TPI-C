internal class Localizacion
{
    public TipoLocalizacion Tipo { get; set; } // se generan solos los getters y setters
    public int CoordenadaX { get; set; }
    public int CoordenadaY { get; set; }

    public Localizacion(TipoLocalizacion tipo, int x, int y)
    {
        Tipo = tipo;
        CoordenadaX = x;
        CoordenadaY = y;
    }
    public double calcularDistanciaViaje(int coordenadaXDestino, int coordenadaYDestino){
        double distancia = Math.Abs(coordenadaXDestino-CoordenadaX) + Math.Abs(coordenadaYDestino-CoordenadaY);
        return distancia;
    }
}