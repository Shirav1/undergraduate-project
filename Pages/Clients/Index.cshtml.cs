using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Clients
{
    public class IndexModel : PageModel
    {
        public List<ClientInfo> listClients = new List<ClientInfo>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\local;Initial Catalog=Transactiondb;Integrated Security=True";
               
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                 connection.Open();
                 string sql = "select * from Client";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientInfo clientInfo = new ClientInfo();
                                clientInfo.ClientID =reader.GetInt32(0);
                                clientInfo.name = reader.GetString(1);
                                clientInfo.surname = reader.GetString(2);
                                clientInfo.ClientBalance=reader.GetDecimal(3);
                               

                                listClients.Add(clientInfo);
                              
                          
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception:" + ex.ToString());
            }
        }
    }
    public class ClientInfo
    {
        public int ClientID;
        public string name;
        public string surname;
        public decimal ClientBalance;


    }
}
