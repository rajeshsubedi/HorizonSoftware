using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorizonSoftware
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoanPage : ContentPage
    {
        public class mysqlList
        {
            public string AccountNumber { get; set; }
            public string AccountHolder { get; set; }
            public string PrincipleAmount { get; set; }
            public string InterestRate { get; set; }
            public string FineAmount { get; set; }
            public string FineRate { get; set; }
            public string RecievedAmount { get; set; }
            public string TotalDue { get; set; }
        }


        SqlConnection sqlConnection;
        public LoanPage()
        {
            InitializeComponent();
            string srvrdbname = "mydb";
            string srvrname = "192.168.1.69";
            string srvrusername = "Rajesh";
            string srvrpassword = "12345";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
        }

        private void Button_Clicked(object sender, EventArgs e)
        {

        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }

        private async void SearchBar_SearchButtonPressed(object sender, EventArgs e)
        {
            //Console.WriteLine(AccountNumber.Text);
            //await App.Current.MainPage.DisplayAlert("Alert",AccountNumber.Text, "Ok");
            try
            {

                List<mysqlList> mysqlLists = new List<mysqlList>();
                sqlConnection.Open();
                string queryString = $"Select * from dbo.LoanTable WHERE AccountNumber='{AccountNumber.Text}'";
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    mysqlLists.Add(new mysqlList
                    {
                        AccountNumber = reader["AccountNumber"].ToString(),
                        AccountHolder = reader["AccountHolder"].ToString(),
                        PrincipleAmount = reader["PrincipleAmount"].ToString(),
                        InterestRate = reader["InterestRate"].ToString(),
                        FineAmount = reader["FineAmount"].ToString(),
                        FineRate = reader["FineRate"].ToString(),
                        TotalDue = reader["TotalDue"].ToString(),
                        RecievedAmount = reader["RecievedAmount"].ToString(),
                    }
                    );

                }
                reader.Close();
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                throw;
            }
        }
    }
}