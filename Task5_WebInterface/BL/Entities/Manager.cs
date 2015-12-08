using System;

namespace BL.Entities
{
    public class Manager
    {
        public Manager(string secondName)
        {
            if (string.IsNullOrWhiteSpace(secondName)) throw new ArgumentException("Must be not empty");
            
            SecondName = secondName;
        }


        public int Id { get; set; }
        public string SecondName { get; }
    }
}