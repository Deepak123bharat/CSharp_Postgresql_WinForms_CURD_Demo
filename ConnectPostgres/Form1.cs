using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ConnectPostgres
{
    public partial class Form1 : Form
    {
        private string connstring = string.Format("Server={0};Port={1};" +
            "User Id={2};Password={3};Database={4};",
            "localhost", 5432, "postgres", "deepak", "Demo");

        private NpgsqlConnection conn;
        private string sql;
        private NpgsqlCommand cmd;
        private DataTable dt;
        private int rowIndex = -1;
        public Form1()
        {
            InitializeComponent();
        }
        private void Form1_Load_1(object sender, EventArgs e)
        {
            conn = new NpgsqlConnection(connstring);
            Select();
        }
        private void Select()
        {
            try
            {
                conn.Open();
                sql = @"select * from st_select()";
                cmd = new NpgsqlCommand(sql, conn);
                dt = new DataTable();
                dt.Load(cmd.ExecuteReader());
                dgvData.DataSource = null;
                dgvData.DataSource = dt;
                conn.Close();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Error: " + ex.Message);
            }
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }   

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void dgvData_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(e.RowIndex >= 0)
            {
                rowIndex = e.RowIndex;
                txtFirstname.Text = dgvData.Rows[e.RowIndex].Cells["firstname"].Value.ToString();
                txtMidname.Text = dgvData.Rows[e.RowIndex].Cells["midname"].Value.ToString();
                txtLastname.Text = dgvData.Rows[e.RowIndex].Cells["lastname"].Value.ToString();
            }
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
            int result = 0;
            try
            {
                conn.Open();
                sql = @"select * from st_insert(:_firstname,:_midname,:_lastname)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("_firstname", txtFirstname.Text);
                cmd.Parameters.AddWithValue("_midname", txtMidname.Text);
                cmd.Parameters.AddWithValue("_lastname", txtLastname.Text);
                result = (int)cmd.ExecuteScalar();
                conn.Close();
                if (result == 1)
                {
                    MessageBox.Show("Inserted successfully");
                    Select();
                }
                else
                {
                    MessageBox.Show("Insertion failed");
                }
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Inserted fail. Error: " + ex.Message);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if(rowIndex < 0)
            {
                MessageBox.Show("Please choose student to update"); 
            }
            else
            {
                int result = 0;
                try
                {
                    conn.Open();
                    sql = @"select * from st_update(:_id,:_firstname,:_midname,:_lastname)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_id", int.Parse(dgvData.Rows[rowIndex].Cells["id"].Value.ToString()));
                    cmd.Parameters.AddWithValue("_firstname", txtFirstname.Text);
                    cmd.Parameters.AddWithValue("_midname", txtMidname.Text);
                    cmd.Parameters.AddWithValue("_lastname", txtLastname.Text);
                    result = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Updated successfully");
                        Select();
                    }
                    else
                    {
                        MessageBox.Show("Update failed");
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Update fail. Error: " + ex.Message);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (rowIndex < 0)
            {
                MessageBox.Show("Please choose student to delete");
            }
            try
            {
                conn.Open();
                sql = @"select * from st_delete(:id)";
                cmd = new NpgsqlCommand(sql, conn);
                cmd.Parameters.AddWithValue("id", int.Parse(dgvData.Rows[rowIndex].Cells["id"].Value.ToString()));
                
                if ((int)cmd.ExecuteScalar() == 1)
                {
                    MessageBox.Show("Student data deleted successfully");
                    rowIndex = -1;
                }
                conn.Close();
                Select();
            }
            catch (Exception ex)
            {
                conn.Close();
                MessageBox.Show("Delete fail. Error:" + ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            int result = 0;

            if (rowIndex < 0 )
            {
                try
                {
                    conn.Open();
                    sql = @"select * from st_insert(:_firstname,:_midname,:_lastname)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_firstname", txtFirstname.Text);
                    cmd.Parameters.AddWithValue("_midname", txtFirstname.Text);
                    cmd.Parameters.AddWithValue("_lastname", txtMidname.Text);
                    result = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if(result == 1)
                    {
                        MessageBox.Show("Inserted successfully");
                        Select();
                    }
                    else
                    {
                        MessageBox.Show("Insertion failed"); 
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Inserted fail. Error: " + ex.Message);
                }
            }
            else
            {
                try
                {
                    conn.Open();
                    sql = @"select * from st_update(:_id,:_firstname,:_midname,:_lastname)";
                    cmd = new NpgsqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("_id", int.Parse(dgvData.Rows[rowIndex].Cells["id"].Value.ToString()));
                    cmd.Parameters.AddWithValue("_firstname", txtFirstname.Text);
                    cmd.Parameters.AddWithValue("_midname", txtMidname.Text);
                    cmd.Parameters.AddWithValue("_lastname", txtLastname.Text);
                    result = (int)cmd.ExecuteScalar();
                    conn.Close();
                    if (result == 1)
                    {
                        MessageBox.Show("Updated successfully");
                        Select();
                    }
                    else
                    {
                        MessageBox.Show("Update failed");
                    }
                }
                catch (Exception ex)
                {
                    conn.Close();
                    MessageBox.Show("Update fail. Error: " + ex.Message);
                }
            }
        }
    }
}
