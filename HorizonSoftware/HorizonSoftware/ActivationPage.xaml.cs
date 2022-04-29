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
    public partial class ActivationPage : ContentPage
    {
        public class mysqlList
        {
            public int Liscence { get; set; }
            public int Token { get; set; }
        }

        SqlConnection sqlConnection;

        public ActivationPage()
        {
            InitializeComponent();
            string srvrdbname = "mydb";
            string srvrname = "192.168.1.67";
            string srvrusername = "Rajesh";
            string srvrpassword = "12345";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
        }




        private async void Button_Clicked_1(object sender, EventArgs e)


        //{
        //    if (txtliscence.Text == "12345" && txttoken.Text == "12345")
        //    {
        //        Navigation.PushAsync(new MainPage());
        //    }

        //    else
        //    {
        //        DisplayAlert("Sorry", "liscence or token is incorrect.", "OK");
        //    }
        //}

        {
            //Console.WriteLine(txtliscence.Text);
            try
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand($"SELECT Liscence FROM dbo.myactivation WHERE Liscence={txtliscence.Text} and Token={txttoken.Text}", sqlConnection))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Activated", "Ok");

                        _ = Navigation.PushAsync(new LoginPage());
                    }



                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Not activated", "Ok");
                    }

                    reader.Close();
                    reader.Dispose();
                }

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
    
    
