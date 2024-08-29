using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using registroElettronico;
using System.IO;
using registroElettronico.Properties;

namespace registroElettronico
{
    public partial class Form1 : Form
    {
        List<Studente> studenti;
        List<Professore> professori;
        bool passwordVisibile = false;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            studenti = Deserialize.studenti;
            professori = Deserialize.professori;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            string username = txtUserName.Text;  
            string password = txtpassword.Text;  

            var parts = username.Split('_');
            if (parts.Length == 2)
            {
                string cognome = parts[0];
                string nome = parts[1];

                var studente = studenti.FirstOrDefault(s => s.cognome == cognome && s.nome == nome && s.matricola.ToString() == password);
                var professore = professori.FirstOrDefault(p => p.cognome == cognome && p.nome == nome && p.matricola.ToString() == password);
                

                if (studente != null)
                {
                    Deserialize.studenteLoggato = studente;
                    new Form2().Show();
                    this.Hide();
                    Deserialize.loggato = true;
                }
                else if(professore != null)
                {
                    Deserialize.professoreLoggato = professore;
                    new Form3().Show();
                    this.Hide();
                    Deserialize.loggato = true;
                }
                else
                {
                    MessageBox.Show("Login failed. Incorrect username or password.");
                }
            }
            else
            {
                MessageBox.Show("Invalid username format. Use 'cognome_nome'.");
            }
        }

        private void btnPassword_Click(object sender, EventArgs e)
        {
            if (passwordVisibile)
            {
                passwordVisibile = false;
                txtpassword.UseSystemPasswordChar = true;
                btnPassword.BackgroundImage = Resources.visualizzabile;
            }
            else
            {
                passwordVisibile = true;
                txtpassword.UseSystemPasswordChar = false;
                btnPassword.BackgroundImage = Resources.nonvisualizzabile;
            }
        }
    }
}
