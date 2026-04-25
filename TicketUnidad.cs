public class TicketUnidad
{
    public static int serie = 0;
    public int       n_serie;
    public Producto  Producto;
    public bool      Vendido { get; set; } 

    public TicketUnidad(Producto producto)
    {
        n_serie  = ++serie;
        Producto = producto;
        Vendido  = false;
    }
}
