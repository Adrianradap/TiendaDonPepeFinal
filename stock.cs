public class stock
{
    public Producto             Producto;
    public List<TicketUnidad>   unidades;

    public stock(Producto pro, int cantidad)
    {
        Producto = pro;
        unidades = new List<TicketUnidad>();         
        for (int i = 0; i < cantidad; i++)
            unidades.Add(new TicketUnidad(pro));
    }

    public void agregar(Producto pro, int cantidad)
    {
        for (int i = 0; i < cantidad; i++)
            unidades.Add(new TicketUnidad(pro));
    }

    public int disponibles()
    {
        int contador = 0;
        for (int i = 0; i < unidades.Count; i++)
            if (!unidades[i].Vendido) contador++;   
        return contador;
    }
}
