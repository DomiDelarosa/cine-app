using System;
using System.Collections.Generic;
using CineReservas.Modelo;
using CineReservas.Enums;

namespace CineReservas.Servicios
{
   public class GestorReservas
   {
      public List<Pelicula> Peliculas { get; private set; }
      public List<Sala> Salas { get; private set; }
      public List<Funcion> Funciones { get; private set; }
      public List<Reserva> Reservas { get; private set; }
      public List<Cliente> Clientes { get; private set; }
      public List<Empleado> Empleados { get; private set; }

      public GestorReservas()
      {
         Peliculas = new List<Pelicula>();
         Salas = new List<Sala>();
         Funciones = new List<Funcion>();
         Reservas = new List<Reserva>();
         Clientes = new List<Cliente>();
         Empleados = new List<Empleado>();

         CargarDatosSemilla();
      }

      // Datos semilla

      private void CargarDatosSemilla()
      {
         // Películas
         Peliculas.Add(new Pelicula(
            "Dune: Parte Dos", "Ciencia Ficción", "Denis Villeneuve", 166,
            ClasificacionPelicula.PG13,
            "Paul Atreides se une a los Fremen para vengar a su familia."));

         Peliculas.Add(new Pelicula(
            "Oppenheimer", "Drama / Historia", "Christopher Nolan", 180,
            ClasificacionPelicula.R,
            "La historia del padre de la bomba atómica."));

         Peliculas.Add(new Pelicula(
            "El Reino del Planeta de los Simios", "Aventura / Ciencia Ficción", "Wes Ball", 145,
            ClasificacionPelicula.PG13,
            "Un nuevo capítulo en el universo de los simios."));

         // Salas
         Salas.Add(new Sala("Sala 1", TipoSala.Estandar, 8, 10));
         Salas.Add(new Sala("Sala IMAX", TipoSala.IMAX, 10, 12));
         Salas.Add(new Sala("Sala VIP", TipoSala.VIP, 5, 8));

         // Funciones
         DateTime ahora = DateTime.Now;
         Funciones.Add(new Funcion(Peliculas[0], Salas[0], ahora.AddHours(2), 18000m));
         Funciones.Add(new Funcion(Peliculas[0], Salas[1], ahora.AddHours(5), 27000m));
         Funciones.Add(new Funcion(Peliculas[1], Salas[0], ahora.AddHours(3), 18000m));
         Funciones.Add(new Funcion(Peliculas[1], Salas[2], ahora.AddHours(6), 36000m));
         Funciones.Add(new Funcion(Peliculas[2], Salas[0], ahora.AddHours(1), 18000m));
         Funciones.Add(new Funcion(Peliculas[2], Salas[1], ahora.AddDays(1), 27000m));

         // Clientes 
         Clientes.Add(new Cliente("Ana", "García", 19, "ana@mail.com", "3001234567", TipoMembresia.VIP));
         Clientes.Add(new Cliente("Juan", "Pérez", 17, "juan@mail.com", "3109876543", TipoMembresia.Estudiante));

         // Empleado
         Empleados.Add(new Empleado("Carlos", "López", 31, "carlos@cine.com", "3200000001", "Taquillero", 2500000m));
      }

      // Reservas

      public Reserva CrearReserva(Cliente cliente, Funcion funcion, List<Asiento> asientos)
      {
         foreach (Asiento a in asientos)
         {
            if (!a.EstaDisponible())
               throw new InvalidOperationException($"El asiento {a.GetCodigo()} no está disponible.");
         }

         if (!funcion.Pelicula.PuedeVerPelicula(cliente.Edad))
         {
            throw new InvalidOperationException($"El cliente debe tener al menos {funcion.Pelicula.GetEdadMinima()} para ver '{funcion.Pelicula.Titulo}' ({funcion.Pelicula.Clasificacion}).");
         }

         Reserva reserva = new Reserva(cliente, funcion, asientos);
         Reservas.Add(reserva);
         return reserva;
      }

      public bool CancelarReserva(string codigoReserva)
      {
         Reserva reserva = BuscarReservaPorCodigo(codigoReserva);
         if (reserva == null) return false;
         reserva.Cancelar();
         return true;
      }

      public Reserva BuscarReservaPorCodigo(string codigo)
      {
         foreach (Reserva r in Reservas)
         {
            if (r.CodigoReserva == codigo)
               return r;
         }
         return null;
      }

      // Clientes

      public Cliente RegistrarCliente(string nombre, string apellido, int edad, string email,
                                      string telefono, TipoMembresia tipo)
      {
         Cliente cliente = new Cliente(nombre, apellido, edad, email, telefono, tipo);
         Clientes.Add(cliente);
         return cliente;
      }

      // Consultas

      public List<Funcion> GetFuncionesPorPelicula(Pelicula pelicula)
      {
         List<Funcion> resultado = new List<Funcion>();
         foreach (Funcion f in Funciones)
         {
            if (f.Pelicula == pelicula && f.EstaActiva)
               resultado.Add(f);
         }
         return resultado;
      }

      public List<Funcion> GetFuncionesActivas()
      {
         List<Funcion> resultado = new List<Funcion>();
         foreach (Funcion f in Funciones)
         {
            if (f.EstaActiva)
               resultado.Add(f);
         }
         return resultado;
      }

      public List<Reserva> GetReservasActivas()
      {
         List<Reserva> resultado = new List<Reserva>();
         foreach (Reserva r in Reservas)
         {
            if (r.Estado == EstadoReserva.Activa)
               resultado.Add(r);
         }
         return resultado;
      }
   }
}