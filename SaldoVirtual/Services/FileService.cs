using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Extensions.Configuration;

namespace SaldoVirtual.Services
{
    public class FileService : IFileService
    {
        private VirtualDirectory virtualDirectory;
        private IDisplayService displayService;
        private IConfiguration configuration;
        private IAttachmentFileStorage attachmentFileStorage;

        public FileService(IDisplayService displayService, IConfiguration configuration, IAttachmentFileStorage attachmentFileStorage)
        {
            this.displayService = displayService ?? throw new ArgumentNullException("DisplayService is null");
            this.configuration = configuration ?? throw new ArgumentNullException("configuration is null");
            this.attachmentFileStorage = attachmentFileStorage ?? throw new ArgumentNullException("attachmentFileStorage is null");
            this.Init();
        }

        public void Init()
        {
            try
            {
                var dbFile = this.configuration.GetSection("databaseFile").Value;

                using (FileStream fs = File.Open(dbFile, FileMode.OpenOrCreate, FileAccess.Read))
                {
                    BinaryFormatter b = new BinaryFormatter();
                    this.virtualDirectory = (VirtualDirectory)b.Deserialize(fs);
                    fs.Close();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Database File not exist, will be created", ex);
                this.virtualDirectory = new VirtualDirectory();
                this.propertyChanged();
            }
        }

        public void ChangeFolder(string name)
        {
            this.virtualDirectory.ChangeFolder(name);
            this.propertyChanged();
        }

        public void AddVirtualFolder(string name)
        {
            this.virtualDirectory.AddFolder(name);
            this.propertyChanged();
        }

        public VirtualFile AddFile(string name, string localFile)
        {
            Guid id = Guid.NewGuid();

            var fileName = string.Concat(Directory.GetCurrentDirectory() + "\\" + this.configuration.GetSection("directory").Value + "\\", id.ToString(), name);
            this.attachmentFileStorage.SaveAttachmentFile(localFile, fileName);
            var file = this.virtualDirectory.AddFile(name, fileName, id);
            
            //TODO add RemoveAdded file if failed to add in virtual system

            this.propertyChanged();

            return file;
        }

        public void RemoveFile(string name)
        {
            var file = this.virtualDirectory.RemoveFile(name);
            if (file != null)
            {
                attachmentFileStorage.RemoveAttachmentFile(file.GetFilePath());
                this.propertyChanged();
            }

        }

        public void RemoveVirtualFolder(string name)
        {
            this.virtualDirectory.RemoveFolder(name);
            this.propertyChanged();
        }

        public string GetCurrent()
        {
            return this.virtualDirectory.Current.Text;
        }

        public void DisplayTree()
        {
            this.displayService.DisplayTree(this.virtualDirectory);
        }

        public void DisplayList()
        {
            this.virtualDirectory.DisplayFiles();
        }

        private void propertyChanged()
        {
            var file = configuration.GetSection("databaseFile").Value;

            using (FileStream fs = File.Open(file, FileMode.Open, FileAccess.Write, FileShare.None))
            {
                BinaryFormatter b = new BinaryFormatter();
                b.Serialize(fs, this.virtualDirectory);
                fs.Close();
            }
        }
    }
}