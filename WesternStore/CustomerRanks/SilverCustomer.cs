namespace WesternStore.CustomerLevel
{
    public class SilverCustomer : Customer
    {
        public SilverCustomer(string name, string password) : base(name, password)
        {

        }

        public override double DiscountRate => 0.10;
        public override string LevelType => "Silver";
        public override double CalculateDiscount(double total)
        {
            return total * 0.90;
        }
    }
}
