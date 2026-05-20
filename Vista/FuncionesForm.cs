using System.Drawing;
using System.Windows.Forms;
using CineReservas.Servicios;
using CineReservas.Utilidades;

namespace CineReservas.Vista
{

    public class FuncionesForm : Form
    {
        private ListView listView;
        private GestorReservas _gestor;

        public FuncionesForm(GestorReservas gestor)
        {
            _gestor = gestor;
            InitializeComponent();
            CargarFunciones();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(20, 20, 20);

            var lblHeader = new Label
            {
                Text = "Funciones Disponibles",
                Dock = DockStyle.Top,
                Height = 50,
                ForeColor = Color.Orange,
                Font = new Font("Segoe UI", 14, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            listView = new ListView
            {
                Dock = DockStyle.Fill,
                View = View.Details,
                FullRowSelect = true,
                GridLines = false,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.FromArgb(220, 220, 220),
                BorderStyle = BorderStyle.None,
                Font = new Font("Segoe UI", 10)
            };

            listView.Columns.Add("Película", 200);
            listView.Columns.Add("Fecha y Hora", 150);
            listView.Columns.Add("Sala", 130);
            listView.Columns.Add("Precio Base", 110);
            listView.Columns.Add("Disponibles", 100);

            this.Controls.Add(listView);
            this.Controls.Add(lblHeader);
        }

        private void CargarFunciones()
        {
            listView.Items.Clear();
            foreach (var f in _gestor.GetFuncionesActivas())
            {
                var item = new ListViewItem(f.Pelicula.Titulo);
                item.SubItems.Add(Formateador.FormatearFechaHora(f.FechaHora));
                item.SubItems.Add(f.Sala.Nombre);
                item.SubItems.Add(Formateador.FormatearPrecio(f.PrecioBase));
                item.SubItems.Add(f.LugaresDisponibles.ToString());
                item.Tag = f;

                if (f.LugaresDisponibles < 5)
                    item.ForeColor = Color.FromArgb(255, 100, 80);

                listView.Items.Add(item);
            }
        }
    }
}
