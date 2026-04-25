public class carrito
{
    private Inventario          _inventario;
    public  List<TicketUnidad>  Tickets = new();

    public usuario comprador {get; set;}

    public carrito(Inventario inventario, usuario comp) { _inventario = inventario; comprador = comp; }

    public bool AgregarProducto(int id, int cantidad)
    {
        if (!_inventario.comprobar(id, cantidad)) return false;

        int agregados = 0;
        for (int i = 0; i < _inventario.Stock.Count && agregados < cantidad; i++)
        {
            if (_inventario.Stock[i].Producto.ID == id)
            {
                for (int j = 0; j < _inventario.Stock[i].unidades.Count && agregados < cantidad; j++)
                {
                    var ticket = _inventario.Stock[i].unidades[j];
                    if (!ticket.Vendido)
                    {
                        ticket.Vendido = true;
                        Tickets.Add(ticket);
                        agregados++;
                    }
                }
            }
        }
        return true;
    }

    public void Cancelar()
    {
        for (int i = 0; i < Tickets.Count; i++)
            Tickets[i].Vendido = false;
        Tickets.Clear();
    }

    public float total() => Tickets.Sum(p=> p.Producto.obt_precio());

    public carrito confirma_compra() { return this; }

    public void vaciar_carrito() { Tickets.Clear(); }  
}
