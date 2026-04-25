public class usuario
{
    public string nombre;
    private string Contra  { get; set; }
    public  bool   estado  { get; set; }
    public  bool   EsAdmin { get; set; }

    public usuario(string Nombre, string contra, bool esAdmin = false)
    {
        nombre  = Nombre;
        Contra  = contra;
        estado  = false;
        EsAdmin = esAdmin;
    }

    // login no puede leel
    
    public bool VerificarContra(string contra)  => Contra == contra;
    public void CambiarContra(string nueva)      => Contra = nueva;
}
