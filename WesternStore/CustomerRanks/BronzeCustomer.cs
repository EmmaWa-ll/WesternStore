namespace WesternStore.CustomerLevel
{
    public class BronzeCustomer : Customer
    {
        public BronzeCustomer(string name, string password) : base(name, password)
        {
        }

        public override double DiscountRate => 0.05;
        public override string LevelType => "Bronze";
        public override double CalculateDiscount(double total)
        {
            return total * 0.95;
        }
    }
}
