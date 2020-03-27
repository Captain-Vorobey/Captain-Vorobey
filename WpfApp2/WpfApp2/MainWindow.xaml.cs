using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp2
{
    public class Computer
    {
        public string Title { get; set; } // модель ноута
        public string Company { get; set; } // производитель
        public double Price { get; set; } // путь к изображению

        public override string ToString()
        {
            return $"{Title} {Company} {Price}";
        }
    }

    public partial class MainWindow : Window
    {


        public MainWindow()
        {
            InitializeComponent();
            DataTable dt_user = Select("SELECT * FROM [dbo].[users]"); // получаем данные из таблицы

            for (int i = 0; i < dt_user.Rows.Count; i++)
            { // перебираем данные
                MessageBox.Show(dt_user.Rows[i][0] + "|" + dt_user.Rows[i][1]); // выводим данные
            }
        }

        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dataTable = new DataTable("dataBase");                // создаём таблицу в приложении
                                                                            // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection("server=LAPTOP-HKUCMH0N/SQLEXPRESS01;Trusted_Connection=Yes;DataBase=TEST;");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            return dataTable;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string name = nameTextBox.Text;
            double price = Convert.ToDouble(priceTextBox.Text);
            string manufacturer = manufacturerTextBox.Text;


            Computer computer = new Computer()
            {
                Company = manufacturer,
                Title = name,
                Price = price
            };

            System.Windows.MessageBox.Show(computer.ToString());
        }
    }
}
