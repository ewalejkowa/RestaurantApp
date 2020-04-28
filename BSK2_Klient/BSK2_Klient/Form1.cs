using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BSK2_Service;
using System.ServiceModel;
using System.ServiceModel.Configuration;
using System.Configuration;
using System.Security.Cryptography;
using Microsoft.AspNet.Identity;
using System.ServiceModel.Description;

namespace BSK2_Klient
{
    public partial class Form1 : Form
    {
        Form2 logged_in;
        public Form1()
        {
            InitializeComponent();
            this.MinimumSize = new Size(1000, 700);
            this.MaximumSize = new Size(1000, 700);

        }

        public static Uri set_endpoint()
        {
            var serviceModel = ServiceModelSectionGroup.GetSectionGroup(ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None));
            var endpoints = serviceModel.Client.Endpoints;
            foreach (ChannelEndpointElement e in endpoints)
            {
                if (e.Name == "default_endpoint")
                {
                    return e.Address;
                }
            }
            return new Uri("net.tcp:000.000.0.00:000");
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                EndpointAddress endpoint = new EndpointAddress(set_endpoint(), EndpointIdentity.CreateDnsIdentity("MyBank"));
                NetTcpBinding binding = new NetTcpBinding(SecurityMode.Transport);
                binding.Security.Transport.ClientCredentialType = TcpClientCredentialType.None;
                binding.Security.Transport.ProtectionLevel = System.Net.Security.ProtectionLevel.EncryptAndSign;
                binding.Security.Message.ClientCredentialType = MessageCredentialType.None;
                var fact = new ChannelFactory<IService1>(new WebHttpBinding(), new EndpointAddress("http://localhost:1101/bsk"));
                fact.Endpoint.Behaviors.Add(new WebHttpBehavior());
                var client = fact.CreateChannel();
                var md5 = new MD5CryptoServiceProvider();
                string login = textBox1.Text;
                string password = textBox2.Text;

                var attempt_login = client.Login(login);

                if (attempt_login)
                {
                    var currentUser = login;
                    label3.Text = "JESTES ZALOGOWANY, " + currentUser;

                    logged_in = new Form2(login, password);
                    this.Visible = false;
                    logged_in.ShowDialog();
                }
                else
               {
                    label5.Text = "Nie udalo sie zalogowac.";
                }

            }
            catch (System.ServiceModel.EndpointNotFoundException v)
            {

                MessageBox.Show("Brak połączenia z serwerem", "Błąd", MessageBoxButtons.OK, MessageBoxIcon.Question);
                Application.Exit();
            }

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
