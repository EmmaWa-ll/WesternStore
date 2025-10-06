namespace WesternStore.CustomerLevel
{
    public class GoldCustomer : Customer
    {
        public GoldCustomer(string name, string password) : base(name, password)
        {
        }

        public override double DiscountRate => 0.15;
        public override string LevelType => "Gold";
        public override double CalculateDiscount(double total)
        {
            return total * 0.85;
        }
    }
}
