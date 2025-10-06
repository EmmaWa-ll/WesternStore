namespace WesternStore.Products
{
    public abstract class Product
    {
        public string Name { get; private set; }
        public string Brand { get; private set; }
        public double Price { get; private set; }


        protected Product(string name, string brand, double price)
        {
            Name = name;
            Brand = brand;
            Price = price;
        }

        public abstract string ProductInfo();

        public double TotalPrice(int amount)
        {
            return Price * amount;
        }
    }
}
