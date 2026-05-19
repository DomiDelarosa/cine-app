using System;

namespace CineReservas.Modelo
{
   public class Reserva
   {
      public string CodigoReserva { get; private set; }
      public Cliente Cliente { get; private set; }
      public Funcion Funcion { get; private set; }
      public Asiento Asiento { get; private set; }
      public decimal PrecioFinal { get; private set; }
      public DateTime FechaCreacion { get; private set; }
      public EstadoReserva Estado { get; private set; }

      public Reserva(Cliente cliente, Funcion funcion, Asiento asiento)
      {
         CodigoReserva = GenerarCodigo();
         Cliente = cliente;
         Funcion = funcion;
         Asiento = asiento;
         FechaCreacion = DateTime.Now;
         Estado = EstadoReserva.Activa;
         PrecioFinal = funcion.CalcularPrecioConDescuento(cliente.ObtenerDescuento());

         asiento.Reservar();
         cliente.AgregarReserva(this);
      }

      private static string GenerarCodigo()
      {
         return $"RSV-{DateTime.Now:yyyy-MMfffmm}";
      }

      public void Cancelar()
      {
         if (Estado != EstadoReserva.Activa)
         {
            throw new InvalidOperationException("Solo se pueden cancelar reservas activas.");
         }
         Estado = EstadoReserva.Cancelada;
         Asiento.Liberar();
         Cliente.EliminarReserva(this);
      }

      public void Completar()
      {
         if (Estado == EstadoReserva.Activa)
         {
            Estado = EstadoReserva.Completada;
         }
      }

      public string GetResumen() =>
         $"Código:   {CodigoReserva}\n" +
         $"Cliente:  {Cliente.GetNombreCompleto()} ({Cliente.TipoMembresia})\n" +
         $"Película: {Funcion.Pelicula.Titulo}\n" +
         $"Función:  {Funcion.FechaHora:dd/MM/yyyy HH:mm}\n" +
         $"Sala:     {Funcion.Sala.Nombre} | Asiento: {Asiento.GetCodigo()}\n" +
         $"Total:    ${PrecioFinal:F0} | Estado: {Estado}";

      public override string ToString() => $"{CodigoReserva} — {Funcion.Pelicula.Titulo} — {Asiento.GetCodigo()} — ${PrecioFinal:F0} [{Estado}]";
   }
}
