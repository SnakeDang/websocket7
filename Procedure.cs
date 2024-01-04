using System.Data.SqlClient;
using Dapper;

namespace Procedure
{
    public static class ProcedureHandler
    {
    
        public static List<string> GetRoom()
        {
            var connectionString = "Server=10.14.17.245;Database=WebSocketAMR;User ID=sa;password=thaco@1234;MultipleActiveResultSets=true;Timeout=300;";
            using (var conn = new SqlConnection(connectionString))
            {
                if (conn.State == System.Data.ConnectionState.Closed)
                    conn.Open();

               var result = conn.Query<string>("PR_getAllRoom");
                foreach (var item in result.ToList())
                {
                    Console.WriteLine( "giá trị phòng là ::::: " + item);
                }
                return result.ToList();
            }
        }
    }

}