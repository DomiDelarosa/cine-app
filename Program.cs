using System.Windows.Forms;
using CineReservas.Vista;
using CineReservas.Servicios;

Application.EnableVisualStyles();
Application.SetCompatibleTextRenderingDefault(false);
GestorReservas gestor = new GestorReservas();
Application.Run(new MainForm(gestor));