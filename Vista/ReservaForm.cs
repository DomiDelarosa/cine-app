using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using CineReservas.Modelo;
using CineReservas.Servicios;
using CineReservas.Utilidades;
using CineReservas.Enums;

namespace CineReservas.Vista
{
    public class ReservaForm : Form
    {
        private TabControl tabControl;
        private TabPage tabPelicula, tabFuncion, tabAsiento, tabCliente, tabConfirmacion;
        private ListBox lstPeliculas;
        private Label lblSinopsis;
        private ListBox lstFunciones;
        private FlowLayoutPanel panelAsientos;
        private TextBox txtNombre, txtApellido, txtEdad, txtEmail, txtTelefono;
        private ComboBox cmbMembresia, cmbClienteExistente;
        private RadioButton rdNuevo, rdExistente;
        private RichTextBox rtbResumen;
        private Pelicula _pelicula;
        private Funcion _funcion;
        private List<Asiento> _asientosSeleccionados = new List<Asiento>();

        private Cliente _cliente;
        private GestorReservas _gestor;


        public ReservaForm(GestorReservas gestor)
        {
            _gestor = gestor;
            InitializeComponent();
            CargarPeliculas();
        }

        private void InitializeComponent()
        {
            this.BackColor = Color.FromArgb(40, 40, 40);

            var lblHeader = new Label
            {
                Text = "Nueva Reserva",
                Dock = DockStyle.Top,
                Height = 45,
                ForeColor = Color.Orange,
                Font = new Font("Segoe UI", 13, FontStyle.Bold),
                TextAlign = ContentAlignment.MiddleLeft,
                Padding = new Padding(10, 0, 0, 0)
            };

            tabControl = new TabControl
            {
                Dock = DockStyle.Fill,
                Font = new Font("Segoe UI", 10)
            };

            // Tab 1: Película
            tabPelicula = new TabPage("1. Película") { BackColor = Color.FromArgb(20, 20, 20) };

            lstPeliculas = new ListBox
            {
                Dock = DockStyle.Left,
                Width = 300,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11)
            };

            lblSinopsis = new Label
            {
                Dock = DockStyle.Fill,
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11),
                Padding = new Padding(15),
                Text = "Seleccione una película para ver su sinopsis."
            };

            lstPeliculas.SelectedIndexChanged += (s, e) =>
            {
                _pelicula = lstPeliculas.SelectedItem as Pelicula;
                if (_pelicula != null)
                    lblSinopsis.Text = _pelicula.Sinopsis + "\n\n" + _pelicula.GetInfo();
            };

            var btn1 = CrearBotonSiguiente("Continuar -> Elegir Función");
            btn1.Click += (s, e) =>
            {
                if (_pelicula == null) { MsgError("Seleccione una película."); return; }
                CargarFunciones();
                tabControl.SelectedTab = tabFuncion;
            };

            tabPelicula.Controls.Add(lblSinopsis);
            tabPelicula.Controls.Add(lstPeliculas);
            tabPelicula.Controls.Add(btn1);

            // Tab 2: Función
            tabFuncion = new TabPage("2. Función") { BackColor = Color.FromArgb(20, 20, 20) };

            lstFunciones = new ListBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                Font = new Font("Segoe UI", 11)
            };

            lstFunciones.SelectedIndexChanged += (s, e) =>
                _funcion = lstFunciones.SelectedItem as Funcion;

            var btn2 = CrearBotonSiguiente("Continuar -> Elegir Asiento");
            btn2.Click += (s, e) =>
            {
                if (_funcion == null) { MsgError("Seleccione una función."); return; }
                CargarAsientos();
                tabControl.SelectedTab = tabAsiento;
            };

            tabFuncion.Controls.Add(lstFunciones);
            tabFuncion.Controls.Add(btn2);

            // Tab 3: Asiento
            tabAsiento = new TabPage("3. Asiento") { BackColor = Color.FromArgb(20, 20, 20) };
            var lblPantalla = new Label
            {
                Text = "[ PANTALLA ]",
                Dock = DockStyle.Top,
                Height = 30,
                TextAlign = ContentAlignment.MiddleCenter,
                ForeColor = Color.Orange,
                BackColor = Color.FromArgb(40, 40, 40),
                Font = new Font("Segoe UI", 9)
            };

            panelAsientos = new FlowLayoutPanel
            {
                Dock = DockStyle.Fill,
                AutoScroll = true,
                Padding = new Padding(10),
                BackColor = Color.FromArgb(40, 40, 40)
            };

            var btn3 = CrearBotonSiguiente("Continuar -> Datos del Cliente");

            btn3.Click += (s, e) =>
            {
                if (_asientosSeleccionados == null || _asientosSeleccionados.Count == 0) { MsgError("Seleccione un asiento."); return; }
                tabControl.SelectedTab = tabCliente;
            };

            tabAsiento.Controls.Add(panelAsientos);
            tabAsiento.Controls.Add(lblPantalla);
            tabAsiento.Controls.Add(btn3);

            // Tab 4: Cliente
            tabCliente = new TabPage("4. Cliente") { BackColor = Color.FromArgb(20, 20, 20) };

            rdNuevo = new RadioButton
            {
                Text = "Nuevo cliente",
                Checked = true,
                ForeColor = Color.White,
                Location = new Point(20, 20),
                AutoSize = true
            };

            rdExistente = new RadioButton
            {
                Text = "Cliente existente",
                ForeColor = Color.White,
                Location = new Point(170, 20),
                AutoSize = true
            };

            cmbClienteExistente = new ComboBox
            {
                Location = new Point(20, 50),
                Width = 300,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Visible = false
            };

            foreach (var c in _gestor.Clientes)
                cmbClienteExistente.Items.Add(c);

            txtNombre = CrearTextBox(new Point(20, 90), "Nombre *");
            txtApellido = CrearTextBox(new Point(20, 140), "Apellido *");
            txtEdad = CrearTextBox(new Point(20, 190), "Edad *");
            txtEdad.MaxLength = 3;
            txtEmail = CrearTextBox(new Point(20, 240), "Email *");
            txtTelefono = CrearTextBox(new Point(20, 290), "Teléfono");

            cmbMembresia = new ComboBox { Location = new Point(20, 390), Width = 280, BackColor = Color.FromArgb(40, 40, 40), ForeColor = Color.White, FlatStyle = FlatStyle.Flat };
            foreach (TipoMembresia tm in Enum.GetValues(typeof(TipoMembresia)))
                cmbMembresia.Items.Add(tm);
            cmbMembresia.SelectedIndex = 0;

            rdNuevo.CheckedChanged += (s, e) =>
            {
                bool nuevo = rdNuevo.Checked;
                cmbClienteExistente.Visible = !nuevo;
                txtNombre.Visible = txtApellido.Visible = txtEdad.Visible = txtEmail.Visible = txtTelefono.Visible = cmbMembresia.Visible = nuevo;
            };

            var btn4 = CrearBotonSiguiente("Continuar -> Confirmar Reserva");
            btn4.Click += (s, e) =>
            {
                if (!ValidarCliente()) return;
                ObtenerOCrearCliente();
                GenerarResumen();
                tabControl.SelectedTab = tabConfirmacion;
            };

            tabCliente.Controls.AddRange(new Control[]
            {
                rdNuevo, rdExistente, cmbClienteExistente, 
                txtNombre, txtApellido, txtEdad, txtEmail, 
                txtTelefono, cmbMembresia, btn4
            });

            // Tab 5: Confirmación
            tabConfirmacion = new TabPage("5. Confirmar") { BackColor = Color.FromArgb(20, 20, 20) };

            rtbResumen = new RichTextBox
            {
                Dock = DockStyle.Fill,
                BackColor = Color.FromArgb(40, 40, 40),
                ForeColor = Color.FromArgb(220, 220, 220),
                Font = new Font("Segoe UI", 11),
                ReadOnly = true,
                Padding = new Padding(15)
            };

            var btnConfirmar = new Button
            {
                Text = "✅  CONFIRMAR RESERVA",
                Dock = DockStyle.Bottom,
                Height = 50,
                BackColor = Color.FromArgb(50, 160, 90),
                ForeColor = Color.White,
                FlatStyle = FlatStyle.Flat,
                Font = new Font("Segoe UI", 11, FontStyle.Bold),
                Cursor = Cursors.Hand,
                FlatAppearance = { BorderSize = 0 }
            };

            btnConfirmar.Click += BtnConfirmar_Click;
            tabConfirmacion.Controls.Add(rtbResumen);
            tabConfirmacion.Controls.Add(btnConfirmar);

            tabControl.TabPages.AddRange(new[] { tabPelicula, tabFuncion, tabAsiento, tabCliente, tabConfirmacion });
            this.Controls.Add(tabControl);
            this.Controls.Add(lblHeader);
        }

        // Helpers de UI

        private Button CrearBotonSiguiente(string texto) => new Button
        {
            Text = texto,
            Dock = DockStyle.Bottom,
            Height = 44,
            BackColor = Color.FromArgb(255, 140, 0),
            ForeColor = Color.Black,
            FlatStyle = FlatStyle.Flat,
            Font = new Font("Segoe UI", 10, FontStyle.Bold),
            Cursor = Cursors.Hand,
            FlatAppearance = { BorderSize = 0 }
        };

        private TextBox CrearTextBox(System.Drawing.Point loc, string placeholder) => new TextBox
        {
            Location = loc,
            Width = 280,
            BackColor = Color.FromArgb(50, 50, 50),
            ForeColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Font = new Font("Segoe UI", 10),
            PlaceholderText = placeholder
        };

        private void MsgError(string msg) =>
            MessageBox.Show(msg, "Atención", MessageBoxButtons.OK, MessageBoxIcon.Warning);

        // Carga de datos

        private void CargarPeliculas()
        {
            lstPeliculas.Items.Clear();

            foreach (var p in _gestor.Peliculas)
                lstPeliculas.Items.Add(p);
        }

        private void CargarFunciones()
        {
            lstFunciones.Items.Clear();

            foreach (var f in _gestor.GetFuncionesPorPelicula(_pelicula))
                lstFunciones.Items.Add(f);
        }

        private void CargarAsientos()
        {
            panelAsientos.Controls.Clear();
            _asientosSeleccionados = new List<Asiento>();
            string filaActual = "";

            foreach (var asiento in _funcion.Sala.Asientos)
            {
                if (asiento.Fila != filaActual)
                {
                    filaActual = asiento.Fila;
                    panelAsientos.Controls.Add(new Label
                    {
                        Text = $" {filaActual} ",
                        Width = 30,
                        Height = 36,
                        TextAlign = ContentAlignment.MiddleCenter,
                        ForeColor = Color.Gray,
                        Font = new Font("Segoe UI", 9)
                    });
                }

                var btn = new Button
                {
                    Width = 40,
                    Height = 36,
                    Text = asiento.Numero.ToString(),
                    Tag = asiento,
                    FlatStyle = FlatStyle.Flat,
                    Font = new Font("Segoe UI", 8),
                    Cursor = asiento.EstaDisponible() ? Cursors.Hand : Cursors.No,
                    Enabled = asiento.EstaDisponible(),
                    FlatAppearance = { BorderSize = 1 }
                };

                btn.BackColor = !asiento.EstaDisponible()
                    ? Color.FromArgb(80, 40, 40)
                    : asiento.EsPreferencial
                        ? Color.FromArgb(50, 80, 50)
                        : Color.FromArgb(40, 60, 100);

                btn.ForeColor = !asiento.EstaDisponible() ? Color.Gray
                    : asiento.EsPreferencial ? Color.LightGreen : Color.LightBlue;

                btn.Click += (s, e) =>
                {
                    if (_asientosSeleccionados.Contains(asiento))
                    {
                        _asientosSeleccionados.Remove(asiento);
                        btn.BackColor = asiento.EsPreferencial ? Color.FromArgb(50, 80, 50) : Color.FromArgb(40, 60, 100);
                    }
                    else
                    {
                        _asientosSeleccionados.Add(asiento);
                        btn.BackColor = Color.Orange;
                    }
                };

                panelAsientos.Controls.Add(btn);
            }
        }

        // Lógica de cliente 
        private bool ValidarCliente()
        {
            if (rdNuevo.Checked)
            {
                if (!Validador.EsTextoValido(txtNombre.Text) ||
                    !Validador.EsTextoValido(txtApellido.Text))
                {
                    MsgError("Nombre y Apellido son obligatorios (mínimo 2 caracteres).");
                    return false;
                }

                if (!int.TryParse(txtEdad.Text, out int edad) || edad < 1 || edad > 120)
                {
                    MsgError("Ingrese una edad válida (entre 1 y 120 años).");
                    return false;
                }

                if (!Validador.EsEmailValido(txtEmail.Text))
                {
                    MsgError("Ingrese un email válido.");
                    return false;
                }

                if (!string.IsNullOrWhiteSpace(txtTelefono.Text) &&
                    !Validador.EsTelefonoValido(txtTelefono.Text))
                {
                    MsgError("El teléfono ingresado no es válido.");
                    return false;
                }
            }

            else if (cmbClienteExistente.SelectedItem == null)
            {
                MsgError("Seleccione un cliente existente.");
                return false;
            }

            return true;
        }

        private void ObtenerOCrearCliente()
        {
            if (rdExistente.Checked)
            {
                _cliente = cmbClienteExistente.SelectedItem as Cliente;

            }
            else
            {
                var tipo = (TipoMembresia)cmbMembresia.SelectedItem;
                int edad = int.Parse(txtEdad.Text);

                _cliente = _gestor.RegistrarCliente(
                    Formateador.CapitalizarTexto(txtNombre.Text),
                    Formateador.CapitalizarTexto(txtApellido.Text),
                    edad,
                    txtEmail.Text.Trim(),
                    txtTelefono.Text.Trim(),
                    tipo);
            }
        }

        private void GenerarResumen() // -REVISAR- traer GetResumen() de Reserva.cs impide reservar por doble verificación de asientos
        {
            
            decimal descuento = _cliente.ObtenerDescuento();
            decimal precio = _funcion.CalcularPrecioConDescuento(descuento) * _asientosSeleccionados.Count;

            string codigosAsientos = "";
            for (int i = 0; i < _asientosSeleccionados.Count; i++)
            {
                codigosAsientos += _asientosSeleccionados[i].GetCodigo();
                if (i < _asientosSeleccionados.Count - 1)
                    codigosAsientos += ", ";
            }

            rtbResumen.Text =
                "════════════════════════════════\n" +
                "  RESUMEN DE RESERVA\n" +
                "════════════════════════════════\n\n" +
                $"  Cliente:      {_cliente.GetNombreCompleto()}\n" +
                $"  Membresía:    {_cliente.TipoMembresia}\n\n" +
                $"  Película:     {_funcion.Pelicula.Titulo}\n" +
                $"  Función:      {Formateador.FormatearFechaHora(_funcion.FechaHora)}\n" +
                $"  Sala:         {_funcion.Sala.Nombre}\n" +
                $"  Asientos:     {codigosAsientos}\n" +
                (_asientosSeleccionados[0].EsPreferencial ? " ★ Preferencial\n" : "\n") +
                $"\n" +
                $"  Precio base:  {Formateador.FormatearPrecio(_funcion.PrecioBase * _asientosSeleccionados.Count)}\n" +
                $"  Descuento:    {Formateador.FormatearPorcentaje(descuento)}\n" +
                $"  TOTAL:        {Formateador.FormatearPrecio(precio)}\n\n" +
                "════════════════════════════════";
        }

        private void BtnConfirmar_Click(object sender, EventArgs e)
        {
            decimal descuento = _cliente.ObtenerDescuento();
            decimal precio = _funcion.CalcularPrecioConDescuento(descuento) * _asientosSeleccionados.Count;

            string codigosAsientos = "";
            for (int i = 0; i < _asientosSeleccionados.Count; i++)
            {
                codigosAsientos += _asientosSeleccionados[i].GetCodigo();
                if (i < _asientosSeleccionados.Count - 1)
                    codigosAsientos += ", ";
            } // -REVISAR- Duplicación de codigo con GenerarResumen() para mostrar bien el precio

            try
            {
                var reserva = _gestor.CrearReserva(_cliente, _funcion, _asientosSeleccionados);

                MessageBox.Show(
                    $"✅ Reserva confirmada exitosamente.\n\n" +
                    $"Código: {reserva.CodigoReserva}\n" +
                    $"Total:  {Formateador.FormatearPrecio(precio)}",
                    "Reserva Exitosa",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Information);

                // Resetear estado
                _pelicula = null;
                _funcion = null;
                _asientosSeleccionados.Clear();
                _cliente = null;
                lstPeliculas.ClearSelected();
                txtNombre.Clear();
                txtApellido.Clear();
                txtEdad.Clear();
                txtEmail.Clear();
                txtTelefono.Clear();
                tabControl.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MsgError(ex.Message);
            }
        }
    }
}