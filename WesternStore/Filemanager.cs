using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WesternStore.CustomerRanks;

namespace WesternStore
{
    public  class FileManager
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
                        var parts = line.Split(';');
                        if (parts.Length >= 2)
                        {
                            customers.Add(new RegularCustomer(parts[0], parts[1]));
                        }
                    }
                }
            }
            return customers;

        }
        
    }
}
