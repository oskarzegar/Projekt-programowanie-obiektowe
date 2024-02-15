using System.ComponentModel.Design;

namespace Projekt_sklep
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Dzień dobry!");
            menu:
            Console.WriteLine("Wybierz pozycję z menu:\n1.Strona logowania.\n2.Strona rejestracji.\n3.Przeglądaj produkty.\n4.Wyjście.");
            var userInput = Convert.ToInt32(Console.ReadLine());
            switch(userInput)
            {
                case 1:
                    User loggedUser = Login.Auth();
                    Console.WriteLine($"Witaj {loggedUser.Username}\nŚrodki na koncie: {loggedUser.Balance}");
                    if (loggedUser.Administrator == true)
                    {
                        adminmenu:
                        Console.WriteLine("1. Panel klienta.\n2. Panel administratora");
                        string submenu = Console.ReadLine();
                        if(submenu == "1")
                        {
                            ClientPanel.ClientMenu(loggedUser);
                        }
                        else if(submenu == "2")
                        {
                            AdminPanel.AdminMenu();
                        }
                        else
                        {
                            Console.Clear();
                            Console.WriteLine("Nie wybrano żadnej pozycji, spróbuj ponowanie.");
                            goto adminmenu;
                        }
                    }
                    else
                    {
                        Console.Clear();
                        ClientPanel.ClientMenu(loggedUser);
                    }
                    break;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Wprowadź dane do rejestracji");
                    User.Registration(false);
                    submenu:
                    Console.WriteLine("1.Powrót.\n2.Wyjście");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    if (choice == 1)
                    {
                        Console.Clear();
                        goto menu;
                    }
                    else if(choice == 2)
                    {
                        Environment.Exit(0);
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Nie wybrano żadnej pozycji, spróbuj ponownie.");
                        goto submenu;
                    }
                    break;
                case 3:
                    Console.Clear();
                    Console.WriteLine("Lista produktów: \n");
                    List<Product> products = FileIO.ReadProcuctsFromFile();
                    foreach(Product p in products)
                    {
                        Console.WriteLine($"[ID: {p.id}], {p.name}, {p.price}zł, {p.quantity}");
                    }
                    Console.WriteLine("Zapraszamy do rejestracji aby dokonać zakupu.");
                    Console.WriteLine("[1] Strona rejestracji\n[2] Wyjście");
                    string regUserChoice = Console.ReadLine();
                    if(regUserChoice == "1")
                    {
                        User.Registration(false);
                    }
                    else
                    {
                        Environment.Exit(0);
                    }
                    break;
                case 4:
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wprowadzono błędne dane, spróbuj ponownie");
                    goto menu;
            }
        }
    }
}
