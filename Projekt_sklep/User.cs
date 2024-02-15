using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace Projekt_sklep
{
    public class User : FileIO
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public bool WholesaleClient { get; set; }
        public bool Administrator { get; set; }
        public double Balance { get; set; }

        public User(int id, string firstName, string lastName, string email, string phoneNumber, string address, string username, string password, bool wholesaleClient, bool administrator, double balance)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            PhoneNumber = phoneNumber;
            Address = address;
            Username = username;
            Password = password;
            WholesaleClient = wholesaleClient;
            Administrator = administrator;
            Balance = balance;
        }
        public static void Registration(bool admin)
        {
        reg:
            Console.WriteLine("Podaj imię:");
            string firstName = Console.ReadLine();
            Console.WriteLine("Podaj nazwisko:");
            string lastName = Console.ReadLine();
            Console.WriteLine("Podaj adres E-mail");
            string email = Console.ReadLine();
            Console.WriteLine("Podaj numer telefonu");
            string phoneNumber = Console.ReadLine();
            Console.WriteLine("Podaj adres:");
            string address = Console.ReadLine();
            Console.WriteLine("Podaj nazwę użytkownika:");
            string username = Console.ReadLine();
            Console.WriteLine("Podaj hasło:");
            string password = Console.ReadLine();
            Console.WriteLine("Czy jesteś klientem hurtowym: [Tak/Nie]");
            string wholesaleClientTemp = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(firstName) ||
               string.IsNullOrWhiteSpace(lastName) ||
               string.IsNullOrWhiteSpace(email) ||
               string.IsNullOrWhiteSpace(phoneNumber) ||
               string.IsNullOrWhiteSpace(address) ||
               string.IsNullOrWhiteSpace(username) ||
               string.IsNullOrWhiteSpace(password) ||
               string.IsNullOrWhiteSpace(wholesaleClientTemp))
            {
                Console.Clear();
                Console.WriteLine("Nie zostały wypełnione wszystkie pola, spróbuj ponownie.");
                goto reg;
            }
            else
            {
                bool wholesaleClient = false;
                if (wholesaleClientTemp == "Tak")
                {
                    wholesaleClient = true;
                }
                else if (wholesaleClientTemp == "Nie")
                {
                    wholesaleClient = false;
                }
                List<User> users = ReadUsersFromFile();
                int newId = users.Max(u => u.Id)+1;
                User newUser = new User(newId, firstName, lastName, email, phoneNumber, address, username, password, wholesaleClient, admin, 0);
                users.Add(newUser);
                try
                {
                    SaveUsersToFile(users);
                    Console.WriteLine("Pomyślnie zarejestrowano.");
                }
                catch (Exception)
                {

                    Console.WriteLine("Wystąpił nieoczekiwany błąd, spróbuj ponownie.");
                }

            }
        }
    }
}