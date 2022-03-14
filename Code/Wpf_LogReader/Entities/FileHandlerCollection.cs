using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wpf_LogReader
{
    public class FileHandlerCollection
    {
        public FileHandlerCollection()
        {
            this.Files = new List<FileHandler>();
        }
        public List<FileHandler> Files { get; set; }

        public void Add(FileHandler file)
        {
            if (!this.Contains(file))
            {
                this.Files.Add(file);
            }
        }

        private bool Contains(FileHandler file)
        {
            return this.Files.Any(f => f.Id == file.Id);
        }

    }
}
