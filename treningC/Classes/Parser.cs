using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IniParser;
using IniParser.Model;

namespace treningC.Classes
{
    class Parser
    {
        private FileIniDataParser parser = null;
        public IniData data = null;


        public Parser ()
        {
            parser = new FileIniDataParser();
            
        }
        public FileValues readFile(string filename)
        {
            FileValues file = new FileValues();
            if (filename != null)
            { 
            data = parser.ReadFile(filename);
                foreach (SectionData section in data.Sections)
                {
                    Console.WriteLine("[" + section.SectionName + "]");
             
                    file.group = section.SectionName;
                    //Iterate through all the keys in the current section
                    //printing the values
                    foreach (KeyData key in section.Keys)
                    {
                        Console.WriteLine(key.KeyName + " = " + key.Value);
                        file.oneFileKeymapList.Add(new Keymap(key.KeyName.ToString(), key.Value.ToString()));
                    }
                        
                }



            }
            return file;
        }
    }
}
