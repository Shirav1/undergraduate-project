using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Clients
{
    public class viewModel : PageModel
    {
        public List<ClientTransaction> ViewClients = new List<ClientTransaction>();
        public void OnGet()
        {
            try
            {
                string connectionString = "Data Source=(localdb)\\local;Initial Catalog=Transactiondb;Integrated Security=True";

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql2 = "select * from TransactionTable";
                    using (SqlCommand cmd = new SqlCommand(sql2, connection))
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                ClientTransaction clientTransaction = new ClientTransaction();
                                clientTransaction.TransactionID = reader.GetInt32(0);
                                clientTransaction.Amount = reader.GetDecimal(1);
                                clientTransaction.TransactionTypeId = reader.GetInt32(2);
                                clientTransaction.ClientID=reader.GetInt32(3);
                                clientTransaction.Comment = reader.GetString(4);



                                ViewClients.Add(clientTransaction);

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



    public class ClientTransaction
    {
        public int ClientID;
        public int TransactionID;
        public decimal Amount;
        public string Comment;
        public int TransactionTypeId;


    }
}

