public class BookInventory
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        // Check if book already exists
        var existingBook = FindBookByTitle(book.Title);
        if (existingBook != null)
        {
            existingBook.Quantity += book.Quantity;
        }
        else
        {
            books.Add(book);
        }
    }

    public Book FindBookByTitle(string title)
    {
        return books.FirstOrDefault(b =>
            b.Title.Equals(title, StringComparison.OrdinalIgnoreCase));
    }

    public bool SellBook(string title, int quantity)
    {
        var book = FindBookByTitle(title);
        if (book == null || book.Quantity < quantity)
            return false;

        book.Quantity -= quantity;
        return true;
    }

    public List<Book> GetAllBooks()
    {
        return new List<Book>(books);
    }

    public bool HasBooks()
    {
        return books.Count > 0;
    }
}