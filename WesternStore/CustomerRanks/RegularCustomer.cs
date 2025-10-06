namespace WesternStore.CustomerRanks
{
    public class RegularCustomer : Customer
    {
        public RegularCustomer(string name, string password) : base(name, password)
        {
        }

        public override double CalculateDiscount(double total)
        {
            return total;
        }
    }
}
