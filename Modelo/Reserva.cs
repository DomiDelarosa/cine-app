using System;
using System.Collections.Generic;
using CineReservas.Utilidades;
using CineReservas.Enums;

namespace CineReservas.Modelo
{
   public class Reserva
   {
      public string CodigoReserva { get; private set; }
      public Cliente Cliente { get; private set; }
      public Funcion Funcion { get; private set; }
      public List<Asiento> Asientos { get; private set; }
      public decimal PrecioFinal { get; private set; }
      public DateTime FechaCreacion { get; private set; }
      public EstadoReserva Estado { get; private set; }

      public Reserva(Cliente cliente, Funcion funcion, List<Asiento> asientos)
      {
         CodigoReserva = GenerarCodigo();
         Cliente = cliente;
         Funcion = funcion;
         Asientos = asientos;
         FechaCreacion = DateTime.Now;
         Estado = EstadoReserva.Activa;
         PrecioFinal = funcion.CalcularPrecioConDescuento(cliente.ObtenerDescuento()) * asientos.Count;

         foreach (var asiento in asientos)
         {
            asiento.Reservar();
         }
         cliente.AgregarReserva(this);
      }

      private static string GenerarCodigo()
      {
         return $"RSV-{DateTime.Now:yyyy-MMfffmm}";
      }

      public void Cancelar()
      {
         if (Estado != EstadoReserva.Activa)
            throw new InvalidOperationException("Solo se pueden cancelar reservas activas.");

         if ((Funcion.FechaHora - DateTime.Now).TotalMinutes < Constantes.MinutosAntesCancelacion)
            throw new InvalidOperationException($"No se puede cancelar con menos de {Constantes.MinutosAntesCancelacion} minutos de anticipación.");

         Estado = EstadoReserva.Cancelada;
         foreach (var asiento in Asientos)
            asiento.Liberar();

         Cliente.EliminarReserva(this);
      }

      public void Completar()
      {
         if (Estado != EstadoReserva.Activa)
            throw new InvalidOperationException("Solo se pueden completar reservas activas.");

         Estado = EstadoReserva.Completada;
         foreach (var asiento in Asientos)
            asiento.Ocupar();
      }

      public string GetResumen() =>
         $"Código:   {CodigoReserva}\n" +
         $"Cliente:  {Cliente.GetNombreCompleto()} ({Cliente.TipoMembresia})\n" +
         $"Película: {Funcion.Pelicula.Titulo}\n" +
         $"Función:  {Funcion.FechaHora:dd/MM/yyyy HH:mm}\n" +
         $"Sala:     {Funcion.Sala.Nombre}\n" +
         $"Asientos: {ObtenerCodigosAsientos()}\n" +
         $"Total:    ${Formateador.FormatearPrecio(PrecioFinal)}\n" +
         $" Estado: {Estado}";

      public string ObtenerCodigosAsientos()
      {
         string codigos = "";
         for (int i = 0; i < Asientos.Count; i++)
         {
            codigos += Asientos[i].GetCodigo();
            if (i < Asientos.Count - 1)
               codigos += ", ";
         }
         return codigos;
      }

      public override string ToString() => $"{CodigoReserva} — {Funcion.Pelicula.Titulo} — {ObtenerCodigosAsientos()} — {Formateador.FormatearPrecio(PrecioFinal)} [{Estado}]";
   }
}