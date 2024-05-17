using Nevron.UI.WinForm.Controls;
using System;
using System.Data;
using System.Data.SQLite;
using System.IO;
using System.Windows.Forms;
using EL;
namespace SmartLoadicator.Views
{
    public partial class Form1 : Form
    {
        private SQLiteConnection connection;
        private string databaseFile = StaticCache.DBInfoEntity.ServerPath; //"test.sqlite";

        public Form1()
        {
            InitializeComponent();
            string connectionString = $"Data Source={databaseFile};Version=3;";
            connection = new SQLiteConnection(connectionString);
            //if (File.Exists(databaseFile))
            //{
            //    string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            //    string sqliteFilePath = System.IO.Path.Combine(baseDirectory, databaseFile);

            //    Console.WriteLine("SQLite file location: " + sqliteFilePath);
            //    File.Delete(databaseFile);
            //}
                // Create database file and table if they don't exist
                if (!File.Exists(databaseFile))
            {
                CreateDatabase();
                CreateTable();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // Load data into DataGridView
            LoadData();
        }
        private void CreateDatabase()
        {
            SQLiteConnection.CreateFile(databaseFile);
        }

        private void CreateTable()
        {
            try
            {
                connection.Open();
                string query = "CREATE TABLE IF NOT EXISTS Q1 (ID INTEGER PRIMARY KEY AUTOINCREMENT, Column1 TEXT, Column2 TEXT)";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        private void LoadData()
        {
            try
            {
                connection.Open();
                string query = "SELECT * FROM Q1";
                SQLiteDataAdapter adapter = new SQLiteDataAdapter(query, connection);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                connection.Open();
                string query = "INSERT INTO Q1 (Column1, Column2) VALUES (@param1, @param2)";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@param1", textBox1.Text);
                command.Parameters.AddWithValue("@param2", textBox2.Text);
                command.ExecuteNonQuery();
               
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            LoadData();

            MessageBox.Show("Record inserted successfully.");
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.CurrentRow == null) return;
                connection.Open();
                string query = "UPDATE Q1 SET Column1 = @param1, Column2 = @param2 WHERE ID = @param3";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@param1", textBox1.Text);
                command.Parameters.AddWithValue("@param2", textBox2.Text);
                
                command.Parameters.AddWithValue("@param3", dataGridView1.CurrentRow.Cells[0].Value);
                command.ExecuteNonQuery();
         
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            LoadData();
            MessageBox.Show("Record updated successfully.");

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.Rows.Count==0 ||  dataGridView1[0,0].Value == null)  return;
                connection.Open();
                string query = "DELETE FROM Q1 WHERE ID = @param1";
                SQLiteCommand command = new SQLiteCommand(query, connection);
                command.Parameters.AddWithValue("@param1", dataGridView1.CurrentRow.Cells[0].Value);
                command.ExecuteNonQuery();
             
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
            }
            finally
            {
                connection.Close();
            }
            LoadData();

            MessageBox.Show("Record deleted successfully.");
        }
    }
}
