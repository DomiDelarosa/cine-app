using System;

namespace CineReservas.Modelo
{
   public class Empleado : Persona
   {
      private static int _contadorId = 1;

      public int IdEmpleado { get; private set; }
      public string Cargo { get; set; }
      public decimal Salario { get; set; }
      public DateTime FechaContratacion { get; private set; }

      protected Empleado(string nombre, string apellido, string email, string telefono, string cargo, decimal salario) 
               : base(nombre, apellido, email, telefono)
      {
         IdEmpleado = _contadorId++;
         Cargo = cargo;
         Salario = salario;
         FechaContratacion = DateTime.Now;
      }

      public override string GetRol() => Cargo;
   }
}