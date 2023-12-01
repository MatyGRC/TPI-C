using System;

public class Terreno
{
    private const int TamanoTerreno = 100; // En kilómetros cuadrados
    private Localizacion[,] terreno = new Localizacion[TamanoTerreno, TamanoTerreno]; //100*100
    private Random random = new Random(); //para elegir la zona
    //limites de tipo de terreno, cuarteles 3 y reciclajes 5
    private int cuartelesActuales = 0;
    private int sitiosReciclajeActuales = 0;

    private List<Cuartel> cuartelesEnTerreno = new List<Cuartel>();

    internal List<Cuartel> getCuarteles()
    {
        return cuartelesEnTerreno; 
    }

    public Terreno()
    {
        GenerarTerreno();
    }

    private void GenerarTerreno()
    {
        for (int i = 0; i < TamanoTerreno; i++)
        {
            for (int j = 0; j < TamanoTerreno; j++)
            {
                terreno[i, j] = GenerarLocalizacionAleatoria(i,j);
            }
        }
    }

    private Localizacion GenerarLocalizacionAleatoria(int coordenadaX, int coordenadaY)
    {
        TipoLocalizacion tipo = (TipoLocalizacion)random.Next(Enum.GetNames(typeof(TipoLocalizacion)).Length); //me da la cantidad de enums

        if (tipo == TipoLocalizacion.Cuartel)
        {
            if (cuartelesActuales < 3)
            {
                cuartelesActuales++;
                Cuartel nuevoCuartel = new Cuartel(new List<Operador>(), 0, tipo, coordenadaX, coordenadaY);

                // Agrega el cuartel a la lista de cuarteles en el terreno
                cuartelesEnTerreno.Add(nuevoCuartel);

                return nuevoCuartel;
            }
            else
            {
                return GenerarLocalizacionAleatoria(coordenadaX, coordenadaY); // Intentar de nuevo si alcanzó el límite de cuarteles
            }
        }
        else if (tipo == TipoLocalizacion.SitioReciclaje)
        {
            if (sitiosReciclajeActuales < 5)
            {
                sitiosReciclajeActuales++;
                return new Localizacion(tipo,coordenadaX,coordenadaY);
            }
            else
            {
                return GenerarLocalizacionAleatoria(coordenadaX, coordenadaY); // Intentar de nuevo si alcanzó el límite de sitios de reciclaje
            }
        }
        else
        {
            return new Localizacion(tipo,coordenadaX,coordenadaY);
        }
    }

    public void calcularVertederoCercano()
    {

    }
    public void MostrarTerreno()
    {
        for (int i = 0; i < TamanoTerreno; i++)
        {
            for (int j = 0; j < TamanoTerreno; j++)
            {
                Console.Write($"{terreno[i, j].Tipo.ToString()} ");
            }
            Console.WriteLine();
        }
    }

}
