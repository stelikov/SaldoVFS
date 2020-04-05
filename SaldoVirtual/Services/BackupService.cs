using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SaldoVirtual.Services
{
    public class BackupService : IBackupService
    {
        private IConfiguration configuration;

        public string ftpAddress { get; }
        
        private string ftpUser { get; }
        
        private string ftpPassword { get; }

        public BackupService(IConfiguration configuration)
        {
            this.configuration = configuration ?? throw new ArgumentNullException();

            this.ftpAddress = this.configuration.GetSection("FtpAddress").Value;
            this.ftpUser = this.configuration.GetSection("FtpUser").Value;
            this.ftpPassword = this.configuration.GetSection("FtpPassword").Value;
        }

        public void BackupFile(string filePath, string fileName)
        {
            FtpWebRequest request = (FtpWebRequest)WebRequest.Create(string.Concat(this.ftpAddress, fileName));
            request.Credentials = new NetworkCredential(this.ftpUser, this.ftpPassword);
            request.Method = WebRequestMethods.Ftp.UploadFile;

            using (Stream fileStream = File.OpenRead(filePath)) 
            using (Stream ftpStream = request.GetRequestStream())
            {
                fileStream.CopyTo(ftpStream);
            }
        }
        public void BackupFiles()
        {
            string[] fileEntries = Directory.GetFiles(Directory.GetCurrentDirectory() + "\\" + this.configuration.GetSection("directory").Value);
            foreach (string fileName in fileEntries)
            {
                this.BackupFile(fileName, Path.GetFileName(fileName));
            }
        }
    }
}