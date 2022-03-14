using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_LogReader
{
    public class FileHandler
    {
        private string id;
        public string Id
        {
            get { return this.id; }
            set
            {
                this.id = value;
                this.Name = Path.GetFileName(value);
            }
        }

        public string Name { get; set; }
    }

}
