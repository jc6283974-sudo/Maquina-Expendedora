namespace Maquina
{
    using System.Collections.Generic;
    using System.Data.SqlClient;

    public class ProductoRepository
    {
        private readonly Conexion conexion;

        public ProductoRepository()
        {
            conexion = new Conexion();
        }

        public List<Producto> ObtenerProductos()
        {
            var productos = new List<Producto>();

            using (var conn = conexion.GetConnection())
            {
                conn.Open();
                string query = "SELECT Id, Nombre, Categoria, Precio, Stock, Imagenes FROM Productos";
                SqlCommand cmd = new SqlCommand(query, conn);
                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    productos.Add(new Producto
                    {
                        Id = reader.GetInt32(0),
                        Nombre = reader.GetString(1),
                        Categoria = reader.IsDBNull(2) ? null : reader.GetString(2),
                        Precio = reader.GetDecimal(3),
                        Stock = reader.GetInt32(4),
                        Imagenes = reader.GetString(5) // Nueva columna
                    });
                }
            }

            return productos;
        }

        public void ActualizarStock(int productoId, int cantidadVendida)
        {
            using (var conn = conexion.GetConnection())
            {
                conn.Open();
                string query = "UPDATE Productos SET Stock = Stock - @cantidad WHERE Id = @id";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@cantidad", cantidadVendida);
                cmd.Parameters.AddWithValue("@id", productoId);
                cmd.ExecuteNonQuery();
            }
        }
    }
}
