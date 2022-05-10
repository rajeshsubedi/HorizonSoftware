using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace HorizonSoftware
{
    public partial class LoginPage : ContentPage

    {
        //public class mysqlList
        //{
        //    public string username { get; set; }
        //    public string password { get; set; }
        //}


        SqlConnection sqlConnection;

        public LoginPage()
        {
            InitializeComponent();
            string srvrdbname = "mydb";
            string srvrname = "192.168.1.75";
            string srvrusername = "Rajesh";
            string srvrpassword = "12345";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            //if (txtUserName.Text == "admin" && txtPassword.Text == "12345")
            //{
            //    Navigation.PushAsync(new HomePage());
            //}

            //else
            //{
            //    DisplayAlert("Sorry", "Username or Password is incorrect.", "OK");
            //}

            {
                Console.WriteLine($"SELECT username From dbo.mylogin WHERE username='{txtUsername.Text}' and password='{txtPassword.Text}'");
                try
                {
                    //App.Current.MainPage.DisplayAlert("Alert", $"SELECT  FROM dbo.logindb WHERE username ={txtUsername.Text} and password = {txtPassword.Text}", "Ok");
                    //Console.WriteLine($"SELECT  FROM dbo.logindb WHERE username={txtUsername.Text} and password={txtPassword.Text}");
                    sqlConnection.Open();

                    //using (var sqlCommand = new SqlCommand($"SELECT username FROM dbo.mylogin WHERE username={txtUsername.Text} and password={txtPassword.Text}", sqlConnection))
                    //{
                    using (var sqlCommand = new SqlCommand($"SELECT username From dbo.mylogin WHERE username='{txtUsername.Text}' and password='{txtPassword.Text}'", sqlConnection))
                    { 
                            SqlDataReader reader = sqlCommand.ExecuteReader();
                        if (reader.Read())
                        {
                            //await App.Current.MainPage.DisplayAlert("Alert", "Login Successful.", "Ok");
                            _ = Navigation.PushAsync(new HomePage());
                        }
                        else
                        {
                            await App.Current.MainPage.DisplayAlert("Alert", "Username and Password is incorrect.", "Ok");
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

        private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            {
                Navigation.PushAsync(new SignupPage());
            }
        }

        private void TapGestureRecognizer_Tapped_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new ForgotPasswordPage(txtUsername.Text));
        }
    }
}
