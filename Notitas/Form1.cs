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


namespace Notitas
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
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

        private void button1_Click(object sender, EventArgs e)
        {
            //obtener datos de textboxes
            string names = textBox1.Text;
            string lastname = textBox2.Text;
            string age = textBox3.Text;
            string height = textBox4.Text;
            string phone = textBox5.Text;
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
    }
}
