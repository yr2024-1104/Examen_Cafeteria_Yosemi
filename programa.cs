using System;
using System.Globalization;
using System.Windows.Forms;

namespace Examen_Cafeteria_Yosemi
{
    public partial class frmPedidos : Form
    {
        private decimal subtotal = 0m;

        private const decimal AUMENTO_GRANDE = 25m;
        private const decimal AUMENTO_EXTRA = 30m;
        private const decimal COSTO_EMPAQUE = 20m;
        private const decimal PORCENTAJE_DESCUENTO = 0.10m;
        private const decimal PORCENTAJE_ITBIS = 0.18m;

        private readonly CultureInfo culturaRD =
            CultureInfo.GetCultureInfo("es-DO");

        public frmPedidos()
        {
            InitializeComponent();

            Load += frmPedidos_Load;
            btnAgregar.Click += btnAgregar_Click;
            btnCalcular.Click += btnCalcular_Click;
            btnLimpiar.Click += btnLimpiar_Click;
            btnSalir.Click += btnSalir_Click;
        }

        // EVENTO LOAD
        private void frmPedidos_Load(object sender, EventArgs e)
        {
            lblEncabezado.Text =
                "Cant".PadRight(6) +
                "Producto".PadRight(24) +
                "Precio".PadLeft(10) +
                "Total".PadLeft(12);

            dtpFecha.Value = DateTime.Today;
            nudCantidad.Minimum = 1;
            nudCantidad.Value = 1;
            rbtNormal.Checked = true;
            rbtLocal.Checked = true;

            MostrarResultadosEnCero();
        }

        // EVENTO AGREGAR AL PEDIDO
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtProducto.Text))
            {
                MessageBox.Show("Digite el nombre del producto.",
                    "Dato requerido", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtProducto.Focus();
                return;
            }

            decimal precio;
            string precioTexto = txtPrecio.Text.Trim().Replace(',', '.');

            if (!decimal.TryParse(precioTexto, NumberStyles.Number,
                CultureInfo.InvariantCulture, out precio))
            {
                MessageBox.Show("Digite un precio válido.",
                    "Precio incorrecto", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return;
            }

            if (precio < 0)
            {
                MessageBox.Show("El precio no puede ser negativo.",
                    "Precio incorrecto", MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
                txtPrecio.Focus();
                return;
            }

            decimal precioUnitario = precio;
            string nombreProducto = txtProducto.Text.Trim();

            if (rbtGrande.Checked)
            {
                precioUnitario += AUMENTO_GRANDE;
                nombreProducto += " (Grande)";
            }

            if (chkPorcionExtra.Checked)
            {
                precioUnitario += AUMENTO_EXTRA;
                nombreProducto += " (Extra)";
            }

            int cantidad = Convert.ToInt32(nudCantidad.Value);
            decimal totalLinea = precioUnitario * cantidad;

            string linea =
                cantidad.ToString().PadRight(6) +
                nombreProducto.PadRight(24) +
                precioUnitario.ToString("N2", culturaRD).PadLeft(10) +
                totalLinea.ToString("N2", culturaRD).PadLeft(12);

            lstPedido.Items.Add(linea);
            subtotal += totalLinea;
            lblSubtotal.Text = FormatearDinero(subtotal);

            lblDescuento.Text = FormatearDinero(0m);
            lblEmpaque.Text = FormatearDinero(0m);
            lblItbis.Text = FormatearDinero(0m);
            lblTotal.Text = FormatearDinero(0m);

            // Reiniciar los controles del producto.
            txtProducto.Clear();
            txtPrecio.Clear();
            nudCantidad.Value = 1;
            rbtNormal.Checked = true;
            chkPorcionExtra.Checked = false;
            txtProducto.Focus();
        }

        // EVENTO CALCULAR TOTAL
        private void btnCalcular_Click(object sender, EventArgs e)
        {
            if (lstPedido.Items.Count == 0)
            {
                MessageBox.Show("Debe agregar productos al pedido antes de calcular.",
                    "Pedido vacío", MessageBoxButtons.OK,
                    MessageBoxIcon.Information);
                return;
            }

            decimal descuento = chkFrecuente.Checked
                ? subtotal * PORCENTAJE_DESCUENTO : 0m;

            decimal empaque = rbtLlevar.Checked
                ? COSTO_EMPAQUE : 0m;

            decimal baseImponible = subtotal - descuento + empaque;
            decimal itbis = baseImponible * PORCENTAJE_ITBIS;
            decimal total = baseImponible + itbis;

            lblSubtotal.Text = FormatearDinero(subtotal);
            lblDescuento.Text = FormatearDinero(descuento);
            lblEmpaque.Text = FormatearDinero(empaque);
            lblItbis.Text = FormatearDinero(itbis);
            lblTotal.Text = FormatearDinero(total);
        }

        // EVENTO NUEVO PEDIDO
        private void btnLimpiar_Click(object sender, EventArgs e)
        {
            txtCliente.Clear();
            chkFrecuente.Checked = false;
            dtpFecha.Value = DateTime.Today;
            txtProducto.Clear();
            txtPrecio.Clear();
            nudCantidad.Value = 1;
            rbtNormal.Checked = true;
            chkPorcionExtra.Checked = false;
            rbtLocal.Checked = true;

            lstPedido.Items.Clear();
            subtotal = 0m;

            MostrarResultadosEnCero();
            txtCliente.Focus();
        }

        // EVENTO SALIR
        private void btnSalir_Click(object sender, EventArgs e)
        {
            DialogResult respuesta = MessageBox.Show(
                "¿Está seguro de que desea salir?",
                "Confirmar salida",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question);

            if (respuesta == DialogResult.Yes)
                Application.Exit();
        }

        // MÉTODOS AUXILIARES
        private string FormatearDinero(decimal cantidad)
        {
            return "RD$ " + cantidad.ToString("N2", culturaRD);
        }

        private void MostrarResultadosEnCero()
        {
            lblSubtotal.Text = FormatearDinero(0m);
            lblDescuento.Text = FormatearDinero(0m);
            lblEmpaque.Text = FormatearDinero(0m);
            lblItbis.Text = FormatearDinero(0m);
            lblTotal.Text = FormatearDinero(0m);
        }
    }
}
