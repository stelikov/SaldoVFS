using System;
using System.Collections.Generic;

namespace SaldoVirtual.Services
{
    public interface IBackupService 
    {
        void BackupFile(string path, string fileName);

        void BackupFiles();
    }
}