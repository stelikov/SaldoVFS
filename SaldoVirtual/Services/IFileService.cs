using System;

namespace SaldoVirtual
{
    public interface IFileService
    {
        void AddVirtualFolder(string name);

        void RemoveVirtualFolder(string name);

        void ChangeFolder(string name);

        string GetCurrent();

        void DisplayList();

        void DisplayTree(); // TODO MERGE TO ONE method

        VirtualFile AddFile(string name, string file);

        void RemoveFile(string name);
    }
}