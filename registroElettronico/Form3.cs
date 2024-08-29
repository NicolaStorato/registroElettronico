using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace registroElettronico
{
    public partial class Form3 : Form
    {
        string[] materie;
        Professore professoreLoggato;
        List<Studente> studenti;

        public Form3()
        {
            InitializeComponent();

            materie = new string[8] { "Italiano", "Storia", "Matematica", "Inglese", "Informatica", "Sistemi e Reti", "Tpsit", "Telecomunicazioni" };
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            professoreLoggato = Deserialize.professoreLoggato;
            label1.Text = "Buongiorno, " + professoreLoggato.cognome + " " + professoreLoggato.nome;
            comboBox3.Items.Add(" ");
            comboBox3.Items.AddRange(materie);

            listaStudenti();
        }

        public void listaStudenti()
        {
            studenti = Deserialize.studenti;

            foreach (var student in studenti)
            {
                if (Deserialize.studentiDic.ContainsKey(student.classe))
                {
                    Deserialize.studentiDic[student.classe].Add(student);
                }
                else 
                {
                    Deserialize.studentiDic[student.classe] = new List<Studente> { student };
                }
            }
            comboBox1.Items.AddRange(Deserialize.studentiDic.Keys.ToArray());
            comboBox1.SelectedIndex = 0;

            var nomiStudenti = Deserialize.studentiDic[comboBox1.SelectedItem.ToString()].Select(s => s.nome + " " + s.cognome).ToArray();
            comboBox2.Items.Clear();
            comboBox2.Items.Add(" ");
            comboBox2.Items.AddRange(nomiStudenti);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            var nomiStudenti = Deserialize.studentiDic[comboBox1.SelectedItem.ToString()].Select(s => s.nome + " " + s.cognome).ToArray();
            comboBox2.Items.Clear();
            comboBox2.Items.Add(" ");
            comboBox2.Items.AddRange(nomiStudenti);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            string nomeCognome = comboBox2.SelectedItem.ToString();

            var studenteSelezionato = Deserialize.studenti.FirstOrDefault(s => s.nome + " " + s.cognome == nomeCognome);

            if (studenteSelezionato != null)
            {
                aggiornaListView(studenteSelezionato);
            }
        }

        private void aggiornaListView(Studente studenteSelezionato)
        {
            listView1.Items.Clear();
            listView1.Columns.Clear();

            listView1.Font = new Font("Bahnschrift", 14);
            listView1.GridLines = true;

            listView1.View = View.Details;
            listView1.Columns.Add("Materia", 200);
            listView1.Columns.Add("Voti", 400);

            foreach (var materia in studenteSelezionato.voti)
            {
                string votiString = string.Join("   ", materia.Value.Select(v => v.ToString()));
                ListViewItem item = new ListViewItem(materia.Key);
                item.SubItems.Add(votiString);
                listView1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox2.SelectedItem == null || comboBox3.SelectedItem == " ")
            {
                MessageBox.Show("Please select a student, a subject, and a valid grade.");
                return;
            }

            string nomeCognome = comboBox2.SelectedItem.ToString();

            var studenteSelezionato = studenti.FirstOrDefault(s => s.nome + " " + s.cognome == nomeCognome);

            string materiaSelezionata = comboBox3.SelectedItem.ToString();

            int voto = (int)numericUpDown1.Value;

            if (studenteSelezionato != null)
            {
                if (studenteSelezionato.voti.ContainsKey(materiaSelezionata))
                {
                    studenteSelezionato.voti[materiaSelezionata].Add(voto);
                }
                else
                {
                    studenteSelezionato.voti[materiaSelezionata] = new List<int> { voto };
                }

                SalvaDatiStudenti();

                aggiornaListView(studenteSelezionato);

                MessageBox.Show("Grade added successfully.");
            }
            else
            {
                MessageBox.Show("Selected student not found.");
            }
        }

        public void SalvaDatiStudenti()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "students.json");

            try
            {
                string jsonData = JsonConvert.SerializeObject(studenti, Formatting.Indented);

                File.WriteAllText(filePath, jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            var nuovoStudente = new Studente
            {
                matricola = (int)numericUpDown2.Value,
                nome = textBox1.Text,
                cognome = textBox2.Text,
                dataNascita = dateTimePicker1.Value,
                luogoNascita = textBox3.Text,
                classe = textBox4.Text,
                voti = new Dictionary<string, List<int>>()  
            };

            Deserialize.studenti.Add(nuovoStudente);

            SalvaDatiStudenti(Deserialize.studenti);

            resetCampiRefreshLista();
        }

        public static void SalvaDatiStudenti(List<Studente> studenti)
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "students.json");

            try
            {
                string jsonData = JsonConvert.SerializeObject(studenti, Formatting.Indented);

                File.WriteAllText(filePath, jsonData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error saving data: " + ex.Message);
            }
        }

        private void resetCampiRefreshLista()
        {
            textBox1.Text = string.Empty;  
            textBox2.Text = string.Empty;  
            textBox3.Text = string.Empty;  
            textBox4.Text = string.Empty;  
            numericUpDown2.Value = numericUpDown2.Minimum;  
            dateTimePicker1.Value = DateTime.Now;  

            comboBox1.Items.Clear(); 

            Deserialize.studentiDic.Clear();
            foreach (var studente in Deserialize.studenti)
            {
                if (Deserialize.studentiDic.ContainsKey(studente.classe))
                {
                    Deserialize.studentiDic[studente.classe].Add(studente);
                }
                else
                {
                    Deserialize.studentiDic[studente.classe] = new List<Studente> { studente };
                }
            }

            comboBox1.Items.AddRange(Deserialize.studentiDic.Keys.ToArray());
            if (comboBox1.Items.Count > 0)
            {
                comboBox1.SelectedIndex = 0;
            }

            aggiornaListaStudenti();
        }

        private void aggiornaListaStudenti()
        {
            comboBox2.Items.Clear();

            string classeSelezionata = comboBox1.SelectedItem?.ToString();

            if (classeSelezionata != null && Deserialize.studentiDic.ContainsKey(classeSelezionata))
            {
                var nomiStudenti = Deserialize.studentiDic[classeSelezionata].Select(s => s.nome + " " + s.cognome).ToArray();
                comboBox2.Items.AddRange(nomiStudenti);
            }

            if (comboBox2.Items.Count > 0)
            {
                comboBox2.SelectedIndex = 0;
            }
        }
    }
}
