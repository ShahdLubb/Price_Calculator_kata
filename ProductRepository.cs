using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class ProductRepository 
    {
        private List<Product> _products;

        public ProductRepository()
        {
            _products = new List<Product>();
        }

        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Remove(Product product)
        {
            _products.Remove(product);
        }

        public Product GetByUPC(int upc)
        {
            return _products.FirstOrDefault(p => p.UPC == upc);
        }

        
        public void ApplyDiscountToAll(IDiscountCalculator Discount)
        {
            foreach( Product product in _products)
            {
                product.Discounts.Add(Discount); 
            }
        }

        public void PrintPriceReport()
        {
            foreach (Product product in _products)
            {
                Console.WriteLine(product.ReportPriceDetails());
            }
        }



    }

}
