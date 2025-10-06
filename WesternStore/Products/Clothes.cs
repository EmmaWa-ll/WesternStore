namespace WesternStore.Products
{
    public class Clothes : Product
    {

        public string Size { get; private set; }
        public Clothes(string name, string brand, string size, double price) : base(name, brand, price)
        {
            Size = size;
        }

        public override string ProductInfo()
        {
            return $"{Name}: {Brand}   | Size: {Size}   | Price: {Price}  ";
        }
    }
}
