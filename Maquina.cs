using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Maquina; // Importa las clases ProductoRepository y VentaService

namespace Maquina
{
    public partial class Maquina : Form
    {
        private ProductoRepository repo = new ProductoRepository();
        private VentaService ventaService = new VentaService();
        private string productoSeleccionado = ""; // aquí se guarda el Id digitado
        private decimal saldo = 0;

        // Carpeta de recursos dentro del proyecto
        private string rutaRecursosProyecto = Path.Combine(Application.StartupPath, "Resources");

        // Carpeta absoluta en tu PC
        private string rutaRecursosPC = @"C:\Users\DELL\source\repos\Maquina\Maquina\Resources";

        public Maquina()
        {
            InitializeComponent();
        }

        // Botones numéricos (los que el diseñador llama button1, button2, etc.)
        private void button1_Click(object sender, EventArgs e) { productoSeleccionado += "1"; lblId.Text = productoSeleccionado; }
        private void button2_Click(object sender, EventArgs e) { productoSeleccionado += "2"; lblId.Text = productoSeleccionado; }
        private void button3_Click(object sender, EventArgs e) { productoSeleccionado += "3"; lblId.Text = productoSeleccionado; }
        private void button6_Click(object sender, EventArgs e) { productoSeleccionado += "4"; lblId.Text = productoSeleccionado; }
        private void button5_Click(object sender, EventArgs e) { productoSeleccionado += "5"; lblId.Text = productoSeleccionado; }
        private void button4_Click(object sender, EventArgs e) { productoSeleccionado += "6"; lblId.Text = productoSeleccionado; }
        private void button12_Click(object sender, EventArgs e) { productoSeleccionado += "7"; lblId.Text = productoSeleccionado; }
        private void button11_Click(object sender, EventArgs e) { productoSeleccionado += "8"; lblId.Text = productoSeleccionado; }
        private void button10_Click(object sender, EventArgs e) { productoSeleccionado += "9"; lblId.Text = productoSeleccionado; }
        private void button8_Click(object sender, EventArgs e) { productoSeleccionado += "0"; lblId.Text = productoSeleccionado; }

        private void btnBackspace_Click(object sender, EventArgs e)
        {
            if (productoSeleccionado.Length > 0)
            {
                productoSeleccionado = productoSeleccionado.Substring(0, productoSeleccionado.Length - 1);
                lblId.Text = productoSeleccionado;
            }
        }

        private void btnEnter_Click(object sender, EventArgs e)
        {
            if (int.TryParse(productoSeleccionado, out int productoId))
            {
                var producto = repo.ObtenerProductos().Find(p => p.Id == productoId);

                if (producto != null)
                {
                    lblProducto.Text = producto.Nombre;
                    lblPrecio.Text = producto.Precio.ToString("C");
                    lblCategoria.Text = producto.Categoria; // Mostrar categoría

                    // Mostrar imagen del producto (dos rutas posibles)
                    string rutaImagenProyecto = Path.Combine(rutaRecursosProyecto, producto.Imagenes);
                    string rutaImagenPC = Path.Combine(rutaRecursosPC, producto.Imagenes);

                    if (File.Exists(rutaImagenProyecto))
                    {
                        pbProducto.Image = Image.FromFile(rutaImagenProyecto);
                        pbProducto.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else if (File.Exists(rutaImagenPC))
                    {
                        pbProducto.Image = Image.FromFile(rutaImagenPC);
                        pbProducto.SizeMode = PictureBoxSizeMode.StretchImage;
                    }
                    else
                    {
                        pbProducto.Image = null;
                        lblError.Text = "Imagen no encontrada: " + producto.Imagenes;
                    }

                    // Validar saldo y procesar venta
                    if (saldo >= producto.Precio)
                    {
                        ventaService.VenderProducto(productoId, 1);
                        pbEntrega.BackColor = Color.Green;

                        decimal cambio = saldo - producto.Precio;
                        lblCambio.Text = cambio.ToString("C");

                        saldo = 0;
                        lblError.Text = "";
                    }
                    else
                    {
                        pbEntrega.BackColor = Color.Red;
                        lblError.Text = "Saldo insuficiente";
                    }
                }
                else
                {
                    lblError.Text = "Producto no encontrado";
                }
            }
            else
            {
                lblError.Text = "Código inválido";
            }

            productoSeleccionado = "";
            lblId.Text = "";
        }

        private void txtSaldo_TextChanged(object sender, EventArgs e) { }

        private void btnEntrarSaldo_Click(object sender, EventArgs e)
        {
            if (decimal.TryParse(txtSaldo.Text, out decimal monto))
            {
                saldo += monto;
                lblError.Text = "";
                lblCambio.Text = "0"; // reinicia el cambio
            }
            else
            {
                lblError.Text = "Monto inválido";
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Inventario inv = new Inventario();
            inv.Show();
            this.Hide();
        }

        private void btnInventario_Click(object sender, EventArgs e)
        {
            Inventario inv = new Inventario();
            inv.Show();
            this.Hide();
        }

        // Métodos vacíos para evitar errores del diseñador
        private void pbProducto_Click(object sender, EventArgs e) { }
        private void lblProducto_Click(object sender, EventArgs e) { }
        private void lblPrecio_Click(object sender, EventArgs e) { }
        private void lblId_Click(object sender, EventArgs e) { }
        private void lblCambio_Click(object sender, EventArgs e) { }
        private void lblError_Click(object sender, EventArgs e) { }
        private void pbEntrega_Click(object sender, EventArgs e) { }
        private void lblCategoria_Click(object sender, EventArgs e) { }
        private void progressBar1_Click(object sender, EventArgs e) { }

        private void Maquina_Load(object sender, EventArgs e)
        {

        }
    }
}
