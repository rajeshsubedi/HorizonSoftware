using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net.NetworkInformation;
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
            public string Liscence { get; set; }
            public string Token { get; set; }
        }

        SqlConnection sqlConnection;

        public ActivationPage()
        {
            InitializeComponent();
            string srvrdbname = "mydb";
            string srvrname = "192.168.1.71";
            string srvrusername = "Rajesh";
            string srvrpassword = "12345";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
            CheckForActivation();
        }

        public void CheckForActivation()
        {
            try
            {
                sqlConnection.Open();

                using (var sqlCommand = new SqlCommand($"SELECT Liscence FROM dbo.myactivation WHERE MacAddress='{GetMacAddress().ToString()}'", sqlConnection))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        
                        _ = Navigation.PushAsync(new LoginPage());
                    }

                }
                sqlConnection.Close();
            }
            catch (Exception)
            {
                sqlConnection.Close();
            }
        }


        //for activation save in database
        public static PhysicalAddress GetMacAddress()
        {
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
            {
                // Only consider Ethernet network interfaces
                if (nic.NetworkInterfaceType == NetworkInterfaceType.Ethernet &&
                    nic.OperationalStatus == OperationalStatus.Up)
                {
                    return nic.GetPhysicalAddress();
                }
            }
            return null;
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

                using (var sqlCommand = new SqlCommand($"SELECT Liscence FROM dbo.myactivation WHERE Liscence='{txtliscence.Text}' and Token='{txttoken.Text}'", sqlConnection))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        sqlConnection.Close();
                        sqlConnection.Open();
                        using (SqlCommand command = new SqlCommand("UPDATE  dbo.myactivation SET MacAddress = @MacAddress WHERE Liscence=@Liscence and Token=@Token", sqlConnection))
                        {
                            command.Parameters.Add(new SqlParameter("MacAddress", GetMacAddress().ToString()));
                            command.Parameters.Add(new SqlParameter("Liscence", txtliscence.Text));
                            command.Parameters.Add(new SqlParameter("Token", txttoken.Text));
                            command.ExecuteNonQuery();
                        }
                        await App.Current.MainPage.DisplayAlert("Alert", "Activated", "Ok");

                        // update myactivation table status for particular liscence and token
                        //store current liscence tokrn and ststus in sqlite

                        _ = Navigation.PushAsync(new LoginPage());
                    }



                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Not activated", "Ok");
                    }

                    reader.Close();
                    reader.Dispose();


                    sqlConnection.Close();
                }

            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                throw;
            }

        }
    }
}
    
    
