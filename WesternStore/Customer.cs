using System.Text;
using WesternStore.CustomerLevel;
using WesternStore.CustomerRanks;
using WesternStore.Products;

namespace WesternStore
{
    public abstract class Customer
    {

        public string Name { get; private set; }
        protected string Password { get; private set; }

        public virtual double DiscountRate => 0.0;
        public virtual string LevelType => "Regular";
        public double TotalSpent { get; private set; }
        public string CurrentLevel => TargetLevel(TotalSpent);


        List<CartItems> cart = new List<CartItems>();


        public bool VerifyPassword(string inputPW)
        {
            return Password == inputPW;
        }

        public void AddToCart(Product p, int amount)
        {
            if (p == null || amount <= 0)
            {
                return;
            }

            var row = cart.FirstOrDefault(ci => ci.Product == p);
            if (row != null)
            {
                row.Add(amount);
            }
            else
            {
                cart.Add(new CartItems(p, amount));
            }

        }


        public void ClearCart()
        {
            cart.Clear();
        }


        public double Total()
        {
            double totalSum = 0;
            foreach (CartItems item in cart)
            {
                totalSum += item.Total();
            }
            return totalSum;
        }


        public override string ToString()
        {
            if (cart.Count == 0)
            {
                return $"{Name} (Pw: {Password}),  The Cart is empty!";
            }

            var sb = new StringBuilder(); //StringBuilder = 
            sb.AppendLine($"{Name} (Pw: {Password})"); //AppendLine =
            sb.AppendLine($"Level: {LevelType} |  Discount: {DiscountRate * 100}%");
            Console.WriteLine("\nCart: ");
            sb.AppendLine(new string('-', 26));


            foreach (var item in cart)
            {
                sb.AppendLine($"{item.Product.Name,-22} {item.Product.Price,6} kr x {item.Amount,2} = {item.Total(),6} kr.");
            }
            sb.AppendLine(new string('-', 26));
            sb.AppendLine($"Total: {Total()} kr");
            double discounted = CalculateDiscount(Total());
            sb.AppendLine($"Discounted totall: {discounted:F2}");
            return sb.ToString();
        }

        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
            cart = new List<CartItems>();
        }

        public string GetPassword() => Password;

        public abstract double CalculateDiscount(double total);

        public void AddSpent(double amount)
        {
            if (amount > 0)
            {
                TotalSpent += amount;
            }
        }

        public static string TargetLevel(double spent)
        {
            if (spent >= 10000)
                return "Gold";
            if (spent >= 5000)
                return "Silver";
            if (spent >= 2000)
                return "Bronze";

            return "Regular";
        }


        public static Customer UpgradeIfNeeded(Customer c)
        {
            var target = TargetLevel(c.TotalSpent);

            if (target == c.LevelType)
            {
                return c;
            }
            Customer upgraded;

            if (target == "Gold")
            {
                upgraded = new GoldCustomer(c.Name, c.GetPassword());
            }
            if (target == "Silver")
            {
                upgraded = new SilverCustomer(c.Name, c.GetPassword());
            }
            if (target == "Bronze")
            {
                upgraded = new BronzeCustomer(c.Name, c.GetPassword());
            }
            else
            {
                upgraded = new RegularCustomer(c.Name, c.GetPassword());
            }
            upgraded.TotalSpent = c.TotalSpent;
            return upgraded;
        }

    }
}
