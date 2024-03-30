using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Semana03
{
    /// <summary>
    /// Lógica de interacción para Conexion.xaml
    /// </summary>
    public partial class Conexion : Window
    {
        private string connectionString = "Data Source=LAB1504-17\\SQLEXPRESS;Initial Catalog=yerly;User Id=USER_YERLY;Password=123456";

        public Conexion()
        {
            InitializeComponent();
        }


        private void SearchButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Cadena de conexión
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                List<Student> students = new List<Student>();

                string txtFirstName = searchTextBox.Text;

                //Comandos de TRANSACT SQL
                // Consulta SQL con parámetro
                SqlCommand command = new SqlCommand($"SELECT * FROM Students WHERE FirstName LIKE '{txtFirstName}'", connection);


                //CONECTADA
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int studentId = reader.GetInt32("StudentId");
                    string firstName = reader.GetString("FirstName");
                    string lastName = reader.GetString("LastName");

                    students.Add(new Student { StudentId = studentId, FirstName = firstName, LastName = lastName });

                }
                connection.Close();

                studentData.ItemsSource = students;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

                //throw;
            }
        }

        private void DataReader_Click(object sender, RoutedEventArgs e)
        {
            List<Student> students = new List<Student>();
            try
            {
                // Cadena de conexión
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Comandos de TRANSACT SQL
                SqlCommand command = new SqlCommand("SELECT * FROM Students", connection);

                // CONECTADA
                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    int studentId = reader.GetInt32(reader.GetOrdinal("StudentId"));
                    string firstName = reader.GetString(reader.GetOrdinal("FirstName"));
                    string lastName = reader.GetString(reader.GetOrdinal("LastName"));

                    students.Add(new Student { StudentId = studentId, FirstName = firstName, LastName = lastName });
                }

                connection.Close();

                studentData.ItemsSource = students;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        private void DataTable_Click(object sender, RoutedEventArgs e)
        {
            DataTable dataTable = new DataTable();
            try
            {
                // Cadena de conexión
                SqlConnection connection = new SqlConnection(connectionString);
                connection.Open();

                // Comandos de TRANSACT SQL
                SqlCommand command = new SqlCommand("SELECT * FROM Students", connection);

                // Intermediario
                SqlDataAdapter adapter = new SqlDataAdapter(command);

                adapter.Fill(dataTable);

                connection.Close();
                studentData.ItemsSource = dataTable.DefaultView;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}