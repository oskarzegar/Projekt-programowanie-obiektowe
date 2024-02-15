using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace Projekt_sklep
{
    public class AdminPanel : FileIO
    {
        public static void AdminMenu()
        {
            menu:
            Console.WriteLine("Obsługa użytkowników:\n1.Dodaj użytkownika.\n2.Usuń użytkownika.\n3.Edytuj użytkownika\n");
            Console.WriteLine("Zarządzanie towarem:\n4.Dodaj produkt.\n5.Usuń produkt.\n6.Edytuj produkt\n");
            Console.WriteLine("7.Wyjście.");
            int userInput = Convert.ToInt32(Console.ReadLine());
            List<User> users = ReadUsersFromFile();
            List<Product> products = ReadProcuctsFromFile();

            switch (userInput)
            {
                case 1:
                    Console.Clear();
                    Console.WriteLine("Menu dodawania użytkownika");
                    reg:
                    Console.WriteLine("Czy użytkownik będzie administratorem? [Tak/Nie]\n[0] - wyjście");
                    string adminTemp = Console.ReadLine();
                    bool admin = false;
                    if(adminTemp == "Tak")
                    {
                        admin = true;
                    }
                    else if(adminTemp == "Nie")
                    {
                        admin = false;
                    }
                    else if(adminTemp == "0")
                    {
                        goto menu;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Niepoprawna odpowiedź. Wybierz 'Tak' lub 'Nie'");
                        goto reg;
                    }
                    User.Registration(admin);
                    Console.Clear();
                    Console.WriteLine("Poprawnie dodano nowego użytkownika, powrót do menu.");
                    goto menu;
                case 2:
                    Console.Clear();
                    Console.WriteLine("Menu usuwania użytkownika");
                    Console.WriteLine("Podaj UID uzytkownika do usunięcia.");
                    for(int i = 0; i < users.Count; i++)
                    {
                        Console.WriteLine("**************************");
                        Console.WriteLine($"UID: {i}");
                        Console.WriteLine($"Imię: {users[i].FirstName}");
                        Console.WriteLine($"Nazwisko: {users[i].LastName}");
                        Console.WriteLine($"Email: {users[i].Email}");
                        Console.WriteLine($"Numer telefonu: {users[i].PhoneNumber}");
                        Console.WriteLine($"Adres: {users[i].Address}");
                        Console.WriteLine($"Nazwa użytkownika: {users[i].Username}");
                        Console.WriteLine($"Kient hurtowy: {users[i].WholesaleClient}");
                        Console.WriteLine($"Administrator: {users[i].Administrator}");
                        Console.WriteLine("\n");
                    }
                    int idDelete;
                    var idDeleteTemp = Console.ReadLine();
                    if (string.IsNullOrWhiteSpace(idDeleteTemp))
                    {
                        Console.Clear();
                        goto menu;
                    }
                    else
                    {
                        idDelete = Convert.ToInt32(idDeleteTemp);
                    }
                    Console.WriteLine("Wpisz ponowanie ID użytkownika aby potwierdzić.");
                    int checkDelete = Convert.ToInt32(Console.ReadLine());
                    if(checkDelete == idDelete)
                    {
                        users.RemoveAt(idDelete);
                        SaveUsersToFile(users);
                        Console.Clear();
                        Console.WriteLine("Poprawnie usunięto użytkownika, powrót do menu.");
                        goto menu;
                    }
                    else
                    {
                        Console.Clear();
                        Console.WriteLine("Nie udało się potwierdzić wyboru. Powrót do menu.");
                        goto menu;
                    }
                case 3:
                    usersModMenu:
                    Console.Clear();
                    Console.WriteLine("Menu edycji użytkownika");
                    for(int i = 0; i < users.Count; i++)
                    {
                        Console.WriteLine("**************************");
                        Console.WriteLine($"UID: {i}");
                        Console.WriteLine($"Imię: {users[i].FirstName}");
                        Console.WriteLine($"Nazwisko: {users[i].LastName}");
                        Console.WriteLine($"Email: {users[i].Email}");
                        Console.WriteLine($"Numer telefonu: {users[i].PhoneNumber}");
                        Console.WriteLine($"Adres: {users[i].Address}");
                        Console.WriteLine($"Nazwa użytkownika: {users[i].Username}");
                        Console.WriteLine($"Kient hurtowy: {users[i].WholesaleClient}");
                        Console.WriteLine($"Administrator: {users[i].Administrator}");
                        Console.WriteLine("\n");
                    }
                    Console.WriteLine("Podaj UID uzytkownika do edycji.");
                    var idModifyTemp = Console.ReadLine();
                    int idModify;
                    if (string.IsNullOrWhiteSpace(idModifyTemp))
                    {
                        Console.Clear();
                        goto menu;
                    }
                    else
                    {
                        idModify = Convert.ToInt32(idModifyTemp);
                    }
                    if(idModify < users.Count)
                    {
                        Console.Clear();
                        Console.WriteLine($"[1] Imię: {users[idModify].FirstName}");
                        Console.WriteLine($"[2] Nazwisko: {users[idModify].LastName}");
                        Console.WriteLine($"[3] Email: {users[idModify].Email}");
                        Console.WriteLine($"[4] Numer telefonu: {users[idModify].PhoneNumber}");
                        Console.WriteLine($"[5] Adres: {users[idModify].Address}");
                        Console.WriteLine($"[6] Nazwa użytkownika: {users[idModify].Username}");
                        Console.WriteLine($"[7] Klient hurtowy: {users[idModify].WholesaleClient}");
                        Console.WriteLine($"[8] Administrator: {users[idModify].Administrator}");
                    }
                    else
                    {
                        Console.Clear();
                        goto usersModMenu;
                    }
                   
                    userModMenu:
                    Console.WriteLine("\nWpisz numer pola aby edytować jego wartość.");
                    int field = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    switch (field)
                    {
                        case 1:
                            Console.WriteLine($"Imię: {users[idModify].FirstName}");
                            Console.WriteLine("Wprowadź imię:\n");
                            string newFirstName = Console.ReadLine();
                            if(string.IsNullOrWhiteSpace(newFirstName)) goto userModMenu;
                            users[idModify].FirstName = newFirstName;
                            Console.Clear();
                            SaveUsersToFile(users);
                            Console.WriteLine("Poprawnie zmieniono dane. Powrót do menu.");
                            goto menu;
                        case 2:
                            Console.WriteLine($"Nazwisko: {users[idModify].LastName}");
                            Console.WriteLine("Wprowadź nazwisko:\n");
                            string newLastName = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(newLastName)) goto userModMenu;
                            users[idModify].LastName = newLastName;
                            Console.Clear();
                            SaveUsersToFile(users);
                            Console.WriteLine("Poprawnie zmieniono dane. Powrót do menu.");
                            goto menu;
                        case 3:
                            Console.WriteLine($"Email: {users[idModify].Email}");
                            Console.WriteLine("Wprowadź email:\n");
                            string newEmail = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(newEmail)) goto userModMenu;
                            users[idModify].Email = newEmail;
                            Console.Clear();
                            SaveUsersToFile(users);
                            Console.WriteLine("Poprawnie zmieniono dane. Powrót do menu.");
                            goto menu;
                        case 4:
                            Console.WriteLine($"Numer telefonu: {users[idModify].PhoneNumber}");
                            Console.WriteLine("Wprowadź numer telefonu:\n");
                            string newPhoneNumber = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(newPhoneNumber)) goto userModMenu;
                            users[idModify].PhoneNumber = newPhoneNumber;
                            Console.Clear();
                            SaveUsersToFile(users);
                            Console.WriteLine("Poprawnie zmieniono dane. Powrót do menu.");
                            goto menu;
                        case 5:
                            Console.WriteLine($"Adres: {users[idModify].Address}");
                            Console.WriteLine("Wprowadź adres:\n");
                            string newAddress = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(newAddress)) goto userModMenu;
                            users[idModify].Address = newAddress;
                            Console.Clear();
                            SaveUsersToFile(users);
                            Console.WriteLine("Poprawnie zmieniono dane. Powrót do menu.");
                            goto menu;
                        case 6:
                            Console.WriteLine($"Nazwa użytkownika: {users[idModify].Username}");
                            Console.WriteLine("Wprowadź nazwę użytkownika:\n");
                            string newUsername = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(newUsername)) goto userModMenu;
                            users[idModify].Username = newUsername;
                            Console.Clear();
                            SaveUsersToFile(users);
                            Console.WriteLine("Poprawnie zmieniono dane. Powrót do menu.");
                            goto menu;
                        case 7:
                            Console.WriteLine($"Klient hurtowy: {users[idModify].WholesaleClient}");
                            Console.WriteLine("[true - klient hurtowy, false - klient detaliczny]:\n");
                            string newWholesaleClientTemp = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(newWholesaleClientTemp)) goto userModMenu;
                            bool newWholesaleClient = users[idModify].WholesaleClient;
                            if (newWholesaleClientTemp == "true") newWholesaleClient = true;
                            if (newWholesaleClientTemp == "false") newWholesaleClient = false;
                            users[idModify].WholesaleClient = newWholesaleClient;
                            Console.Clear();
                            SaveUsersToFile(users);
                            Console.WriteLine("Poprawnie zmieniono dane. Powrót do menu.");
                            goto menu;
                        case 8:
                            Console.WriteLine($"Administrator: {users[idModify].Administrator}");
                            Console.WriteLine("[true - administrator, false - klient]:\n");
                            string newAdminTemp = Console.ReadLine();
                            if (string.IsNullOrWhiteSpace(newAdminTemp)) goto userModMenu;
                            bool newAdmin = users[idModify].Administrator;
                            if (newAdminTemp == "true") newAdmin = true;
                            if (newAdminTemp == "false") newAdmin = false;
                            users[idModify].Administrator = newAdmin;
                            Console.Clear();
                            SaveUsersToFile(users);
                            Console.WriteLine("Poprawnie zmieniono dane. Powrót do menu.");
                            goto menu;
                        default:
                            Console.WriteLine("Wprowadzono błędne dane, spróbuj ponownie.");
                            goto userModMenu;
                     }
                case 4:
                    Console.Clear();
                    Console.WriteLine("Menu dodawania nowego produktu");
                    int newProductId = products.Max(i => i.id) + 1;
                    addNewProduct:
                    Console.WriteLine("Podaj nazwę produktu: ");
                    string name = Console.ReadLine();
                    Console.WriteLine("Podaj cenę produktu: [zł,gr]");
                    double price = Convert.ToDouble(Console.ReadLine());
                    Console.WriteLine("Podaj ilość produktu w magazynie");
                    int quantity = Convert.ToInt32(Console.ReadLine());

                    if (string.IsNullOrWhiteSpace(name) || double.IsNaN(price) || price <= 0 || (quantity <= 0))
                    {
                        Console.Clear();
                        Console.WriteLine("Nie wszystkie pola zostały poprawnie wypełnione, spróbuj ponownie.");
                        goto addNewProduct;
                    }
                    else
                    {
                        Product newProduct = new Product(newProductId, name, price, quantity);
                        products.Add(newProduct);
                        try
                        {
                            SaveProductsToFile(products);
                            Console.WriteLine("Pomyślnie dodano nowy produkt. Powrót do menu.");
                            goto menu;
                        }
                        catch (Exception)
                        {

                            Console.WriteLine("Wystąpił nieoczekiwany błąd, spróbuj ponownie.");
                            goto addNewProduct;
                        }
                    }
                case 5:
                    Console.Clear();
                    Console.WriteLine("Menu usuwania produktu");
                    for(int i = 0; i < products.Count; i++)
                    {
                        Console.WriteLine("**************************");
                        Console.WriteLine($"[ID: {i}]    {products[i].name}    {products[i].price}   {products[i].quantity}");
                        Console.WriteLine("\n");

                    }
                    Console.WriteLine("Wybierz produkt do usunięcia wpisując jego ID");
                    int choice = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Aby potwierdzić ponownie wprowadź ID produktu.");
                    int choice2 = Convert.ToInt32(Console.ReadLine());
                    if(choice == choice2)
                    {
                        products.RemoveAt(choice-1);
                        SaveProductsToFile(products);
                        Console.Clear();
                        Console.WriteLine("Poprawnie usunięto produkt, powrót do menu.");
                        goto menu;
                    }
                    break;
                case 6:
                    Console.Clear();
                    prodModMenu:
                    Console.WriteLine("Menu edycji produktu");
                    for(int i  = 0; i < products.Count; i++)
                    {
                        Console.WriteLine("**************************");
                        Console.WriteLine($"[PID: {i}]    {products[i].name}    {products[i].price}   {products[i].quantity}");
                        Console.WriteLine("\n");
                    }
                    Console.WriteLine("Wybierz PID produktu który chcesz edytować: ");
                    int pId = Convert.ToInt32(Console.ReadLine());
                    Console.Clear();
                    Console.WriteLine("Wybierz pole:");
                    Console.WriteLine($"[1] Nazwa: {products[pId].name}\n[2] Cena: {products[pId].price}\n[3] Ilość w magazynie: {products[pId].quantity}");
                    int pField = Convert.ToInt32(Console.ReadLine());
                    switch (pField)
                    {
                        case 1:
                            Console.Clear();
                            Console.WriteLine(products[pId].name);
                            Console.WriteLine("Wprowadź nową nazwę:");
                            string newName = Console.ReadLine();
                            if(!string.IsNullOrEmpty(newName)) 
                            {
                                products[pId].name = newName;
                                SaveProductsToFile(products);
                                Console.Clear();
                                Console.WriteLine("Popranie zmieniono nazwę. Powrót do menu.");
                                goto menu;
                            }
                            else
                            {
                                Console.WriteLine("Wprowadzono błędne dane, spróbuj ponownie.");
                                goto prodModMenu;
                            }
                        case 2:
                            Console.Clear();
                            Console.WriteLine(products[pId].price);
                            Console.WriteLine("Wprowadź nową cenę:");
                            string newPrice = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newPrice))
                            {
                                products[pId].price = Convert.ToDouble(newPrice);
                                SaveProductsToFile(products);
                                Console.Clear();
                                Console.WriteLine("Popranie zmieniono cenę. Powrót do menu.");
                                goto menu;
                            }
                            else
                            {
                                Console.WriteLine("Wprowadzono błędne dane, spróbuj ponownie.");
                                goto prodModMenu;
                            }
                        case 3:
                            Console.Clear();
                            Console.WriteLine(products[pId].quantity);
                            Console.WriteLine("Wprowadź nową ilość produktu:");
                            string newQuantity = Console.ReadLine();
                            if (!string.IsNullOrEmpty(newQuantity))
                            {
                                products[pId].quantity = Convert.ToInt32(newQuantity);
                                SaveProductsToFile(products);
                                Console.Clear();
                                Console.WriteLine("Popranie zmieniono ilość produktu w magazynie. Powrót do menu.");
                                goto menu;
                            }
                            else
                            {
                                Console.WriteLine("Wprowadzono błędne dane, spróbuj ponownie.");
                                goto prodModMenu;
                            }
                        default:
                            Console.WriteLine("Nie wybrano żadnej pozycji, powrót do menu.");
                            goto menu;
                    }
                case 7:
                    Console.Clear();
                    Console.WriteLine("Wyjście");
                    Environment.Exit(0);
                    break;
                default:
                    Console.Clear();
                    Console.WriteLine("Wprowadzono błędne dane, spróbuj ponownie.");
                    goto menu;
            }
        }
    }
}
