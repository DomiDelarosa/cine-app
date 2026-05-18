using System;
using CineReservas.Enums;

namespace CineReservas.Modelo
{
   public class Cliente : Persona
   {
      private static int _contadorId = 1;

      public int IdCliente { get; private set; }
      public DateTime FechaRegistro { get; private set; }
      public TipoMembresia TipoMembresia { get; set; }
      protected Cliente(string nombre, string apellido, string email, string telefono, TipoMembresia tipoMembresia) 
               : base(nombre, apellido, email, telefono)
      {
         IdCliente = _contadorId++;
         FechaRegistro = DateTime.Now;
         TipoMembresia = tipoMembresia;
      }

      public override string GetRol() => "Cliente";
      
      public decimal ObtenerDescuento()
      {
         switch (TipoMembresia)
         {
            case TipoMembresia.Estudiante:
               return 0.20m;

            case TipoMembresia.VIP:
               return 0.30m;

            default:
               return 0.00m;
         }
      }

      public override string ToString() => $"[{IdCliente}] {GetNombreCompleto()} - {TipoMembresia}";
   }
}  