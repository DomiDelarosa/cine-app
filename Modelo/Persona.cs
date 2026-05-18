namespace CineReservas.Modelo
{
   public abstract class Persona
   {
      public string Nombre { get; set; }
      public string Apellido { get; set; }
      public int Edad { get; set; }
      public string Email { get; set; }
      public string Telefono { get; set; }

      protected Persona(string nombre, string apellido, string email, string telefono)
      {
         Nombre = nombre;
         Apellido = apellido;
         Email = email;
         Telefono = telefono;
      }

      public string GetNombreCompleto() => $"{Nombre} {Apellido}";

      public abstract string GetRol();

      public override string ToString() => GetNombreCompleto();
   }
}
