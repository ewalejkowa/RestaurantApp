using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.ServiceModel;
using BSK2_Service;
using System.ServiceModel.Description;

namespace BSK2_Klient
{
    public partial class Form2 : Form
    {
        //funkcja wyświtlająca tabelę po stringu
        public void show_table(string name)
        {
            var fact = new ChannelFactory<IService1>(new WebHttpBinding(), new EndpointAddress("http://localhost:1101/bsk"));
            fact.Credentials.ServiceCertificate.Authentication.RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck;
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

        public void show_selective_table()
        {
            show_grid();
            var fact = new ChannelFactory<IService1>(new WebHttpBinding(), new EndpointAddress("http://localhost:1101/bsk"));
            fact.Endpoint.Behaviors.Add(new WebHttpBehavior());

            var client = fact.CreateChannel();
            DataTable dt = new DataTable();
            dt = client.GetSelectiveTable(dataGridView1.Name, comboBox5.Text, textBox19.Text);
            if (string.IsNullOrEmpty(textBox19.Text))
            {
                show_table(dataGridView1.Name);
            }
            else
            {
                if (dt != null)
                {
                    dataGridView1.DataSource = dt;
                
                    if (dataGridView1.Columns.Count > 2)
                    {
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
                } else
                {
                    for (int i = 0; i < dataGridView1.Columns.Count; i++)
                    {
                        dataGridView1.Columns[i].Visible = false;
                        dataGridView1.RowHeadersVisible = false;

                    }
                }
            }
        }

        public void show_edit()
        {
            DataGridViewButtonColumn btn2 = new DataGridViewButtonColumn();
            btn2.HeaderText = "Funkcje";
            btn2.Text = "Edytuj";
            btn2.Name = "Buttons_edit";
            btn2.UseColumnTextForButtonValue = true;
            btn2.DataPropertyName = "Buttons_edit";
            dataGridView1.Columns.Add(btn2);

        }
        public void show_delete()
        {
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            btn.HeaderText = "Funkcje";
            btn.Text = "Usuń";
            btn.Name = "Buttons_usun";
            btn.UseColumnTextForButtonValue = true;
            btn.DataPropertyName = "Buttons_usun";
            dataGridView1.Columns.Add(btn);
        }

        public void hide_grid()
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Visible = false;
                dataGridView1.RowHeadersVisible = false;
                
            }
            comboBox5.Visible = false;
            label22.Visible = false;
            label23.Visible = false;
            textBox19.Visible = false;
            button12.Visible = false;
            label21.Visible = false;

        }
        public void show_grid()
        {
            for (int i = 0; i < dataGridView1.Columns.Count; i++)
            {
                dataGridView1.Columns[i].Visible = true;
                dataGridView1.RowHeadersVisible = true;
            }
            comboBox5.Visible = true;
            label22.Visible = true;
            label23.Visible = true;
            textBox19.Visible = true;
            button12.Visible = true;
            label21.Visible = true;
        }
        public void update_combobox()
        {
            comboBox5.Items.Clear();
            foreach (DataGridViewColumn column in dataGridView1.Columns)
            {
                if (column.Index > 1)
                {
                    string headerText = column.HeaderText;
                    comboBox5.Items.Add(headerText);
                }
            }
            //comboBox5.SelectedIndex = 0;
        }
        
        public void delete_buttons()
        {
            dataGridView1.Columns.Clear();
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
            label18.Visible = false;
            textBox2.Visible = false;
            textBox3.Visible = false;
            textBox4.Visible = false;
            textBox5.Visible = false;
            textBox6.Visible = false;
            textBox7.Visible = false;
            textBox1.Visible = false;
            button7.Visible = false;
            label11.Visible = false;
            comboBox1.Visible = false;
            comboBox2.Visible = false;
            textBox18.Visible = false;
            textBox17.Visible = false;
        }
        //usuwanie
        private bool button_usun(string name_of_table, string name_of_key, string name_of_column)
        {
            var fact = new ChannelFactory<IService1>(new WebHttpBinding(), new EndpointAddress("http://localhost:1101/bsk"));
            fact.Credentials.ServiceCertificate.Authentication.RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck;
            fact.Endpoint.Behaviors.Add(new WebHttpBehavior());

            var client = fact.CreateChannel();
          return client.delete_row(name_of_table, name_of_key, name_of_column);
            
        }

        private void edit(int row_index)
        {

            label4.Text = dataGridView1.Rows[row_index].Cells[2].Value.ToString();
            label4.Visible = false;
            textBox1.Text = dataGridView1.Rows[row_index].Cells[2].Value.ToString();
            textBox1.Visible = true;
            label3.Text = dataGridView1.Columns[2].HeaderText;
            label3.Visible = true;

            label5.Text = dataGridView1.Columns[3].HeaderText;
            label5.Visible = true;
            label5.Width = 50;
            textBox2.Visible = true;
            textBox2.Text = dataGridView1.Rows[row_index].Cells[3].Value.ToString();
            if (dataGridView1.Columns.Count - 2 > 2)
            {
                label6.Text = dataGridView1.Columns[4].HeaderText;
                label6.Visible = true;
                textBox3.Visible = true;
                textBox3.Text = dataGridView1.Rows[row_index].Cells[4].Value.ToString();
            }
            else
            {
                label6.Visible = false;
                textBox3.Text = "";
                textBox3.Visible = false;
            }
            if (dataGridView1.Columns.Count - 2 > 3)
            {
                label7.Text = dataGridView1.Columns[5].HeaderText;
                label7.Visible = true;
                textBox4.Visible = true;
                textBox4.Text = dataGridView1.Rows[row_index].Cells[5].Value.ToString();
            }
            else
            {
                label7.Visible = false;
                textBox4.Text = "";
                textBox4.Visible = false;
            }
            if (dataGridView1.Columns.Count - 2 > 4)
            {
                label8.Text = dataGridView1.Columns[6].HeaderText;
                label8.Visible = true;
                textBox5.Visible = true;
                textBox5.Text = dataGridView1.Rows[row_index].Cells[6].Value.ToString();
            }
            else
            {
                label8.Visible = false;
                textBox5.Text = "";
                textBox5.Visible = false;
            }
            if (dataGridView1.Columns.Count - 2 > 5)
            {
                label9.Text = dataGridView1.Columns[7].HeaderText;
                label9.Visible = true;
                textBox6.Visible = true;
                textBox6.Text = dataGridView1.Rows[row_index].Cells[7].Value.ToString();
            }
            else
            {
                label9.Visible = false;
                textBox6.Text = "";
                textBox6.Visible = false;
            }
            if (dataGridView1.Columns.Count - 2 > 6)
            {
                label10.Text = dataGridView1.Columns[8].HeaderText;
                label10.Visible = true;
                textBox7.Visible = true;
                textBox7.Text = dataGridView1.Rows[row_index].Cells[8].Value.ToString();
            }
            else
            {
                label10.Visible = false;
                textBox7.Text = "";
                textBox7.Visible = false;
            }
            button7.Visible = true;

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
            textBox16.Visible = false;
            textBox15.Visible = false;

 
            textBox8.Text = "";
            textBox9.Text = "";
            textBox10.Text = "";
            textBox11.Text = "";
            textBox12.Text = "";
            textBox13.Text = "";
            textBox14.Text = "";
            textBox15.Text = "";
            textBox16.Text = "";
       
            label12.Visible = false;
            label13.Visible = false;
            label14.Visible = false;
            label15.Visible = false;
            label16.Visible = false;
            label17.Visible = false;
            label19.Visible = false;
            label20.Visible = false;

            comboBox3.Visible = false;
            comboBox4.Visible = false;

            button9.Visible = false;
            button10.Text = "Dodaj nowy rekord";
        }
             private void show_add_controls(string name)
             {
                 if (name == "ZAMOWIENIE")
                 {
                     logged_in = new Form3(this.login);
                     this.Visible = false;
                     logged_in.ShowDialog();
                 }
                
                 //1
                 int i = 0;
                 textBox8.Visible = true;
                 var fact = new ChannelFactory<IService1>(new WebHttpBinding(), new EndpointAddress("http://localhost:1101/bsk"));
                 fact.Credentials.ServiceCertificate.Authentication.RevocationMode = System.Security.Cryptography.X509Certificates.X509RevocationMode.NoCheck;
                 fact.Endpoint.Behaviors.Add(new WebHttpBehavior());
                 textBox8.Text = (client.GetLabel(name)+1).ToString();
                 label12.Text = dataGridView1.Columns[2].HeaderText;
                 label12.Visible = true;               

                 //2
                 label13.Text = dataGridView1.Columns[3].HeaderText;
                 label13.Visible = true;
                 textBox9.Visible = true;
 
                 if (dataGridView1.Columns.Count - 2 >= 3)
                 {
                     label14.Text = dataGridView1.Columns[4].HeaderText;
                     label14.Visible = true;
                     textBox10.Visible = true;
                 }
                 else
                 {
                     label14.Visible = false;
                     textBox10.Text = "";
                     textBox10.Visible = false;
                 }
                 if (dataGridView1.Columns.Count - 2 >= 4)
                 {
                     label15.Text = dataGridView1.Columns[5].HeaderText;
                     label15.Visible = true;
                     textBox11.Visible = true;
 
                 }
                 else
                 {
                     label15.Visible = false;
                     textBox11.Text = "";
                     textBox11.Visible = false;
                 }
                 if (dataGridView1.Columns.Count - 2 >=5)
                 {
                     label16.Text = dataGridView1.Columns[6].HeaderText;
                     label16.Visible = true;
                     textBox12.Visible = true;
  
                 }
                 else
                 {
                     label16.Visible = false;
                     textBox12.Text = "";
                     textBox12.Visible = false;
                 }
                 if (dataGridView1.Columns.Count - 2 > 6)
                 {
                     label17.Text = dataGridView1.Columns[7].HeaderText;
                     label17.Visible = true;
                     textBox13.Visible = true;

                 }
                 else
                 {
                     label17.Visible = false;
                     textBox13.Text = "";
                     textBox13.Visible = false;
                 }

             }
    }



}