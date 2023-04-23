﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Price_Calculator_kata
{
    public class DiscountService
    {
        private readonly ProductRepository _productRepository;

        public DiscountService(ProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public static IDiscountCalculator CreateRelativeDiscount(double discountPercentage)
        {
            return new RelativeDiscountCalculator(discountPercentage);
        }

        public static IDiscountCalculator CreateSelectiveRelativeDiscount(double discountPercentage, int UPC)
        {
            return new SelectiveRelativeDiscountCalculator(discountPercentage,UPC);
        }
        public void ApplyDiscountForAllProducts(IDiscountCalculator Discount)
        {
           _productRepository.ApplyDiscountToAll(Discount);
           _productRepository.PrintPriceReport();

        }
        public void ApplyDiscount(IDiscountCalculator Discount, int upc)
        {
            _productRepository.ApplyDiscount(Discount,upc);
            _productRepository.PrintPriceReport();

        }
    }
}
