using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using treningC.Classes;

namespace treningC
{
    public partial class Form1 : Form
    {
        Connector connector = null;
        int numberOfFiles = 0;
        public List<Renci.SshNet.Sftp.SftpFile> listOfFiles = new List<Renci.SshNet.Sftp.SftpFile>();
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {


            PathSettings.localPath= @"C:\Users\radek\Documents\";
            PathSettings.remotePath = @"/home/radek/Pliki/";
            ConnectionSettings connection = new ConnectionSettings();
            connection.Ip = ipTextBox.Text;
            int n;
            bool isNumeric = int.TryParse(portTextBox.Text, out n);

            connection.Port = portTextBox.Text;
            connection.Username = "root";
            connection.Password = "radek2";
            
            if (!isNumeric)
            {
                var result = System.Windows.Forms.MessageBox.Show("Błąd", "Niepoprawny format portu", System.Windows.Forms.MessageBoxButtons.OK);

            }
            else
            {
                CustomController controller = new CustomController(connector = new Connector(connection,customProgressBar));
                controller.processDownloads();
          
            }


        //    statusLabel.Text = connector.getConnectionStatus();
//
        //    listOfFiles = connector.sendCommand();
        //    Thread thr = new Thread(new ThreadStart(connector.downloadFile));
         //   thr.Start();

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Parser p = new Parser();
            ContainerList container = new ContainerList();
            for (int i = 0; i < listOfFiles.Count; i ++)
            {

                container.list.Add(p.readFile(listOfFiles.ElementAt(i).FullName));


            }
        
           // for (int i=0; i< )
            //List<string> list = new List<string>();
           /// int size = list.Count;
            MyCustomForm f = new MyCustomForm();
            f.Show();
           // TabPage[] tabs = new TabPage[size];
        }

        public void createInterface()  
        {

        }
    }
}
