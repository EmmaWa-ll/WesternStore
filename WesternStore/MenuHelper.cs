namespace WesternStore
{
    public class MenuHelper
    {
        private readonly StoreHelper Store;
        private readonly List<Customer> Customers;
        private Currency _currency;

        public MenuHelper(StoreHelper store, List<Customer> customers, Currency currency)
        {
            Store = store;
            Customers = customers;
            _currency = CurrencyHelper.CurrentCurrency;
        }

        public MenuHelper(StoreHelper store, List<Customer> customers)
        {
            Store = store;
            Customers = customers;
        }

        public void MainMenu()
        {

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine("======================================");
                Console.WriteLine("      W E S T E R N   S T O R E   ");
                Console.WriteLine("======================================\n");
                Console.ResetColor();
                Console.WriteLine("[1] Login");
                Console.WriteLine("[2] Register New Customer ");
                Console.WriteLine("[3] Exit shop");
                Console.WriteLine("-------------------------------------");
                Console.Write("\nEnter choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Console.WriteLine("== Login ==");
                        var LoggedInCustomer = AuthService.CustomerLogIn(Customers);
                        if (LoggedInCustomer != null)
                        {
                            CustomerMenu(LoggedInCustomer);
                        }
                        break;
                    case "2":
                        AuthService.RegisterCustomer(Customers);
                        break;
                    case "3":
                        Console.WriteLine("Hope we'll see you back soon!");
                        return;
                    default:
                        Console.Write("Invalid choice. Press any key to continue.");
                        Console.ReadKey();
                        break;
                }
            }

        }

        public void CustomerMenu(Customer customer)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("======================================");
                Console.WriteLine($"      W E L C O M E   {customer.Name} ");
                Console.WriteLine("======================================\n");
                Console.WriteLine("[1] Shop Products");
                Console.WriteLine("[2] View Cart");
                Console.WriteLine("[3] Check Out (buy)");
                Console.WriteLine("[4] Change currency (SEK/EUR(USD)");
                Console.WriteLine("[0] Log Out");
                Console.WriteLine("-------------------------------------");
                Console.Write("\nEnter choice: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        Store.ShopProducts(customer);
                        break;
                    case "2":
                        StoreHelper.ViewCart(customer);
                        break;
                    case "3":
                        customer = StoreHelper.CheckOut(customer);
                        break;
                    case "4":
                        _currency = CurrencyHelper.ChangeCurrency(_currency);
                        break;
                    case "0":
                        return;
                    default:
                        Console.Write("Invalid choice. Press any key to continue.");
                        Console.ReadKey();
                        break;

                }
            }
        }


    }
}
