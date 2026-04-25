public class Producto
{
    public string Nombre { get; private set; }
    public int ID { get; private set; }
    private float Precio;

    public Producto(int id, float precio, string nombre)
    {
        ID     = id;
        Nombre = nombre;
        Precio = precio;
    }

    public float obt_precio() => Precio;
    public void  cambiar_precio(float p)  => Precio = p;
}
