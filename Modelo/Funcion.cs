using System;
using System.Collections.Generic;
using CineReservas.Utilidades;

namespace CineReservas.Modelo
{
   public class Funcion
   {
      private static int _contadorId = 1;

      public int IdFuncion { get; private set; }
      public DateTime FechaHora { get; set; }
      public decimal PrecioBase { get; set; }
      public Pelicula Pelicula { get; set; }
      public Sala Sala { get; set; }

      public bool EstaActiva => FechaHora > DateTime.Now;
      public int LugaresDisponibles => Sala.GetCantidadDisponibles();

      public Funcion(Pelicula pelicula, Sala sala, DateTime fechaHora, decimal precioBase)
      {
         IdFuncion = _contadorId++;
         Pelicula = pelicula;
         Sala = sala;
         FechaHora = fechaHora;
         PrecioBase = precioBase;
      }

      public List<Asiento> GetLugaresDisponibles() => Sala.GetAsientosDisponibles();

      public decimal CalcularPrecioConDescuento(decimal descuento) => PrecioBase * (1 - descuento);

      public decimal CalcularPrecioSegunTipoSala()
      {
         switch (Sala.TipoSala)
         {
            case TipoSala.IMAX:
               return PrecioBase * Constantes.MultiplicadorIMAX;

            case TipoSala.Cuatrodx:
               return PrecioBase * Constantes.MultiplicadorCuatrodx;

            case TipoSala.VIP:
               return PrecioBase * Constantes.MultiplicadorVIP;

            default:
               return PrecioBase;
         }
      }

      public override string ToString() => $"{Pelicula.Titulo} — {FechaHora:dd/MM/yyyy HH:mm} | {Sala.Nombre} | ${PrecioBase:F0}";
   }
}
