using WesternStore.Products;

namespace WesternStore
{
    public class CartItems
    {
        public Product Product { get; }
        public int Amount { get; private set; }

        public CartItems(Product product, int amount)
        {
            Product = product;
            Amount = amount;
        }

        public double Total()
        {
            return Product.Price * Amount;
        }
    }
}
