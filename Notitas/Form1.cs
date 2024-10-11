using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using MySql.Data.MySqlClient; //importar mysql.data.dll


namespace Notitas
{
    public partial class Form1 : Form
    {
        //Declaración de datos para la conexión a la base de datos de XAMPP
        string Sqlconnection = "Server=localhost;port=3306;Database=avanzaprogra;Uid=root;Pwd=laluznu7S!;";
        //metodo para insertar registros

        public Form1()
        {
            InitializeComponent();

            //event handlers for textboxes
            textBox1.Leave += checkNames;
            textBox2.Leave += checkNames;
            textBox3.Leave += checkAge;
            textBox4.Leave += checkHeight;
            //tbPhone.TextChanged += checkPhone;
            textBox5.Leave += checkPhone;
        }

        private void insertarRegistro(string names, string lastname, int age, float height, long phone, string sexo)
        {
            using (MySqlConnection conection = new MySqlConnection(Sqlconnection))
            {
                conection.Open();
                //Mismo orden que esta en .....
                string insertQuery = "INSERT INTO tabla (notname, lastname, age, height, phone, sexo)" +
                                     "Values (@names, @lastname, @age, @height, @phone, @sexo)";

                using (MySqlCommand comando = new MySqlCommand(insertQuery, conection))
                {
                    comando.Parameters.AddWithValue("@names", names);
                    comando.Parameters.AddWithValue("@lastname", lastname);
                    comando.Parameters.AddWithValue("@age", age);
                    comando.Parameters.AddWithValue("@height", height);
                    comando.Parameters.AddWithValue("@phone", phone);
                    comando.Parameters.AddWithValue("@sexo", sexo);

                    comando.ExecuteNonQuery();
                }
                conection.Close();
            }
        }

        private bool isValidInt(string str)
        {
            int result;
            return int.TryParse(str, out result);
        }
        private bool isValidFloat(string str)
        {
            decimal result;
            return decimal.TryParse(str, out result);
        }
        private bool isValidTenDigitNum(string str)
        {
            long result;
            return long.TryParse(str, out result) && str.Length == 10;
        }
        private bool isValidText(string str)
        {
            return Regex.IsMatch(str, @"^[a-zA-Z\s]+$");
        }

        private void checkAge(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length != 0)
            {
                if (!isValidInt(textBox.Text))
                {
                    MessageBox.Show("Ingrese un valor valido para edad", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Clear();
                }
            }
        }

        private void checkHeight(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length != 0)
            {
                if (!isValidFloat(textBox.Text))
                {
                    MessageBox.Show("Ingrese un valor valido para altura", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Clear();
                }
            }
        }

        private void checkNames(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length != 0)
            {
                if (!isValidText(textBox.Text))
                {
                    MessageBox.Show("Ingrese un valor valido para nombre y apellido", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Clear();
                }
            }
        }

        private void checkPhone(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;
            if (textBox.Text.Length != 0)
            {
                if (!isValidTenDigitNum(textBox.Text))
                {
                    MessageBox.Show("Ingrese un valor valido para un número de 10 digitos", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    textBox.Clear();
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e) //save
        {
            //obtener datos de textboxe
            string names = textBox1.Text;
            string lastname = textBox2.Text;
            int age = int.Parse(textBox3.Text);
            float height = float.Parse(textBox4.Text);
            long phone = long.Parse(textBox5.Text);
            //obtener datos de checkboxes
            string sexo = "";
            if (checkBox1.Checked)
            {
                sexo = "Hombre";
            }
            else if (checkBox2.Checked) 
            {
                sexo = "Mujer";
            }

            //escribir cadena de datos
            string data = $"Nombre: {names}\r\nApellidos: {lastname}\r\nEdad: {age}\r\nAltura: {height}\r\nTelefono: {phone}\r\nSexo: {sexo}";

            //Guardar datos en un archivo TXT
            string rutaArchivo = "C:/Users/Lenovo/Documents/dato shark/datasa.txt";
            //verifica si archivo existe
            bool archivoExiste = File.Exists(rutaArchivo);

            using (StreamWriter writer = new StreamWriter(rutaArchivo, true))
            {
                if (archivoExiste)
                {
                    //si el archivo existe, añadir un separador del nuevo registro.
                    writer.WriteLine();
                    
                }
                writer.WriteLine(data);
            }
            //mostrar mensajes con los datos capturados
            insertarRegistro(names, lastname, age, height, phone, sexo);

            MessageBox.Show("Datos guardados con éxito: \n\n" + data, "Información", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }

        private void button2_Click(object sender, EventArgs e)  
        {
            //borrar los datos escritos
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
            textBox5.Clear();
            checkBox1.Checked = false;
            checkBox2.Checked = false;

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}