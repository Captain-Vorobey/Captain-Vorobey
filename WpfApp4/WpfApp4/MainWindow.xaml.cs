﻿using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp4
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public DataTable Select(string selectSQL) // функция подключения к базе данных и обработка запросов
        {
            DataTable dataTable = new DataTable("dataBase");                // создаём таблицу в приложении
                                                                            // подключаемся к базе данных
            SqlConnection sqlConnection = new SqlConnection("Server=.\\SQLEXPRESS01;Database=TEST;Trusted_Connection=True;");
            sqlConnection.Open();                                           // открываем базу данных
            SqlCommand sqlCommand = sqlConnection.CreateCommand();          // создаём команду
            sqlCommand.CommandText = selectSQL;                             // присваиваем команде текст
            SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand); // создаём обработчик
            sqlDataAdapter.Fill(dataTable);                                 // возращаем таблицу с результатом
            return dataTable;
        }

         private void acceptButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt_user = Select("SELECT * FROM [dbo].[Table_1]"); // получаем данные из таблицы

            for (int i = 0; i < dt_user.Rows.Count; i++)
            { // перебираем данные
                MessageBox.Show(dt_user.Rows[i][0] + "|" + dt_user.Rows[i][1]); // выводим данные
            }
        }
 
        private void escButton_Click(object sender, RoutedEventArgs e)
        {
            DataTable dt_user = Select("SELECT [Title] FROM [dbo].[Table_1]"); // получаем данные из таблицы

            for (int i = 0; i < dt_user.Rows.Count; i++)
            { // перебираем данные
                MessageBox.Show(dt_user.Rows[0][0].ToString()); // выводим данные
            }
        }
    }
}
