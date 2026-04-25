public class PantallaAdmin
{
    private login      _login;
    private Inventario _inventario;
    private ventas     _ventas;
    private usuario    _admin;

    public PantallaAdmin(login login, Inventario inventario, ventas ventas, usuario admin)
    {
        _login      = login;
        _inventario = inventario;
        _ventas     = ventas;
        _admin      = admin;
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
            Console.WriteLine($"║  Bienvenido, {_admin.nombre,-26}║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine("║  ── PRODUCTOS ──                       ║");
            Console.WriteLine("║   1. Listar productos                  ║");
            Console.WriteLine("║   2. Agregar producto                  ║");
            Console.WriteLine("║   3. Actualizar producto               ║");
            Console.WriteLine("║   4. Eliminar producto                 ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine("║  ── USUARIOS ──                        ║");
            Console.WriteLine("║   5. Listar usuarios                   ║");
            Console.WriteLine("║   6. Agregar usuario                   ║");
            Console.WriteLine("║   7. Actualizar usuario                ║");
            Console.WriteLine("║   8. Eliminar usuario                  ║");
            Console.WriteLine("╠════════════════════════════════════════╣");
            Console.WriteLine("║   9. Cerrar sesión                     ║");
            Console.WriteLine("║  10. Cerrar Tienda                     ║");
            Console.WriteLine("╚════════════════════════════════════════╝");
            Console.Write("Opcion: ");
            string op = Console.ReadLine() ?? "";

            if      (op == "1")  ListarProductos();
            else if (op == "2")  AgregarProducto();
            else if (op == "3")  ActualizarProducto();
            else if (op == "4")  EliminarProducto();
            else if (op == "5")  ListarUsuarios();
            else if (op == "6")  AgregarUsuario();
            else if (op == "7")  ActualizarUsuario();
            else if (op == "8")  EliminarUsuario();
            else if (op == "9")  { _admin.estado = false; sesionActiva = false; }
            else if (op == "10") { cerrarTienda = true; sesionActiva = false; }
            else                 { Console.WriteLine("Opcion invalida."); Console.ReadLine(); }
        }
        return cerrarTienda;
    }

    private void ListarProductos()
    {
        Console.Clear();
        for (int i = 0; i < _inventario.Stock.Count; i++)
            Console.WriteLine($"{_inventario.Stock[i].Producto.ID} | {_inventario.Stock[i].Producto.Nombre} | ${_inventario.Stock[i].Producto.obt_precio()} | stock: {_inventario.Stock[i].disponibles()}");
        if (_inventario.Stock.Count == 0) Console.WriteLine("No hay productos.");
        Console.ReadLine();
    }

    private void AgregarProducto()
    {
        Console.Clear();
        Console.Write("ID: ");     int    id     = int.Parse(Console.ReadLine());
        Console.Write("Nombre: "); string nombre = Console.ReadLine();
        Console.Write("Precio: "); float  precio = float.Parse(Console.ReadLine());
        Console.Write("Stock: ");  int    cant   = int.Parse(Console.ReadLine());
        _inventario.crear_producto(id, nombre, precio, cant);
        Console.WriteLine("Producto agregado."); Console.ReadLine();
    }

    private void ActualizarProducto()
    {
        Console.Clear();
        ListarProductos();
        Console.Write("ID a actualizar: "); int id = int.Parse(Console.ReadLine());
        var s = _inventario.buscar_stock(id);
        if (s == null) { Console.WriteLine("Error: no existe."); Console.ReadLine(); return; }
        Console.Write($"Nuevo precio (actual {s.Producto.obt_precio()}): "); s.Producto.cambiar_precio(float.Parse(Console.ReadLine()));
        Console.Write("Unidades a agregar (0 = ninguna): "); int mas = int.Parse(Console.ReadLine());
        if (mas > 0) s.agregar(s.Producto, mas);
        Console.WriteLine("Actualizado."); Console.ReadLine();
    }

    private void EliminarProducto()
    {
        Console.Clear();
        ListarProductos();
        Console.Write("ID a eliminar: "); int id = int.Parse(Console.ReadLine());
        _inventario.eliminar_producto(id);
        Console.WriteLine("Eliminado."); Console.ReadLine();
    }

    private void ListarUsuarios()
    {
        Console.Clear();
        for (int i = 0; i < _login.usuarios.Count; i++)
            Console.WriteLine($"{_login.usuarios[i].nombre} | {(_login.usuarios[i].EsAdmin ? "Admin" : "Cliente")}");
        if (_login.usuarios.Count == 0) Console.WriteLine("No hay usuarios.");
        Console.ReadLine();
    }

    private void AgregarUsuario()
    {
        Console.Clear();
        Console.Write("Nombre: ");     string nombre  = Console.ReadLine();
        Console.Write("Contrasena: "); string contra  = Console.ReadLine();
        Console.Write("Admin s/n: "); bool   esAdmin = Console.ReadLine() == "s";
        _login.registrar(nombre, contra, esAdmin);
        Console.WriteLine("Usuario registrado."); Console.ReadLine();
    }

    private void ActualizarUsuario()
    {
        Console.Clear();
        Console.Write("Nombre a actualizar: "); string nombre = Console.ReadLine();
        int pos = _login.Buscarnombre(nombre);
        if (pos == -1) { Console.WriteLine("Error: no existe."); Console.ReadLine(); return; }
        Console.Write("Nueva contrasena: "); _login.usuarios[pos].CambiarContra(Console.ReadLine());
        Console.Write("Admin? s/n: ");       _login.usuarios[pos].EsAdmin = Console.ReadLine() == "s";
        Console.WriteLine("Actualizado."); Console.ReadLine();
    }

    private void EliminarUsuario()
    {
        Console.Clear();
        Console.Write("Nombre a eliminar: "); string nombre = Console.ReadLine();
        if (nombre == _admin.nombre) { Console.WriteLine("Error: no puede eliminarse a si mismo."); Console.ReadLine(); return; }
        int pos = _login.Buscarnombre(nombre);
        if (pos == -1) { Console.WriteLine("Error: no existe."); Console.ReadLine(); return; }
        _login.eliminar_user(pos, _admin);
        Console.WriteLine("Eliminado."); Console.ReadLine();
    }
}
