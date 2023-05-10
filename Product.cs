using System;

namespace Price_Calculator_kata
{
    public class Product
    {
        private double _Price;
<<<<<<< HEAD
=======
>>>>>>> 1093ff0c3d374856a59a55d48cfb2415375798e0
        public string Name { get; set; }
        public int UPC { get; set; }
        public Product(string Name, int UPC, double Price)
        {
            this.Name = Name;
            this.UPC = UPC;
            this.Price = Price;
        }
        
        public double Price {
            get => _Price;
            set {
                if (value > 0)
                   _Price = Math.Round(value, 2); } 
        }

<<<<<<< HEAD
=======
>>>>>>> 1093ff0c3d374856a59a55d48cfb2415375798e0
        
        public override string ToString()
        {
            return $"Product Name:{this.Name}\nProduct UPC:{this.UPC}\nProduct Name:{this.Price}";
        }

    }
 
}