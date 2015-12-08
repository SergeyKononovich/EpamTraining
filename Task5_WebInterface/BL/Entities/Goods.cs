using System;

namespace BL.Entities
{
    public class Goods
    {
        public Goods(string name, int cost)
        {
            if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Must be not empty");
            if (cost < 0) throw new ArgumentException("Must be greater than 0.");
            
            Name = name;
            Cost = cost;
        }


        public int Id { get; set; }
        public string Name { get; }
        public int Cost { get; }
    }
}