using System;

namespace Task4_FolderMonitor.BL.Entities
{
    public class Goods
    {
        public int Id { get; set; }
        public string Name { get; }
        public int Cost { get; }


        public Goods(string name, int cost)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Nust be not empty");
            if (cost < 0) throw new ArgumentException("Must be greater than 0.");
            
            Name = name;
            Cost = cost;
        }
    }
}