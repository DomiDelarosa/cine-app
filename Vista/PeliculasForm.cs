using System.Drawing;
using System.Windows.Forms;
using CineReservas.Servicios;
using CineReservas.Utilidades;

namespace CineReservas.Vista
{
    public class PeliculasForm : Form
    {
        private ListView listView;
        private GestorReservas _gestor;

        public PeliculasForm(GestorReservas gestor)
        {
            _gestor = gestor;
            InitializeComponent();
            CargarPeliculas();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(20, 20, 20);

            var lblHeader = new Label
            {
                Text = "Películas en Cartelera",
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

            listView.Columns.Add("Título", 220);
            listView.Columns.Add("Director", 160);
            listView.Columns.Add("Género", 150);
            listView.Columns.Add("Duración", 90);
            listView.Columns.Add("Clasificación", 110);

            this.Controls.Add(listView);
            this.Controls.Add(lblHeader);
        }

        private void CargarPeliculas()
        {
            listView.Items.Clear();
            
            foreach (var p in _gestor.Peliculas)
            {
                var item = new ListViewItem(p.Titulo);
                item.SubItems.Add(p.Director);
                item.SubItems.Add(p.Genero);
                item.SubItems.Add(Formateador.FormatearDuracion(p.DuracionMinutos));
                item.SubItems.Add(p.Clasificacion.ToString());
                item.Tag = p;
                listView.Items.Add(item);
            }
        }
    }
}
