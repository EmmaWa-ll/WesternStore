using WesternStore.CustomerRanks;
namespace WesternStore
{
    internal class Program
    {
        static void Main(string[] args)
        {

            StoreHelper store = new StoreHelper();

            string FilePath = "customers.txt";
            var customers = new List<Customer>();

            if (File.Exists(FilePath))
            {
                using (StreamReader sr = new StreamReader(FilePath))
                {
                    string? line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        var parts = line.Split(';');
                        if (parts.Length >= 2)
                        {
                            customers.Add(new RegularCustomer(parts[0], parts[1]));
                        }
                    }
                }
            }

            MenuService menuService = new MenuService(store, customers);

            menuService.MainMenu();
        }
    }
}
