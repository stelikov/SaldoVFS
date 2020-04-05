using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;

namespace SaldoVirtual
{
    [Serializable]
    public class VirtualDirectory
    {
        public VirtualDirectory Parent { get; set; }

        public VirtualDirectory Current { get; set; }

        private int Level { get; set; }

        public string Text { get; set; }

        public List<VirtualDirectory> Items { get; set; } = new List<VirtualDirectory>();

        public List<VirtualFile> DirectoryData { get; set; } = new List<VirtualFile>();

        public VirtualDirectory()
        {
            this.Current = this;
            this.Parent = null;

            if (this.DirectoryData == null)
            {
                this.DirectoryData = new List<VirtualFile>();
            }
        }

        public int GetLevel()
        {
            return this.Level;
        }

        public void AddFolder(string name)
        {
            var item = this.Current.Items.Select(t => t.Text == name).FirstOrDefault();
            if (item)
            {
                throw new Exception("already exists");
            }

            int level = this.Current == null ? 0 : this.Current.Level + 1;
            this.Current.Items.Add(new VirtualDirectory() { Text = name, Parent = this.Current, Level = level });
        }

        public void RemoveFolder(string name)
        {
            var itemToRemove = this.Current.Items.SingleOrDefault(r => r.Text == name);
            if (itemToRemove != null)
            {
                this.Current.Items.Remove(itemToRemove);
            }
        }

        public void ChangeFolder(string name)
        {
            if (name == "..")
            {
                this.Current = this.Current.Parent;
            }
            else
            {
                var folder = this.Current.Items.Where(t => t.Text == name).FirstOrDefault();
                if (folder != null)
                {
                    this.Current = folder;
                    this.Parent = this;
                }
            }
        }

        public VirtualFile AddFile(string name, string path, Guid id)
        {
            var item = this.Current.DirectoryData.Select(t => t.GetFileName() == name).FirstOrDefault();
            if (item)
            {
                throw new Exception("File with this name already exists");
            }

            var file = new VirtualFile(name, path, id);
           
            this.Current.DirectoryData.Add(file);
            return file;
        }

        public VirtualFile RemoveFile(string name)
        {
            VirtualFile item = this.Current.DirectoryData.Where(t => t.GetFileName() == name).FirstOrDefault();
            if (item != null)
            {
                var copy = item;
                this.Current.DirectoryData.Remove(item);
                return copy;
            }

            return null;
        }

        public void DisplayFiles()
        {
            foreach (var item in this.Current.DirectoryData)
            {
                Console.WriteLine(item.GetFileName());
            }
        }
    }
}
