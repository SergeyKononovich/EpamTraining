using System;

namespace BL.Entities
{
    public class Client
    {
        public Client(string fullName)
        {
            if (string.IsNullOrWhiteSpace(fullName)) throw new ArgumentException("Must be not empty");
            
            FullName = fullName;
        }


        public int Id { get; set; }
        public string FullName { get; }
    }
}