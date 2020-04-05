using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using SaldoVirtual.Services;

namespace SaldoVirtual
{
    public class ConsoleApplication
    {
        private IFileService fileService;
        private IBackupService backupService;
        private IConfiguration configuration;

        public ConsoleApplication(IFileService fileService, IBackupService backupService, IConfiguration configuration)
        {
            this.fileService = fileService ?? throw new ArgumentNullException();
            this.configuration = configuration ?? throw new ArgumentNullException();
            this.backupService = backupService ?? throw new ArgumentNullException();
        }

        public void Intro()
        {
            Console.WriteLine(string.Empty);
            Console.WriteLine("Commands Available:");
            Console.WriteLine("/addFolder [name]  - Add Folder To Current");
            Console.WriteLine("/removeFolder [name]  - Remove Folder From Current");
            Console.WriteLine("/current  - Get Current Folder");
            Console.WriteLine("/commands  - Get commands Menu");
            Console.WriteLine("/cd [name] - switch to folder (..) go back ");
            Console.WriteLine("/listFiles - List Files in Current Folder");
            Console.WriteLine("/tree - List Files in Current Folder");
            Console.WriteLine("/backup - Backup");
            Console.WriteLine(string.Empty);
            Console.WriteLine(string.Empty);
        }

        public void Run()
        {
            this.Intro();
            bool quitNow = false;
            while (!quitNow)
            {
                Console.WriteLine(string.Empty);
                Console.WriteLine("EnterCommand");
                Console.WriteLine(string.Empty);
                string command;
                command = Console.ReadLine();
                var items = command.Split(" ");

                switch (items[0])
                {
                    case "/addFolder":
                        Console.WriteLine("Adding VirtualFolder.");
                        try
                        {
                            this.fileService.AddVirtualFolder(items[1]);
                            Console.WriteLine("Vritual Folder Added");
                        } 
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error Occured {0}", ex);
                        }

                        break;

                    case "/removeFolder":
                        Console.WriteLine("Removing VirtualFolder.");

                        try
                        {
                            this.fileService.RemoveVirtualFolder(items[1]);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error Occured", ex);
                        }

                        break;
                    case "/current":
                        var name = this.fileService.GetCurrent();
                        if (string.IsNullOrEmpty(name))
                        {
                            Console.WriteLine("Root Folder:");
                        }
                        else
                        {
                            Console.WriteLine("You are in in {0}", name);
                        }
                      
                        break;

                    case "/listFiles":
                        Console.WriteLine("Files in current Folder");
                        this.fileService.DisplayList();
                        break;
                    case "/commands":
                        this.Intro();
                        break;
                    case "/addFile":
                        Console.WriteLine("adding File");
                        try
                        {
                            var vfile = this.fileService.AddFile(items[1], items[2]);
                            Console.WriteLine("File Added Successfully");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Error occured During adding File", ex);
                        }
                        break;

                    case "/cd":
                        Console.WriteLine("Switching Folder:");
                        this.fileService.ChangeFolder(items[1]);
                        break;

                    case "/tree":
                        Console.WriteLine("Displaying Virtual Tree");
                        Console.WriteLine(string.Empty);
                        this.fileService.DisplayTree();
                        Console.WriteLine(string.Empty);
                        break;

                    case "/backup":
                        Console.WriteLine("Backuping");
                        try
                        {
                            this.backupService.BackupFile(Directory.GetCurrentDirectory() + "\\" + this.configuration.GetSection("databaseFile").Value, this.configuration.GetSection("databaseFile").Value);
                            this.backupService.BackupFiles();
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine("Backup Failed", ex);
                        }

                        break;
                    case "/quit":
                        quitNow = true;
                        break;

                    default:
                        Console.WriteLine("Unknown Command " + command);
                        break;
                }
            }
        }
    }
}