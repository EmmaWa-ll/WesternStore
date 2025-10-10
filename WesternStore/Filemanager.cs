using WesternStore.CustomerRanks;

namespace WesternStore
{
    public class FileManager
    {
        private static string FilePath = "customers.txt";

        public static List<Customer> LoadCustomers()
        {

            var customers = new List<Customer>();

            if (File.Exists(FilePath))
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        if (string.IsNullOrWhiteSpace(line)) continue;
                        var parts = line.Split(';');
                        if (parts.Length >= 2)
                        {
                            customers.Add(new RegularCustomer(parts[0], parts[1]));
                        }
                    }
                }
            }

            if (customers.Count == 0)
            {
                customers = new List<Customer>
                {
                    new RegularCustomer("Knatte", "123"),
                    new RegularCustomer("Fnatte", "321"),
                    new RegularCustomer("Tjatte", "213")
                };
                SaveCustomers(customers);
            }

            return customers;
        }
        public static void SaveCustomers(List<Customer> customers)
        {
            using (StreamWriter sw = new StreamWriter(FilePath))
            {
                foreach (var c in customers)
                {
                    sw.WriteLine($"{c.Name};{c.GetPassword()}");
                }
            }
        }


    }
}
