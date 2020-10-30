using System;

namespace BackendSolution.Entity
{
    public class Product
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
