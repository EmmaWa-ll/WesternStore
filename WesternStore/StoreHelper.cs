using WesternStore.CustomerLevel;
using WesternStore.Products;

namespace WesternStore
{
    public class StoreHelper
    {

        public void ShopProducts(Customer customer)
        {

            List<Product> currentList = null;

            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== SHOP ===\n");
                Console.WriteLine("[1] Clothes  [2] Horse Tack  [3] Supplies  [0] Back ");
                Console.Write("\nEnter choice: ");
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        currentList = ListsOfProducts.Get<Clothes>().Cast<Product>().ToList();
                        break;
                    case "2":
                        currentList = ListsOfProducts.Get<HorseTack>().Cast<Product>().ToList();
                        break;
                    case "3":
                        currentList = ListsOfProducts.Get<Supplies>().Cast<Product>().ToList();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again!");
                        Console.ReadKey();
                        break;

                }
                if (currentList == null)
                {
                    Console.WriteLine("Invalid chooice, Press any key to try again...");
                    Console.ReadKey();
                    continue;
                }
                ShowAndAdd(customer, currentList);
            }
        }

        private static void ShowAndAdd(Customer customer, List<Product> list)
        {

            var currency = CurrencyHelper.CurrentCurrency;
            string symbol = CurrencyHelper.GetSymbol(currency);
            while (true)
            {
                Console.Clear();
                Console.WriteLine("=== Products ===\n ");
                Console.WriteLine("Idx | Name                  | Brand         | Size   | Price");
                Console.WriteLine("----+------------------------+-----------------+--------+------------");

                for (int i = 0; i < list.Count; i++)
                {
                    var p = list[i];
                    string size = p is Clothes c ? c.Size : "";
                    double converted = CurrencyHelper.ConvertCurrency(p.Price, currency);
                    string price = converted.ToString("N0") + " " + symbol;  //N0 (noll ej ¨bokatsv O) = visae ingen decimal
                    Console.WriteLine(
                        $"{i + 1,3} | " +
                        $"{p.Name,-22} | " +
                        $"{p.Brand,-12} | " +
                        $"{size,-4} | " + "" +
                        $"{price,10}");
                    Console.WriteLine("----+------------------------+-----------------+--------+------------");
                }
                Console.WriteLine("Press '0' to go back.");

                int choice;
                while (true)
                {
                    Console.Write("\nChoice : ");
                    var input = Console.ReadLine().Trim();

                    if (input == "0") return;

                    if (!int.TryParse(input, out choice))
                    {
                        Console.WriteLine("Invalid. Choose a number.");
                        continue;
                    }
                    if (choice < 1 || choice > list.Count) continue;
                    break;
                }

                int amount;
                while (true)
                {
                    Console.Write("Amount: ");
                    string? input = Console.ReadLine();

                    if (int.TryParse(input, out amount) && amount > 0)
                    {
                        break;
                    }
                    Console.WriteLine("Invalid, choose a number.");

                }

                if (amount >= 1)
                {
                    customer.AddToCart(list[choice - 1], amount);
                    Console.Write("\nAdded to the cart. Press any key to continue...");
                    Console.ReadKey();
                }
                else
                {
                    Console.WriteLine("Invalid choice. Try again. ");
                    Console.WriteLine("Press any key to continue...");
                    Console.ReadKey();
                }
            }


        }

        public static void ViewCart(Customer customer)
        {
            Console.Clear();
            Console.WriteLine(customer.ToString());
            Console.Write("\nPress any key to continue...");
            Console.ReadKey();
        }


        public static Customer CheckOut(Customer customer)
        {
            Console.Clear();
            Console.WriteLine("=== CHECKOUT ===");

            Console.WriteLine(customer.ToString());

            if (customer.Total() <= 0)
            {
                Console.Write("\n The cart is empty. Press any key to go back...");
                Console.ReadKey();
                return customer;

            }

            Console.Write("Confirm (yes/no)?: ");
            string answer = Console.ReadLine().Trim().ToLower();

            if (answer == "yes" || answer == "Yes")
            {
                double total = customer.Total();
                double discountTotal = customer.CalculateDiscount(total);
                Console.WriteLine("\n=== RECEIPT ===");
                Console.WriteLine($"Original total: {total} kr");
                Console.WriteLine($"Discount total: {discountTotal} kr");
                customer.AddSpent(discountTotal);
                string newLevel = Customer.DecideLevel(customer.TotalSpent);

                if (newLevel == "Gold" && customer is not GoldCustomer)
                {
                    customer = new GoldCustomer(customer.Name, customer.GetPassword());

                }
                else if (newLevel == "Silver" && customer is not SilverCustomer)
                {

                    customer = new SilverCustomer(customer.Name, customer.GetPassword());
                }
                else if (newLevel == "Bronze" && customer is not BronzeCustomer)
                {
                    customer = new BronzeCustomer(customer.Name, customer.GetPassword());
                }

                Console.Write("\nThank you for your purchase.");
                customer.ClearCart();
                Console.Write("\nPress any key to continue...");
                Console.ReadKey();
                return customer;
            }
            else
            {
                Console.WriteLine("\nOrder cancelled.");
                Console.Write("\nPress any key to continue...");
                Console.ReadKey();
                return customer;
            }
        }

    }
}
