namespace WesternStore.Products
{
    public class Supplies : Product
    {

        public Supplies(string name, string brand, double price) : base(name, brand, price)
        {
        }

        public override string ProductInfo()
        {
            return $"{Name} - Brand: {Brand}  | Price: {Price}  ";
        }
    }
}
