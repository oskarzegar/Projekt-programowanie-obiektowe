using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_sklep
{
    public class ClientPanel : FileIO
    {
        public static void ClientMenu(User loggedUser)
        {
            List<Product> products = FileIO.ReadProcuctsFromFile();
            List<User> users = ReadUsersFromFile();
            Console.WriteLine("Witaj w naszym sklepie, aby przejść dalej wybierz jedną z poniższych opcji:");
            menu:
            Console.WriteLine("[1] Sklep\n[2] Ustawienia konta\n[3] Doładowanie środków\n[4] Wyjście");
            string userInput = Console.ReadLine();
            switch (userInput)
            {
                case "1":
                    Console.Clear();
                    Console.WriteLine("[1] Lista produktów\n[2] Poprzednie transakcje\n[3] Powrót");
                    string choice = Console.ReadLine();
                    switch (choice)
                    {
                        case "1":
                            Console.Clear();
                            for (int i = 1; i <= products.Count; i++)
                            {
                                Console.WriteLine("**************************");
                                Console.WriteLine($"[PID: {i}]    {products[i-1].name}    {products[i-1].price}   {products[i-1].quantity}");
                                Console.WriteLine("\n");
                            }
                            Console.WriteLine("Wybierz produkt żeby go zakupić lub wpisz [0] aby wrócić do menu.");
                            string submenuChoice = Console.ReadLine();
                            if(submenuChoice == "0" || string.IsNullOrEmpty(submenuChoice))
                            {
                                Console.Clear();
                                goto menu;
                            }
                            else
                            {
                                int id = Convert.ToInt32(submenuChoice);
                                orderQuan:
                                Console.WriteLine($"Wybrano: {products[id-1].name}, cena: {products[id - 1].price}, ilość w magazynie {products[id].quantity}");
                                Console.WriteLine("Ilość:");
                                int orderQuantity = Convert.ToInt32(Console.ReadLine());
                                double totalCost;
                                if (!(orderQuantity > products[id-1].quantity))
                                {
                                    if(loggedUser.WholesaleClient == true)
                                    {
                                        totalCost = orderQuantity * products[id - 1].price*0.9;
                                    }
                                    else
                                    {
                                        totalCost = orderQuantity * products[id - 1].price;
                                    }
                                    anotherTry:
                                    Console.WriteLine($"Kosz wynosi: {totalCost}");
                                    Console.WriteLine("Aby potwierdzić zakup wprowadź swoje hasło");
                                    string passw = Console.ReadLine();
                                    if(passw == loggedUser.Password && totalCost <= loggedUser.Balance)
                                    {
                                        users.Find(u => u.Id == loggedUser.Id).Balance -= totalCost;
                                        products[id - 1].quantity -= orderQuantity;
                                        Console.Clear();
                                        Console.WriteLine("Dokonano zakupu. Powrót do menu.");
                                        SaveUsersToFile(users);
                                        SaveProductsToFile(products);
                                        goto menu;
                                    }
                                    else if(passw != loggedUser.Password)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Błędne hasło. Spróbuj ponownie.");
                                        goto anotherTry;
                                    }
                                    else if(totalCost > loggedUser.Balance)
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Niewystarczające saldo. Doładuj konto i spróbuj ponownie.");
                                        goto menu;
                                    }
                                    else
                                    {
                                        Console.Clear();
                                        Console.WriteLine("Wystąpił błąd. Spróbuj ponownie.");
                                        goto menu;
                                    }

                                }
                                else
                                {
                                    Console.Clear();
                                    Console.WriteLine("Zbyt mały stan w magazynie.");
                                    goto orderQuan;
                                }
                            }
                        case "2": Console.WriteLine("Historia transakcji"); break;
                        case "3": Environment.Exit(0); break;
                    }
                    break;
                case "2":
                    Console.WriteLine("Ustawienia konta");
                    Console.WriteLine("Usuń konto, zmień dane, zmień login/hasło, informacje o koncie");
                    //Do zrobienia w przyszłości
                    break;
                case "3":
                    Console.Clear();
                    Console.WriteLine("Dodawanie środków.");
                    Console.WriteLine("Wprowadź ilość środków które chcesz dodać do konta:");
                    string rechargeTemp = Console.ReadLine();
                    double recharge;
                    if (!string.IsNullOrWhiteSpace(rechargeTemp))
                    {
                        recharge = Convert.ToDouble(rechargeTemp);
                        users.Find(u => u.Id == loggedUser.Id).Balance += recharge;
                        Console.Clear();
                        SaveUsersToFile(users);
                        Console.WriteLine("Pomyślnie dodano środki do konta. Powrót do menu.");
                        goto menu;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Wprowadzono nieprawidłową ilość środków. Powrót do menu.");
                        goto menu;
                    }
                case "4": Environment.Exit(0); break;
            }
        }
    }
}
