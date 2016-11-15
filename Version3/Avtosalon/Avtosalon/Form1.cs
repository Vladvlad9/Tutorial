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
    public partial class Form1 : Form {

        int id = 0;
        public int nomerKnopki = 0;
        public PictureBox[] massPict;
        public PictureBox curButton;
        public int speed = 0;
        public string connStr;
        public static MySqlConnection conn = new MySqlConnection(Server.connStr);
        public bool podrobno = false;

        public Form1() {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e) {
            massPict = new PictureBox[6];

            massPict[0] = Nazad_pictureBox;
            massPict[1] = Avtomobili_pictureBox;
            massPict[2] = Klienti_pictureBox;
            massPict[3] = Sklad_pictureBox;
            massPict[4] = Zayavki_pictureBox;
            massPict[5] = Postavschiki_pictureBox;


        }

        private void Osnovnii_Knopki_timer_Tick(object sender, EventArgs e) {//Основной таймер Для главных кнопок

            curButton.Location = new Point(curButton.Location.X, curButton.Location.Y - speed);
            if (curButton.Location.Y <= 116) {
                Osnovnii_Knopki_timer.Stop();
                Dopol_Knopki_timer.Start();
                Dop_Avto_Panel.Start();
            }
        }

        private void Dopol_Knopki_timer_Tick(object sender, EventArgs e) {//Дополнительная панель с кнопками 
            if (curButton.Name == "Avtomobili_pictureBox" || curButton.Name == "Klienti_pictureBox") {
                Dopol_Knopki_timer.Stop();
                panel1.Size = new Size(0, panel1.Height);
            }
            
            panel1.Size = new Size(panel1.Width + 10, panel1.Height);
            if (panel1.Width >= 190) 
                Dopol_Knopki_timer.Stop();
        }

        private void Dop_Avto_Panel_Tick(object sender, EventArgs e) {
            if (curButton.Name == "Klienti_pictureBox" || curButton.Name == "Sklad_pictureBox" || curButton.Name == "Zayavki_pictureBox" || curButton.Name == "Postavschiki_pictureBox") {
                Dop_Avto_Panel.Stop();
                panel2.Size = new Size(0, panel1.Height);
            }
            panel2.Size = new Size(panel2.Width + 10, panel2.Height);
            if (panel2.Width >= 190)
                Dop_Avto_Panel.Stop();
        }

        private void Poisk_timer_Tick(object sender, EventArgs e) {
            if (curButton.Name == "Klienti_pictureBox" || curButton.Name == "Sklad_pictureBox" || curButton.Name == "Zayavki_pictureBox" || curButton.Name == "Postavschiki_pictureBox") {
                Poisk_timer.Stop();
                groupBox3.Size = new Size(groupBox3.Width, 0);
            }
            groupBox3.Size = new Size(groupBox3.Width, groupBox3.Height + 10);
            if (groupBox3.Height >= 200) {
                Poisk_timer.Stop();
            }
        }

        private void Poisk_pictureBox_Click(object sender, EventArgs e) {//Кнопка поиск
            Poisk_timer.Start();
        }

        private void Nazad_pictureBox_Click(object sender, EventArgs e) {//Кнопка Назад
            Funkcii.Otobrazit(false, massPict, 0);
            Funkcii.ButtonsOFf(true, massPict);

            Avtomobili_pictureBox.Location = new Point(26, 140);
            Klienti_pictureBox.Location = new Point(26, 250);
            Sklad_pictureBox.Location = new Point(26, 354);
            Zayavki_pictureBox.Location = new Point(26, 465);
            Postavschiki_pictureBox.Location = new Point(26, 566);

            panel1.Size = new Size(0, panel1.Height);
            panel2.Size = new Size(0, panel2.Height);
            groupBox3.Size = new Size(groupBox3.Width, 0);
        }

        private void Avtomobili_pictureBox_Click(object sender, EventArgs e) {//Кнопка Автомобили
            dataGridView1.Size = new Size(1145,290);
            speed = 5;
            curButton = Avtomobili_pictureBox;
            Funkcii.Otobrazit(true, massPict, 1);
            Funkcii.ButtonsOFf(false, massPict);
            Osnovnii_Knopki_timer.Start();

            conn.Open();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DA.SelectCommand = new MySqlCommand("select avtomobili.id_avtomobili as 'Автомобиль №', marka.nazvanie as 'Марка', model.nazvanie as 'Модель'," +
                "kuzov.nazvanie as 'Кузов', god_vipuska as 'Год выпуска', korobka_peredach.nazvanie as 'Коробка передач', privod.nazvanie as 'Привод'," + 
                "max_skorost as 'Максимальная скорость', obyom as 'Обьем двигателя', tsena as 'Цена', fakticheskya_massa as 'Фактическая масса'" +
                "from avtomobili " +
                "inner join marka on avtomobili.id_marka = marka.id " +
                "inner join kuzov on avtomobili.id_kuzov = kuzov.id " +
                "inner join model on avtomobili.id_model = model.id " +
                "inner join privod on avtomobili.id_privod = privod.id " +
                "inner join korobka_peredach on avtomobili.id_korobka_peredach = korobka_peredach.id " +
                "inner join tip_dvigatelya on avtomobili.id_tip_dvigatela = tip_dvigatelya.id ", conn);

            DataTable table = new DataTable();
            DA.Fill(table);

            BindingSource BS = new BindingSource();
            BS.DataSource = table;

            dataGridView1.DataSource = BS;

            id = Convert.ToInt32(dataGridView1.Rows[0].Cells[0].Value);
            conn.Close();
        }

        private void Klienti_pictureBox_Click(object sender, EventArgs e) {//Кнопка Клиенты
            speed = 10;
            curButton = Klienti_pictureBox;
            Funkcii.Otobrazit(true, massPict, 2);
            Funkcii.ButtonsOFf(false, massPict);
            Osnovnii_Knopki_timer.Start();

            conn.Open();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DA.SelectCommand = new MySqlCommand("SELECT id_kliyenty as 'Клиент №', фамилия as 'Фамилия', имя as 'Имя', отчество as 'Отчество', " + 
                "телефон as 'Телефон', adres.gorod as 'Город', adres.ulitsa as 'Улица', adres.dom as 'Дом', adres.kvartira as 'Квартира', " + 
                "sayavka.дата as 'Дата', sayavka.сумма  as 'Сумма'" +
                "FROM kliyenty " +
                "inner join adres on kliyenty.id_adres = adres.id " +
                "inner join sayavka on sayavka.id_sayavka = kliyenty.id_zayavka ", conn);
            DataTable table = new DataTable();
            DA.Fill(table);

            BindingSource BS = new BindingSource();
            BS.DataSource = table;

            dataGridView1.DataSource = BS;
            conn.Close();
        }

        private void Sklad_pictureBox_Click(object sender, EventArgs e) {//Кнопка Склад
            speed = 15;
            curButton = Sklad_pictureBox;
            Funkcii.Otobrazit(true, massPict, 3);
            Funkcii.ButtonsOFf(false, massPict);
            Osnovnii_Knopki_timer.Start();

            conn.Open();
            MySqlDataAdapter DA = new MySqlDataAdapter();
            DA.SelectCommand = new MySqlCommand("SELECT id_sayavka, rabotnik_avtosalona.familiya, rabotnik_avtosalona.imya, rabotnik_avtosalona.otchestvo, дата, сумма, sayavka.id_avtomobila, kliyenty.фамилия, kliyenty.имя, kliyenty.отчество " +
                "FROM sayavka " +
                "inner join rabotnik_avtosalona on sayavka.id_rabotnik = rabotnik_avtosalona.id_rabotnik_avtosalona " +
                "inner join kliyenty on sayavka.id_klienta = kliyenty.id_kliyenty ", conn);
            DataTable table = new DataTable();
            DA.Fill(table);

            BindingSource BS = new BindingSource();
            BS.DataSource = table;

            dataGridView1.DataSource = BS;
            conn.Close();
        }

        private void Zayavki_pictureBox_Click(object sender, EventArgs e) {//Кнопка Заявка
            speed = 20;
            curButton = Zayavki_pictureBox;
            Funkcii.Otobrazit(true, massPict, 4);
            Funkcii.ButtonsOFf(false, massPict);
            Osnovnii_Knopki_timer.Start();
        }

        private void Postavschiki_pictureBox_Click(object sender, EventArgs e) {//Кнопка Поставщики
            speed = 20;
            curButton = Postavschiki_pictureBox;
            Funkcii.Otobrazit(true, massPict, 5);
            Funkcii.ButtonsOFf(false, massPict);
            Osnovnii_Knopki_timer.Start();
        }

        private void Dobavit_pictureBox_Click(object sender, EventArgs e) {
            Form2 form2 = new Form2();
            form2.Owner = this;
            form2.ShowDialog();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e) {
            id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells[0].Value);
        }

        private void Polnya_Infa_pictureBox_Click(object sender, EventArgs e) {
            Polnya_Informacia form3 = new Polnya_Informacia();
            form3.Owner = this;
            
            form3.id = id;

            conn.Open();
            string Query = "select marka.nazvanie as 'Марка', model.nazvanie as 'Модель', tip_dvigatelya.Nazvanie as 'Название', komplektacia.cvet as 'Цвет', " +
                "kuzov.nazvanie as 'Кузов', god_vipuska as 'Год выпуска', privod.nazvanie as 'Привод', " +
                "obyom as 'Обьем двигателя', tsena as 'Цена'" +
                "from avtomobili " +
                "inner join marka on avtomobili.id_marka = marka.id " +
                "inner join model on avtomobili.id_model = model.id " +
                "inner join kuzov on avtomobili.id_kuzov = kuzov.id " +
                "inner join privod on avtomobili.id_privod = privod.id " +
                "inner join tip_dvigatelya on avtomobili.id_tip_dvigatela = tip_dvigatelya.id " +
                "inner join komplektacia on avtomobili.id_komplektacia = komplektacia.id " +
                "where id_avtomobili = " + id;
            MySqlCommand MSC = new MySqlCommand(Query, conn);
            MySqlDataReader reader = MSC.ExecuteReader();
            while (reader.Read()) {
                form3.label1.Text = reader.GetString("Марка");
                form3.label2.Text = reader.GetString("Цена");
                form3.label3.Text = reader.GetString("Кузов");
                form3.label4.Text = reader.GetString("Обьем двигателя");
                form3.label5.Text = reader.GetString("Название");
                form3.label6.Text = reader.GetString("Год выпуска");
                form3.label7.Text = reader.GetString("Привод");
                form3.label8.Text = reader.GetString("Цвет");
                form3.label10.Text = reader.GetString("Модель");
            }
            form3.ShowDialog();
            conn.Close();
            podrobno = false;
        }

        
    }
}
