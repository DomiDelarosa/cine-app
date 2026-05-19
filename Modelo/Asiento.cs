namespace CineReservas.Modelo
{
   public class Asiento
   {
      public string Fila { get; private set; }
      public int Numero { get; private set; }
      public EstadoAsiento Estado { get; set; }
      public bool EsPreferencial { get; set; }

      public Asiento(string fila, int numero, bool esPreferencial = false)
      {
         Fila = fila.ToUpper();
         Numero = numero;
         Estado = EstadoAsiento.Disponible;
         EsPreferencial = esPreferencial;
      }

      public string GetCodigo() => $"{Fila}{Numero}";
      public bool EstaDisponible() => Estado == EstadoAsiento.Disponible;

      public void Reservar() => Estado = EstadoAsiento.Reservado;
      public void Ocupar() => Estado = EstadoAsiento.Ocupado;
      public void Liberar() => Estado = EstadoAsiento.Disponible;

      public override string ToString() => $"Asiento {GetCodigo()} [{Estado}]{(EsPreferencial ? " ★" : "")}";
   }
}