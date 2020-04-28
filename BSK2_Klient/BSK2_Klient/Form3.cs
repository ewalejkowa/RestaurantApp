using BSK2_Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace BSK2_Klient
{
    public partial class Form3 : Form
    {
        IService1 client;
        Form2 logged_in;
        string login;
        public Form3(string login)
        {
            this.login = login;
            InitializeComponent();
            this.MinimumSize = new Size(1000, 700);
            this.MaximumSize = new Size(1000, 700);
            label1.Text = "Zalogowany jako: "+login;
            var fact = new ChannelFactory<IService1>(new WebHttpBinding(), new EndpointAddress("http://localhost:1101/bsk"));
            fact.Endpoint.Behaviors.Add(new WebHttpBehavior());
            client = fact.CreateChannel();
            DataTable dt = new DataTable();

            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            dataGridView1.DataSource = dt;
            //dopasowanie kolumn do conetntu
            dataGridView1.AutoResizeColumns();
            dataGridView1.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.AllCells;
            button8.Text = "Wróć";

            //dodanie buttonów do funkcji
            //usuwanie
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Funkcje";
            btn.Text = "Usuń";
            btn.Name = "Buttons_usun";
            btn.UseColumnTextForButtonValue = true;
            btn.DataPropertyName = "Buttons_usun";
            dataGridView1.Columns.Add(btn);


            //edytowanie
            DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn2);
            btn2.HeaderText = "Funkcje";
            btn2.Text = "Edytuj";
            btn2.Name = "Buttons_edit";
            btn2.UseColumnTextForButtonValue = true;
            btn2.DataPropertyName = "Buttons_edit";

            //grid readonly
            dataGridView1.ReadOnly = true;
            //dodanie event na kliknięcie w komórce
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            show_table("ZAMOWIENIE");
            textBox15.Text = client.averageOrderValue().ToString();
            

        }

        //powrót do wcześniejszego widoku
        private void button8_Click(object sender, EventArgs e)
        {
            logged_in = new Form2(this.login, "a");
            this.Visible = false;
            logged_in.ShowDialog();
        }

        private void hide_add_controls()
        {
            textBox8.Visible = false;
            textBox9.Visible = false;
            textBox10.Visible = false;
            textBox11.Visible = false;
            textBox12.Visible = false;
            textBox13.Visible = false;
            textBox14.Visible = false;
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            label18.Visible = false;
            label19.Visible = false;
            comboBox3.Visible = false;
            comboBox4.Visible = false;
            button3.Visible = false;
            button1.Text = "Dodaj nowy rekord";
        }
        private void show_add_controls(string name)
        {
            var fact = new ChannelFactory<IService1>(new WebHttpBinding(), new EndpointAddress("http://localhost:1101/bsk"));
            fact.Endpoint.Behaviors.Add(new WebHttpBehavior());
            client = fact.CreateChannel();

                label12.Text = "ID_ZAMOWIENIA";
                label13.Text = "CENA";
                label14.Text = "DATA_ZLOZENIA";
                label15.Text = "UWAGI";
                label16.Text = "CZY_ZAPLACONE";
                label17.Text = "FK_STATUS";
                label19.Text = "FK_PRACOWNIK_PRZYJMUJACY";
                label12.Visible = true;
                label14.Visible = true;
                label13.Visible = true;
                label19.Visible = true;
                label15.Visible = true;
                label16.Visible = true;
                label17.Visible = true;

                textBox8.Text = (client.GetLabel("ZAMOWIENIE") + 1).ToString();
                textBox8.Visible = true;
                textBox9.Visible = true;
                textBox10.Visible = true;
                textBox11.Visible = true;
                textBox12.Visible = true;
                textBox13.Visible = true;
                textBox14.Visible = true;
            
            }
      

        //dodawanie
        private void button1_Click(object sender, EventArgs e)
        {
            if (button1.Text == "Dodaj nowy rekord")
            {
                button3.Visible = true;
                button1.Text = "Zapisz";
                //show controls
                show_add_controls(dataGridView1.Name);

            }
            else
            {
                DialogResult res = MessageBox.Show("Czy chcesz dodać wiersz?", "Dodawanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //nie
                if (res == DialogResult.No) { }
                //tak
                else
                {
                    //dodawanie do bazy
                    var fact = new ChannelFactory<IService1>(new WebHttpBinding(), new EndpointAddress("http://localhost:1101/bsk"));
                    fact.Endpoint.Behaviors.Add(new WebHttpBehavior()); 
                    var client = fact.CreateChannel();

                        //byte[] c =new byte[0];
                    client.add_row(dataGridView1.Name, textBox9.Text, label13.Text, textBox10.Text , label14.Text,
    textBox11.Text, label15.Text, textBox12.Text, label16.Text, textBox13.Text, label17.Text, textBox14.Text, label19.Text, "","",new byte[0], "");

                    
                    show_table(dataGridView1.Name);
                    button1.Text = "Dodaj nowy rekord";
                    button3.Visible = false;
                    hide_add_controls();
                }
               
            }

        }

        private void button3_Click(object sender, EventArgs e)
        {
            //hide add controls           
            hide_add_controls();
        }

        //funkcja wyświtlająca tabelę po stringu
        public void show_table(string name)
        {
            var fact = new ChannelFactory<IService1>(new WebHttpBinding(), new EndpointAddress("http://localhost:1101/bsk"));
            fact.Endpoint.Behaviors.Add(new WebHttpBehavior());

            var client = fact.CreateChannel();
            int labelnumber;
            DataTable dt = new DataTable();
            dt = client.GetTable(name);
            dataGridView1.DataSource = dt;
            //nadanie nazwy widokowi
            dataGridView1.Name = name;
            //labelnumber = client.GetTableLabel(name);

            // wyświatlanie kolumny z funkcajmi jako ostatnią, trochę ułomne, ale sie poprawi
            foreach (DataGridViewColumn col in dataGridView1.Columns)
            {
                if ((col.DataPropertyName == "Buttons_edit" || col.DataPropertyName == "Buttons_usun") && col.DisplayIndex != dataGridView1.Columns.Count - 1)
                {
                    if (col.DataPropertyName == "Buttons_edit")
                    {
                        col.DisplayIndex = dataGridView1.Columns.Count - 1;
                    }
                    else col.DisplayIndex = dataGridView1.Columns.Count - 1;
                    continue;
                }
            }



        }
        private void button6_Click_1(object sender, EventArgs e)
        {
            //log off
            this.Visible = false;
            Form1 main_window = new BSK2_Klient.Form1();
            main_window.ShowDialog();
        }
        //tabele
        private void button5_Click(object sender, EventArgs e)
        {
            show_table("ZAMOWIENIE");
            hideControls();
            hide_add_controls();
        }

        //użytkownicy
        private void button2_Click(object sender, EventArgs e)
        {
            show_table("Pracownik");
            hideControls();
            hide_add_controls();
        }
        //usuwanie
        private void button_usun(string name_of_table, string name_of_key, string name_of_column)
        {
            var fact = new ChannelFactory<IService1>(new WebHttpBinding(), new EndpointAddress("http://localhost:1101/bsk"));
            fact.Endpoint.Behaviors.Add(new WebHttpBehavior()); 
            var client = fact.CreateChannel();
            client.delete_row(name_of_table, name_of_key, name_of_column);
        }
        //w przypadku kliknięcia na dany wiersz
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridView dgv = sender as DataGridView;
            if (dgv == null)
                return;
            //edytowanie
            if (e.ColumnIndex == this.dataGridView1.Columns["Buttons_edit"].Index && e.RowIndex > -1 && dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
            {
               edit(e.RowIndex);
            }
            //usuwanie
            if (e.ColumnIndex == this.dataGridView1.Columns["Buttons_usun"].Index && e.RowIndex > -1 && dataGridView1.Rows[e.RowIndex].Cells[0].Value != null)
            {
                DialogResult res = MessageBox.Show("Czy chcesz usunąć wiersz?", "Usuwanie", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                //nie
                if (res == DialogResult.No) { }
                //tak
                else
                {
                    //usuwanie z bazy
                    button_usun(dgv.Name, dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString(), dataGridView1.Columns[2].HeaderText);
                    //usuwanie wizualne
                    dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                }
            }
        }
        private void edit(int row_index)
        {

            label4.Text = dataGridView1.Rows[row_index].Cells[2].Value.ToString();
            label4.Visible = false;
            textBox1.Text = dataGridView1.Rows[row_index].Cells[2].Value.ToString();
            textBox1.Visible = true;
            label3.Text = dataGridView1.Columns[2].HeaderText;
            label3.Visible = true;

            label5.Text = "CZY_ZAPLACONE";
            label5.Visible = true;
            label5.Width = 50;

            textBox2.Visible = true;
            textBox2.Text = dataGridView1.Rows[row_index].Cells[8].Value.ToString();
            if (dataGridView1.Columns.Count - 2 > 2)
            {
                label6.Text = "DATA_WYDANIA";
                label6.Visible = true;
                textBox3.Visible = true;
                textBox3.Text = dataGridView1.Rows[row_index].Cells[5].Value.ToString();
            }
            else
            {
                label6.Visible = false;
                textBox3.Text = "";
                textBox3.Visible = false;
            }
            if (dataGridView1.Columns.Count - 2 > 3)
            {
                label7.Text = "DATA_PRZGOTOWANIA";
                label7.Visible = true;
                textBox4.Visible = true;
                textBox4.Text = dataGridView1.Rows[row_index].Cells[4].Value.ToString();
            }
            else
            {
                label7.Visible = false;
                textBox4.Text = "";
                textBox4.Visible = false;
            }
            if (dataGridView1.Columns.Count - 2 > 4)
            {
                label8.Text = "FK_STATUS";
                label8.Visible = true;
                textBox5.Visible = true;
                textBox5.Text = dataGridView1.Rows[row_index].Cells[9].Value.ToString();
            
            }
            else
            {
                label8.Visible = false;
                textBox5.Text = "";
                textBox5.Visible = false;
            }
            if (dataGridView1.Columns.Count - 2 > 5)
            {
                label9.Text = "FK_PRACOWNIK_WYDAJACY";
                label9.Visible = true;
                textBox6.Visible = true;
                textBox6.Text = dataGridView1.Rows[row_index].Cells[10].Value.ToString();
            }
            else
            {
                label9.Visible = false;
                textBox6.Text = "";
                textBox6.Visible = false;
            }
          
                label11.Visible = false;
                comboBox1.Visible = false;
                label10.Visible = false;
                textBox7.Text = "";
                textBox7.Visible = false;
            
            button7.Visible = true;

        }
        private void hideControls()
        {
            label3.Visible = false;
            label4.Visible = false;
            label5.Visible = false;
            label6.Visible = false;
            label7.Visible = false;
            label8.Visible = false;
            label9.Visible = false;
            label10.Visible = false;
            label11.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox1.Visible = false;
            button7.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;

        }
        //update

        private void button7_Click_1(object sender, EventArgs e)
        {
            var fact = new ChannelFactory<IService1>(new WebHttpBinding(), new EndpointAddress("http://localhost:1101/bsk"));
            fact.Endpoint.Behaviors.Add(new WebHttpBehavior());
            var client = fact.CreateChannel();

                client.update_row(dataGridView1.Name, label4.Text, dataGridView1.Columns[2].HeaderText, textBox2.Text, label5.Text,
                     textBox3.Text , label6.Text,  textBox4.Text , label7.Text, textBox5.Text, label8.Text, textBox6.Text, label9.Text, "", "", "", "");
            
                show_table(dataGridView1.Name);
            this.hideControls();
        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox14_TextChanged(object sender, EventArgs e)
        {

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox8_TextChanged(object sender, EventArgs e)
        {

        }

    }
}
