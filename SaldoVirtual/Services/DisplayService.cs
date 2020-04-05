using System;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace SaldoVirtual.Services
{
    public class DisplayService : IDisplayService
    {
        public DisplayService()
        {
          
        }

        public void DisplayTree(VirtualDirectory tree)
        {
            string intend = string.Empty;
            int i = 0;
            while (i <= tree.GetLevel())
            {
                intend = string.Concat(intend, "-----");
                i++;
            }
           
            foreach (var file in tree.DirectoryData)
            {
                Console.WriteLine(string.Concat(intend, " File ", file.GetFileName(), !string.IsNullOrEmpty(file.GetFileGuid()) ? file.GetFileGuid() : "-"));
            }

            foreach (var dir in tree.Items)
            {
                Console.WriteLine(string.Concat(intend, " Directory ", dir.Text));
                if (dir != null)
                {
                    this.DisplayTree(dir);
                }
            }
        }
        
    }
}