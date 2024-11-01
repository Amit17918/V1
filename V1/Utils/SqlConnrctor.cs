using System.Data.SqlClient;

namespace V1.Utils
{
    public class SqlConnrctor
    {

        private string ConnectionString =
          "Data Source=DESKTOP-1QKKGS7\\SQLEXPRESS;" +
          "Initial Catalog=PRJV1.0;" +
          "User id=sa;" +
          "Password=1234567;";
        private SqlConnection connection = null;

        public SqlConnection Connection()
        {
            connection = new SqlConnection();
            connection.ConnectionString = ConnectionString;
            return connection;
        }

        public void Open()
        {
            connection.Open();
        }

        public void Close()
        {
            connection.Close();
        }
    }
}
