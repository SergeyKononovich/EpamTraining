using System;

namespace Task4_FolderMonitor.BL.Entities
{
    public class Manager
    {
        public int Id { get; set; }
        public string SecondName { get; }


        public Manager(string secondName)
        {
            if (string.IsNullOrWhiteSpace(secondName)) throw new ArgumentException("Nust be not empty");
            
            SecondName = secondName;
        }
    }
}