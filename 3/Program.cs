using System;
using System.Collections.Generic;

namespace OOP_A3_ADRIAN_BENEDICT_CUARTERO
{
    public class Menu
    {
        public List<string> Items { get; private set; }
        public List<decimal> Prices { get; private set; }

        public Menu()
        {
            Items = new List<string> { "Coffee", "Tea", "Cake" };
            Prices = new List<decimal> { 2.50m, 1.50m, 3.00m };
        }

        public void AddItem(string itemName, decimal itemPrice)
        {
            Items.Add(itemName);
            Prices.Add(itemPrice);
            Console.WriteLine($"{itemName} added to the menu.");
        }

        public void RemoveItem(int index)
        {
            if (index >= 0 && index < Items.Count)
            {
                Console.WriteLine($"{Items[index]} removed from the menu.");
                Items.RemoveAt(index);
                Prices.RemoveAt(index);
            }
            else
            {
                Console.WriteLine("Invalid item index.");
            }
        }

        public void DisplayMenu()
        {
            Console.Clear();
            Console.WriteLine("Menu:");
            for (int i = 0; i < Items.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Items[i]} - {Prices[i]:C}");
            }
        }
    }

    public class Identity
    {
        private readonly Dictionary<string, string> users;

        public Identity()
        {
            users = new Dictionary<string, string>
            {
                { "admin", "password123" }
            };
        }

        public string? AuthenticateUser()
        {
            Console.Write("Please enter username: ");
            string? username = Console.ReadLine()?.Trim();

            Console.Write("Please enter password: ");
            string? password = Console.ReadLine()?.Trim();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                if (users.TryGetValue(username, out string storedPassword) && storedPassword == password)
                {
                    Console.WriteLine("Login successful.");
                    return username;
                }
                else
                {
                    Console.WriteLine("Invalid login credentials.");
                }
            }
            else
            {
                Console.WriteLine("Username and password cannot be empty.");
            }

            return null;
        }
    }

    public class Order
    {
        private readonly List<int> orderedItems;
        private readonly Menu menu;
        public decimal Discount { get; private set; }

        public Order(Menu menu)
        {
            orderedItems = new List<int>();
            this.menu = menu;
            Discount = 0;
        }

        public void AddItemToOrder(int itemNumber)
        {
            itemNumber--;
            if (itemNumber >= 0 && itemNumber < menu.Items.Count)
            {
                orderedItems.Add(itemNumber);
                Console.WriteLine($"{menu.Items[itemNumber]} added to your order.");
            }
            else
            {
                Console.WriteLine("Invalid item number. Please try again.");
            }
        }

        public void DisplayOrder()
        {
            Console.WriteLine("Your Order:");
            if (orderedItems.Count > 0)
            {
                foreach (int itemIndex in orderedItems)
                {
                    Console.WriteLine($"{menu.Items[itemIndex]} - {menu.Prices[itemIndex]:C}");
                }
            }
            else
            {
                Console.WriteLine("No items in your order.");
            }
        }

        public void ApplyDiscount(decimal discountPercentage)
        {
            if (discountPercentage >= 0 && discountPercentage <= 100)
            {
                Discount = discountPercentage / 100;
                Console.WriteLine($"Discount of {discountPercentage}% applied.");
            }
            else
            {
                Console.WriteLine("Invalid discount percentage.");
            }
        }

        public void CalculateTotal(string currentUser)
        {
            decimal total = 0m;
            foreach (int itemIndex in orderedItems)
            {
                total += menu.Prices[itemIndex];
            }

            if (Discount > 0)
            {
                total -= total * Discount;
            }

            Console.WriteLine($"Total amount for {currentUser}: {total:C}");
        }
    }

    public class PointOfSale
    {
        private readonly Menu menu;
        private readonly Identity identity;
        private Order? currentOrder;
        private string? currentUser;

        public PointOfSale()
        {
            menu = new Menu();
            identity = new Identity();
        }

        public void Start()
        {
            Console.WriteLine("-------WELCOME-----");
            Console.WriteLine("System starting......");
            Console.WriteLine("System successfully started.....");
            Console.WriteLine("-------------------------------------\n\n");

            currentUser = identity.AuthenticateUser();
            if (currentUser == null)
            {
                Console.WriteLine("Force System shutdown!");
                return;
            }

            currentOrder = new Order(menu);
            bool running = true;

            while (running)
            {
                ShowMenu();

                string? choice = Console.ReadLine()?.Trim();
                switch (choice)
                {
                    case "1":
                        AddMenuItem();
                        break;
                    case "2":
                        RemoveMenuItem();
                        break;
                    case "3":
                        menu.DisplayMenu();
                        break;
                    case "4":
                        PlaceOrder();
                        break;
                    case "5":
                        currentOrder.DisplayOrder();
                        break;
                    case "6":
                        ApplyDiscount();
                        break;
                    case "7":
                        currentOrder.CalculateTotal(currentUser);
                        break;
                    case "8":
                        ShowAboutSystem();
                        break;
                    case "9":
                        running = false;
                        Console.WriteLine("Exiting... Press any key to close.");
                        Console.ReadKey();
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please try again.");
                        break;
                }
            }
        }

        private void ShowMenu()
        {
            Console.Clear();
            Console.WriteLine("-------------------");
            Console.WriteLine("Welcome to the Coffee Shop!");
            Console.WriteLine("-------------------");
            Console.WriteLine("1. - Add Menu Item");
            Console.WriteLine("-------------------");
            Console.WriteLine("2. - Remove Menu Item");
            Console.WriteLine("-------------------");
            Console.WriteLine("3. - View Menu");
            Console.WriteLine("-------------------");
            Console.WriteLine("4. - Place Order");
            Console.WriteLine("-------------------");
            Console.WriteLine("5. - View Order");
            Console.WriteLine("-------------------");
            Console.WriteLine("6. - Apply Discount");
            Console.WriteLine("-------------------");
            Console.WriteLine("7. - Calculate Total");
            Console.WriteLine("-------------------");
            Console.WriteLine("8. - About System");
            Console.WriteLine("-------------------");
            Console.WriteLine("9. - Exit");
            Console.WriteLine("-------------------");
            Console.WriteLine("-------------------");
            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.Write("Select your desired action: ");
        }

        private void AddMenuItem()
        {
            Console.Write("Enter item name: ");
            string? itemName = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(itemName))
            {
                Console.WriteLine("Item name cannot be empty.");
                return;
            }

            Console.Write("Enter item price: ");
            if (decimal.TryParse(Console.ReadLine(), out decimal itemPrice))
            {
                menu.AddItem(itemName, itemPrice);
            }
            else
            {
                Console.WriteLine("Invalid price format.");
            }
        }

        private void RemoveMenuItem()
        {
            menu.DisplayMenu();
            Console.Write("Enter the item number to remove: ");
            if (int.TryParse(Console.ReadLine(), out int itemNumber))
            {
                itemNumber--;
                menu.RemoveItem(itemNumber);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        private void PlaceOrder()
        {
            menu.DisplayMenu();
            Console.Write("Enter the item number to order: ");
            if (int.TryParse(Console.ReadLine(), out int itemNumber))
            {
                currentOrder.AddItemToOrder(itemNumber);
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a number.");
            }
        }

        private void ApplyDiscount()
        {
            Console.Write("Enter discount percentage (e.g., 50 for 50% off): ");
            if (decimal.TryParse(Console.ReadLine(), out decimal discountPercentage))
            {
                currentOrder.ApplyDiscount(discountPercentage);
            }
            else
            {
                Console.WriteLine("Invalid discount percentage.");
            }
        }

        private void ShowAboutSystem()
        {
            Console.Clear();
            Console.WriteLine("-------------------");
            Console.WriteLine("About the System");
            Console.WriteLine("-------------------");
            Console.WriteLine("Welcome to the Coffee Shop System!");
            Console.WriteLine("Version 1.0");
            Console.WriteLine("Developed by Adrian Benedict Cuartero.");
            Console.WriteLine("-------------------");
            Console.WriteLine("Press any key to return to the main menu...");
            Console.ReadKey();
        }

    }

    internal class Program
    {
        public static void Main(string[] args)
        {
            var pos = new PointOfSale();
            pos.Start();
        }
    }
}
