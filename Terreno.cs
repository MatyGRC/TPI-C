using System;

public class Terreno
{
    private const int TamanoTerreno = 100; // En kilómetros cuadrados
    private Localizacion[,] terreno = new Localizacion[TamanoTerreno, TamanoTerreno]; //100*100
    private Random random = new Random(); //para elegir la zona
    //limites de tipo de terreno, cuarteles 3 y reciclajes 5
    private int cuartelesActuales = 0;
    private int sitiosReciclajeActuales = 0;

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
                terreno[i, j] = GenerarLocalizacionAleatoria();
            }
        }
    }

    private Localizacion GenerarLocalizacionAleatoria()
    {
        TipoLocalizacion tipo = (TipoLocalizacion)random.Next(Enum.GetNames(typeof(TipoLocalizacion)).Length); //me da la cantidad de enums

        if (tipo == TipoLocalizacion.Cuartel)
        {
            if (cuartelesActuales < 3)
            {
                cuartelesActuales++;
                return new Localizacion(tipo);
            }
            else
            {
                return GenerarLocalizacionAleatoria(); // Intentar de nuevo si alcanzó el límite de cuarteles
            }
        }
        else if (tipo == TipoLocalizacion.SitioReciclaje)
        {
            if (sitiosReciclajeActuales < 5)
            {
                sitiosReciclajeActuales++;
                return new Localizacion(tipo);
            }
            else
            {
                return GenerarLocalizacionAleatoria(); // Intentar de nuevo si alcanzó el límite de sitios de reciclaje
            }
        }
        else
        {
            return new Localizacion(tipo);
        }
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
}
