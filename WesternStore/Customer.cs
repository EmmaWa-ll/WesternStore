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
        public string CurrentLevel => DecideLevel(TotalSpent);


        List<CartItems> cart = new List<CartItems>();


        public bool VerifyPassword(string inputPW)
        {
            return Password == inputPW;
        }

        public string GetPassword() => Password;

        public abstract double CalculateDiscount(double total);

        public void AddToCart(Product p, int amount)
        {
            if (p == null || amount <= 0)
            {
                return;
            }

            CartItems itemInCart = null;
            foreach (var item in cart)
            {
                if (item.Product == p)
                {
                    itemInCart = item;
                    break;
                }
            }

            if (itemInCart != null)
            {
                itemInCart.Add(amount);
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

            Console.WriteLine($"{Name} (Pw: {Password})");
            Console.WriteLine("\nCart: \n");
            Console.WriteLine("---------------------------------------------------");
            var currency = CurrencyHelper.CurrentCurrency;   //hämtar den valuta som används just nu
            string symbol = CurrencyHelper.GetSymbol(currency); //hämtar rätt symbol för aktuell valuta 

            foreach (var item in cart)
            {
                double price = CurrencyHelper.ConvertCurrency(item.Product.Price, currency);
                double totalItemPrice = CurrencyHelper.ConvertCurrency(item.Total(), currency);
                Console.WriteLine($"{item.Product.Name,-20} {price:F2} {symbol} Amount: {item.Amount,2} Total: {totalItemPrice:F2} ");
            }
            Console.WriteLine("---------------------------------------------------");
            double total = Total();
            double discountedTotal = CalculateDiscount(total);


            Console.WriteLine($"Total: {CurrencyHelper.ConvertCurrency(total, currency):F2}");
            Console.WriteLine($"After discount: {CurrencyHelper.ConvertCurrency(discountedTotal, currency):F2} {symbol}  ");


            return string.Join("\n, lines");


        }


        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
            cart = new List<CartItems>();
        }

        public void AddSpent(double amount)
        {
            if (amount > 0)
            {
                TotalSpent += amount;
            }
        }

        public static string DecideLevel(double spent)
        {
            if (spent >= 10000)
            {
                return "Gold";

            }
            else if (spent >= 5000)
            {
                return "Silver";
            }
            else if (spent >= 2000)
            {

                return "Bronze";
            }
            else
            {
                return "Regular";
            }
        }


        public static Customer UpgradeIfNeeded(Customer c)
        {
            var newLevel = DecideLevel(c.TotalSpent);

            if (newLevel == c.LevelType)
            {
                return c;
            }
            Customer upgraded;

            if (newLevel == "Gold")
            {
                upgraded = new GoldCustomer(c.Name, c.GetPassword());
            }
            if (newLevel == "Silver")
            {
                upgraded = new SilverCustomer(c.Name, c.GetPassword());
            }
            if (newLevel == "Bronze")
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
