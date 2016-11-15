using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace Avtosalon {
    public partial class Polnya_Informacia : Form {
        
        public int id;

        public Polnya_Informacia() {
            InitializeComponent();
        }

        private void Polnya_Informacia_Load(object sender, EventArgs e) {
            MySqlConnection conn = new MySqlConnection(Server.connStr);
            conn.Open();
            string Query = "select avtomobili.id_avtomobili as 'Автомобиль', avtomobili.obyom as 'Объем',komplektacia.* " +
                "from avtomobili " +
                "inner join komplektacia on avtomobili.id_komplektacia = komplektacia.id " +
                "where avtomobili.id_avtomobili = " + id;
            MySqlCommand MSC = new MySqlCommand(Query, conn);
            MySqlDataReader reader = MSC.ExecuteReader();

            while (reader.Read()) {
                if (reader.GetString("kondicioner") == "Есть") 
                    pictureBox3.Image = Properties.Resources.galochka;
                if (reader.GetString("kojani_salon") == "Есть")
                    pictureBox4.Image = Properties.Resources.galochka;
                if (reader.GetString("legkosplavnie_diski") == "Есть")
                    pictureBox5.Image = Properties.Resources.galochka;
                if (reader.GetString("parktronik") == "Есть")
                    pictureBox6.Image = Properties.Resources.galochka;
                if (reader.GetString("podogrev_sidenii") == "Есть")
                    pictureBox7.Image = Properties.Resources.galochka;
                if (reader.GetString("navigacia") == "Есть")
                    pictureBox8.Image = Properties.Resources.galochka;
                if (reader.GetString("gromkya_svyaz") == "Есть")
                    pictureBox9.Image = Properties.Resources.galochka;
            }
            conn.Close();

        }
    }
}
