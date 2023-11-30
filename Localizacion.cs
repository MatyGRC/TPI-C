class Localizacion
{
    public TipoLocalizacion Tipo { get; set; } //se generan solos los getters y setters

    public Localizacion(TipoLocalizacion tipo)
    {
        Tipo = tipo;
    }
}