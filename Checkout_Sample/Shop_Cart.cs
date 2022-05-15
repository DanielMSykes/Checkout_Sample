using System.Collections.Generic;

namespace Checkout_Sample
{
    public class Shop_Cart
    {
        private List<Product> stockUnits = new List<Product>();

        public void AddStock(Product stock)
        {
            this.stockUnits.Add(stock);
        }

        public  double CalculatePrice()
        {
            return Shop.CalculatePrice(this.stockUnits);
        }
    }
}
