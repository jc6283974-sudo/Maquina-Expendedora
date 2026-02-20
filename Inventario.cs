using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Maquina
{
    public partial class Inventario : Form
    {
        private ProductoRepository repo = new ProductoRepository();

        public Inventario()
        {
            InitializeComponent();
            CargarProductos(); // carga productos al abrir el form
        }

        private void CargarProductos()
        {
            dataGridView1.DataSource = null;
            dataGridView1.DataSource = repo.ObtenerProductos(); // usamos el método correcto
        }

        private void btnCrear_Click(object sender, EventArgs e)
        {
            try
            {
                Producto p = new Producto
                {
                    Nombre = txtNombre.Text,
                    Categoria = txtCategoria.Text, // nuevo campo
                    Precio = decimal.Parse(txtPrecio.Text),
                    Stock = int.Parse(txtStock.Text),
                    Imagenes = txtImagen.Text // nuevo campo
                };

                // Aquí deberías implementar repo.Insert(p) en tu ProductoRepository
                // Por ahora lo dejamos como comentario si aún no lo tienes
                // repo.Insert(p);

                CargarProductos();
                MessageBox.Show("Producto creado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al crear producto: " + ex.Message);
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                Producto p = new Producto
                {
                    Id = int.Parse(txtId.Text),
                    Nombre = txtNombre.Text,
                    Categoria = txtCategoria.Text, // nuevo campo
                    Precio = decimal.Parse(txtPrecio.Text),
                    Stock = int.Parse(txtStock.Text),
                    Imagenes = txtImagen.Text // nuevo campo
                };

                // Aquí deberías implementar repo.Update(p) en tu ProductoRepository
                // repo.Update(p);

                CargarProductos();
                MessageBox.Show("Producto actualizado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar producto: " + ex.Message);
            }
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            try
            {
                int id = int.Parse(txtId.Text);

                // Aquí deberías implementar repo.Delete(id) en tu ProductoRepository
                // repo.Delete(id);

                CargarProductos();
                MessageBox.Show("Producto eliminado correctamente");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al eliminar producto: " + ex.Message);
            }
        }

        private void btnVolver_Click(object sender, EventArgs e)
        {
            this.Hide();
            Maquina maquina = new Maquina(); // usa el nombre real de tu form principal
            maquina.Show();
        }

        // Métodos vacíos para evitar errores del diseñador
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e) { }
        private void txtId_TextChanged(object sender, EventArgs e) { }
        private void txtNombre_TextChanged(object sender, EventArgs e) { }
        private void txtCategoria_TextChanged(object sender, EventArgs e) { }
        private void txtPrecio_TextChanged(object sender, EventArgs e) { }
        private void txtStock_TextChanged(object sender, EventArgs e) { }
        private void txtImagen_TextChanged(object sender, EventArgs e) { }
    }
}

