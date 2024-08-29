using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace registroElettronico
{
    public static class Deserialize
    {
        public static bool loggato = false;

        public static List<Studente> studenti;
        public static Studente studenteLoggato;

        public static Dictionary<string, List<Studente>> studentiDic = new Dictionary<string, List<Studente>>();
        

        public static List<Professore> professori;
        public static Professore professoreLoggato;

        static Deserialize()
        {
            string filePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "students.json");

            if (File.Exists(filePath))
            {
                string jsonData = File.ReadAllText(filePath);

                studenti = JsonConvert.DeserializeObject<List<Studente>>(jsonData);
            }
            else
            {
                MessageBox.Show("Error: JSON file not found.");
            }

            string filePath2 = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "teachers.json");

            if (File.Exists(filePath2))
            {
                string jsonData2 = File.ReadAllText(filePath2);

                professori = JsonConvert.DeserializeObject<List<Professore>>(jsonData2);
            }
            else
            {
                MessageBox.Show("Error: JSON file not found.");
            }
        }
    }
}
