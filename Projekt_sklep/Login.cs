using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace Projekt_sklep
{
    public class Login : FileIO
    {
        public static User loggedUser { get; private set; }
        public static User Auth()
        {
            login:
            Console.WriteLine("Wprowadź dane logowanie:");
            Console.WriteLine("Login:");
            string usernameInput = Console.ReadLine();
            Console.WriteLine("Hasło:");
            string passwordInput = Console.ReadLine();
            Console.Clear();

            List<User> users = ReadUsersFromFile();
            foreach(User u  in users)
            {
                if(u.Username == usernameInput &&  u.Password == passwordInput)
                {
                    loggedUser = u;
                }
            }
            if(loggedUser != null)
            {
                return loggedUser;
            }
            else
            {
                Console.Clear();
                Console.WriteLine("Nieprawidłowe dane logowanie, spróbuj ponownie.");
                goto login;
            }
        }
    }
}
