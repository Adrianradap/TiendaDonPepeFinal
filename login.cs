public class login
{
    public List<usuario> usuarios = new();


    public usuario? IniciarSesion(string nombre, string contra)
    {
        for (int i = 0; i < usuarios.Count; i++)
        {
            if (usuarios[i].nombre == nombre && usuarios[i].VerificarContra(contra))
            {
                usuarios[i].estado = true;
                return usuarios[i];
            }
        }
        return null;
    }

    public int Buscarnombre(string nombre)
    {
        for (int i = 0; i < usuarios.Count; i++)
            if (usuarios[i].nombre == nombre) return i;
        return -1;
    }

    public void eliminar_user(int posicion, usuario usuarioActual)
    {
        if (posicion != -1 && usuarioActual.estado == true)
            usuarios.RemoveAt(posicion);
    }

    public void registrar(string nombre, string contra, bool esAdmin = false)
    {
        var user = new usuario(nombre, contra, esAdmin);
        usuarios.Add(user);
    }
}
