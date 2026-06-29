using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Configuration;

namespace S26Week8ConnectedModel
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        string connStr = ConfigurationManager.ConnectionStrings["Northwind"].ConnectionString;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void LoadData()
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                string query = "Select * from Employees";
                SqlCommand cmd = new SqlCommand(query, conn);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                DataTable tbl = new DataTable();
                tbl.Load(reader);
                grdEmployees.ItemsSource = tbl.DefaultView;
            }
            //conn.Close();
        }

        private void btnLoad_Click(object sender, RoutedEventArgs e)
        {
            LoadData();
        }

        private void btnSearch_Click(object sender, RoutedEventArgs e)
        {
            // string concatenation - NO
            //string query = "select * from Employees where FirstName='" + txtFirstname.Text + "'";

            // command parameters - YES
            string query = "select * from Employees where FirstName=@fn";

            using (SqlConnection conn = new SqlConnection(connStr))
            {
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.AddWithValue("fn", txtFirstname.Text);

                conn.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                DataTable tbl = new DataTable();
                tbl.Load(reader);

                grdEmployees.ItemsSource = tbl.DefaultView;
            }
        }
    }
}