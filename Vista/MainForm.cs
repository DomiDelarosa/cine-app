using System;
using System.Drawing;
using System.Windows.Forms;
using CineReservas.Servicios;

namespace CineReservas.Vista
{
   public class MainForm : Form
   {
      private Panel panelMenu;
      private Panel panelMain;
      private GestorReservas _gestor;

      public MainForm(GestorReservas gestor)
      {
         _gestor = gestor;
         InitializeComponent();
      }

      private void InitializeComponent()
      {
         Text = "Cine Reservas";
         Size = new Size(1000, 700);
         StartPosition = FormStartPosition.CenterScreen;
         BackColor = Color.FromArgb(20, 20, 20);
         Font = new Font("Segoe UI", 9);

         // MENU
         panelMenu = new Panel
         {
            Dock = DockStyle.Left,
            Width = 200,
            BackColor = Color.FromArgb(40, 40, 40)
         };

         var lblTitle = new Label
         {
            Text = "CINE",
            Dock = DockStyle.Top,
            Height = 70,
            ForeColor = Color.White,
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleCenter,
            BackColor = Color.FromArgb(20, 20, 20)
         };

         var btnPeliculas = CrearBoton("Películas");
         var btnFunciones = CrearBoton("Funciones");
         var btnReserva = CrearBoton("Reservas");
         var btnLista = CrearBoton("Listado");

         btnPeliculas.Click += (s, e) => Abrir(new PeliculasForm(_gestor));
         btnFunciones.Click += (s, e) => Abrir(new FuncionesForm(_gestor));
         btnReserva.Click += (s, e) => Abrir(new ReservaForm(_gestor));
         btnLista.Click += (s, e) => Abrir(new ReservasListForm(_gestor));

         panelMenu.Controls.Add(btnLista);
         panelMenu.Controls.Add(btnReserva);
         panelMenu.Controls.Add(btnFunciones);
         panelMenu.Controls.Add(btnPeliculas);
         panelMenu.Controls.Add(lblTitle);

         panelMain = new Panel
         {
            Dock = DockStyle.Fill,
            BackColor = Color.FromArgb(30, 30, 30),
            Padding = new Padding(10)
         };

         MostrarInicio();

         Controls.Add(panelMain);
         Controls.Add(panelMenu);
      }

      private Button CrearBoton(string texto) => new Button
      {
         Text = texto,
         Dock = DockStyle.Top,
         Height = 45,
         FlatStyle = FlatStyle.Flat,
         ForeColor = Color.White,
         BackColor = Color.FromArgb(40, 40, 40),
         FlatAppearance = { BorderSize = 0 },
         Cursor = Cursors.Hand
      };

      private void Abrir(Form form)
      {
         panelMain.Controls.Clear();

         form.TopLevel = false;
         form.FormBorderStyle = FormBorderStyle.None;
         form.Dock = DockStyle.Fill;

         panelMain.Controls.Add(form);
         form.Show();
      }

      private void MostrarInicio()
      {
         var lbl = new Label
         {
            Text =
               "Sistema de reservas\n\n" +
               $"Películas: {_gestor.Peliculas.Count}\n" +
               $"Salas: {_gestor.Salas.Count}\n" +
               $"Funciones: {_gestor.GetFuncionesActivas().Count}\n" +
               $"Reservas: {_gestor.GetReservasActivas().Count}",
            Dock = DockStyle.Fill,
            ForeColor = Color.LightGray,
            Font = new Font("Segoe UI", 12),
            TextAlign = ContentAlignment.MiddleCenter
         };

         panelMain.Controls.Add(lbl);
      }
   }
}