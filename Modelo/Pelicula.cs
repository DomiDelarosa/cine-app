using CineReservas.Enums;
using CineReservas.Utilidades;

namespace CineReservas.Modelo
{
   public class Pelicula
   {
      private static int _contadorId = 1;

      public int IdPelicula { get; private set; }
      public string Titulo { get; set; }
      public string Genero { get; set; }
      public string Director { get; set; }
      public int DuracionMinutos { get; set; }
      public ClasificacionPelicula Clasificacion { get; set; }
      public string Sinopsis { get; set; }

      public Pelicula(string titulo, string genero, string director,
                      int duracionMinutos, ClasificacionPelicula clasificacion, string sinopsis)
      {
         IdPelicula = _contadorId++;
         Titulo = titulo;
         Genero = genero;
         Director = director;
         DuracionMinutos = duracionMinutos;
         Clasificacion = clasificacion;
         Sinopsis = sinopsis;
      }

      public string GetInfo() => $"{Titulo} ({Clasificacion}) - Dir: {Director} - {DuracionMinutos} min - {Genero}";

      public string GetDuracionFormateada() => Formateador.FormatearDuracion(DuracionMinutos);

      public override string ToString() => Titulo;
   }
}