using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SaldoVirtual
{
    [Serializable]
    public class VirtualFile
    {
        private string text { get; set; }

        private string path { get; set; }

        private Guid guid { get; set; }

        public VirtualFile(string text, string path, Guid id)
        {
            this.path = path;
            this.text = text;
            this.guid = id;
        }
        public string GetFilePath()
        {
            return this.path;
        }

        public string GetFileName()
        {
            return this.text;
        }

        public string GetFileGuid()
        {
            return this.guid.ToString();
        }
    }
}
