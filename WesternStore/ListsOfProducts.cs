using WesternStore.Products;

namespace WesternStore
{
    public class ListsOfProducts
    {

        private static readonly List<Product> products = new List<Product>
        {
            new Clothes("Cowboy Hat", "Stetson", "6" , 2999 ),
            new Clothes("Leather Boots", "Ariat", "37", 1999),
            new Clothes("Work Jacket", "Carhartt", "M", 1700),
            new Clothes("Bootcut Jeans","Wrangler", "M", 999),

            new HorseTack("Leather Saddle", "Circle Y", 3499),
            new HorseTack("Western Bridle", "Weaver Lether", 899),
            new HorseTack("Saddle Bag (Leather)", "Outback", 999),
            new HorseTack("Reins (Split Leather)", "Weaver Lether", 499),

            new Supplies("Coffe Beans", "Arbuckle's", 89),
            new Supplies("Whiskey Bottle", "Old Crow", 199),
            new Supplies("Cigaretts", "Marlboro", 59),
            new Supplies("Wool Blanket", "Praire Co", 399),
        };

        public static List<T> Get<T>() where T : Product => products.OfType<T>().ToList();

    }
}
