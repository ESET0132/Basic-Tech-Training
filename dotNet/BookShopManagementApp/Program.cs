using System.Diagnostics;

namespace BookShopManagementSystem
{
    class Program
    {
        static List<string> bookTitles = new List<string>();
        static List<string> bookAuthors = new List<string>();
        static List<decimal> bookPrices = new List<decimal>();
        static List<int> bookQuantities = new List<int>();

        static List<string> saleCustomers = new List<string>();
        static List<string> saleBooks = new List<string>();
        static List<int> saleQuantities = new List<int>();
        static List<decimal> saleAmounts = new List<decimal>();

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to Book Shop Management System!");

            bool exit = false;
            while (!exit)
            {
                DisplayMenu();
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook();
                        break;
                    case "2":
                        SellBook();
                        break;
                    case "3":
                        ViewBooks();
                        break;
                    case "4":
                        ViewSalesReport();
                        break;
                    case "5":
                        exit = true;
                        Console.WriteLine("Thanks for using Book Shop Management! Visit Again.");
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }

                Console.WriteLine("\nPress any key to continue...");
                Console.ReadKey();
            }
        }

        static void DisplayMenu()
        {
           
            Console.WriteLine("====== BOOK SHOP MENU ======");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. Sell Book");
            Console.WriteLine("3. View Books");
            Console.WriteLine("4. View Sales Report");
            Console.WriteLine("5. Exit");
            Console.Write("Please Enter Your Choice: ");
        }

        static void AddBook()
        {
            Console.WriteLine("=== ADD NEW BOOK ===");

            Console.Write("Enter book title You Want to Add: ");
            string title = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title))
            {
                Console.WriteLine("Title is empty! please enter a book title");
                return;
            }

            Console.Write("Enter book author: ");
            string author = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Author is empty!, please enter book author");
                return;
            }

            Console.Write("Enter the price: ");
            string priceI = Console.ReadLine();
            if (!decimal.TryParse(priceI, out decimal price) || price <= 0)
            {
                Console.WriteLine("Invalid price! Please enter a positive number.");
                return;
            }

            Console.Write("Enter quantity of Book: ");
            string quantityI = Console.ReadLine();
            if (!int.TryParse(quantityI, out int quantity) || quantity < 0)
            {
                Console.WriteLine("Invalid quantity! Please enter a positive number.");
                return;
            }

            // Check if book already exists
            int existInd = -1;
            for (int i = 0; i < bookTitles.Count; i++)
            {
                if (bookTitles[i].Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    existInd = i;
                    break;
                }
            }

            if (existInd >= 0)
            {
                // Update existing book quantity
                bookQuantities[existInd] += quantity;
                Console.WriteLine($"Updated quantity for '{title}'. New quantity is: {bookQuantities[existInd]}");
            }
            else
            {
                // Add new book
                bookTitles.Add(title);
                bookAuthors.Add(author);
                bookPrices.Add(price);
                bookQuantities.Add(quantity);
                Console.WriteLine("Book added successfully.");
            }
        }

        static void SellBook()
        {
           
            Console.WriteLine("=== SELL YOUR BOOK ===");

            if (bookTitles.Count == 0)
            {
                Console.WriteLine("Stock is Empty, Come Back Later!!.");
                return;
            }

            Console.Write("Enter book title that you want to sell: ");
            string title = Console.ReadLine();

            int bookI = -1;
            for (int i = 0; i < bookTitles.Count; i++)
            {
                if (bookTitles[i].Equals(title, StringComparison.OrdinalIgnoreCase))
                {
                    bookI = i;
                    break;
                }
            }

            if (bookI == -1)
            {
                Console.WriteLine("OOPS !! Book not found!");
                return;
            }

            Console.Write($"Enter quantity to sell (Available: {bookQuantities[bookI]}): ");
            string quantityI = Console.ReadLine();
            if (!int.TryParse(quantityI, out int quantity) || quantity <= 0)
            {
                Console.WriteLine("Invalid quantity! Please enter a positive number.");
                return;
            }

            if (quantity > bookQuantities[bookI])
            {
                Console.WriteLine($"Sorry !! Out of Stock,  Only {bookQuantities[bookI]} copies available.");
                return;
            }

            Console.Write("Please Enter Your Name: ");
            string customerName = Console.ReadLine();

            if (customerName is null)
            {
                Console.WriteLine("Customer name cannot be empty!");
                return;
            }

            // Update book quantity
            bookQuantities[bookI] -= quantity;

            // Record the sale made
            decimal amount = bookPrices[bookI] * quantity;
            saleCustomers.Add(customerName);
            saleBooks.Add(bookTitles[bookI]);
            saleQuantities.Add(quantity);
            saleAmounts.Add(amount);

            Console.WriteLine($"Sold {quantity} copies of '{bookTitles[bookI]}' to {customerName}.");
        }

        static void ViewBooks()
        {
           
            Console.WriteLine("--- Book Inventory ---");

            if (bookTitles.Count == 0)
            {
                Console.WriteLine("No books in inventory.");
                return;
            }

            Console.WriteLine("Title\t\tAuthor\t\tPrice\t\tQuantity");
            Console.WriteLine("--------------------------------------------------");

            for (int i = 0; i < bookTitles.Count; i++)
            {
                Console.WriteLine($"{bookTitles[i]}\t{bookAuthors[i]}\t${bookPrices[i]:F2}\t\t{bookQuantities[i]}");
            }
        }

        static void ViewSalesReport()
        {
           
            Console.WriteLine("--- Sales Report ---");

            if (saleCustomers.Count == 0)
            {
                Console.WriteLine("No sales recorded yet.");
                return;
            }

            Console.WriteLine("Customer Name\t\tBook\t\tQuantity Purchased\tAmount");
            Console.WriteLine("----------------------------------------------------------------");

            decimal totalAmount = 0;
            for (int i = 0; i < saleCustomers.Count; i++)
            {
                Console.WriteLine($"{saleCustomers[i]}\t\t{saleBooks[i]}\t{saleQuantities[i]}\t\t\t${saleAmounts[i]:F2}");
                totalAmount += saleAmounts[i];
            }

            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine($"Total Amount Spent: ${totalAmount:F2}");
        }
    }
}