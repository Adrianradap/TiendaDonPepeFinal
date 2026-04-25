public class ventas
{
    public List<carrito> Ventas = new();

    public ventas() { }

    public void agregarventa(carrito caleto) { Ventas.Add(caleto); }
    public int  obt_actual()                 { return Ventas.Count; }
}
