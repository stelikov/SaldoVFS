using NUnit.Framework;
using SaldoVirtual;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using SaldoVirtual.Services;
using Moq;
using Microsoft.Extensions.Configuration;

namespace SaldoVirtualUnitTest
{
    public class FileServiceTests
    {
        [SetUp]
        public void Setup()
        {
            using (FileStream fs = File.Open("temp.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
            }
            
            Directory.CreateDirectory(Directory.GetCurrentDirectory() +"\\test");
        }

        [Test]
        public void CreateFile_ReturnVirtualFileWithCorrectName()
        {

            Mock<IDisplayService> displayServiceMock = new Mock<IDisplayService>();
            Mock<IConfiguration> configurationMock = new Mock<IConfiguration>();
            Mock<IAttachmentFileStorage> attachmentFileStorageMock = new Mock<IAttachmentFileStorage>();

            configurationMock.Setup(e => e.GetSection("databaseFile").Value).Returns("temp.dat");
            configurationMock.Setup(e => e.GetSection("directory").Value).Returns("test");
            attachmentFileStorageMock.Setup(e => e.SaveAttachmentFile(It.IsAny<string>(), It.IsAny<string>())).Verifiable();

            FileService fileService = new FileService(displayServiceMock.Object, configurationMock.Object, attachmentFileStorageMock.Object);
            var file = fileService.AddFile("test", "d");

            Assert.AreEqual(file.GetFileName(), "test");
             
          
        }
    }
}