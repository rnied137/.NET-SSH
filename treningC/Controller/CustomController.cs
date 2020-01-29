using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace treningC.Classes
{
    class CustomController
    {
        private Thread thr = null;
        private Connector connector = new Connector();

        public CustomController(Connector c)
        {
            connector = c;
        }
   
        
          
        
        public void processDownloads()
        {
            connector.getConnected();
            if(connector.checkConnectionStatus())
                
            {
                {
                     thr = new Thread(new ThreadStart(connector.downloadFile));
                    thr.Start();
                }
             
            }
        }


        public void processFiles()
        {
           // if (!thr.IsAlive && connector.)
        }


    }
}
