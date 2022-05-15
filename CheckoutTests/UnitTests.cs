using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.ComponentModel;
using DescriptionAttribute = Microsoft.VisualStudio.TestTools.UnitTesting.DescriptionAttribute;
using C = Checkout_Sample;

namespace CheckoutTests
{
    [TestClass]
    public class UnitTests
    {
        [TestMethod, Description("Price of 1x each item in basket, should equal 120.")]
        public void Sumof1ofEachItem()
        {
            var shopCart = new C.Shop_Cart();
            shopCart.AddStock(C.Shop.GetProduct(typeof(C.Products.A)));
            shopCart.AddStock(C.Shop.GetProduct(typeof(C.Products.B)));
            shopCart.AddStock(C.Shop.GetProduct(typeof(C.Products.C)));
            shopCart.AddStock(C.Shop.GetProduct(typeof(C.Products.D)));

            Assert.AreEqual(120, shopCart.CalculatePrice());
        }


        [TestMethod, Description("Price of 3x B, should equal 40.")]
        public void CheckDiscountB()
        {
            var shopCart = new C.Shop_Cart();
            shopCart.AddStock(C.Shop.GetProduct(typeof(C.Products.B)));
            shopCart.AddStock(C.Shop.GetProduct(typeof(C.Products.B)));
            shopCart.AddStock(C.Shop.GetProduct(typeof(C.Products.B)));

            Assert.AreEqual(40.00, shopCart.CalculatePrice());
        }


        [TestMethod, Description("Price of 4x D, should equal 165.")]
        public void CheckDiscountD()
        {
            var shopCart = new C.Shop_Cart();
            shopCart.AddStock(C.Shop.GetProduct(typeof(C.Products.D)));
            shopCart.AddStock(C.Shop.GetProduct(typeof(C.Products.D)));
            shopCart.AddStock(C.Shop.GetProduct(typeof(C.Products.D)));
            shopCart.AddStock(C.Shop.GetProduct(typeof(C.Products.D)));

            Assert.AreEqual(165.00, shopCart.CalculatePrice());
        }
    }
}