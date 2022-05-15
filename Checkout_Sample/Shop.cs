using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout_Sample
{
    public static class Shop
    {
        /* Product/Price mappings */
        public static Dictionary<Type, decimal> SKUs = new Dictionary<Type, decimal>() 
        {
            {typeof(Products.A), 10m },
            {typeof(Products.B), 15m },
            {typeof(Products.C), 40m },
            {typeof(Products.D), 55m },
        };

        /*Introduce discounts*/
        public static Dictionary<Type, Func<List<Product>, decimal>> Discounts = new Dictionary<Type, Func<List<Product>, decimal>>()
        {
            {typeof(Products.B) sku => {
                var multipackB = sku.Where(s => s.GetType() == typeof(Products.B)).ToList();
                var count = multipackB.Count;

                if (count > 0)
                {
                    var price = multipackB[0].Price;
                    var discountPrice = Math.Floor((count / 3.0m) * 5);

                    return (price - discountPrice);
                }
                else
                {
                    return 0m;
                }
            }},
            {typeof(Products.D) sku => {
            
            }}
        };

        /*Return the actual price of products*/
        public static Product? GetProduct(Type product)
        {
            decimal price;

            if (Shop.SKUs.TryGetValue(product, out price))
            {
                return Activator.CreateInstance(product, new object[] { price }) as Product;
            }
            else
            {
                throw new ArgumentOutOfRangeException("SKU does not exist");
            }
        }
    }
}
