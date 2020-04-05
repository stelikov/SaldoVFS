using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using SaldoVirtual;
using SaldoVirtual.Services;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SaldoVirtualUnitTest
{
    public class BackupServiceTests
    {
        [SetUp]
        public void Setup()
        {
            using (FileStream fs = File.Open("temp.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
            }
        }

        [Test]
        public void BackupService_InitializeFromConfig()
        {
            var builder = new ConfigurationBuilder();

            builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                 { "FtpAddress", "value" }
            });
            VirtualDirectory test = new VirtualDirectory();
            test.AddFolder("test");
            var configuration = builder.Build();
            BackupService b = new BackupService(configuration);
            Assert.AreEqual(b.ftpAddress, "value");
        }

        [Test]
        public void BackupService_InitializeFromConfigWrong()
        {
            var builder = new ConfigurationBuilder();

            builder.AddInMemoryCollection(new Dictionary<string, string>
            {
                 { "FtpAddress", "xxx" }
            });
            VirtualDirectory test = new VirtualDirectory();
            test.AddFolder("test");
            var configuration = builder.Build();
            BackupService b = new BackupService(configuration);
            Assert.AreEqual(b.ftpAddress, "value");
        }
    }
}