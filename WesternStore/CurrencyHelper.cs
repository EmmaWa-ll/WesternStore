namespace WesternStore
{

    public enum Currency { SEK, EUR, USD }

    public class CurrencyHelper

    {
        public static Currency CurrentCurrency { get; set; } = Currency.SEK;
        public static double ConvertCurrency(double sekPrice, Currency currency)
        {
            return currency switch
            {
                Currency.EUR => sekPrice * 0.09,
                Currency.USD => sekPrice * 0.10,
                _ => sekPrice

            };
        }

        public static string GetSymbol(Currency currency)
        {
            switch (currency)
            {
                case Currency.SEK:
                    return "kr";
                case Currency.USD:
                    return "$";
                case Currency.EUR:
                    return "€";
                default:
                    return "";
            }
        }


        public static Currency ChangeCurrency(Currency current)
        {
            Console.Clear();
            Console.WriteLine("=== CHANGE CURRENCY ===");
            Console.WriteLine("[1] SEK");
            Console.WriteLine("[2] EUR");
            Console.WriteLine("[3] USD");
            Console.Write("Choice: ");
            var choice = Console.ReadLine();

            Currency newCurrency = choice switch
            {
                "1" => Currency.SEK,  //=> betyder return 
                "2" => Currency.EUR,
                "3" => Currency.USD,
                _ => current  // _ används som default 

            };

            CurrentCurrency = newCurrency;

            Console.WriteLine($"\nCurrency changed to: {newCurrency}");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();

            return newCurrency;
        }

    }
}
