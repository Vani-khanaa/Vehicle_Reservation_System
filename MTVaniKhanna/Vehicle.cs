using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MTVaniKhanna
{
    public enum Category
    {
        Hatchback,
        Sedan,
        SUV,
        Cruiser,
        Sports,
        Dirt
    }

    public enum Type
    {
        Standard,
        Exotic,
        Bike,
        Trike
    }
    public abstract class Vehicle
    {
        private int id;
        public int ID { get; set; }

        private string name;
        public string Name { get; set; }

        private double rentalPrice;
        public double RentalPrice { get; set; }

        private string category;
        public string Category { get; set; }

        private string type;
        public string Type { get; set; }

        private bool isReserved;
        public bool IsReserved { get; set; }

        public Vehicle(int id, string name, double rentalPrice, string category, string type, bool isReserved)
        {
            ID = id;
            Name = name;
            RentalPrice = rentalPrice;
            Category = category;
            Type = type;
            IsReserved = isReserved;
        }

      

    }
}
