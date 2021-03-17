using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ColorsDB1
{
    public partial class Form1 : Form
    {
        string connectionString;
        SqlConnection connection;
        public Form1()
        {
            InitializeComponent();
            connectionString = ConfigurationManager.ConnectionStrings["ColorsDB1.Properties.Settings.ColorsConnectionString"].ConnectionString;
        }
        private void PopualtePrimaryColos()
        {
            using (connection = new SqlConnection(connectionString))
            using (SqlDataAdapter adapter = new SqlDataAdapter("SELECT * FROM PrimaryColor", connection))
            {
                DataTable primaryTable = new DataTable();
                adapter.Fill(primaryTable);

                listPrimaryColors.DisplayMember = "PrimaryColorType";
                listPrimaryColors.ValueMember = "Id";
                listPrimaryColors.DataSource = primaryTable;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PopualtePrimaryColos();
        }
        private void PopulateShades()
        {
            string query = "SELECT Shades.ColorName FROM PrimaryColor JOIN Shades ON Shades.PrimaryColorid = PrimaryColor.Id WHERE PrimaryColor.Id = @PrimaryColorType";
            using (connection = new SqlConnection(connectionString))
            using (SqlCommand command= new SqlCommand(query, connection))
            using (SqlDataAdapter adapter = new SqlDataAdapter(command))
            {
                command.Parameters.AddWithValue("@PrimaryColorType", listPrimaryColors.SelectedValue);
                DataTable ShadeTable = new DataTable();
                adapter.Fill(ShadeTable);

                listShades.DisplayMember = "ColorName";
                listShades.ValueMember = "Id";
                listShades.DataSource = ShadeTable;
            }
        }

        private void listPrimaryColors_SelectedIndexChanged(object sender, EventArgs e)
        {
            PopulateShades();
        }
    }
}
