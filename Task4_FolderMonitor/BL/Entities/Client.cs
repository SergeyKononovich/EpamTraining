using System;

namespace Task4_FolderMonitor.BL.Entities
{
    public class Client
    {
        public int Id { get; set; }
        public string FullName { get; }


        public Client(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("Nust be not empty");
            
            FullName = fullName;
        }
    }
}