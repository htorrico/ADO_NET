using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ADO_NET
{
    public partial class Form1 : Form
    {
        SqlConnection connection = new SqlConnection("Data Source=DESKTOP-8DIVAMC;Initial Catalog=TecsupDB;Integrated Security=True");
        public Form1()
        {
            InitializeComponent();
        }


        private void btnConectar_Click(object sender, EventArgs e)
        {
            try
            {
                              
                connection.Open();

                MessageBox.Show("Conexión Correcta");
            }
            catch (Exception)
            {
                MessageBox.Show("Conexión Incorrecta");
                //throw;
            }
            



        }

        private void btnDesconectar_Click(object sender, EventArgs e)
        {
            try
            {

                connection.Close();

                MessageBox.Show("Conexión Cerrada");
            }
            catch (Exception)
            {
                MessageBox.Show("Conexión Cerrada Error");
                //throw;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            //Forma desconectada
            
            connection.Open();
            DataTable dataTable = new DataTable();
            SqlCommand command = new SqlCommand("Select * from people", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(dataTable);
            connection.Close();

            dgvPeople.DataSource = dataTable;


        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Forma Conectada
            List<Person> people = new List<Person>();

            connection.Open();
            SqlCommand command = new SqlCommand("Select * from people", connection);
            SqlDataReader dataReader = command.ExecuteReader();
            

            while (dataReader.Read())
            {
                people.Add(new Person
                {
                    PersonId = dataReader[0].ToString(),
                    LastName = dataReader[1].ToString(),
                    FirstName = dataReader[2].ToString(),
                });
                
            }

            connection.Close();
            dgvPeople2.DataSource = people;



        }

        private void button3_Click(object sender, EventArgs e)
        {

            //Forma Conectada Procedimiento Almacenado
            List<Person> people = new List<Person>();


            
            connection.Open();
            SqlCommand command = new SqlCommand("USP_GetPeople", connection);
            command.CommandType = CommandType.StoredProcedure;

            SqlParameter parameter1 = new SqlParameter();
            parameter1.SqlDbType = SqlDbType.VarChar;
            parameter1.Size = 50;
            parameter1.Value = txtLastName.Text.Trim();
            parameter1.ParameterName = "@LastName";


            SqlParameter parameter2 = new SqlParameter();
            parameter2.SqlDbType = SqlDbType.VarChar;
            parameter2.Size = 50;
            parameter2.Value = txtFirstName.Text.Trim();
            parameter2.ParameterName = "@FistName";


            command.Parameters.Add(parameter1);
            command.Parameters.Add(parameter2);



            SqlDataReader dataReader = command.ExecuteReader();


            while (dataReader.Read())
            {
                people.Add(new Person
                {
                    PersonId = dataReader[0].ToString(),
                    LastName = dataReader[1].ToString(),
                    FirstName = dataReader[2].ToString(),
                });

            }

            connection.Close();
            dgvPeople3.DataSource = people;




        }

        private void button4_Click(object sender, EventArgs e)
        {

            try
            {

               
                connection.Open();
                
                SqlCommand command = new SqlCommand("USP_InsPeople", connection);
                command.CommandType = CommandType.StoredProcedure;

                SqlParameter parameter1 = new SqlParameter();
                parameter1.SqlDbType = SqlDbType.VarChar;
                parameter1.Size = 50;
                parameter1.Value = txtLastName.Text.Trim();
                parameter1.ParameterName = "@LastName";

                SqlParameter parameter2 = new SqlParameter();
                parameter2.SqlDbType = SqlDbType.VarChar;
                parameter2.Size = 50;
                parameter2.Value = txtFirstName.Text.Trim();
                parameter2.ParameterName = "@FirstName";

                command.Parameters.Add(parameter1);
                command.Parameters.Add(parameter2);

                command.ExecuteNonQuery();
                connection.Close();
                MessageBox.Show("Persona Registrada");


            }
            catch (Exception)
            {
                MessageBox.Show("Error");
                
            }

           

        }
    }
}
