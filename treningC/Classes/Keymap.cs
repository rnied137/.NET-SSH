using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace treningC.Classes
{
    class Keymap

    {
        public Keymap() { }
        public Keymap(string k, string v) { key = k; value = v; }    
        public string key { get; set; }
        public string value { get; set; }
    }
}
