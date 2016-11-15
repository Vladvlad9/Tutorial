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
    public partial class Form2 : Form {

        public static MySqlConnection conn = new MySqlConnection(Server.connStr);

        public Form2() {
            InitializeComponent();
        }

        public static int Check(ref MySqlConnection connektion, string table, string curname) {
            int result = -5;
            string SQLcheck = "select * from " + table;

            MySqlCommand checkcomm = new MySqlCommand(SQLcheck, connektion);
            MySqlDataReader checkrider = checkcomm.ExecuteReader();

            while (checkrider.Read()) {
                if (checkrider.GetString("Nazvanie") == curname) {
                    result = checkrider.GetInt32("id");
                }
            }
            checkrider.Close();
            return result;
            
        }

        private void button1_Click(object sender, EventArgs e) {
            conn.Open();
            int idMarka = Check(ref conn, "marka", comboBox1.Text);
            int idModel = Check(ref conn, "model", textBox2.Text);
            int idKuzov = Check(ref conn, "kuzov", textBox1.Text);
            int idDvigatel = Check(ref conn, "tip_dvigatelya", textBox3.Text);
            int idPeredachi = Check(ref conn, "korobka_peredach", textBox9.Text);
            int idPrivod = Check(ref conn, "privod", textBox10.Text);

            if (idMarka == -5) {//Марка
                string SQLzapros = "insert into marka (`Nazvanie`) values ('" + this.comboBox1.Text + "')";

                MySqlCommand MSC = new MySqlCommand(SQLzapros, conn);
                MSC.ExecuteNonQuery();

                idMarka = Check(ref conn, "marka", comboBox1.Text);
            }

            if (idModel == -5) {//модель
                string SQLzaprosModel = "insert into model (`Nazvanie`) values ('" + this.textBox2.Text + "')";

                MySqlCommand MSC = new MySqlCommand(SQLzaprosModel, conn);
                MSC.ExecuteNonQuery();

                idModel = Check(ref conn, "model", textBox2.Text);
            }

            if (idKuzov == -5) {//Кузов
                string SQLzaprosKuzov = "insert into kuzov (`Nazvanie`) values ('" + this.textBox1.Text + "')";

                MySqlCommand MSC = new MySqlCommand(SQLzaprosKuzov, conn);
                MSC.ExecuteNonQuery();

                idKuzov = Check(ref conn, "kuzov", textBox1.Text);
                
            }

            if (idDvigatel == -5) {//Двигатель
                string SQLzaprosDvigatelya = "insert into tip_dvigatelya (`Nazvanie`) values ('" + this.textBox3.Text + "')";

                MySqlCommand MSC = new MySqlCommand(SQLzaprosDvigatelya, conn);
                MSC.ExecuteNonQuery();

                idDvigatel = Check(ref conn, "tip_dvigatelya", textBox3.Text);
                
            }
            

            if (idPeredachi == -5) {//Передачи
                string SQLzaprosPeredachi = "insert into korobka_peredach (`Nazvanie`) values ('" + this.textBox9.Text + "')";

                MySqlCommand MSC = new MySqlCommand(SQLzaprosPeredachi, conn);
                MSC.ExecuteNonQuery();

                idPeredachi = Check(ref conn, "korobka_peredach", textBox9.Text);
                
            }

            if (idPrivod == -5) {//Привод
                string SQLzaprosPrivod = "insert into privod (`Nazvanie`) values ('" + this.textBox10.Text + "')";

                MySqlCommand MSC = new MySqlCommand(SQLzaprosPrivod, conn);
                MSC.ExecuteNonQuery();

                idPrivod = Check(ref conn, "privod", textBox10.Text);
                
            }

            string SQLzaprosavto = "INSERT INTO avtomobili(`id_marka`, `id_model`, `id_kuzov`, `id_tip_dvigatela`, `god_vipuska`, " +
                "`max_skorost`, `obyom`, `tsena`, `fakticheskya_massa`, `id_korobka_peredach`, `id_privod`, `id_sklad`, `id_komplektacia`) " +
                "VALUES ('" + idMarka + "','" + idModel + "','" + idKuzov + "', '" + idDvigatel + "','2014', '200', '2', '2000', '1500', '" + idPeredachi + "', '" + idPrivod + "', '0', '5')";
            

            MySqlCommand MSC1 = new MySqlCommand(SQLzaprosavto, conn);
            MSC1.ExecuteNonQuery();
            conn.Close();

        }

        private void Form2_Load(object sender, EventArgs e) {
            Form1 form1 = this.Owner as Form1;

            MySqlConnection connec = new MySqlConnection(Server.connStr);

            connec.Open();
            MySqlCommand sqlcomannd = new MySqlCommand("SELECT Nazvanie FROM avtosalon.marka", connec);
            MySqlDataReader rider = sqlcomannd.ExecuteReader();
            while (rider.Read()) {
                comboBox1.Items.Add(rider.GetString("Nazvanie"));
            }
            connec.Close();
        }
    }
}
