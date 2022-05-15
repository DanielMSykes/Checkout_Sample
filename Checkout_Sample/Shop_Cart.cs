using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Checkout_Sample
{
    public class Shop_Cart
    {
        private List<Product> stock = new List<Product>();

        public void AddStock(Product stock)
        {
            this.stock.Add(stock);
        }

        public  decimal CalculateCost()
        {
            return Shop.CalculateCost(this.stock);
        }
    }
}
