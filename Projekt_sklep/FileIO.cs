using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_sklep
{
    public class FileIO
    {
        public static List<User> ReadUsersFromFile()
        {
            var users = new List<User>();

            try
            {
                using (StreamReader reader = new StreamReader("users.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] userData = line.Split(';');

                        if (userData.Length == 11)
                        {
                            int Id = int.Parse(userData[0].Trim());
                            string firstName = userData[1].Trim();
                            string lastName = userData[2].Trim();
                            string email = userData[3].Trim();
                            string phoneNumber = userData[4].Trim();
                            string address = userData[5].Trim();
                            string username = userData[6].Trim();
                            string password = userData[7].Trim();
                            string wholesaleClientTemp = userData[8].Trim();
                            string adminTemp = userData[9].Trim();
                            double balace = Convert.ToDouble(userData[10].Trim());

                            bool wholesaleClient = false;
                            bool admin = false;
                            if (wholesaleClientTemp == "True")
                            {
                                wholesaleClient = true;
                            }
                            else
                            {
                                wholesaleClient = false;
                            }

                            if (adminTemp == "True")
                            {
                                admin = true;
                            }
                            else
                            {
                                admin = false;
                            }


                            User user = new User(Id, firstName, lastName, email, phoneNumber, address, username, password, wholesaleClient, admin, balace);
                            users.Add(user);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas odczytu pliku: {ex.Message}");
            }

            return users;
        }
        public static void SaveUsersToFile(List<User> users)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("users.txt"))
                {
                    foreach (User user in users)
                    {
                        writer.WriteLine($"{user.Id};{user.FirstName};{user.LastName};{user.Email};{user.PhoneNumber};{user.Address};{user.Username};{user.Password};{user.WholesaleClient};{user.Administrator};{user.Balance}");

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisu do pliku: {ex.Message}");
            }
        }

        public static List<Product> ReadProcuctsFromFile()
        {
            var products = new List<Product>();

            try
            {
                using (StreamReader reader = new StreamReader("products.txt"))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();
                        string[] productData = line.Split(';');

                        if (productData.Length == 4)
                        {
                            int id = int.Parse(productData[0].Trim());
                            string name = productData[1].Trim();
                            double price = Convert.ToDouble(productData[2].Trim());
                            int quantity = int.Parse(productData[3].Trim());

                            Product product = new Product(id, name, price, quantity);
                            products.Add(product);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas odczytu pliku: {ex.Message}");
            }

            return products;
        }
        public static void SaveProductsToFile(List<Product> products)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter("products.txt"))
                {
                    foreach (Product p in products)
                    {
                        writer.WriteLine($"{p.id};{p.name};{p.price};{p.quantity}");

                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd podczas zapisu do pliku: {ex.Message}");
            }
        }
    }
    
}
