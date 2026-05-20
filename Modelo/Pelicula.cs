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

      public bool PuedeVerPelicula(int edad)
      {
         switch (Clasificacion)
         {
            case ClasificacionPelicula.G:
               return true; // Todas las edades

            case ClasificacionPelicula.PG:
               return edad >= 7; // Guía parental sugerida

            case ClasificacionPelicula.PG13:
               return edad >= 13;

            case ClasificacionPelicula.R:
               return edad >= 17; // Restringida

            case ClasificacionPelicula.NC17:
               return edad >= 18; // Solo adultos

            default:
               return false;
         }
      }

      public string GetEdadMinima()
      {
         switch (Clasificacion)
         {
            case ClasificacionPelicula.G: 
               return "Todas las edades";
            
            case ClasificacionPelicula.PG: 
               return "7+ años";
            
            case ClasificacionPelicula.PG13: 
               return "13+ años";
            
            case ClasificacionPelicula.R: 
               return "17+ años";
            
            case ClasificacionPelicula.NC17: 
               return "18+ años";
            
            default: 
               return "No especificado";
         }
      }

      public override string ToString() => Titulo;
   }
}