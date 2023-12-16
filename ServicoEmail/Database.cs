using Dapper;
using System.Data.SqlClient;
namespace ServicoEmail
{
    internal class Database
    {
        public void ConnectionDatabase()
        {
            string connectionString = "SuaStringDeConexao"; // Substitua pela sua string de conexão

            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();

                var resultado = connection.QueryFirstOrDefault<Usuarios>("SELECT * FROM Consulta WHERE AlgumaCondicao = @Valor", new { Valor = 123 });

                if (resultado != null)
                {

                }
                else
                {

                }
            }
        }
    }
}
