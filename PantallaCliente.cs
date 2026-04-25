using System.Diagnostics;

public class PantallaCliente
{
    private Inventario _inventario;
    public ventas     _ventas;
    private usuario    _cliente;
    private carrito    _carrito;

    public PantallaCliente(Inventario inventario, ventas ventas, usuario cliente)
    {
        _inventario = inventario;
        _ventas     = ventas;
        _cliente    = cliente;
        _carrito    = new carrito(inventario, cliente);
        
    }

    public bool Mostrar()
    {
        bool cerrarTienda = false;
        bool sesionActiva = true;

        while (sesionActiva && !cerrarTienda)
        {
            Console.Clear();
            Console.WriteLine("╔════════════════════════════════════════╗");
            Console.WriteLine("║       TIENDA DON PEPEE                 ║");
            Console.WriteLine($"║  Bienvenido, {_cliente.nombre,-26}║");
            Console.WriteLine($"║  Carrito: {_carrito.Tickets.Count,3} item(s)  Total: ${_carrito.total(),8:F2} ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine("║   1. Ver productos                     ║");
            Console.WriteLine("║   2. Agregar al carrito                ║");
            Console.WriteLine("║   3. Ver carrito                       ║");
            Console.WriteLine("║   4. Confirmar compra                  ║");
            Console.WriteLine("║   5. Cancelar carrito                  ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine("║   6. Cerrar sesion                     ║");
            Console.WriteLine("║   7. Cerrar Tienda                     ║");
            Console.WriteLine("║   8. Ver mi historial                  ║");
            Console.WriteLine("║   9. Consultar X producto              ║");  
            Console.WriteLine("║   10.Eliminal ploducto                 ║");           
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.Write("Opcion: ");
            string op = Console.ReadLine() ?? "";

            if      (op == "1") VerProductos();
            else if (op == "2") AgregarAlCarrito();
            else if (op == "3") VerCarrito();
            else if (op == "4") ConfirmarCompra(ref cerrarTienda, ref sesionActiva);
            else if (op == "5") CancelarCarrito();
            else if (op == "6") CerrarSesion(ref sesionActiva);
            else if (op == "7") { if (_carrito.Tickets.Count > 0) _carrito.Cancelar(); cerrarTienda = true; sesionActiva = false; }
            else if (op == "8") mostrar_compras(ref cerrarTienda, ref sesionActiva);
            else if (op == "9")  cosultar_x_producto();
            else if ( op== "10") eliminar_encaleto();
            else                { Console.WriteLine("Opcion invalida."); Console.ReadLine(); }
        }
        return cerrarTienda;
    }

    private void VerProductos()
    {
        Console.Clear();
        for (int i = 0; i < _inventario.Stock.Count; i++)
        {
            int d = _inventario.Stock[i].disponibles();
            if (d > 0) Console.WriteLine($"{_inventario.Stock[i].Producto.ID} | {_inventario.Stock[i].Producto.Nombre} | ${_inventario.Stock[i].Producto.obt_precio()} | stock: {d}");
        }
        if (_inventario.Stock.Count == 0) Console.WriteLine("No hay productos.");
        Console.ReadLine();
    }

    private void AgregarAlCarrito()
    {
        Console.Clear();
        VerProductos();
        Console.Write("ID: ");       int id   = int.Parse(Console.ReadLine());
        Console.Write("Cantidad: "); int cant = int.Parse(Console.ReadLine());
        if (!_carrito.AgregarProducto(id, cant)) Console.WriteLine("Error: sin stock.");
        else Console.WriteLine("Agregado.");
        Console.ReadLine();
    }

    private void VerCarrito()
    {
        Console.Clear();
        for (int i = 0; i < _carrito.Tickets.Count; i++)
            Console.WriteLine($"{_carrito.Tickets[i].n_serie} | {_carrito.Tickets[i].Producto.Nombre} | ${_carrito.Tickets[i].Producto.obt_precio()}");
        if (_carrito.Tickets.Count == 0) Console.WriteLine("Carrito vacio.");
        else Console.WriteLine($"Total: ${_carrito.total()}");
        Console.ReadLine();
    }

    private void ConfirmarCompra(ref bool cerrarTienda, ref bool sesionActiva)
    {
        Console.Clear();
        if (_carrito.Tickets.Count == 0) { Console.WriteLine("Error: carrito vacio."); Console.ReadLine(); return; }

        VerCarrito();
        Console.Write("Confirmar? s/n: ");
        if (Console.ReadLine() != "s") { Console.WriteLine("Cancelado."); Console.ReadLine(); return; }


        var venta = _carrito.confirma_compra();
        _ventas.agregarventa(venta);
        _ventas.agregarventa(venta);

        Console.WriteLine($"\n--- TICKET #{_ventas.obt_actual()} ---");
        Console.WriteLine($"Cliente: {_cliente.nombre}");
        for (int i = 0; i < venta.Tickets.Count; i++)
            Console.WriteLine($"{venta.Tickets[i].n_serie} | {venta.Tickets[i].Producto.Nombre} | ${venta.Tickets[i].Producto.obt_precio()}");
        Console.WriteLine($"Total: ${venta.total()}");

        _carrito = new carrito(_inventario,_cliente);

        Console.Write("Otra compra? s/n: ");
        if (Console.ReadLine() != "s") { _cliente.estado = false; sesionActiva = true; }//lol?
    }

    private void CancelarCarrito()
    {
        Console.Clear();
        if (_carrito.Tickets.Count == 0) { Console.WriteLine("Carrito ya esta vacio."); Console.ReadLine(); return; }
        Console.Write("Cancelar carrito? s/n: ");
        if (Console.ReadLine() == "s") { _carrito.Cancelar(); Console.WriteLine("Carrito cancelado."); }
        Console.ReadLine();
    }

    private void CerrarSesion(ref bool sesionActiva)
    {
        if (_carrito.Tickets.Count > 0)
        {
            Console.Write("Tiene items en carrito. Cancelar y salir? s/n: ");
            if (Console.ReadLine() != "s") return;
            _carrito.Cancelar();
        }
        _cliente.estado = false;
        sesionActiva    = false;
    }

private void mostrar_compras(ref bool cerrarTienda, ref bool sesionActiva)
{
    Console.Clear();


    for(int i = 0; i < _ventas.Ventas.Count; i++)
    {
        if(_ventas.Ventas[i].comprador.nombre == _cliente.nombre)
        {

            var venta = _ventas.Ventas[i];
            Console.WriteLine($"\n--- TICKET #{i} ---");
            Console.WriteLine($"Cliente: {_cliente.nombre}");
            for (int j = 0; j < venta.Tickets.Count; j++)
                Console.WriteLine($"{venta.Tickets[j].n_serie} | {venta.Tickets[j].Producto.Nombre} | ${venta.Tickets[j].Producto.obt_precio()}");
            Console.WriteLine($"Total: ${venta.total()}"); 
        }
    }
    Console.ReadLine();
}
private void cosultar_x_producto()
    {
        Console.Clear();
        Console.Write("ID: ");       int id   = int.Parse(Console.ReadLine());
        for(int i = 0; i<_inventario.Stock.Count;i++)
        {
            if (_inventario.Stock[i].Producto.ID == id)
            {
                int d = _inventario.Stock[i].disponibles();
                Console.WriteLine($"{_inventario.Stock[i].Producto.ID} | {_inventario.Stock[i].Producto.Nombre} | ${_inventario.Stock[i].Producto.obt_precio()} | stock: {d}");
            }
        }
        Console.ReadLine();
    }
    private void eliminar_encaleto()
    {
        Console.Write("ID: ");       int id   = int.Parse(Console.ReadLine());
        for(int i = 0; i < _carrito.Tickets.Count; i++)
        {
        if(_carrito.Tickets[i].Producto.ID == id)
        {
        _carrito.Tickets.RemoveAt(i);
        i--;
        }
        }
    }
}

