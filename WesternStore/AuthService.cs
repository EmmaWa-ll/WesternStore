namespace WesternStore
{
    public static class AuthService
    {

        private const string FilePath = "customer.txt";


        public static void RegisterCustomer(List<Customer> customers)
        {
            Console.Clear();
            Console.WriteLine("== Register New Customer. ==\n");

            Console.Write("Enter name: ");
            string name = Console.ReadLine().Trim();
            Console.Write("Enter password: ");
            string password = Console.ReadLine();

            bool isRegistered = TryRegisterCustomer(customers, name, password);

            if (isRegistered)
            {
                Console.WriteLine("Customer created! ");
                using (StreamWriter sw = new StreamWriter(FilePath, append: true))
                {
                    sw.WriteLine($"{name};{password}");
                }
            }
            else
            {
                Console.WriteLine("The name is already registered as a customer.");
            }
        }

        public static Customer CustomerLogIn(List<Customer> customers)
        {

            while (true)
            {

                Console.Clear();
                Console.WriteLine("== LogIn ==\n");

                Console.Write("Name: ");
                string inputName = Console.ReadLine();

                var customer = customers.FirstOrDefault(c => c.Name == inputName);
                if (customer == null)
                {
                    Console.WriteLine("Customer doesn't exist.");
                    Console.WriteLine("Would you like to register new customer? (yes/no): ");
                    string answer = Console.ReadLine().Trim().ToLower();

                    if (answer == "yes")
                    {
                        RegisterCustomer(customers);
                    }
                    else
                    {
                        return null;  //tillbaka till huvudmeny
                    }
                    continue;
                }
                Console.Write("Password: ");
                string inputPW = Console.ReadLine();

                if (!customer.VerifyPassword(inputPW))
                {
                    Console.WriteLine("Wrong password, try again! ");
                    Console.ReadKey();
                    continue;
                }
                Console.WriteLine($"\nLogIn succesful. Welcome {customer.Name}");
                Console.ReadKey();
                return customer;
            }


        }
        public static Customer FindCustomerByname(List<Customer> customers, string inputName)
        {

            foreach (Customer c in customers)
            {
                if (c.Name == inputName)  //jämför kundens namn med det använd skrev in, om lika (rätt kund hittad)
                {
                    return c;
                }
            }
            return null;  //skickar tillbaka 'null' om det ej fanns någon kund med det namnet.
        }


        public static bool TryRegisterCustomer(List<Customer> customers, string name, string password)
        {
            foreach (Customer c in customers)//kolla om namnet redan finns
            {

                if (c.Name == name)
                {
                    return false;   //registrering misslyckades 
                }
            }
            customers.Add(new Customer(name, password));
            return true;  //registering lyckasdess. 
        }


    }
}
