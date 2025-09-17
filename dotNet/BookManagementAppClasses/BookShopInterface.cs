public class BookShopUI
{
    private BookInventory inventory = new BookInventory();
    private SalesManager salesManager = new SalesManager();

    public void Run()
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

    private void DisplayMenu()
    {
        Console.WriteLine("====== BOOK SHOP MENU ======");
        Console.WriteLine("1. Add Book");
        Console.WriteLine("2. Sell Book");
        Console.WriteLine("3. View Books");
        Console.WriteLine("4. View Sales Report");
        Console.WriteLine("5. Exit");
        Console.Write("Please Enter Your Choice: ");
    }

    private void AddBook()
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
        if (!decimal.TryParse(Console.ReadLine(), out decimal price) || price <= 0)
        {
            Console.WriteLine("Invalid price! Please enter a positive number.");
            return;
        }

        Console.Write("Enter quantity of Book: ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity < 0)
        {
            Console.WriteLine("Invalid quantity! Please enter a positive number.");
            return;
        }

        var book = new Book(title, author, price, quantity);
        inventory.AddBook(book);
        Console.WriteLine("Book added successfully.");
    }

    private void SellBook()
    {
        Console.WriteLine("=== SELL YOUR BOOK ===");

        if (!inventory.HasBooks())
        {
            Console.WriteLine("Stock is Empty, Come Back Later!!.");
            return;
        }

        Console.Write("Enter book title that you want to sell: ");
        string title = Console.ReadLine();

        var book = inventory.FindBookByTitle(title);
        if (book == null)
        {
            Console.WriteLine("OOPS !! Book not found!");
            return;
        }

        Console.Write($"Enter quantity to sell (Available: {book.Quantity}): ");
        if (!int.TryParse(Console.ReadLine(), out int quantity) || quantity <= 0)
        {
            Console.WriteLine("Invalid quantity! Please enter a positive number.");
            return;
        }

        if (!inventory.SellBook(title, quantity))
        {
            Console.WriteLine($"Sorry !! Out of Stock, Only {book.Quantity} copies available.");
            return;
        }

        Console.Write("Please Enter Your Name: ");
        string customerName = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(customerName))
        {
            Console.WriteLine("Customer name cannot be empty!");
            return;
        }

        decimal amount = book.Price * quantity;
        salesManager.RecordSale(customerName, book.Title, quantity, amount);

        Console.WriteLine($"Sold {quantity} copies of '{book.Title}' to {customerName}.");
    }

    private void ViewBooks()
    {
        Console.WriteLine("--- Book Inventory ---");

        var books = inventory.GetAllBooks();
        if (books.Count == 0)
        {
            Console.WriteLine("No books in inventory.");
            return;
        }

        Console.WriteLine("Title\t\tAuthor\t\tPrice\t\tQuantity");
        Console.WriteLine("--------------------------------------------------");

        foreach (var book in books)
        {
            Console.WriteLine(book.ToString());
        }
    }

    private void ViewSalesReport()
    {
        Console.WriteLine("--- Sales Report ---");

        if (!salesManager.HasSales())
        {
            Console.WriteLine("No sales recorded yet.");
            return;
        }

        Console.WriteLine("Customer Name\t\tBook\t\tQuantity Purchased\tAmount");
        Console.WriteLine("----------------------------------------------------------------");

        foreach (var sale in salesManager.GetAllSales())
        {
            Console.WriteLine(sale.ToString());
        }

        Console.WriteLine("----------------------------------------------------------------");
        Console.WriteLine($"Total Amount Spent: ${salesManager.GetTotalSalesAmount():F2}");
    }
}