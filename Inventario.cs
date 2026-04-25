public class Inventario
{
    private int         ID_inventario { get; set; }
    public  List<stock> Stock = new();

    public Inventario(int i) { ID_inventario = i; }

    public void crear_producto(int id, string nombre, float precio, int cant)
    {
        for (int i = 0; i < Stock.Count; i++)
        {
            if (Stock[i].Producto.ID == id || Stock[i].Producto.Nombre == nombre)
            {
                Stock[i].agregar(Stock[i].Producto, cant);
                return;
            }
        }
        var produc = new Producto(id, precio, nombre);
        var stocko = new stock(produc, cant);
        Stock.Add(stocko);
    }

    public bool comprobar(int id, int cantidad_vender)
    {
        for (int i = 0; i < Stock.Count; i++)
        {
            if (Stock[i].Producto.ID == id)
            {
                int disp = Stock[i].disponibles();
                if (disp >= cantidad_vender) return true;
                else return false;            
            }
        }
        return false;                         
    }

    public void eliminar_producto(int id)
    {
        for (int i = 0; i < Stock.Count; i++)
        {
            if (Stock[i].Producto.ID == id) { Stock.RemoveAt(i); return; }
        }
    }

    public stock ? buscar_stock(int id)
    {
        foreach (var s in Stock)
            if (s.Producto.ID == id) return s;
        return null;
    }
}
