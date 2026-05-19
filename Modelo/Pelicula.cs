using CineReservas.Enums;

namespace CineReservas.Modelo
{
   public class Pelicula
   {
      private static int _contadorId = 1;

      public int IdPelicula { get; private set; }
      public string Titulo { get; set; }
      public string Genero { get; set; }
      public int AñoEstreno { get; set; }
      public string Director { get; set; }
      public int DuracionMinutos { get; set; }
      public ClasificacionPelicula Clasificacion { get; set; }
      public string Sinopsis { get; set; }

      public Pelicula(string titulo, string genero, int añoEstreno, string director,
                      int duracionMinutos, ClasificacionPelicula clasificacion, string sinopsis)
      {
         IdPelicula = _contadorId++;
         Titulo = titulo;
         Genero = genero;
         AñoEstreno = añoEstreno;
         Director = director;
         DuracionMinutos = duracionMinutos;
         Clasificacion = clasificacion;
         Sinopsis = sinopsis;
      }

      public string GetInfo() => $"{Titulo} ({Clasificacion}) - Dir: {Director} - {DuracionMinutos} min - {Genero}";

      public string GetDuracionFormateada()
      {
         int h = DuracionMinutos / 60; 
         int m = DuracionMinutos % 60;
         return h > 0 ? $"{h}h {m}min" : $"{m}min"; // si h es mayor a 0, muestra horas y minutos, sino solo minutos
      }

      public override string ToString() => Titulo;
   }
}