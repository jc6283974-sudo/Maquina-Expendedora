namespace Maquina
{
    using System;

    public class VentaService
    {
        private readonly ProductoRepository productoRepo;

        public VentaService()
        {
            productoRepo = new ProductoRepository();
        }

        public void VenderProducto(int productoId, int cantidad)
        {
            var productos = productoRepo.ObtenerProductos();
            var producto = productos.Find(p => p.Id == productoId);

            if (producto == null)
            {
                Console.WriteLine("Producto no encontrado.");
                return;
            }

            if (producto.Stock < cantidad)
            {
                Console.WriteLine("Stock insuficiente.");
                return;
            }

            productoRepo.ActualizarStock(productoId, cantidad);
            Console.WriteLine($"Venta realizada: {cantidad} unidad(es) de {producto.Nombre}.");
            Console.WriteLine($"Imagen asociada: {producto.Imagenes}");
        }
    }
}
