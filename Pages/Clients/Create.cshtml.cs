using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Data.SqlClient;

namespace WebApplication1.Pages.Clients
{
    public class CreateModel : PageModel
    {
        public ClientTransaction clientTransaction = new ClientTransaction();
        string errormessage = "";
        string successmessage = "";
        public void OnGet()
        {
        }
        public void OnPost()
        {
            decimal clientamount = new decimal();
            int transactionTypeId = new int();
            int clientId = new int();

            if (decimal.TryParse(Request.Form["Amount"], out clientamount))
            {
                errormessage = "Please enter a valid Decimal";
                return;
            }
            if (int.TryParse(Request.Form["TransactionTypeId"], out transactionTypeId))
            {
                errormessage = "Please enter a valid Id";
                return;
            }
            if (int.TryParse(Request.Form["id"], out clientId))
            {
                errormessage = "Please enter a valid Id";
                return;
            }

            clientTransaction.Amount = clientamount;
            clientTransaction.TransactionTypeId = transactionTypeId;
            clientTransaction.ClientID = clientId;
            clientTransaction.Comment = Request.Form["comment"];


            if (clientTransaction.Comment.Length == 0 || clientTransaction.Amount == 0 || clientTransaction.TransactionTypeId == 0
                || clientTransaction.TransactionID == 0)
            {
                errormessage = "All fields are required";
                return;
            }

            try
            {
                string connectionString = "Data Source=(localdb)\\local;Initial Catalog=Transactiondb;Integrated Security=True";
               
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    string sql = "INSERT INTO TransactionTable" + "(Amount,TransactionTypeID,ClientID,Comment) VALUES" +
                        "(@Amount,@TransactionTypeID,@ClientID,@Comment);";

                    using (SqlCommand Command = new SqlCommand(sql, connection))
                    {
                        Command.Parameters.AddWithValue("@Amount", clientTransaction.Amount);
                        Command.Parameters.AddWithValue("@TransactionTypeID",clientTransaction.TransactionTypeId);
                        Command.Parameters.AddWithValue("@ClientID", clientTransaction.ClientID);
                        Command.Parameters.AddWithValue("@Comment", clientTransaction.Comment);

                        Command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                errormessage = ex.Message;
                return;
            }
            clientTransaction.Comment = "";
            successmessage = "New Client added correctly";

        }
    }
}
