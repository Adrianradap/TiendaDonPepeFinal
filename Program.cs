using System;
using System.Text;

class Program
{
    static void Main(string[] args)
    {
        Console.OutputEncoding = Encoding.UTF8;

        var inventario = new Inventario(1);
        var loginSvc   = new login();
        var ventasSvc  = new ventas();

        loginSvc.registrar("admin", "admin123", esAdmin: true);
        loginSvc.registrar("eito", "123", esAdmin: false);

        inventario.crear_producto(1, "Hielo",                  6,  5);
        inventario.crear_producto(2, "Limon",                  5,  3);
        inventario.crear_producto(3, "Sky",                   60,  7);
        inventario.crear_producto(4, "Ron con Agua para eito",70, 10);

        bool tiendaAbierta = true;

        while (tiendaAbierta)
        {
            Console.Clear();
            Console.WriteLine("╔══════════════════════════════════════════╗");
            Console.WriteLine("║       BIENVENIDO A LA TIENDA DON PEPE    ║");
            Console.WriteLine("╠══════════════════════════════════════════╣");
            Console.WriteLine("║  Ingrese sus credenciales para entrar    ║");
            Console.WriteLine("╚══════════════════════════════════════════╝");
            Console.Write("\n  Usuario    : ");
            string nombre = Console.ReadLine() ?? "";
            Console.Write("  Contraseña : ");
            string contra = Console.ReadLine() ?? "";

            var usuarioActual = loginSvc.IniciarSesion(nombre, contra);

            if (usuarioActual == null)
            {
                Console.WriteLine("Credenciales incorrectas. Intente de nuevo.");
                Console.WriteLine("  Presione Enter");
                Console.ReadLine();
                continue;
            }

            bool cerrar;
            if (usuarioActual.EsAdmin)
            {
                var pantalla = new PantallaAdmin(loginSvc, inventario, ventasSvc, usuarioActual);
                cerrar = pantalla.Mostrar();
            }
            else
            {
                var pantalla = new PantallaCliente(inventario, ventasSvc, usuarioActual);
                cerrar = pantalla.Mostrar();
            }

            if (cerrar) tiendaAbierta = false;
        }


        Console.Clear();
        Console.WriteLine("╔══════════════════════════════════════════╗");
        Console.WriteLine("║       Gracias por usar                   ║");
        Console.WriteLine("║             ¡Hasta pronto!               ║");
        Console.WriteLine("╚══════════════════════════════════════════╝");
        Console.WriteLine();
    }
}
