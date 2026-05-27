using System;

namespace CineReservas.Utilidades
{
   public static class Formateador
   {
      public static string FormatearPrecio(decimal valor) => $"${valor:N2}";

      public static string FormatearFechaHora(DateTime fecha) => fecha.ToString("dd/MM/yyyy HH:mm");

      public static string FormatearDuracion(int minutos)
      {
         int h = minutos / 60;
         int m = minutos % 60;

         return h > 0 ? $"{h}h {m}min" : $"{m}min";
      }

      public static string FormatearPorcentaje(decimal fraccion) => $"{fraccion:P0}";

      public static string CapitalizarTexto(string texto)
      {
         if (string.IsNullOrWhiteSpace(texto)) 
            return texto;
         
         texto = texto.Trim().ToLower();
         return char.ToUpper(texto[0]) + texto[1..];
      }
   }
}
