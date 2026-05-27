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

      public bool EstaActiva => Validador.EsFechaFutura(FechaHora);
      public int LugaresDisponibles => Sala.GetCantidadDisponibles();

      public Funcion(Pelicula pelicula, Sala sala, DateTime fechaHora, decimal precioBase)
      {
         if (!Validador.EsPrecioValido(precioBase))
            throw new ArgumentException("El precio base debe ser mayor a cero.");
            
         IdFuncion = _contadorId++;
         Pelicula = pelicula;
         Sala = sala;
         FechaHora = fechaHora;
         PrecioBase = precioBase;
      }

      public List<Asiento> GetLugaresDisponibles() => Sala.GetAsientosDisponibles();

      public decimal CalcularPrecioConDescuento(decimal descuento) => PrecioBase * (1 - descuento);

      public decimal CalcularPrecioSegunTipoSala() => Sala.TipoSala switch
      {
         TipoSala.IMAX => PrecioBase * Constantes.MultiplicadorIMAX,
         TipoSala.Cuatrodx => PrecioBase * Constantes.MultiplicadorCuatrodx,
         TipoSala.VIP => PrecioBase * Constantes.MultiplicadorVIP,
         _ => PrecioBase
      };

      public override string ToString() => $"{Pelicula.Titulo} — {Formateador.FormatearFechaHora(FechaHora)} | {Sala.Nombre} | {Formateador.FormatearPrecio(PrecioBase)}";
   }
}
