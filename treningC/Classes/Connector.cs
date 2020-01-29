using Renci.SshNet;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using treningC.Classes;

namespace treningC
{
    class Connector
    {
        private ProgressBar progressBar1 = null;
        private ConnectionSettings connectionSettings = null;
        private ConnectionInfo connectionInfo = null;
        private SftpClient client = null;

        private List<string> listOfFilesToDownload = new List<string>();
        public Connector() { }
        public Connector(ConnectionSettings conn,ProgressBar p)
        {
            connectionSettings = conn;
            progressBar1 = p;
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




        public bool checkConnectionStatus()
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
                Console.WriteLine("Panie mamy blad ale niewiadomo jaki. Tudno." + EX.StackTrace);
                Console.WriteLine(EX.Message);
            }
            //
            //  if (client.IsConnected)
            //    return  Console.WriteLine("Połączono");
            //  else
            //    return "Nie połączono";

            if (client.IsConnected)
                return true;
            else
                return false;

        }

        public void downloadFile()
        {
            List<Renci.SshNet.Sftp.SftpFile> listWithFullInfoParams = new List<Renci.SshNet.Sftp.SftpFile>();
            listWithFullInfoParams = sendCommand();
            int numbberOfFilesToDownload = listWithFullInfoParams.Count;
            if (numbberOfFilesToDownload > 0)
            {

                foreach (Renci.SshNet.Sftp.SftpFile file in listWithFullInfoParams)
                {
                    Console.WriteLine("Plik" + file.Name);
                    Console.WriteLine("Plik Full Name" + file.FullName);


                    string fileLocAndName = String.Format("{0}\\{1}",
                              PathSettings.localPath, file.Name.ToString());

                    using (Stream fileStream = File.Create(fileLocAndName))
                    {
                        progressBar1.Invoke(
               (MethodInvoker)delegate {
                   progressBar1.Maximum = (int)file.Attributes.Size; });
                        //w watku ktorym sie aktualnie znajduje wywoluje delegata ktory ustawia maximum
                        //progress bar ma metoda invoke jako element gui

                        ////@
                        //Invoke metoda synchronizacji z watkiem glownym dla gui
                        //delegate {..} funkcja anonimowa czyli to co po delegate to jest ta funkcja wykonywana w watku i atrybut ktory ustawia
                        //musi byc zgodny z tym co w invoke

                        client.DownloadFile(file.FullName, fileStream, DownloadProgresBar);
                        
                        Console.WriteLine("Plik " + file.FullName);

                    }

                }
            }
            else
                Console.WriteLine("Brak plikow do pobrania !!!");
        }

        void setVal(ulong val)
        {
            progressBar1.Value = (int)val;

        }
        
        private delegate void delSetProgress(ulong val);
        delSetProgress mydel;
        private void DownloadProgresBar(ulong uploaded)
        {
            // Update progress bar on foreground thread
            
            mydel = setVal;
            progressBar1.Invoke(mydel,uploaded);

            //    progressBar1.Invoke((MethodInvoker)delegate { progressBar1.Value = (int)uploaded; });
        }

        public void uploadFile()

        {

        }

        //if list == 0 

        private List<Renci.SshNet.Sftp.SftpFile> retrieveListOfFilesInDir(string path)
        {
            List<Renci.SshNet.Sftp.SftpFile> list = null;
            list = client.ListDirectory(path).ToList();
            return list;
        }
        public List<Renci.SshNet.Sftp.SftpFile> sendCommand()
        {
            StringBuilder s = new StringBuilder();
            // client.
            List<Renci.SshNet.Sftp.SftpFile> list = retrieveListOfFilesInDir("/home/radek/Pliki/");
            List<Renci.SshNet.Sftp.SftpFile> listOfFiles = new List<Renci.SshNet.Sftp.SftpFile>();
            //var result = System.Windows.Forms.MessageBox.Show(s.ToString(), "Pliki do pobrania", System.Windows.Forms.MessageBoxButtons.OK);

            foreach (Renci.SshNet.Sftp.SftpFile file in list)
            {
                if (!file.IsDirectory)
                {
                    listOfFiles.Add(file);
                    //list.Remove(file);
                }
            }
            return listOfFiles;
        }
    }
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
