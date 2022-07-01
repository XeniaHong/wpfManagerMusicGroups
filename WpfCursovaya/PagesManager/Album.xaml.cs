﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
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

namespace WpfCursovaya.PagesManager
{
    /// <summary>
    /// Логика взаимодействия для Album.xaml
    /// </summary>
    public partial class Album : Page
    {
        public Album()
        {
            InitializeComponent();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {

            NavigationService.Navigate(new Uri("/PagesManager/AlbumEdit.xaml", UriKind.Relative));
        }

        public void refresh()
        {
            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = "SELECT * FROM Albums";
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(cmd.CommandText, con))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    DataGridShedules.ItemsSource = dataTable.AsDataView();

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }
        }

        private void DataGrid_Loaded(object sender, RoutedEventArgs e)
        {
            refresh();
        }


        private void Search_Click(object sender, RoutedEventArgs e)
        {
            string searAl = Search.Text;


            var con = new SQLiteConnection("Data Source=appDb.db");
            try
            {
                con.Open();
                SQLiteCommand cmd = con.CreateCommand();
                cmd.CommandText = @"SELECT * FROM Albums  where name like '%" + searAl + "%'";
                using (SQLiteDataAdapter dataAdapter = new SQLiteDataAdapter(cmd.CommandText, con))
                {
                    DataTable dataTable = new DataTable();
                    dataAdapter.Fill(dataTable);

                    DataGridShedules.ItemsSource = dataTable.AsDataView();

                }
            }
            catch (Exception exp)
            {
                MessageBox.Show(exp.Message);
            }

        }

        private void Word_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("К сожаления эта функция не работает");
        }

        private void btnSong_Click(object sender, RoutedEventArgs e)
        {
            if (DataGridShedules.SelectedItems.Count == 0) return;
            string iddel = ((DataRowView)DataGridShedules.SelectedItems[0]).Row["idAlbum"].ToString();

            Songs s = new Songs(iddel);
            s.Show();
        }
    }
}
