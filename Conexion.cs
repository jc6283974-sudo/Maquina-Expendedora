namespace Maquina
{
    using System.Data.SqlClient;

    public class Conexion
    {
        private readonly string connectionString;

        public Conexion()
        {
            // Ajusta el nombre de tu servidor y base de datos
            connectionString = @"Server=MONKIBLA\SQLEXPRESS;Database=ExpendedoraDB;Trusted_Connection=True;";
        }

        public SqlConnection GetConnection()
        {
            return new SqlConnection(connectionString);
        }
    }
}
