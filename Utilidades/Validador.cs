using System;

namespace CineReservas.Utilidades
{
   public static class Validador
   {
      public static bool EsEmailValido(string email)
      {
         if (string.IsNullOrWhiteSpace(email))
            return false;

         return email.Contains('@') &&
                email.Contains('.');
      }

      public static bool EsEdadValida(int edad)
      {
         return edad >= Constantes.EdadMinima && edad <= Constantes.EdadMaxima;
      }

      public static bool EsTelefonoValido(string telefono)
      {
         if (string.IsNullOrWhiteSpace(telefono))
            return false;

         int cantidadDigitos = 0;
         foreach (char c in telefono)
         {
            if (char.IsDigit(c))
               cantidadDigitos++;
         }

         return cantidadDigitos == 10; // Nota. El telefono es estricto, solo acepta telefonos nacionales (COL), de otro modo es refactorizar con: return cantidadDigitos >= 10 && cantidadDigitos <= 15;
      }

      public static bool EsTextoValido(string texto,
                                       int minLen = 2,
                                       int maxLen = 100)
      {
         if (string.IsNullOrWhiteSpace(texto))
            return false;

         return texto.Trim().Length >= minLen && texto.Trim().Length <= maxLen;
      }

      public static bool EsFechaFutura(DateTime fecha) => fecha > DateTime.Now;

      public static bool EsPrecioValido(decimal precio) => precio > 0;
   }
}
