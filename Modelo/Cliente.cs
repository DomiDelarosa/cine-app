using System;
using System.Collections.Generic;
using CineReservas.Enums;
using CineReservas.Utilidades;

namespace CineReservas.Modelo
{
   public class Cliente : Persona
   {
      private static int _contadorId = 1;

      public int IdCliente { get; private set; }
      public DateTime FechaRegistro { get; private set; }
      public TipoMembresia TipoMembresia { get; set; }
      public List<Reserva> Reservas { get; private set; }
      
      public Cliente(string nombre, string apellido, int edad, string email, string telefono, TipoMembresia tipoMembresia) 
               : base(nombre, apellido, edad, email, telefono)
      {
         IdCliente = _contadorId++;
         FechaRegistro = DateTime.Now;
         TipoMembresia = tipoMembresia;
         Reservas = new List<Reserva>();
      }

      public override string GetRol() => "Cliente";
      
      public decimal ObtenerDescuento() => TipoMembresia switch
      {
         TipoMembresia.Estudiante => Constantes.DescuentoEstudiante,
         TipoMembresia.VIP => Constantes.DescuentoVIP,
         _ => 0.00m
      };

      public void AgregarReserva(Reserva reserva)  => Reservas.Add(reserva);
      public void EliminarReserva(Reserva reserva) => Reservas.Remove(reserva);

      public override string ToString() => $"[{IdCliente}] {GetNombreCompleto()} - {TipoMembresia}";
   }
}  