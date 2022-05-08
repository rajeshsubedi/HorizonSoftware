using Rg.Plugins.Popup.Services;
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
    public partial class DepositePage : ContentPage
    {
        public class mysqlList
        {
            public string ANumber { get; set; }
            //public string Holder { get; set; }

        }

        //ObservableCollection<mysqlList> mysqlLists = new ObservableCollection<mysqlList>();
        //public ObservableCollection<mysqlList> AccountNumberList { get { return accountNumbers; } }
        public ObservableCollection<string> mysqlLists { get; set; }


        SqlConnection sqlConnection;

        //public string BindingHolder { get;  set; }

        public DepositePage()
        {
            InitializeComponent();
            string srvrdbname = "mydb";
            string srvrname = "192.168.1.64";
            string srvrusername = "Rajesh";
            string srvrpassword = "12345";
            string sqlconn = $"Data Source={srvrname};Initial Catalog={srvrdbname};User ID={srvrusername};Password={srvrpassword}";
            sqlConnection = new SqlConnection(sqlconn);
            
    
        }







        private async void Button_Clicked(object sender, EventArgs e)
        {
            //try
            //{


            //        sqlConnection.Open();
            //    string AmountTobeUpdated = Amount.Text;
            //    string qerystr = $"UPDATE dbo.mysql SET Amount='{AmountTobeUpdated}' WHERE Id='{AccountNumber}'";

            //    using (SqlCommand command = new SqlCommand(qerystr, sqlConnection))
            //    {
            //        command.ExecuteNonQuery();
            //        sqlConnection.Close();
            //    }
            //    await App.Current.MainPage.DisplayAlert("Alert", "Successfull", "Ok");

            //}
            //catch (Exception ex)
            //{
            //    await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            //    throw;
            //}
            try
            {
                sqlConnection.Open();
                string AmountTobeUpdated = Amount.Text;
                string Accountnumber = AccountNumber.Text;
                using (var sqlCommand = new SqlCommand($"UPDATE dbo.DepositTable SET Amount='{AmountTobeUpdated}' WHERE AccountNumber='{Accountnumber}'", sqlConnection))
                {
                    SqlDataReader reader = sqlCommand.ExecuteReader();
                    if (reader.Read())
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Successfull", "Ok");
                    }
                    else
                    {
                        await App.Current.MainPage.DisplayAlert("Alert", "Something Where Wrong, Please Check! ", "Ok");
                    }
                    reader.Close();
                    reader.Dispose();
                }
                sqlConnection.Close();
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
            }



        }










        private async void AccountNumber_SearchButtonPressed(object sender, EventArgs e)
        {
            try
            {

                List<mysqlList> mysqlLists = new List<mysqlList>();
                sqlConnection.Open();
                string queryString = $"select * from dbo.DepositTable where AccountNumber='{AccountNumber.Text}'";

                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {

                    mysqlLists.Add(new mysqlList
                    {
                        ANumber = reader["AccountNumber"].ToString(),
                        //Holder = reader["AccountHolder"].ToString(),
                    }
                    );
                    AccountNumber.Text = reader["AccountNumber"].ToString();
                    AccountHolder.Text = reader["AccountHolder"].ToString();
                    //BindingHolder = reader["AccountHolder"].ToString();
                }
                else
                {
                    await App.Current.MainPage.DisplayAlert("Alert", "AccountNumber is incorrect.", "Ok");
                }

                reader.Close();
                reader.Dispose();
                sqlConnection.Close();
            }
            //myListView.ItemsSource = mysqlLists;


            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                throw;
            }
        }





        private void Button_Clicked_1(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HomePage());
        }








        private async void AccountNumber_TextChanged(object sender, TextChangedEventArgs e)
        {
            //myListView.ItemsSource = myList.Where(s => s.Number.StartsWith(e.NewTextValue));
            //myListView.ItemsSource =  mysqlLists.Where(s => s.Number.StartsWith(e.NewTextValue));
            try
            {

                List<mysqlList> mysqlLists = new List<mysqlList>();
                sqlConnection.Open();
                string queryString = $"select AccountNumber from dbo.DepositTable Where AccountNumber like '%{e.NewTextValue}%'";

                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    mysqlLists.Add(new mysqlList
                    {
                        ANumber = reader["AccountNumber"].ToString(),
                    }
                    );
                }
                reader.Close();
                sqlConnection.Close();
                //ListView.ItemsSource = mysqlLists;
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                throw;
            }
        }

    
        private async void AccountNumber_Focused(object sender, FocusEventArgs e)
        {
            try
            {
                //PopupNavigation.Instance.PushAsync(new PopupView());
                List<mysqlList> mysqlLists = new List<mysqlList>();
                sqlConnection.Open();
                string queryString = "select AccountNumber from dbo.DepositTable";

                SqlCommand command = new SqlCommand(queryString, sqlConnection);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    mysqlLists.Add(new mysqlList
                    {
                        ANumber = reader["AccountNumber"].ToString(),
                    }
                    );
                }
                reader.Close();
                //IsVisible = false;
                sqlConnection.Close();
                myListView.ItemsSource = mysqlLists;
                
            }
            catch (Exception ex)
            {
                await App.Current.MainPage.DisplayAlert("Alert", ex.Message, "Ok");
                throw;
            }


        }
    }
}