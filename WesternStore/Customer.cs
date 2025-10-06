using System.Text;
using WesternStore.Products;

namespace WesternStore
{
    public class Customer
    {

        public string Name { get; private set; }
        private string Password { get; set; }

        List<CartItems> cart = new List<CartItems>();


        public Customer(string name, string password)
        {
            Name = name;
            Password = password;
            cart = new List<CartItems>();
        }

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
            cart.Add(new CartItems(p, amount));
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
            sb.AppendLine($"{Name} (Pw: {Password}) \n Cart"); //AppendLine =
            sb.AppendLine(new string('=', 26));


            foreach (var item in cart)
            {
                sb.AppendLine($"{item.Product.Name,-22} {item.Product.Price,6} kr x {item.Amount,2} = {item.Total(),6} kr.");
            }
            sb.AppendLine(new string('-', 26));
            sb.AppendLine($"Total: {Total()} kr");
            return sb.ToString();
        }
    }
}
