using System.Text;

namespace WesternStore
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            Console.InputEncoding = Encoding.UTF8;

            var customers = FileManager.LoadCustomers();

            StoreHelper store = new StoreHelper();

            MenuHelper menuService = new MenuHelper(store, customers);

            menuService.MainMenu();
        }
    }
}
