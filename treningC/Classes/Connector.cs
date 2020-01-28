using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace treningC
{
    class Connector
    {
        private ConnectionSettings connectionSettings = null;
        private ConnectionInfo connectionInfo = null;
        private SftpClient client = null;

        private List<string> listOfFilesToDownload = new List<string>();
        public Connector(ConnectionSettings conn)
        {
            connectionSettings = conn;
            
        }

        
        public void setupConnectionSettings(ConnectionSettings conn)
        {
            connectionSettings = conn;
        }
        public void getConnected()
        {
            connectionInfo = new ConnectionInfo(
                 connectionSettings.Ip,
                 Convert.ToInt32(connectionSettings.Port),
                 connectionSettings.Username,
                 new PasswordAuthenticationMethod(connectionSettings.Username, connectionSettings.Password)
                 );

            Console.WriteLine("Wprowadzony IP to " + connectionSettings.Ip + "\n");
            Console.WriteLine("Wprowadzony port to " + connectionSettings.Port + "\n");
            Console.WriteLine("Wprowadzony password to " + connectionSettings.Password + "\n");
            Console.WriteLine("Wprowadzony username to " + connectionSettings.Username + "\n");

            if (checkConnectionStatus())
            {
                var result = System.Windows.Forms.MessageBox.Show("Połączono", "SUCKES", System.Windows.Forms.MessageBoxButtons.OK);
                Console.WriteLine("Polaczono !!!");
            }
            else
            {

                var result = System.Windows.Forms.MessageBox.Show("Nie udalo sie polaczyc", "Niepowodzenie", System.Windows.Forms.MessageBoxButtons.OK);
                Console.WriteLine("Nie powiodlo się !");
            }
            
        }

        
           public string getConnectionStatus()
        {
            if (client.IsConnected)
                return "Połączono";
            else
                return "Nie połączono";
        }

        private bool checkConnectionStatus()
        {
             client = new SftpClient(connectionInfo);
            try
            {
                client.Connect();

            }

            catch (Renci.SshNet.Common.SshConnectionException ex)
            {
                var result = System.Windows.Forms.MessageBox.Show("ConnectionException", ex.Message, System.Windows.Forms.MessageBoxButtons.OK);

            }
            catch (Renci.SshNet.Common.SshAuthenticationException ex)
            {
                var result = System.Windows.Forms.MessageBox.Show("AuthenticationException", ex.Message, System.Windows.Forms.MessageBoxButtons.OK);

            }
            catch (Exception EX)
            {
                Console.WriteLine("Panie mamy blad ale niewiadomo jaki. Tudno.");
            }
           


                if (client.IsConnected)
                return true;
            else
                return false;
          
        }

        public void downloadFile()
        {

          
            int numbberOfFilesToDownload = listOfFilesToDownload.Count;
            if (numbberOfFilesToDownload > 0)
            {
                for (int i = 0; i< numbberOfFilesToDownload; numbberOfFilesToDownload ++ )
                {

                }
            }
        }

        public void uploadFile()

        {

        }

        //if list == 0 

        private List<Renci.SshNet.Sftp.SftpFile> retrieveListOfFilesInDir(string path)
        {
            List<Renci.SshNet.Sftp.SftpFile> list = null;
            list  = client.ListDirectory(path).ToList();
            return list;
        }
        public List<Renci.SshNet.Sftp.SftpFile> sendCommand()
        {
            StringBuilder s = new StringBuilder();
            // client.
            List<Renci.SshNet.Sftp.SftpFile> list = retrieveListOfFilesInDir("/home/radek/Pliki/");
            //var result = System.Windows.Forms.MessageBox.Show(s.ToString(), "Pliki do pobrania", System.Windows.Forms.MessageBoxButtons.OK);

            return list;
        }
         //   List<string> listOfPathsAndFilenames = new List<string>();
           // if (list.Count > 0 ) 
            //{
              
              //  foreach (Renci.SshNet.Sftp.SftpFile file in list)
               // {
                   // if (!file.IsDirectory)
                    //{
                      //  var singleString = string.Join(",", file.FullName);
                       // try
                     //   {
                      //      listOfFilesToDownload.Add(singleString);
                         //   listOfPathsAndFilenames.Add(file.FullName);
                      //  }
                     //   catch (NullReferenceException ex)
                      //  {
                      //      Console.WriteLine(" Null reference exception ");
                      //  }
                       // s.Append(singleString);
                      //  s.Append("\n");
                   // }

                    //var singleString = string.Join(",", file.IsDirectory);
                  
                   
                //}
            

        

            //return listOfPathsAndFilenames;


        }
}
