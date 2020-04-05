using NUnit.Framework;
using SaldoVirtual;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

namespace SaldoVirtualUnitTest
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
            using (FileStream fs = File.Open("temp.dat", FileMode.Create, FileAccess.Write, FileShare.None))
            {
               
            }
        }

        [Test]
        public void CreateFolder_CreatesFolder()
        {
            VirtualDirectory test = new VirtualDirectory();
            test.AddFolder("test");
            Assert.AreEqual(test.Items.Count, 1);
        }

        [Test]
        public void CreateFolderDuplicate_ThrowsException()
        {
            VirtualDirectory vd = new VirtualDirectory();
            vd.AddFolder("test");

            var ex = Assert.Throws<Exception>(() => vd.AddFolder("test"));
            Assert.That(ex.Message, Is.EqualTo("already exists"));
           
        }

        [Test]
        public void CreateFolderDuplicate_ThrowsException2()
        {
            // TODO Method shows that tests are running :) 
            VirtualDirectory vd = new VirtualDirectory();
            vd.AddFolder("test");

            var ex = Assert.Throws<Exception>(() => vd.AddFolder("test"));
            Assert.That(ex.Message, Is.EqualTo("show test are running"));
        }
    }
}