using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

internal class Operadores
{
    public string UID;
    public string estado;
    public int bateria;
    public int bateriaActual;
    public double carga;
    public double cargaActual;
    public double velocidad;
    public string localizacion;
}
internal class Uav : Operadores
{
    public Uav()
    {
        bateria = 4000;
        carga = 5;
    }
}

internal class K9 : Operadores
{
    public K9()
    {
        bateria = 6500;
        carga = 40;
    }
}

internal class M8 : Operadores
{
    public M8()
    {
        bateria = 12250;
        carga = 250;
    }
}
enum Estado
{
    Encendido,
    Apagado,
    EnMovimiento
}