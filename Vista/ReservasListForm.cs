using System;
using System.Drawing;
using System.Windows.Forms;
using CineReservas.Modelo;
using CineReservas.Servicios;
using CineReservas.Utilidades;
using CineReservas.Enums;

namespace CineReservas.Vista
{
   public class ReservasListForm : Form
   {
      private ListView listView;
      private GestorReservas _gestor;

      public ReservasListForm(GestorReservas gestor)
      {
         _gestor = gestor;
         InitializeComponent();
         CargarReservas();
      }

      private void InitializeComponent()
      {
         this.BackColor = Color.FromArgb(20, 20, 20);

         var lblHeader = new Label
         {
            Text = "Todas las Reservas",
            Dock = DockStyle.Top,
            Height = 50,
            ForeColor = Color.Orange,
            Font = new Font("Segoe UI", 14, FontStyle.Bold),
            TextAlign = ContentAlignment.MiddleLeft,
            Padding = new Padding(10, 0, 0, 0)
         };

         var panelBotones = new FlowLayoutPanel
         {
            Dock = DockStyle.Bottom,
            Height = 54,
            BackColor = Color.FromArgb(30, 30, 30),
            Padding = new Padding(8)
         };

         var btnCancelar = new Button
         {
            Text = "X Cancelar Reserva",
            Width = 180,
            Height = 38,
            BackColor = Color.FromArgb(160, 40, 40),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9),
            Cursor = Cursors.Hand,
            FlatAppearance = { BorderSize = 0 }
         };
         btnCancelar.Click += BtnCancelar_Click;

         var btnRefrescar = new Button
         {
            Text = "Actualizar",
            Width = 140,
            Height = 38,
            BackColor = Color.FromArgb(50, 80, 120),
            ForeColor = Color.White,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 9),
            Cursor = Cursors.Hand,
            FlatAppearance = { BorderSize = 0 }
         };
         btnRefrescar.Click += (s, e) => CargarReservas();

         panelBotones.Controls.Add(btnCancelar);
         panelBotones.Controls.Add(btnRefrescar);

         listView = new ListView
         {
            Dock = DockStyle.Fill,
            View = View.Details,
            FullRowSelect = true,
            GridLines = false,
            BackColor = Color.FromArgb(35, 35, 35),
            ForeColor = Color.FromArgb(220, 220, 220),
            BorderStyle = BorderStyle.None,
            Font = new Font("Segoe UI", 10f)
         };

         listView.Columns.Add("Código", 150);
         listView.Columns.Add("Cliente", 160);
         listView.Columns.Add("Película", 180);
         listView.Columns.Add("Función", 140);
         listView.Columns.Add("Asientos", 80);
         listView.Columns.Add("Total", 90);
         listView.Columns.Add("Estado", 90);

         this.Controls.Add(listView);
         this.Controls.Add(panelBotones);
         this.Controls.Add(lblHeader);
      }

      private void CargarReservas()
      {
         listView.Items.Clear();
         foreach (var r in _gestor.Reservas)
         {
            var item = new ListViewItem(r.CodigoReserva);
            item.SubItems.Add(r.Cliente.GetNombreCompleto());
            item.SubItems.Add(r.Funcion.Pelicula.Titulo);
            item.SubItems.Add(Formateador.FormatearFechaHora(r.Funcion.FechaHora));

            item.SubItems.Add(r.ObtenerCodigosAsientos());

            item.SubItems.Add(Formateador.FormatearPrecio(r.PrecioFinal));
            item.SubItems.Add(r.Estado.ToString());
            item.Tag = r;

            item.ForeColor = r.Estado switch
            {
               EstadoReserva.Activa => Color.FromArgb(80, 200, 120),
               EstadoReserva.Cancelada => Color.FromArgb(180, 80, 80),
               _ => Color.FromArgb(150, 150, 150)
            };

            listView.Items.Add(item);
         }
      }

      private void BtnCancelar_Click(object sender, EventArgs e)
      {
         if (listView.SelectedItems.Count == 0)
         {
            MessageBox.Show("Seleccione una reserva.", "Atención",
                            MessageBoxButtons.OK, MessageBoxIcon.Information);
            return;
         }

         var reserva = listView.SelectedItems[0].Tag as Reserva;

         if (MessageBox.Show($"¿Cancelar la reserva {reserva.CodigoReserva}?",
                              "Confirmar", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
         {
            try
            {
               _gestor.CancelarReserva(reserva.CodigoReserva);
               CargarReservas();
               MessageBox.Show("Reserva cancelada.", "Éxito",
                               MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
               MessageBox.Show(ex.Message, "Atención",
                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
         }
      }
   }
}