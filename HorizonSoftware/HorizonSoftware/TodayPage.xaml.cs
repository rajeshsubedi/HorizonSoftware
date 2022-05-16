using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace HorizonSoftware
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class TodayPage : ContentPage
    {
        public class mysqlList
        {
            public string ANumber { get; set; }
            public string AHolder { get; set; }
            public string Amount { get; set; }

        }

        public ObservableCollection<string> mysqlLists { get; set; }
        ObservableCollection<string> data = new ObservableCollection<string>();


        SqlConnection sqlConnection;

        public TodayPage()
        {
            InitializeComponent();
            string srvrdbname = "mydb";
            string srvrname = "192.168.1.72";
            string srvrusername = "Rajesh";
            string srvrpassword = "12345";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
        }
     

        private async void RadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
        {
          
                try
                {
                    List<mysqlList> mysqlLists = new List<mysqlList>();
                    sqlConnection.Open();
                    string queryString = "select AccountNumber,AccountHolder from dbo.DepositeTable";

                    SqlCommand command = new SqlCommand(queryString, sqlConnection);
                    SqlDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        mysqlLists.Add(new mysqlList
                        {
                            ANumber = reader["AccountNumber"].ToString(),
                            AHolder = reader["AccountHolder"].ToString(),
                        }
                        );
                    }
                    reader.Close();
                    //IsVisible = false;
                    sqlConnection.Close();
                MyListView.ItemsSource = mysqlLists;

                }
                catch (Exception ex)
                {
                    await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                    throw;
                }


            }


        

            private  void RadioButton_CheckedChanged_1(object sender, CheckedChangedEventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }

        private void RadioButton_CheckedChanged_2(object sender, CheckedChangedEventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }
    }
}