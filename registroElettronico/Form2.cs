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

namespace registroElettronico
{
    public partial class Form2 : Form
    {
        Studente studenteLoggato;
        public Form2()
        {
            InitializeComponent();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            studenteLoggato = Deserialize.studenteLoggato;

            label1.Text = "Valutazioni di " + studenteLoggato.cognome + " " + studenteLoggato.nome;

            listView1.Font = new Font("Bahnschrift", 14);
            listView1.GridLines = true;

            listView1.View = View.Details;
            listView1.Columns.Add("Materia", 200);
            listView1.Columns.Add("Voti", 400);

            foreach (var materia in studenteLoggato.voti)
            {
                string votiString = string.Join("   ", materia.Value.Select(v => v.ToString()));
                ListViewItem item = new ListViewItem(materia.Key);
                item.SubItems.Add(votiString);
                listView1.Items.Add(item);
            }

        }
    }
}
