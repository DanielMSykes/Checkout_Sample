using System;
using System.Collections.Generic;
using System.Linq;

namespace Checkout_Sample
{
    public static class Shop
    {
        /* Product/Price mappings */
        public static Dictionary<Type, double> SKUs = new Dictionary<Type, double>()
        {
            {typeof(Products.A), 10.00 },
            {typeof(Products.B), 15.00 },
            {typeof(Products.C), 40.00 },
            {typeof(Products.D), 55.00 },
        };

        /*Introduce discounts*/
        public static Dictionary<Type, Func<List<Product>, double>> Discounts = new Dictionary<Type, Func<List<Product>, double>>()
        {
            /*3 for 40 of B*/
            {typeof(Products.B), sku => {
                var multipackB = sku.Where(s => s.GetType() == typeof(Products.B)).ToList();
                var count = multipackB.Count;

                if (count > 0)
                {
                    double price = multipackB[0].Price;
                    double discount = Math.Floor(count / 3.00) * 5;
                    double discountedPrice = price - discount;

                    return discountedPrice;
                }
                else
                {
                    return 0;
                }
            }},
            /*25% off for every 2 of 'D' purchased together*/
            {typeof(Products.D), sku => {

                var multipackD = sku.Where(s => s.GetType() == typeof(Products.D)).ToList();
                var count = multipackD.Count;

                if (count > 0)
                {
                    double price = multipackD[0].Price;
                    double discount = Math.Floor((count / 2) * 27.50);

                    return price - discount;
                }
                else
                {
                    return 0;
                }
            }}
        };

        /*Return the actual price of products*/
        public static Product? GetProduct(Type product)
        {
            double price;

            if (Shop.SKUs.TryGetValue(product, out price))
            {
                return Activator.CreateInstance(product, new object[] { price }) as Product;
            }
            else
            {
                throw new ArgumentOutOfRangeException("SKU does not exist");
            }
        }

             
        /*return the price of the basket with all discounts applied*/
        public static double CalculatePrice(List<Product> stockUnits)
        {
            double sum = 0;

            foreach (Type sku in Shop.SKUs.Keys)
            {
                Func<List<Product>, double> discount;

                if (Shop.Discounts.TryGetValue(sku, out discount))
                {
                    sum += discount(stockUnits);
                }
                else
                {
                    var products = stockUnits.Where(s => s.GetType() == sku).ToList();
                    double count = products.Count();

                    if (count > 0)
                    {
                        sum += count * products[0].Price;
                    }
                }
            }

            return sum;
        }
    }
}
