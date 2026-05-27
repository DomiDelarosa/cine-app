namespace CineReservas.Utilidades
{
   public static class Constantes
   {
      // Descuentos por membresía
      public const decimal DescuentoEstudiante = 0.20m;
      public const decimal DescuentoVIP = 0.30m;

      // Edad mínima y máxima
      public const int EdadMinima = 1;
      public const int EdadMaxima = 120;

      // Multiplicadores de precio por tipo de sala
      public const decimal MultiplicadorIMAX = 1.5m;
      public const decimal MultiplicadorCuatrodx = 1.8m;
      public const decimal MultiplicadorVIP = 2.0m;

      // Límites de sistema
      public const int MaxReservasPorCliente = 10;
      public const int MinutosAntesCancelacion = 30;
   }
}