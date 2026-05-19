using System.Collections.Generic;

namespace CineReservas.Modelo
{
   public class Sala
   {
      private static int _contadorId = 1;

      public int IdSala { get; private set; }
      public string Nombre { get; set; }
      public TipoSala TipoSala { get; set; }
      public List<Asiento> Asientos { get; private set; }

      public int Capacidad => Asientos.Count;

      public Sala(string nombre, TipoSala tipoSala, int filas, int columnas)
      {
         IdSala = _contadorId++;
         Nombre = nombre;
         TipoSala = tipoSala;
         Asientos = new List<Asiento>();
         GenerarAsientos(filas, columnas);
      }

      private void GenerarAsientos(int filas, int columnas)
      {
         const string letras = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
         for (int f = 0; f < filas && f < letras.Length; f++)
            for (int c = 1; c <= columnas; c++)
               Asientos.Add(new Asiento(letras[f].ToString(), c, esPreferencial: f == filas - 1));
      }
      
      public List<Asiento> GetAsientosDisponibles()
      {
         List<Asiento> disponibles = new List<Asiento>();

         foreach (Asiento asiento in Asientos)
         {
            if (asiento.EstaDisponible())
               disponibles.Add(asiento);
         }

         return disponibles;
      }

      public int GetCantidadDisponibles()
      {
         int cantidad = 0;

         foreach (Asiento asiento in Asientos)
         {
            if (asiento.EstaDisponible())
               cantidad++;
         }
         return cantidad;
      }

      public Asiento GetAsiento(string fila, int numero)
      {
         foreach (Asiento asiento in Asientos)
         {
            if (asiento.Fila == fila.ToUpper() &&
               asiento.Numero == numero)
            {
               return asiento;
            }
         }
         return null;
      }

      public override string ToString() => $"{Nombre} ({TipoSala}) — {Capacidad} asientos";
   }
}
