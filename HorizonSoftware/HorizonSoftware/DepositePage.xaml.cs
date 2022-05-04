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
    public partial class DepositePage : ContentPage
    {
        public class mysqlList
        {
            public string AccountNumber { get; set; }
            public string AccountHolder { get; set; }
        }


        SqlConnection sqlConnection;
        public DepositePage()
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

        private async void AccountNumber_SearchButtonPressed(object sender, EventArgs e)
        {
            try
            {

                List<mysqlList> mysqlLists = new List<mysqlList>();
                sqlConnection.Open();
                string queryString = $"Select * from dbo.DepositTable WHERE AccountNumber='{AccountNumber.Text}'";
                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    mysqlLists.Add(new mysqlList
                    {
                        AccountNumber = reader["AccountNumber"].ToString(),
                        AccountHolder = reader["AccountHolder"].ToString(),
                        
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

        private void Button_Clicked_1(object sender, EventArgs e)
        {

        }
    }
}