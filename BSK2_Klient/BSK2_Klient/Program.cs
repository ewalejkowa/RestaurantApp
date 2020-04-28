using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using BSK2_Service;
using System.ServiceModel;

namespace BSK2_Klient
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {

            var fact = new ChannelFactory<IService1>(new WebHttpBinding(), new EndpointAddress("http://localhost:1101/bsk"));

            var client = fact.CreateChannel();
            client.GetType();
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
