using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using System.ServiceModel;

using BSK2_Service;
using System.Security.Cryptography;
using System.ServiceModel.Description;

namespace BSK2_Klient
{

    public partial class Form2 : Form
    {
        IService1 client;
        int label;
        int current_table_label;
        string login;
        Form3 logged_in;
        public Form2(string login, string password)
        {
            this.login = login;
            InitializeComponent();
            this.MinimumSize = new Size(1000, 700);
            this.MaximumSize = new Size(1000, 700);
            int labelnumber;
            label1.Text = "Zalogowany jako: " + login;
            var fact = new ChannelFactory<IService1>(new WebHttpBinding(), new EndpointAddress("http://localhost:1101/bsk"));
            fact.Endpoint.Behaviors.Add(new WebHttpBehavior());
            client = fact.CreateChannel();
            dataGridView1.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.None;
            label2.Visible = false;
            //dodanie buttonów do funkcji
            //usuwanie
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Funkcje";
            btn.Text = "Usuń";
            btn.Name = "Buttons_usun";
            btn.UseColumnTextForButtonValue = true;
            btn.DataPropertyName = "Buttons_usun";
            dataGridView1.Columns.Add(btn);

            DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
            btn2.HeaderText = "Funkcje";
            btn2.Text = "Edytuj";
            btn2.Name = "Buttons_edit";
            btn2.UseColumnTextForButtonValue = true;
            btn2.DataPropertyName = "Buttons_edit";
            dataGridView1.Columns.Add(btn2);

            dataGridView1.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            dataGridView1.Columns[1].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;
            //wyświetlenie pierwszego widoku żeby łyso nie było
            DataTable dt = new DataTable();
            dt = client.GetTable("MENU");
            dataGridView1.Name = "MENU";
            dataGridView1.DataSource = dt;
            //edytowanie
            dataGridView1.ReadOnly = true;

            //dodanie event na kliknięcie w komórce
            this.dataGridView1.CellClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dataGridView1_CellClick);
            System.Globalization.CultureInfo customCulture = (System.Globalization.CultureInfo)System.Threading.Thread.CurrentThread.CurrentCulture.Clone();
            customCulture.NumberFormat.NumberDecimalSeparator = ".";

            System.Threading.Thread.CurrentThread.CurrentCulture = customCulture;
        }
        private void displayTable(string name)
        {
            show_table(name);
            comboBox5.Items.Clear();
            show_grid();
            update_combobox();
            dataGridView1.Refresh();
            this.hideControls();
            hide_add_controls();
            dataGridView1.Name = name;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            displayTable("PRACOWNIK");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            displayTable("ZAMOWIENIE");
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            displayTable("KATEGORIA");        
        }

        private void button4_Click(object sender, EventArgs e)
        {
            displayTable("ELEMENT_ZAMOWIENIA");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            displayTable("MENU");         
        }

        private void button6_Click(object sender, EventArgs e)
        {
            //log off
            this.Visible = false;
            Form1 main_window = new BSK2_Klient.Form1();
            main_window.ShowDialog();


        }
       
        //w przypadku kliknięcia na dany wiersz
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (label11.Visible) label11.Visible = false;
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
                if (res == DialogResult.No){ }
                //tak
                else
                {
                        //usuwanie z bazy
                        bool result=button_usun(dgv.Name, dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[2].Value.ToString(), dataGridView1.Columns[2].HeaderText);
                        if (result)
                        {
                            //usuwanie wizualne
                            dataGridView1.Rows.RemoveAt(dataGridView1.CurrentRow.Index);
                        }
                        else MessageBox.Show("Wiersz nie może byc usunięty", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Question);                     
                }
            }
        }

        //update
        private void button7_Click(object sender, EventArgs e)
        {
            var fact = new ChannelFactory<IService1>(new WebHttpBinding(), new EndpointAddress("http://localhost:1101/bsk"));
            fact.Endpoint.Behaviors.Add(new WebHttpBehavior());

            var client = fact.CreateChannel();
            bool result = false;


               result= client.update_row(dataGridView1.Name, label4.Text, dataGridView1.Columns[2].HeaderText, textBox2.Text, label5.Text,
                textBox3.Text, label6.Text, textBox4.Text, label7.Text, textBox5.Text, label8.Text, textBox6.Text, label9.Text, textBox7.Text, label10.Text, "", "");
               if (!result) MessageBox.Show("Błąd podczas edycji, spraw poprawność parametrów", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Question); 
            
            show_table(dataGridView1.Name);
            this.hideControls();
        }




        private void button10_Click(object sender, EventArgs e)
        {
            if (button10.Text == "Dodaj nowy rekord")
            {
                button9.Visible = true;
                button10.Text = "Zapisz";
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
                    //fact.Credentials.ServiceCertificate.Authentication.RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck;
                    var client = fact.CreateChannel();
                    bool result = false;
                        //byte[] c =new byte[0];

                        result = client.add_row(dataGridView1.Name,  textBox9.Text, label13.Text, textBox10.Text, label14.Text,
                            textBox11.Text, label15.Text, textBox12.Text, label16.Text, textBox13.Text, label17.Text, "", "","","", new byte[0], "");
                        if (!result)
                        {
                            MessageBox.Show("Wiersz nie może byc dodany, sprawdź poprawność parametrów", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Question);
                        }
                    

                        show_table(dataGridView1.Name);
                    
                    button10.Text = "Dodaj nowy rekord";
                    button9.Visible = false;
                    hide_add_controls();

                }

            }
        }

        private void button9_Click(object sender, EventArgs e)
        {
            //hide add controls           
            hide_add_controls();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            displayTable("POZYCJA_MENU");
        }

        private void button11_Click(object sender, EventArgs e)
        {
            show_table("STATUS");

            comboBox5.Items.Clear();

                show_grid();
                

                    dataGridView1.Columns[0].Visible = true;
                    dataGridView1.Columns[1].Visible = true;

                update_combobox();


                button10.Visible = true;

            this.hideControls();
            hide_add_controls();
        }

        private void button12_Click(object sender, EventArgs e)
        {
            show_selective_table();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            displayTable("REALIZACJA_ZAMOWIENIA");
        }
    }

}
